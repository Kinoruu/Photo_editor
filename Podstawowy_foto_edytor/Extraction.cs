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
    public partial class Extraction : Form
    {
        Form1 f1;
        public Extraction(Form1 frm1)
        {
            this.f1 = frm1;
            InitializeComponent();
        }

        public string Send_R
        {
            get
            {
                return e_R_textBox.Text;
            }
        }
        public string Send_G
        {
            get
            {
                return e_G_textBox.Text;
            }
        }
        public string Send_B
        {
            get
            {
                return e_B_textBox.Text;
            }
        }

        private void e_rgb_ok_button_Click(object sender, EventArgs e)
        {
            
        }
    }
}
