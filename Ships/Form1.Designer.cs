
namespace Ships
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSearchFile = new System.Windows.Forms.Button();
            this.labelPathExcel = new System.Windows.Forms.Label();
            this.btnDraw = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSearchFile
            // 
            this.btnSearchFile.BackColor = System.Drawing.Color.White;
            this.btnSearchFile.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSearchFile.Location = new System.Drawing.Point(16, 12);
            this.btnSearchFile.Name = "btnSearchFile";
            this.btnSearchFile.Size = new System.Drawing.Size(244, 37);
            this.btnSearchFile.TabIndex = 0;
            this.btnSearchFile.Text = "Файл корпуса";
            this.btnSearchFile.UseVisualStyleBackColor = false;
            this.btnSearchFile.Click += new System.EventHandler(this.btnSearchFile_Click);
            // 
            // labelPathExcel
            // 
            this.labelPathExcel.AutoSize = true;
            this.labelPathExcel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelPathExcel.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPathExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.labelPathExcel.Location = new System.Drawing.Point(23, 69);
            this.labelPathExcel.Name = "labelPathExcel";
            this.labelPathExcel.Size = new System.Drawing.Size(0, 22);
            this.labelPathExcel.TabIndex = 1;
            // 
            // btnDraw
            // 
            this.btnDraw.BackColor = System.Drawing.Color.White;
            this.btnDraw.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDraw.Location = new System.Drawing.Point(27, 181);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(125, 40);
            this.btnDraw.TabIndex = 2;
            this.btnDraw.Text = "Начертить";
            this.btnDraw.UseVisualStyleBackColor = false;
            this.btnDraw.Visible = false;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(266, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(244, 37);
            this.button2.TabIndex = 4;
            this.button2.Text = "Файл танков";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(760, 242);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.labelPathExcel);
            this.Controls.Add(this.btnSearchFile);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonSearchFile;
        private System.Windows.Forms.Button btnSearchFile;
        private System.Windows.Forms.Label labelPathExcel;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Button button2;
    }
}

