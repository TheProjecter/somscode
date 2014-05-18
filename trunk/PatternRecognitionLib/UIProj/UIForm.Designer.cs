namespace UIProj
{
    partial class UIForm
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
            this.buildButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.конфигурацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.конфигурацияToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.drawBox = new System.Windows.Forms.PictureBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.Image1Button = new System.Windows.Forms.Button();
            this.Image2Button = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buildButton
            // 
            this.buildButton.Location = new System.Drawing.Point(12, 526);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(75, 23);
            this.buildButton.TabIndex = 0;
            this.buildButton.Text = "Построить";
            this.buildButton.UseVisualStyleBackColor = true;
            this.buildButton.Click += new System.EventHandler(this.buildButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.конфигурацияToolStripMenuItem,
            this.конфигурацияToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // конфигурацияToolStripMenuItem
            // 
            this.конфигурацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьToolStripMenuItem,
            this.сохранитьToolStripMenuItem});
            this.конфигурацияToolStripMenuItem.Name = "конфигурацияToolStripMenuItem";
            this.конфигурацияToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.конфигурацияToolStripMenuItem.Text = "Файл";
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // конфигурацияToolStripMenuItem1
            // 
            this.конфигурацияToolStripMenuItem1.Name = "конфигурацияToolStripMenuItem1";
            this.конфигурацияToolStripMenuItem1.Size = new System.Drawing.Size(100, 20);
            this.конфигурацияToolStripMenuItem1.Text = "Конфигурация";
            this.конфигурацияToolStripMenuItem1.Click += new System.EventHandler(this.конфигурацияToolStripMenuItem1_Click);
            // 
            // drawBox
            // 
            this.drawBox.BackColor = System.Drawing.Color.White;
            this.drawBox.Location = new System.Drawing.Point(12, 27);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(760, 450);
            this.drawBox.TabIndex = 2;
            this.drawBox.TabStop = false;
            this.drawBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseClick);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(223, 483);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Очистить";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Image1Button
            // 
            this.Image1Button.Location = new System.Drawing.Point(12, 483);
            this.Image1Button.Name = "Image1Button";
            this.Image1Button.Size = new System.Drawing.Size(75, 23);
            this.Image1Button.TabIndex = 4;
            this.Image1Button.Text = "1 образ";
            this.Image1Button.UseVisualStyleBackColor = true;
            this.Image1Button.Click += new System.EventHandler(this.Image1Button_Click);
            // 
            // Image2Button
            // 
            this.Image2Button.Location = new System.Drawing.Point(93, 483);
            this.Image2Button.Name = "Image2Button";
            this.Image2Button.Size = new System.Drawing.Size(75, 23);
            this.Image2Button.TabIndex = 5;
            this.Image2Button.Text = "2 образ";
            this.Image2Button.UseVisualStyleBackColor = true;
            this.Image2Button.Click += new System.EventHandler(this.Image2Button_Click);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(93, 526);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 6;
            this.NextButton.Text = "Далее";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.Image2Button);
            this.Controls.Add(this.Image1Button);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.drawBox);
            this.Controls.Add(this.buildButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "UIForm";
            this.Text = "Алгоритм минимакса";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buildButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem конфигурацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem конфигурацияToolStripMenuItem1;
        private System.Windows.Forms.PictureBox drawBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button Image1Button;
        private System.Windows.Forms.Button Image2Button;
        private System.Windows.Forms.Button NextButton;
    }
}

