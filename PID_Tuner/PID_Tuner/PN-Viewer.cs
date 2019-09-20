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
    [DefaultProperty("DataSource")]
    public partial class PN_Viewer : UserControl
    {
        private IBindingList _dataSource;
        [Bindable(true),
        Category("Data"),
        DefaultValue(null),
        Description("The data source used to build up the bulleted list."),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IBindingList DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
                _dataSource.ListChanged += _dataSource_ListChanged;
            }
        }

        private void _dataSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            Invalidate();
        }

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
            double radius = Math.Min(this.Width, this.Height) / 2;

            //Draw grid.
            for (int i = 0; i < 20; i++)
            {
                V2D v = new V2D(radius, 0);
                v.Rad = Math.PI * i / 10; 
                if(i < 10)
                    g.DrawCircle(colorSet.Grid, org, radius * (i + 1) / 10);
                g.DrawV2D(colorSet.Grid, v, org);
            }

            //Draw PN points
            if (_dataSource != null)
            {
                foreach (object o in _dataSource)
                {
                    switch (o)
                    {
                        case Zero z:
                            g.DrawCircle(colorSet.Pole, org + z * radius, 7);
                            if (z.Y != 0)
                                g.DrawCircle(colorSet.Pole, org + z.Conjugate() * radius, 7);
                            break;
                        case Pole p:
                            g.DrawCross(colorSet.Zero, org + p * radius, 7);
                            if (p.Y != 0)
                                g.DrawCross(colorSet.Pole, org + p.Conjugate() * radius, 7);
                            break;
                    }
                }
            }
        }
    }


    public class Pole : V2D
    {

    }

    public class Zero : V2D
    {

    }


    public class PNColorSet
    {
        public Color Background { get; set; } = Color.Black;
        public Pen Grid { get; set; } = Pens.LightGray;
        public Pen Pole { get; set; } = new Pen(Color.Red, 2);
        public Pen Zero { get; set; } = new Pen(Color.Red, 2);
    }

}
