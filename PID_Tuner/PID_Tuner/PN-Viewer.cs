using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using MasterLibrary.MathX;
using MasterLibrary.Extentions;

namespace PID_Tuner
{
    public partial class PN_Viewer : UserControl
    {
        Transfer _transfer;
        public Transfer Transfer { get { return _transfer; } set { _transfer = value; Invalidate(); } }
        PNColorSet colorSet = new PNColorSet();
        
        public PN_Viewer()
        {
            InitializeComponent();
        }

        private void PN_Viewer_Load(object sender, EventArgs e)
        {

        }


        private void PN_Viewer_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(colorSet.Background);

            V2D size = this.Size;
            V2D org = size / 2;
            double radius = Math.Min(this.Width, this.Height) / 2 - 2;

            //Draw grid.
            for (int i = 0; i < 20; i++)
            {
                V2D v = new V2D(radius, 0);
                v.Rad = Math.PI * i / 10; 
                if(i < 10)
                    g.DrawCircle(colorSet.Grid, org, radius * (i + 1) / 10);
                g.DrawV2D(colorSet.Grid, v, org);
            }


            if(Transfer != null)
            {
                foreach (Complex pole in Transfer.Poles)
                    g.DrawCross(colorSet.Pole, org + (V2D)(pole * radius),7);

                foreach (Complex zero in Transfer.Zeroes)
                    g.DrawCircle(colorSet.Pole, org + (V2D)(zero * radius), 7);
            }

        }
    }

    public class PNColorSet
    {
        public Color Background { get; set; } = Color.Black;
        public Pen Grid { get; set; } = Pens.LightGray;
        public Pen Pole { get; set; } = new Pen(Color.Red, 2);
        public Pen Zero { get; set; } = new Pen(Color.Red, 2);
    }

}
