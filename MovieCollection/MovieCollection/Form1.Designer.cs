namespace MovieCollection
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelSearchResults = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.panelView = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelStudio = new System.Windows.Forms.Label();
            this.labelActor = new System.Windows.Forms.Label();
            this.labelWriter = new System.Windows.Forms.Label();
            this.labelDirector = new System.Windows.Forms.Label();
            this.labelAge = new System.Windows.Forms.Label();
            this.labelYear = new System.Windows.Forms.Label();
            this.labelGenre = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.vScrollBar2 = new System.Windows.Forms.VScrollBar();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelSearchResults.SuspendLayout();
            this.panelView.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1272, 57);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(670, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(600, 55);
            this.panel3.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(435, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(153, 32);
            this.button3.TabIndex = 2;
            this.button3.Text = "Расширенный поиск";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(333, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 32);
            this.button2.TabIndex = 1;
            this.button2.Text = "Поиск";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(290, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Добавить фильм";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelSearchResults);
            this.panel2.Controls.Add(this.panelView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1272, 596);
            this.panel2.TabIndex = 1;
            // 
            // panelSearchResults
            // 
            this.panelSearchResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearchResults.Controls.Add(this.panel6);
            this.panelSearchResults.Controls.Add(this.vScrollBar1);
            this.panelSearchResults.Location = new System.Drawing.Point(671, 0);
            this.panelSearchResults.Name = "panelSearchResults";
            this.panelSearchResults.Size = new System.Drawing.Size(601, 240);
            this.panelSearchResults.TabIndex = 4;
            this.panelSearchResults.Visible = false;
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(580, 0);
            this.panel6.TabIndex = 2;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(578, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(21, 238);
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // panelView
            // 
            this.panelView.Controls.Add(this.panel5);
            this.panelView.Controls.Add(this.vScrollBar2);
            this.panelView.Controls.Add(this.panel4);
            this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView.Location = new System.Drawing.Point(0, 0);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(1272, 596);
            this.panelView.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.labelActor);
            this.panel5.Controls.Add(this.labelWriter);
            this.panel5.Controls.Add(this.labelDirector);
            this.panel5.Controls.Add(this.labelStudio);
            this.panel5.Controls.Add(this.labelAge);
            this.panel5.Controls.Add(this.labelYear);
            this.panel5.Controls.Add(this.labelGenre);
            this.panel5.Controls.Add(this.labelDescription);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.labelName);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(400, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(851, 596);
            this.panel5.TabIndex = 4;
            // 
            // labelStudio
            // 
            this.labelStudio.AutoSize = true;
            this.labelStudio.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelStudio.Location = new System.Drawing.Point(0, 222);
            this.labelStudio.Name = "labelStudio";
            this.labelStudio.Padding = new System.Windows.Forms.Padding(30, 5, 30, 5);
            this.labelStudio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelStudio.Size = new System.Drawing.Size(242, 27);
            this.labelStudio.TabIndex = 5;
            this.labelStudio.Text = "Студия: //название студии";
            // 
            // labelActor
            // 
            this.labelActor.AutoSize = true;
            this.labelActor.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelActor.Location = new System.Drawing.Point(0, 313);
            this.labelActor.Name = "labelActor";
            this.labelActor.Padding = new System.Windows.Forms.Padding(30, 5, 30, 5);
            this.labelActor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelActor.Size = new System.Drawing.Size(235, 27);
            this.labelActor.TabIndex = 8;
            this.labelActor.Text = "Актёры: //список актёров";
            // 
            // labelWriter
            // 
            this.labelWriter.AutoSize = true;
            this.labelWriter.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelWriter.Location = new System.Drawing.Point(0, 286);
            this.labelWriter.Name = "labelWriter";
            this.labelWriter.Padding = new System.Windows.Forms.Padding(30, 5, 30, 5);
            this.labelWriter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelWriter.Size = new System.Drawing.Size(306, 27);
            this.labelWriter.TabIndex = 7;
            this.labelWriter.Text = "Сценаристы: //список сценарисотов";
            // 
            // labelDirector
            // 
            this.labelDirector.AutoSize = true;
            this.labelDirector.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelDirector.Location = new System.Drawing.Point(0, 249);
            this.labelDirector.Name = "labelDirector";
            this.labelDirector.Padding = new System.Windows.Forms.Padding(30, 15, 30, 5);
            this.labelDirector.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelDirector.Size = new System.Drawing.Size(285, 37);
            this.labelDirector.TabIndex = 6;
            this.labelDirector.Text = "Режиссёры: //список режиссёров";
            // 
            // labelAge
            // 
            this.labelAge.AutoSize = true;
            this.labelAge.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelAge.Location = new System.Drawing.Point(0, 195);
            this.labelAge.Name = "labelAge";
            this.labelAge.Padding = new System.Windows.Forms.Padding(30, 5, 30, 5);
            this.labelAge.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelAge.Size = new System.Drawing.Size(270, 27);
            this.labelAge.TabIndex = 4;
            this.labelAge.Text = "Возрастной рейтинг: //рейтинг";
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelYear.Location = new System.Drawing.Point(0, 168);
            this.labelYear.Name = "labelYear";
            this.labelYear.Padding = new System.Windows.Forms.Padding(30, 5, 30, 5);
            this.labelYear.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelYear.Size = new System.Drawing.Size(187, 27);
            this.labelYear.TabIndex = 3;
            this.labelYear.Text = "Год выпуска: //год";
            // 
            // labelGenre
            // 
            this.labelGenre.AutoSize = true;
            this.labelGenre.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelGenre.Location = new System.Drawing.Point(0, 141);
            this.labelGenre.Name = "labelGenre";
            this.labelGenre.Padding = new System.Windows.Forms.Padding(30, 5, 30, 5);
            this.labelGenre.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelGenre.Size = new System.Drawing.Size(228, 27);
            this.labelGenre.TabIndex = 2;
            this.labelGenre.Text = "Жанры: //список жанров";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelDescription.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDescription.Location = new System.Drawing.Point(0, 85);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Padding = new System.Windows.Forms.Padding(30, 15, 10, 20);
            this.labelDescription.Size = new System.Drawing.Size(6425, 56);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = resources.GetString("labelDescription.Text");
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelName.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.Location = new System.Drawing.Point(0, 0);
            this.labelName.Name = "labelName";
            this.labelName.Padding = new System.Windows.Forms.Padding(30, 15, 10, 10);
            this.labelName.Size = new System.Drawing.Size(314, 54);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "// Название фильма";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(400, 596);
            this.panel4.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(19, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(381, 460);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.button4);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 54);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(851, 31);
            this.panel7.TabIndex = 9;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(30, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(157, 25);
            this.button4.TabIndex = 0;
            this.button4.Text = "Редактировать";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // vScrollBar2
            // 
            this.vScrollBar2.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar2.Location = new System.Drawing.Point(1251, 0);
            this.vScrollBar2.Name = "vScrollBar2";
            this.vScrollBar2.Size = new System.Drawing.Size(21, 596);
            this.vScrollBar2.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 653);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Коллекция фильмов";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panelSearchResults.ResumeLayout(false);
            this.panelView.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelActor;
        private System.Windows.Forms.Label labelWriter;
        private System.Windows.Forms.Label labelDirector;
        private System.Windows.Forms.Label labelStudio;
        private System.Windows.Forms.Label labelAge;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.Label labelGenre;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelSearchResults;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.VScrollBar vScrollBar2;
    }
}

