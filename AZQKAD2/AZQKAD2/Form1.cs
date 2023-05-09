using AZQKAD2.Absztrakt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AZQKAD2
{
    public partial class Form1 : Form
    {
        List<Rating> _ratings = new List<Rating>();
        public Form1()
        {
            InitializeComponent();
            //harmas(valami);
            Displaymovies();
            AutoScroll = true;



        }
        public void Displaymovies()
        {
            var topPosition = 0;
            var sortedMovies = from x in _ratings
                               select x;
            foreach (var item in sortedMovies)
            {
                item.Top = topPosition;
                Controls.Add(item);
                topPosition += item.Height;

            }
        }
        public void harmas()
        {

        }

        public string valami()
        {
            var moviesService = new ServiceReference1.ServiceSoapClient();
            moviesService.GetExportWithTitle("Halloween");
            return ("moviesService");
        }







    }

}

