using _5HET_AZQKAD.Entities; //osztályon belül, ha az Entities-t átírom, akkor azt kell ide beírni (nem a mappa neve a lényeg)
using _5HET_AZQKAD.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace _5HET_AZQKAD
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        public Form1()
        {
            InitializeComponent();

            dataGridView1.DataSource = Rates;

            //var r = GetRates();
            // GetXmlData(r);
            GetXmlData(GetRates());
            PrintChart();
        }

        private void PrintChart()
        {
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            //series.BorderWidth = 2; a zöldek csak formázások

            //var legend = chartRateData.Legends[0];
            //legend.Enabled = false;

            //var chartArea = chartRateData.ChartAreas[0];
            //chartArea.AxisX.MajorGrid.Enabled = false;
            //chartArea.AxisY.MajorGrid.Enabled = false;
            //chartArea.AxisY.IsStartedFromZero = false;
        }

        private string GetRates()
        {
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;

            return result; // mivel string a GetRates, nem pedig void
        }

        private void GetXmlData(string result)
        {
            var xml = new XmlDocument();
            //LoadXml tölti be a string típust
            xml.LoadXml(result);

            foreach (XmlElement item in xml.DocumentElement)
            {
                var date = item.GetAttribute("date");

                var rate = (XmlElement)item.ChildNodes[0];
                var currency = rate.GetAttribute("curr");
                var unit = int.Parse(rate.GetAttribute("unit"));
                var value = decimal.Parse(rate.InnerText);
              



                Rates.Add(new RateData()
                {
                    Date = DateTime.Parse(date),
                    Currency = currency,
                    Value = unit != 0 //ha ez igaz
                        ? value / unit // akkor a kérdőjel utáni értéket adja
                        : 0 // ha nem igaz, akkor ezt
                });
                //Rates.Add(r);
            }
        }
    }
}
