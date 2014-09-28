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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.конфигурацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сделатьРисунокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.конфигурацияToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.drawBox = new System.Windows.Forms.PictureBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.Set1Button = new System.Windows.Forms.ToolStripButton();
            this.Set2Button = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.returnButton = new System.Windows.Forms.ToolStripButton();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.nextButton = new System.Windows.Forms.ToolStripButton();
            this.axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.конфигурацияToolStripMenuItem,
            this.конфигурацияToolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip.Size = new System.Drawing.Size(1164, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // конфигурацияToolStripMenuItem
            // 
            this.конфигурацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сделатьРисунокToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.конфигурацияToolStripMenuItem.Name = "конфигурацияToolStripMenuItem";
            this.конфигурацияToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.конфигурацияToolStripMenuItem.Text = "Файл";
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сделатьРисунокToolStripMenuItem
            // 
            this.сделатьРисунокToolStripMenuItem.Name = "сделатьРисунокToolStripMenuItem";
            this.сделатьРисунокToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.сделатьРисунокToolStripMenuItem.Text = "Сделать рисунок";
            this.сделатьРисунокToolStripMenuItem.Click += new System.EventHandler(this.сделатьРисунокToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
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
            this.drawBox.Location = new System.Drawing.Point(27, 24);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(754, 540);
            this.drawBox.TabIndex = 2;
            this.drawBox.TabStop = false;
            this.drawBox.Paint += new System.Windows.Forms.PaintEventHandler(this.drawBox_Paint);
            this.drawBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseClick);
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Set1Button,
            this.Set2Button,
            this.toolStripSeparator1,
            this.returnButton,
            this.clearButton,
            this.toolStripSeparator2,
            this.startButton,
            this.nextButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(24, 542);
            this.toolStrip.TabIndex = 7;
            this.toolStrip.Text = "toolStrip1";
            // 
            // Set1Button
            // 
            this.Set1Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Set1Button.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Set1Button.Image = ((System.Drawing.Image)(resources.GetObject("Set1Button.Image")));
            this.Set1Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Set1Button.Name = "Set1Button";
            this.Set1Button.Size = new System.Drawing.Size(21, 19);
            this.Set1Button.Text = "X";
            this.Set1Button.ToolTipText = "1-ое множество признаков";
            this.Set1Button.Click += new System.EventHandler(this.Set1Button_Click);
            // 
            // Set2Button
            // 
            this.Set2Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Set2Button.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Set2Button.ForeColor = System.Drawing.Color.Black;
            this.Set2Button.Image = ((System.Drawing.Image)(resources.GetObject("Set2Button.Image")));
            this.Set2Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Set2Button.Name = "Set2Button";
            this.Set2Button.Size = new System.Drawing.Size(21, 19);
            this.Set2Button.Text = "Y";
            this.Set2Button.ToolTipText = "2-ое множество признаков";
            this.Set2Button.Click += new System.EventHandler(this.Set2Button_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(21, 6);
            // 
            // returnButton
            // 
            this.returnButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.returnButton.Image = ((System.Drawing.Image)(resources.GetObject("returnButton.Image")));
            this.returnButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(21, 20);
            this.returnButton.Text = "Удалить последний элемент множества";
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearButton.Image = ((System.Drawing.Image)(resources.GetObject("clearButton.Image")));
            this.clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(21, 20);
            this.clearButton.Text = "Очистить";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(21, 6);
            // 
            // startButton
            // 
            this.startButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(21, 20);
            this.startButton.Text = "Запуск";
            this.startButton.Click += new System.EventHandler(this.buildButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextButton.Image = ((System.Drawing.Image)(resources.GetObject("nextButton.Image")));
            this.nextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(21, 20);
            this.nextButton.Text = "Next";
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // axAcroPDF1
            // 
            this.axAcroPDF1.Enabled = true;
            this.axAcroPDF1.Location = new System.Drawing.Point(780, 27);
            this.axAcroPDF1.Name = "axAcroPDF1";
            this.axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF1.OcxState")));
            this.axAcroPDF1.Size = new System.Drawing.Size(384, 537);
            this.axAcroPDF1.TabIndex = 8;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1164, 566);
            this.ControlBox = false;
            this.Controls.Add(this.axAcroPDF1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.drawBox);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "UIForm";
            this.Text = "Алгоритм минимакса";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem конфигурацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem конфигурацияToolStripMenuItem1;
        private System.Windows.Forms.PictureBox drawBox;
        private System.Windows.Forms.ToolStripMenuItem сделатьРисунокToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton Set1Button;
        private System.Windows.Forms.ToolStripButton Set2Button;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton clearButton;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripButton returnButton;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton nextButton;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF1;
    }
}

