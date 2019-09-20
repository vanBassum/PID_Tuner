using MasterLibrary.MathX;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PID_Tuner
{
    public partial class F_Filter : Form
    {
        BindingList<V2D> pns = new BindingList<V2D>();
        public F_Filter()
        {
            InitializeComponent();
        }

        private void F_Filter_Load(object sender, EventArgs e)
        {
            pN_Viewer1.DataSource = pns;
            listBox1.DataSource = pns;

            pns.Add(new Pole() { X = 0.5, Y = 0.5 });
            pns.Add(new Zero() { X = -1, Y = 0 });
            pns.Add(new Zero() { X = 0, Y = 0 });


            float[] f = new float[] {
    0.0000000f, 0.0017691935f, 0.0000000f, -0.018274935f, -0.035540224f, 0.0000000f,
    0.11765137f, 0.26401309f, 0.33147066f, 0.26401309f, 0.11765137f, 0.0000000f,
    -0.035540224f, -0.018274935f, 0.0000000f, 0.0017691935f
            };





        }
    }
}

