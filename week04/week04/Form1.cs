using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace week04
{
    public partial class Form1 : Form
    {
        List<Flat> _flats;
        RealEstateEntities context = new RealEstateEntities();

        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;

        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
        }

        void LoadData()
        {
            _flats = context.Flats.ToList();
        }

        void CreateExcel()
        {
            try
            {


                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value); //missing value miatt van a reflection a usingnál
                xlSheet = xlWB.ActiveSheet;

                CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string ErrorMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(ErrorMsg, "Error");

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;

            }


        }

        void CreateTable() //private nem feltétlenül kell
        {
            string[] headers = new string[] {  //fejléc
     "Kód",
     "Eladó",
     "Oldal",
     "Kerület",
     "Lift",
     "Szobák száma",
     "Alapterület (m2)",
     "Ár (mFt)",
     "Négyzetméter ár (Ft/m2)"};

            for (int i = 0; i < headers.Length; i++)  // tömb első eleme 0, ezért kell i+1
            {
                xlSheet.Cells[/*sor*/1, /*oszlop*/i+1] = headers[i];
            }

            object[,] values = new object[_flats.Count, headers.Length];

            int counter = 0;
            foreach (Flat f in _flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;
                if (f.Elevator)
                    values[counter, 4] = "Igen";
                else
                    values[counter, 4] = "Nem";

                //módosítani kell, hogy igen/nem jelenjen meg
                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price;
                values[counter, 8] = "="+GetCell(counter+2, 8)+"*1000000"+"/"+ GetCell(counter + 2, 7); //Excelben I oszlop
                counter++;
            }

            xlSheet.get_Range(
            GetCell(2, 1),
            GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            //xlSheet.get_Range(GetCell(2, 1), values.GetLength(0), values.GetLength(1)).Value2

        }
        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }
    }
}
