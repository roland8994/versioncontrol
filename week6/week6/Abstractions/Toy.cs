using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week6.Abstractions
{
    public abstract class Toy : Label
    {
        public Toy()
        {
            Width = 50;
            Height = Width;
            AutoSize = false;
            Paint += Toy_Paint;
        }

        private void Toy_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }

        protected abstract void DrawImage(Graphics g);
        

        public void MoveToy()
        {
            Left += 1;
        }

    }
}
