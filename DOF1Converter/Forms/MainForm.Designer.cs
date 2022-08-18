using System.Windows.Forms;
using DOF1Converter.Forms;

namespace DOF1Converter.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox = new DOF1Converter.Forms.FileListBox();
            this.buttonDst = new System.Windows.Forms.Button();
            this.buttonSrc = new System.Windows.Forms.Button();
            this.textBoxDst = new System.Windows.Forms.TextBox();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.checkBoxCopy = new System.Windows.Forms.CheckBox();
            this.checkBoxMerge = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox.FormattingEnabled = true;
            this.listBox.IntegralHeight = false;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(12, 41);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(380, 397);
            this.listBox.TabIndex = 0;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buttonDst
            // 
            this.buttonDst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDst.Location = new System.Drawing.Point(516, 12);
            this.buttonDst.Name = "buttonDst";
            this.buttonDst.Size = new System.Drawing.Size(75, 23);
            this.buttonDst.TabIndex = 1;
            this.buttonDst.Text = "Select Dst";
            this.buttonDst.UseVisualStyleBackColor = true;
            this.buttonDst.Click += new System.EventHandler(this.buttonDst_Click);
            // 
            // buttonSrc
            // 
            this.buttonSrc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSrc.Location = new System.Drawing.Point(398, 41);
            this.buttonSrc.Name = "buttonSrc";
            this.buttonSrc.Size = new System.Drawing.Size(193, 23);
            this.buttonSrc.TabIndex = 2;
            this.buttonSrc.Text = "Select Src";
            this.buttonSrc.UseVisualStyleBackColor = true;
            this.buttonSrc.Click += new System.EventHandler(this.buttonSrc_Click);
            // 
            // textBoxDst
            // 
            this.textBoxDst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDst.Location = new System.Drawing.Point(12, 12);
            this.textBoxDst.Name = "textBoxDst";
            this.textBoxDst.PlaceholderText = "Output Folder";
            this.textBoxDst.Size = new System.Drawing.Size(498, 23);
            this.textBoxDst.TabIndex = 4;
            this.textBoxDst.TextChanged += new System.EventHandler(this.textBoxDst_TextChanged);
            // 
            // buttonConvert
            // 
            this.buttonConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConvert.Location = new System.Drawing.Point(398, 415);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(193, 23);
            this.buttonConvert.TabIndex = 5;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(398, 70);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(193, 23);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "Clear Src";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // checkBoxCopy
            // 
            this.checkBoxCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxCopy.AutoSize = true;
            this.checkBoxCopy.Checked = true;
            this.checkBoxCopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCopy.Location = new System.Drawing.Point(398, 99);
            this.checkBoxCopy.Name = "checkBoxCopy";
            this.checkBoxCopy.Size = new System.Drawing.Size(100, 19);
            this.checkBoxCopy.TabIndex = 7;
            this.checkBoxCopy.Text = "Copy Textures";
            this.checkBoxCopy.UseVisualStyleBackColor = true;
            // 
            // checkBoxMerge
            // 
            this.checkBoxMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMerge.AutoSize = true;
            this.checkBoxMerge.Checked = true;
            this.checkBoxMerge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMerge.Location = new System.Drawing.Point(398, 124);
            this.checkBoxMerge.Name = "checkBoxMerge";
            this.checkBoxMerge.Size = new System.Drawing.Size(103, 19);
            this.checkBoxMerge.TabIndex = 8;
            this.checkBoxMerge.Text = "Merge Objects";
            this.checkBoxMerge.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 450);
            this.Controls.Add(this.checkBoxMerge);
            this.Controls.Add(this.checkBoxCopy);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.textBoxDst);
            this.Controls.Add(this.buttonSrc);
            this.Controls.Add(this.buttonDst);
            this.Controls.Add(this.listBox);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "DOF1 Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FileListBox listBox;
        private Button buttonDst;
        private Button buttonSrc;
        private TextBox textBoxDst;
        private Button buttonConvert;
        private Button buttonClear;
        private CheckBox checkBoxCopy;
        private CheckBox checkBoxMerge;
    }
}