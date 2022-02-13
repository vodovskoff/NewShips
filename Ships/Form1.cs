using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ships
{
    public partial class Form1 : Form
    {
        Vessel newShip = new Vessel();
        bool isExcelOK = false;
        public Form1()
        {
            InitializeComponent();
        }

        //Ищем файл
        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"c:\\users\user\documents";
                openFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    newShip.readFromExcel(filePath);
                    btnDraw.Visible = true;
                    isExcelOK = true;
                }
            }
            //MessageBox.Show(filePath, "File Content at path: " + filePath, MessageBoxButtons.OK);
            labelPathExcel.Text = filePath +  "\n\n"+ "Файл считан. Объём корпуса: ~" + newShip.Volume();

        }

        //Переход на форму с рисовалкой
        private void btnDraw_Click(object sender, EventArgs e)
        {
            DrawFowm drawFowm = new DrawFowm(newShip);
            drawFowm.Show();
            //drawFowm.draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"c:\\users\user\documents";
                openFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    newShip.readPartsExcel(filePath);
                    btnDraw.Visible = true;
                    isExcelOK = true;
                }
            }
            //MessageBox.Show(filePath, "File Content at path: " + filePath, MessageBoxButtons.OK);
            //labelPathExcel.Text = filePath + "\n\n" + "Файл считан. Объём корпуса: ~" + newShip.Volume();
        }
    }
}
