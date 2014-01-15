namespace WindowsFormsApplication1
{
    partial class ConnectionDialog
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
            this.ipBox = new System.Windows.Forms.TextBox();
            this.oKbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(12, 35);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(201, 20);
            this.ipBox.TabIndex = 0;
            // 
            // oKbutton
            // 
            this.oKbutton.Location = new System.Drawing.Point(219, 35);
            this.oKbutton.Name = "oKbutton";
            this.oKbutton.Size = new System.Drawing.Size(75, 20);
            this.oKbutton.TabIndex = 1;
            this.oKbutton.Text = "Ok";
            this.oKbutton.UseVisualStyleBackColor = true;
            this.oKbutton.Click += new System.EventHandler(this.oKbutton_Click);
            // 
            // ConnectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 107);
            this.Controls.Add(this.oKbutton);
            this.Controls.Add(this.ipBox);
            this.Name = "ConnectionDialog";
            this.Text = "ConnectionDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Button oKbutton;
    }
}