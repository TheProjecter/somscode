namespace WindowsFormsApplication1
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.viewBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // viewBox
            // 
            this.viewBox.BackColor = System.Drawing.SystemColors.Control;
            this.viewBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.viewBox.Location = new System.Drawing.Point(12, 12);
            this.viewBox.Name = "viewBox";
            this.viewBox.ReadOnly = true;
            this.viewBox.Size = new System.Drawing.Size(465, 313);
            this.viewBox.TabIndex = 3;
            this.viewBox.Text = "";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 322);
            this.Controls.Add(this.viewBox);
            this.Name = "ServerForm";
            this.Text = "ServerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox viewBox;
    }
}