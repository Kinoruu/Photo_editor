using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;
using static System.Windows.Forms.TreeNode;


namespace zadanie6
{
    [Serializable]
    public class Element: TreeNode
    {
        public Point p_1_p;
        public Point p_2_k;
        public int szer_okr;
        public int wys_okr;
        public Point p_poczatkowy;
        public Point p_koncowy;
        public Color k_pio;
        public int jaka_figura;
        public float szer_piora;
        public Color k_obiekt;
        public int jaki_kolor;
        public int ile_elem;
        public List<Element> kulka_sasiedzi = new List<Element>();


        public Element(Point p_p, Point p_k, Color k_pioro, int figura, float szer, Color k_figura, int jak_kolor, int ile_el)
        {
            p_poczatkowy = p_p;
            p_koncowy = p_k;
            k_pio = k_pioro;
            jaka_figura = figura;
            szer_piora = szer;
            k_obiekt = k_figura;
            jaki_kolor = jak_kolor;
            ile_elem = ile_el;
        }
        public Element(Point p_p, Point  p_k, Color k_pioro, int szer, int wys, int figura)
        {
            p_1_p= p_p;
            p_2_k = p_k;
            k_pio = k_pioro;
            jaka_figura = figura;
            szer_okr=szer;
            wys_okr = wys;
            //List<Element> kolka_sasiedzi = new List<Element>();
        }
        public Element(Color k_pioro, Point p_p)
        {
            k_pio = k_pioro;
            p_1_p = p_p;
            
        }
    }

    
}
