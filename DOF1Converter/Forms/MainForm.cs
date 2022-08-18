
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using DOF1Converter.Forms;
using DOF1Converter.Converter;

namespace DOF1Converter.Forms;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void buttonDst_Click(object sender, EventArgs e)
    {
        var fd = new FolderBrowserDialog();
        if (fd.ShowDialog() == DialogResult.OK)
            textBoxDst.Text = fd.SelectedPath;
    }

    private void buttonSrc_Click(object sender, EventArgs e)
    {
        var fd = new OpenFileDialog();
        fd.Multiselect = true;
        fd.DefaultExt = "dof";

        if (fd.ShowDialog() == DialogResult.OK)
            addEntries(fd.FileNames);
    }

    private void addEntries(string[] paths)
    {
        listBox.BeginUpdate();

        foreach (string path in paths)
        {
            if (Path.GetExtension(path).ToLower() != ".dof")
                continue;

            listBox.Add(path);
        }

        listBox.EndUpdate();
    }

    private void textBoxDst_TextChanged(object sender, EventArgs e)
    {
        if (textBoxDst.Text == "")
            return;
        else
            textBoxDst.ForeColor = Directory.Exists(textBoxDst.Text) ? Color.Black : Color.Red;
    }

    private void buttonConvert_Click(object sender, EventArgs e)
    {
        if (!Directory.Exists(textBoxDst.Text))
        {
            MessageBox.Show(this, $"Invalid output path '{textBoxDst.Text}'", "Output folder don't exists.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;
        }

        var items = listBox.Items;
        if (items.Count == 0)
        {
            MessageBox.Show(this, $"No .dof files selected", "Items.Count < 1", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;
        }

        var form = new ProgressForm();
        form.Show(this);


        for (int i = 0; i < items.Count; i++)
        {
            var item = (FileListBoxEntry)items[i];

            form.label1.Text = $"({i} / {items.Count}){item.Text}";
            form.progressBar.Maximum = 100;
            form.progressBar.Value = (int)((i / (float)items.Count) * 100f);
            form.Update();

            try
            {
                var model = item.Model = DOFFileReader.Load(item.Path);

                if (checkBoxCopy.Checked)
                {
                    foreach (var mat in model.Materials)
                    {
                        if (mat.Texture == null)
                            continue;

                        string texSrcPath = Path.Combine(Path.GetDirectoryName(item.Path), Path.GetFileName(mat.Texture));
                        if (!File.Exists(texSrcPath))
                            continue;

                        string texDstPath = Path.Combine(textBoxDst.Text, Path.GetFileName(mat.Texture));
                        if (!File.Exists(texDstPath))
                            File.Copy(texSrcPath, texDstPath);
                    }
                }

            }
            catch (InvalidDataException ex)
            {
                item.Valid = false;
                item.Text = $"{Path.GetFileName(item.Path)} -> {ex.Message}";
            }


        }


        if (checkBoxMerge.Checked)
        {
            form.progressBar.Maximum = 1;
            form.label1.Text = $"Create group file...";
            form.Update();

            List<Model> models = new();
            foreach (var item in listBox.Items)
            {
                var citem = item as FileListBoxEntry;
                if (citem.Model != null)
                    models.Add(citem.Model);
            }
                
            var group = Model.Merge(models);

            string dstPath = Path.Join(textBoxDst.Text, "objects.obj");
            var objFileWriter = new ObjFileWriter(dstPath);
            objFileWriter.WriteModel(group);
            objFileWriter.Dispose();
        }
        else
        {
            int i = 0;
            foreach (var item in listBox.Items)
            {
                var citem = item as FileListBoxEntry;
                if (citem.Model == null)
                    continue;

                string dstPath = Path.Join(textBoxDst.Text, Path.GetFileNameWithoutExtension(citem.Path) + ".obj");

                form.label1.Text = $"({i} / {items.Count}) -> {dstPath}";
                form.progressBar.Maximum = 100;
                form.progressBar.Value = (int)((i / (float)items.Count) * 100f);
                form.Update();

                var objFileWriter = new ObjFileWriter(dstPath);
                objFileWriter.WriteModel(citem.Model);
                objFileWriter.Dispose();

                i++;
            }
        }


        form.Close();

    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
        listBox?.Items.Clear();
    }
}