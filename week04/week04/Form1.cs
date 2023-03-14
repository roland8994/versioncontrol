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

                //CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string ErrorMsg= string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(ErrorMsg, "Error");

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;

            }


        }
    }
}
