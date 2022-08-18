using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using DOF1Converter.Converter;

namespace DOF1Converter.Forms;

public class FileListBox : ListBox
{
    public FileListBox()
    {
        DrawMode = DrawMode.OwnerDrawFixed;
    }

    public void Add(string newentry)
    {
        Add(new FileListBoxEntry(newentry));
    }

    public void Add(FileListBoxEntry newentry)
    {
        bool exists = false;

        foreach (var item in Items)
        {
            var citem = item as FileListBoxEntry;
            if (citem.Path == newentry.Path)
                exists = true;
        }

        if (!exists)
            Items.Add(newentry);
    }


    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        if (e.Index < 0 || e.Index >= Items.Count)
            return;

        var g = e.Graphics;
        var item = (FileListBoxEntry)Items[e.Index];

        Brush backBrush = e.Index % 2 == 0 ? Brushes.WhiteSmoke : Brushes.LightGray;
        Brush foreBrush = item.Valid ? Brushes.Black : Brushes.Red;

        var format = new StringFormat();

        g.FillRectangle(backBrush, e.Bounds);
        g.DrawString(item.Text, Font, foreBrush, e.Bounds.Location, format);
    }
}

public class FileListBoxEntry
{
    public string Path;
    public string Text;
    public bool Valid = true;
    public Model Model;

    public FileListBoxEntry(string path)
    {
        Path = path;
        Text = System.IO.Path.GetFileName(Path);
    }
}

