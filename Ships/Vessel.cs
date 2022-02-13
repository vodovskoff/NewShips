using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    //Это класс корабля. Он умеет считывать из икселя, хранить шпангоуты
    //Рассчитывать объём тоже может
    public class Vessel
    {
        public void readFromExcel(string docPath)
        {
            Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(@docPath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

            Microsoft.Office.Interop.Excel.Range usedColumn1 = ObjWorkSheet.UsedRange.Columns[1];
            Microsoft.Office.Interop.Excel.Range usedColumn2 = ObjWorkSheet.UsedRange.Columns[2];

            System.Array myvalues1 = (System.Array)usedColumn1.Cells.Value2;
            System.Array myvalues2 = (System.Array)usedColumn2.Cells.Value2;

            string[] strArray1 = myvalues1.OfType<object>().Select(o => o.ToString()).ToArray();
            string[] strArray2 = myvalues2.OfType<object>().Select(o => o.ToString()).ToArray();

            // Выходим из программы Excel.
            ObjExcel.Quit();

            Console.WriteLine(strArray1[0]);

            bool[] Shpaki = new bool[strArray1.Length];
            for (int i = 0; i < Shpaki.Length; i++)
            {
                if (this.isAFrameCoordinate(strArray1[i])) Shpaki[i] = true;
                else Shpaki[i] = false;
            }

            int thisLineA = 0;
            int thisLineB = 0;
            while (thisLineA < strArray1.Length - 1)
            {

                Vessel.Frame newFrame = new Vessel.Frame();
                newFrame.position = this.getDoubleFrameF(strArray1[thisLineA]);
                thisLineA++;
                while (!Shpaki[thisLineA] && thisLineA < strArray1.Length - 1 && thisLineB < strArray2.Length - 1)
                {
                    newFrame.cordinates.Add(new Vessel.Pair(newFrame.getDoubleFrame(strArray1[thisLineA]), double.Parse(strArray2[thisLineB])));
                    thisLineA++;
                    thisLineB++;
                }
                this.frames.Add(newFrame);
            }
        }
        public List<Frame> frames = new List<Frame>();
        public List<Part> parts = new List<Part>();
        public Frame readOneFrame(ref int LineA, ref int LineB, string[] strArray1, string[] strArray2)
        {
            bool[] Shpaki = new bool[strArray1.Length];
            for (int i = 0; i < Shpaki.Length; i++)
            {
                if (this.isAFrameCoordinate(strArray1[i])) Shpaki[i] = true;
                else Shpaki[i] = false;
            }
            Vessel.Frame newFrame = new Vessel.Frame();
            newFrame.position = this.getDoubleFrameF(strArray1[LineA]);
            LineA++;
            LineB++;
            while (!Shpaki[LineA] && strArray1[LineA]!="Part" && strArray1[LineA] != "Component" && LineA < strArray1.Length - 1 && LineB < strArray2.Length - 1)
            {
                newFrame.cordinates.Add(new Vessel.Pair(newFrame.getDoubleFrame(strArray1[LineA]), double.Parse(strArray2[LineB])));
                LineA++;
                LineB++;
            }
            return newFrame;
        }
        public Part.Component readOneComponent(ref int LineA, ref int LineB, string[] strArray1, string[] strArray2)
        {
            Part.Component newComponent = new Part.Component();
            newComponent.ComponentName = strArray2[LineB];
            LineB++;
            LineA++;
            while(strArray1[LineA] != "Part" && strArray1[LineA] != "Component" && LineA < strArray1.Length - 1 && LineB < strArray2.Length - 1)
            {
                Vessel.Frame newFrame = readOneFrame(ref LineA,ref LineB, strArray1, strArray2);
                newComponent.ComponentFrames.Add(newFrame);
            }
            return newComponent;
        }
        public Part readOnePart(ref int LineA, ref int LineB, string[] strArray1, string[] strArray2)
        {
            Part newPart = new Part();
            newPart.PartName = strArray2[LineB];
            LineB++;
            LineA++;
            while (strArray1[LineA] != "Part" && LineA < strArray1.Length - 1 && LineB < strArray2.Length - 1)
            {
                Part.Component newComponent = readOneComponent(ref LineA, ref LineB, strArray1, strArray2);
                newPart.Components.Add(newComponent);
            }
            return newPart;
        }
        //Проверяет, является ли строка координатой шпангоута
        public bool isAFrameCoordinate(string toCheck)
        {
            if ((toCheck[toCheck.Length - 1] == 'F') || (toCheck[toCheck.Length - 1] == 'A'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void readPartsExcel(string docPath)
        {
            Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(@docPath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

            Microsoft.Office.Interop.Excel.Range usedColumn1 = ObjWorkSheet.UsedRange.Columns[1];
            Microsoft.Office.Interop.Excel.Range usedColumn2 = ObjWorkSheet.UsedRange.Columns[2];

            System.Array myvalues1 = (System.Array)usedColumn1.Cells.Value2;
            System.Array myvalues2 = (System.Array)usedColumn2.Cells.Value2;

            string[] strArray1 = myvalues1.OfType<object>().Select(o => o.ToString()).ToArray();
            string[] strArray2 = myvalues2.OfType<object>().Select(o => o.ToString()).ToArray();

            // Выходим из программы Excel.
            ObjExcel.Quit();

            //Console.WriteLine(strArray1[0]);

            int thisLineA = 0;
            int thisLineB = 0;
            
            while(thisLineA<strArray1.Length-1 && thisLineB < strArray2.Length - 1)
            {
                parts.Add(readOnePart(ref thisLineA, ref thisLineB, strArray1, strArray2));
            }
        }
        //Извлекает координату шпангоута в дабл
        public double getDoubleFrameF(string strCord)
        {
            if (strCord[strCord.Length - 1] == 'F')
            {
                return double.Parse(strCord.Substring(0, strCord.Length - 1));
            }
            else
                return -double.Parse(strCord.Substring(0, strCord.Length - 1));

        }
        //объём через сумму объёмов призм
        public double Volume()
        {
            double vol = 0;
            for (int i = 0; i < frames.Count - 1; i++)
            {
                double h = Math.Abs(frames[i].position - frames[i + 1].position);
                vol = vol + h / 3 *
                    (frames[i].getSquare() + frames[i + 1].getSquare() + Math.Sqrt(frames[i].getSquare() * frames[i + 1].getSquare()));
            }
            return vol;
        }

        //Класс проекций. Хранит лист координат точек и считает площадь по формуле Гаусса.
        public class Frame
        {
            public List<Pair> cordinates = new List<Pair>();

            public double getSquare()
            {
                double sum = 0;
                for (int i = 0; i < cordinates.Count; i++)
                {
                    if (i == cordinates.Count - 1)
                    {
                        sum = sum + cordinates[i].x * (cordinates[0].y - cordinates[i - 1].y);
                    }
                    else
                    if (i == 0)
                    {
                        sum = sum + cordinates[i].x * (cordinates[i + 1].y - cordinates[cordinates.Count - 1].y);
                    }
                    else
                        sum = sum + cordinates[i].x * (cordinates[i + 1].y - cordinates[i - 1].y);
                }
                return Math.Abs(sum);
            }
            public double getDoubleFrame(string strCord)
            {
                if (strCord[strCord.Length - 1] == 'P')
                {
                    return -double.Parse(strCord.Substring(0, strCord.Length - 1));
                }
                else
                if (strCord[strCord.Length - 1] == 'S')
                {
                    return double.Parse(strCord.Substring(0, strCord.Length - 1));
                }
                else
                {
                    return double.Parse(strCord);
                }
            }

            public double position;

        }
        public class Pair
        {
            public double x, y;
            public Pair(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public class Part
        {
            public string PartName;
            public class Component
            {
                public string ComponentName;
                public List<Frame> ComponentFrames = new List<Frame>();
                public double CurrentLoad = 0;
                public double MaxCapacity = 0;
                public void AddCargo(double Cargo)
                {
                    this.CurrentLoad = Cargo;
                }
                public void RemoveCargo(double Cargo)
                {
                    this.CurrentLoad = 0;
                }
            }
            public List<Component> Components = new List<Component>();

        }

    }
}