using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Podstawowy_foto_edytor
{
    public partial class Pen_size : Form
    {
        Form1 f1;
        public Pen_size(Form1 frm1)
        {
            InitializeComponent();
        }

        private void Pen_size_OK_button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
