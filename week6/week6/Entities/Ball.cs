using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week6.Abstractions;

namespace week6.Entities
{
    public class Ball : Toy
    {
        

        protected override void DrawImage(Graphics g)
        {
            // Brush brush = new SolidBrush(Color.Blue);
            g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }

        

    }
}
