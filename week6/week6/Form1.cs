using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week6.Abstractions;
using week6.Entities;

namespace week6
{
    public partial class Form1 : Form
    {
        private Toy _nextToy;
        List<Toy> _toys = new List<Toy>();

        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; DisplayNext(); }

        }


        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
            
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            mainPanel.Controls.Add(toy);
            toy.Left = -toy.Width;
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0; //legjobboldali labda   régebben ball volt toy helyett
            foreach (var toy in _toys)  //minden labdán végigmegyek, ami a listában van
            {
                toy.MoveToy();
                if (toy.Left > maxPosition)
                    maxPosition = toy.Left;
                
            }

            if (maxPosition > 1000)
            {
                var oldestToy = _toys[0];
                _toys.Remove(oldestToy);
                mainPanel.Controls.Remove(oldestToy);
            }
        }

        private void carbtn_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void ballbtn_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory();
        }

        private void DisplayNext()
        {
            if (_nextToy != null)
                Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);

        }

    }
}
