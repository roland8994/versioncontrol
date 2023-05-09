using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AZQKAD2.Absztrakt
{
    public abstract class Rating : Button
    {
        public Rating()
        {
            Width = 200;
            Height = 50;
            Click += Rating_Click;
            
        }

        private void Rating_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetDisplayText);
        }

        private string Title;

        public string _title
        {
            get { return Title; }
            set { Title = value; }
        }

        private string RatingType;

        public string _ratingType
        {
            get { return RatingType; }
            set { RatingType = value;
           //     if (_ratingType = "G") BackColor = Color.White;
             //   if (_ratingType = "PG") BackColor = Color.Blue;
             //   if (_ratingType = "PG-13") BackColor = Color.Green;
              //  if (_ratingType = "R") BackColor = Color.Yellow;
                               
            }

            
        }
        

        protected abstract string GetDisplayText { get; set; }


    }
}
