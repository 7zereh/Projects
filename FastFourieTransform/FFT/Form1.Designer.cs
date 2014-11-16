namespace FFT
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.FFTransform = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.RFT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ZoomIn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(258, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "1. Отваряне на изображение";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox1.Location = new System.Drawing.Point(12, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 300);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(32, 440);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 38);
            this.button2.TabIndex = 2;
            this.button2.Text = "3. Визуализиране на фаза";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox2.Location = new System.Drawing.Point(351, 322);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(256, 256);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox3.Location = new System.Drawing.Point(351, 31);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(256, 256);
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(164, 440);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 38);
            this.button3.TabIndex = 6;
            this.button3.Text = "4. Визуализиране на амплитуда";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FFTransform
            // 
            this.FFTransform.Location = new System.Drawing.Point(32, 396);
            this.FFTransform.Name = "FFTransform";
            this.FFTransform.Size = new System.Drawing.Size(258, 38);
            this.FFTransform.TabIndex = 7;
            this.FFTransform.Text = "2. Преобразование на Фурие";
            this.FFTransform.UseVisualStyleBackColor = true;
            this.FFTransform.Click += new System.EventHandler(this.FFTransform_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBox4.Location = new System.Drawing.Point(645, 184);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(256, 256);
            this.pictureBox4.TabIndex = 8;
            this.pictureBox4.TabStop = false;
            // 
            // RFT
            // 
            this.RFT.Location = new System.Drawing.Point(32, 484);
            this.RFT.Name = "RFT";
            this.RFT.Size = new System.Drawing.Size(258, 38);
            this.RFT.TabIndex = 9;
            this.RFT.Text = "Обратно преобразование на Фурие";
            this.RFT.UseVisualStyleBackColor = true;
            this.RFT.Click += new System.EventHandler(this.RFT_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(11, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 22);
            this.label1.TabIndex = 10;
            this.label1.Text = "Входно изображение:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(348, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "Изображение на фазата:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(347, 301);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 18);
            this.label3.TabIndex = 12;
            this.label3.Text = "Изображение на амплитудата:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(642, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(260, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "Обратно преобразование на Фурие:";
            // 
            // ZoomIn
            // 
            this.ZoomIn.Location = new System.Drawing.Point(351, 584);
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.Size = new System.Drawing.Size(162, 23);
            this.ZoomIn.TabIndex = 14;
            this.ZoomIn.Text = "Приближаване";
            this.ZoomIn.UseVisualStyleBackColor = true;
            this.ZoomIn.Click += new System.EventHandler(this.ZoomIn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(938, 618);
            this.Controls.Add(this.ZoomIn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RFT);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.FFTransform);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "2D Бързо преобразование на Фурие";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button FFTransform;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button RFT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ZoomIn;
    }
}

