using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ships
{
    public partial class DrawFowm : Form
    {
        const int X_OFFSET = 490;
        Vessel ship;
        Vessel.Part tempPart;
        Vessel.Part.Component tempComp;
        public DrawFowm(Vessel s)
        {
            this.ship = s;
            InitializeComponent();
            listBox2.Visible = false;
            label2.Visible = false;
            button4.Visible = false;
        }
        //Кнопка "Рисовать"
        public void button2_Click(object sender, EventArgs e)
        {
            drawTopProjection();
            drawSideProjection();
            drawTanksInSide();
            drawTanksTop();
            listBox1.Visible = true;
            label3.Visible = true;
        }
        private void DrawFowm_Load(object sender, EventArgs e)
        {

        }
        //Вид спереди где шпаки друг на друга накладываются
        public void draw()
        {
            Graphics shipGraph = pictureBox2.CreateGraphics();
            for (int k = 0; k < ship.frames.Count; k++)
            {
                Pen pen = new Pen(Color.Black, 2f);

                //Рисуем правую часть
                PointF[] points = new PointF[ship.frames[k].cordinates.Count];
                for (int i = 0; i < ship.frames[k].cordinates.Count; i++)
                {
                    points[i] = new PointF(40 * (float)ship.frames[k].cordinates[i].x + 350, -40 * (float)ship.frames[k].cordinates[i].y + 500);
                }
                for (int i = 0; i < points.Length - 1; i++)
                {
                    shipGraph.DrawLine(pen, points[i], points[i + 1]);

                }

                //Левую часть
                PointF[] points1 = new PointF[ship.frames[k].cordinates.Count];
                for (int i = 0; i < ship.frames[k].cordinates.Count; i++)
                {
                    points1[i] = new PointF(-40 * (float)ship.frames[k].cordinates[i].x + 350, -40 * (float)ship.frames[k].cordinates[i].y + 500);
                }
                for (int i = 0; i < points.Length - 1; i++)
                {
                    shipGraph.DrawLine(pen, points1[i], points1[i + 1]);

                }
            }
        }
        //проекция с боку
        public void drawSideProjection()
        {
            Graphics shipGraph = pictureBox2.CreateGraphics();
            PointF[] pointsMin = new PointF[ship.frames.Count];
            PointF[] pointsMax = new PointF[ship.frames.Count];
            Pen pen = new Pen(Color.Black, 2f);

            for (int k = 0; k < ship.frames.Count; k++)
            {
                double min = 1000;
                double max = -1000;
                PointF[] points = new PointF[ship.frames[k].cordinates.Count];

                for (int i = 0; i < ship.frames[k].cordinates.Count; i++)
                {
                    if (ship.frames[k].cordinates[i].y < min) min = ship.frames[k].cordinates[i].y;
                    if (ship.frames[k].cordinates[i].y > max) max = ship.frames[k].cordinates[i].y;

                }
                //pointsMin[k] = new PointF(7*(float)min+800, 7*(float) ship.frames[k].position+450);
                //pointsMax[k] = new PointF(7 * (float)max + 800, 7 * (float)ship.frames[k].position + 450);

                pointsMin[k] = new PointF(11 * (float)ship.frames[k].position + X_OFFSET, -11 * (float)min + 470);
                pointsMax[k] = new PointF(11 * (float)ship.frames[k].position + X_OFFSET, -11 * (float)max + 470);
            }
            for (int k = 0; k < ship.frames.Count-1; k++)
            {
                shipGraph.DrawLine(pen, pointsMax[k], pointsMax[k+1]);
                shipGraph.DrawLine(pen, pointsMin[k], pointsMin[k + 1]);
            }
        }
        //Проекция сверху
        public void drawTopProjection()
        {
            {
                Graphics shipGraph = pictureBox2.CreateGraphics();
                PointF[] pointsMin = new PointF[ship.frames.Count];
                PointF[] pointsMax = new PointF[ship.frames.Count];
                PointF[] pointsMin1 = new PointF[ship.frames.Count];
                PointF[] pointsMax1 = new PointF[ship.frames.Count];
                Pen pen = new Pen(Color.Black, 2f);

                for (int k = 0; k < ship.frames.Count; k++)
                {
                    double min = 1000;
                    double max = -1000;
                    PointF[] points = new PointF[ship.frames[k].cordinates.Count];

                    for (int i = 0; i < ship.frames[k].cordinates.Count; i++)
                    {
                        if (ship.frames[k].cordinates[i].x < min) min = ship.frames[k].cordinates[i].x;
                        if (ship.frames[k].cordinates[i].x > max) max = ship.frames[k].cordinates[i].x;

                    }
                    pointsMin[k] = new PointF(11 * (float)ship.frames[k].position + X_OFFSET,  11 * (float)min + 150);
                    pointsMax[k] = new PointF(11 * (float)ship.frames[k].position + X_OFFSET, 11 * (float)max + 150);

                    pointsMin1[k] = new PointF(11 * (float)ship.frames[k].position + X_OFFSET, -11 * (float)min + 150);
                    pointsMax1[k] = new PointF(11 * (float)ship.frames[k].position + X_OFFSET, -11 * (float)max + 150);
                }
                for (int k = 0; k < ship.frames.Count - 1; k++)
                {
                    shipGraph.DrawLine(pen, pointsMax[k], pointsMax[k + 1]);
                    shipGraph.DrawLine(pen, pointsMax1[k], pointsMax1[k + 1]);
                }
                shipGraph.DrawLine(pen, pointsMax1[pointsMax1.Length-1], pointsMax[pointsMax.Length-1]);
            }
        }
        //проекция танков сверху
        public void drawTanksTop()
        {
            Pen pen = new Pen(Color.Black, 2f);
            Graphics shipGraph = pictureBox2.CreateGraphics();

            foreach (Vessel.Part part in ship.parts)
            {
                foreach (Vessel.Part.Component component in part.Components)
                {
                    PointF[] pointsMin = new PointF[component.ComponentFrames.Count];
                    PointF[] pointsMax = new PointF[component.ComponentFrames.Count];
                    PointF[] pointsMin1 = new PointF[component.ComponentFrames.Count];
                    PointF[] pointsMax1 = new PointF[component.ComponentFrames.Count];

                    for (int k = 0; k < component.ComponentFrames.Count; k++)
                    {
                        double min = 1000;
                        double max = -1000;
                        PointF[] points = new PointF[component.ComponentFrames[k].cordinates.Count];

                        for (int i = 0; i < component.ComponentFrames[k].cordinates.Count; i++)
                        {
                            if (component.ComponentFrames[k].cordinates[i].x < min) min = component.ComponentFrames[k].cordinates[i].x;
                            if (component.ComponentFrames[k].cordinates[i].x > max) max = component.ComponentFrames[k].cordinates[i].x;

                        }
                        pointsMin[k] = new PointF(11 * (float)component.ComponentFrames[k].position + X_OFFSET, 11 * (float)min + 150);
                        pointsMax[k] = new PointF(11 * (float)component.ComponentFrames[k].position + X_OFFSET, 11 * (float)max + 150);

                        pointsMin1[k] = new PointF(11 * (float)component.ComponentFrames[k].position + X_OFFSET, -11 * (float)min + 150);
                        pointsMax1[k] = new PointF(11 * (float)component.ComponentFrames[k].position + X_OFFSET, -11 * (float)max + 150);
                    }
                    for (int k = 0; k < component.ComponentFrames.Count - 1; k++)
                    {
                        shipGraph.DrawLine(pen, pointsMax[k], pointsMax[k + 1]);
                        shipGraph.DrawLine(pen, pointsMax1[k], pointsMax1[k + 1]);
                    }
                    shipGraph.DrawLine(pen, pointsMax[0], pointsMax1[0]);
                    shipGraph.DrawLine(pen, pointsMax1[component.ComponentFrames.Count - 1], pointsMax[component.ComponentFrames.Count - 1]);
                    shipGraph.DrawLine(pen, pointsMax1[pointsMax1.Length - 1], pointsMax[pointsMax.Length - 1]);
            }
        }
        }
        //Проекция танков сбоку
        public void drawTanksInSide()
        {
            int comps = 0;
            Pen pen = new Pen(Color.Black, 2f);
            Graphics shipGraph = pictureBox2.CreateGraphics();

            foreach (Vessel.Part part in ship.parts)
            {
                foreach(Vessel.Part.Component component in part.Components)
                {
                    comps++;
                    PointF[] pointsMin = new PointF[component.ComponentFrames.Count];
                    PointF[] pointsMax = new PointF[component.ComponentFrames.Count];
                    for (int k = 0; k<component.ComponentFrames.Count; k++)
                    {
                        double min = 1000;
                        double max = -1000;
                        PointF[] points = new PointF[component.ComponentFrames[k].cordinates.Count];
                        for (int i = 0; i < component.ComponentFrames[k].cordinates.Count; i++)
                        {
                            if (component.ComponentFrames[k].cordinates[i].y < min) min = component.ComponentFrames[k].cordinates[i].y;
                            if (component.ComponentFrames[k].cordinates[i].y > max) max = component.ComponentFrames[k].cordinates[i].y;

                        }
                        pointsMin[k] = new PointF(11 * (float)component.ComponentFrames[k].position + X_OFFSET, -11 * (float)min + 470);
                        pointsMax[k] = new PointF(11 * (float)component.ComponentFrames[k].position + X_OFFSET, -11 * (float)max + 470);
                    }
                    for (int k = 0; k < component.ComponentFrames.Count - 1; k++)
                    {
                        shipGraph.DrawLine(pen, pointsMax[k], pointsMax[k + 1]);
                        shipGraph.DrawLine(pen, pointsMin[k], pointsMin[k + 1]);
                    }
                    shipGraph.DrawLine(pen, pointsMax[0], pointsMin[0]);
                    shipGraph.DrawLine(pen, pointsMin[component.ComponentFrames.Count - 1], pointsMax[component.ComponentFrames.Count - 1]);
                }
            }
            Console.WriteLine(comps);
            FillPartsListBox();
        }
        public void FillPartsListBox()
        {
            int index = 0;
            foreach (var Part in ship.parts)
            {
                listBox1.Items.Insert(index, Part.PartName);
                index++;
            }
        }
        public Vessel.Part GetComponentName()
        {
            listBox2.Visible = true;
            string Selected = listBox1.SelectedItem.ToString();
            int index = 0;
            Vessel.Part CurrentPart = null;
             foreach (var Part in ship.parts)
            {
                if (Part.PartName == Selected)
                {
                    CurrentPart = Part;
                }
            }
            return CurrentPart;
        }
        public void FillComponentsListBox()
        {
            listBox2.Visible = true;
            string Selected = listBox1.SelectedItem.ToString();
            int index = 0;
            Vessel.Part CurrentPart = null;
            foreach (var Part in ship.parts)
            {
                if (Part.PartName == Selected)
                {
                    CurrentPart = Part;
                }
            }
            tempPart = CurrentPart;
            foreach (var Component in CurrentPart.Components)
            {
                listBox2.Items.Insert(index, Component.ComponentName);
                index++;
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            label2.Visible = true;
            //button4.Visible = true;
            FillComponentsListBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            label2.Visible = true;
            //button4.Visible = true;
            FillComponentsListBox();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawTanksTop();
            drawTanksInSide();
            button5.Visible = true;
            textBox1.Visible = true;
            label4.Visible = true;
            var SelectedPart = GetComponentName();
            string SelectedComponent = null;
            if (listBox2.SelectedIndex != -1)
            {
                if (listBox2.SelectedItem.ToString() != null)
                {
                    SelectedComponent = listBox2.SelectedItem.ToString();
                }
                Vessel.Part.Component CurrentComponent = null;
                foreach (var Component in SelectedPart.Components)
                {
                    if (Component.ComponentName == SelectedComponent)
                    {
                        CurrentComponent = Component;
                    }
                }
                Pen pen = new Pen(Color.Red, 2f);
                Graphics shipGraph = pictureBox2.CreateGraphics();

                PointF[] pointsMin = new PointF[CurrentComponent.ComponentFrames.Count];
                PointF[] pointsMax = new PointF[CurrentComponent.ComponentFrames.Count];
                PointF[] pointsMin1 = new PointF[CurrentComponent.ComponentFrames.Count];
                PointF[] pointsMax1 = new PointF[CurrentComponent.ComponentFrames.Count];

                for (int k = 0; k < CurrentComponent.ComponentFrames.Count; k++)
                {
                    double min = 1000;
                    double max = -1000;
                    PointF[] points = new PointF[CurrentComponent.ComponentFrames[k].cordinates.Count];

                    for (int i = 0; i < CurrentComponent.ComponentFrames[k].cordinates.Count; i++)
                    {
                        if (CurrentComponent.ComponentFrames[k].cordinates[i].x < min) min = CurrentComponent.ComponentFrames[k].cordinates[i].x;
                        if (CurrentComponent.ComponentFrames[k].cordinates[i].x > max) max = CurrentComponent.ComponentFrames[k].cordinates[i].x;

                    }
                    pointsMin[k] = new PointF(11 * (float)CurrentComponent.ComponentFrames[k].position + X_OFFSET, 11 * (float)min + 150);
                    pointsMax[k] = new PointF(11 * (float)CurrentComponent.ComponentFrames[k].position + X_OFFSET, 11 * (float)max + 150);

                    pointsMin1[k] = new PointF(11 * (float)CurrentComponent.ComponentFrames[k].position + X_OFFSET, -11 * (float)min + 150);
                    pointsMax1[k] = new PointF(11 * (float)CurrentComponent.ComponentFrames[k].position + X_OFFSET, -11 * (float)max + 150);
                }
                for (int k = 0; k < CurrentComponent.ComponentFrames.Count - 1; k++)
                {
                    shipGraph.DrawLine(pen, pointsMax[k], pointsMax[k + 1]);
                    shipGraph.DrawLine(pen, pointsMax1[k], pointsMax1[k + 1]);
                }
                shipGraph.DrawLine(pen, pointsMax[0], pointsMax1[0]);
                shipGraph.DrawLine(pen, pointsMax1[CurrentComponent.ComponentFrames.Count - 1], pointsMax[CurrentComponent.ComponentFrames.Count - 1]);
                shipGraph.DrawLine(pen, pointsMax1[pointsMax1.Length - 1], pointsMax[pointsMax.Length - 1]);

                PointF[] pointsMinSide = new PointF[CurrentComponent.ComponentFrames.Count];
                PointF[] pointsMaxSide = new PointF[CurrentComponent.ComponentFrames.Count];
                for (int k = 0; k < CurrentComponent.ComponentFrames.Count; k++)
                {
                    double min = 1000;
                    double max = -1000;
                    PointF[] points = new PointF[CurrentComponent.ComponentFrames[k].cordinates.Count];
                    for (int i = 0; i < CurrentComponent.ComponentFrames[k].cordinates.Count; i++)
                    {
                        if (CurrentComponent.ComponentFrames[k].cordinates[i].y < min) min = CurrentComponent.ComponentFrames[k].cordinates[i].y;
                        if (CurrentComponent.ComponentFrames[k].cordinates[i].y > max) max = CurrentComponent.ComponentFrames[k].cordinates[i].y;

                    }
                    pointsMinSide[k] = new PointF(11 * (float)CurrentComponent.ComponentFrames[k].position + X_OFFSET, -11 * (float)min + 470);
                    pointsMaxSide[k] = new PointF(11 * (float)CurrentComponent.ComponentFrames[k].position + X_OFFSET, -11 * (float)max + 470);
                }
                for (int k = 0; k < CurrentComponent.ComponentFrames.Count - 1; k++)
                {
                    shipGraph.DrawLine(pen, pointsMaxSide[k], pointsMaxSide[k + 1]);
                    shipGraph.DrawLine(pen, pointsMinSide[k], pointsMinSide[k + 1]);
                }
                shipGraph.DrawLine(pen, pointsMaxSide[0], pointsMinSide[0]);
                shipGraph.DrawLine(pen, pointsMinSide[CurrentComponent.ComponentFrames.Count - 1], pointsMaxSide[CurrentComponent.ComponentFrames.Count - 1]);
                button5.Text = "Добавить в " + tempPart.PartName + ", " + CurrentComponent.ComponentName;
                tempComp = CurrentComponent;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="")
            tempComp.CurrentLoad = Double.Parse(textBox1.Text);
        }
    }
}
