namespace UIProj
{
    partial class ConfigForm
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
            this.gammaBox = new System.Windows.Forms.TextBox();
            this.omegaBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gammaBox
            // 
            this.gammaBox.Location = new System.Drawing.Point(234, 15);
            this.gammaBox.Name = "gammaBox";
            this.gammaBox.Size = new System.Drawing.Size(73, 20);
            this.gammaBox.TabIndex = 0;
            // 
            // omegaBox
            // 
            this.omegaBox.Location = new System.Drawing.Point(234, 37);
            this.omegaBox.Name = "omegaBox";
            this.omegaBox.Size = new System.Drawing.Size(73, 20);
            this.omegaBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Гамма(величина для критерия останова)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Омега(величина погрешности)";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(109, 73);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 108);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.omegaBox);
            this.Controls.Add(this.gammaBox);
            this.Name = "ConfigForm";
            this.Text = "Конфигурация алгоритма";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox gammaBox;
        private System.Windows.Forms.TextBox omegaBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okButton;
    }
}