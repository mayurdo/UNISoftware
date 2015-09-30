using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GRNSoft.Utility
{
    class UtilityClass
    {
        public static void AcceptOnlyNumber(KeyPressEventArgs e)
        {
            int key = Convert.ToInt32(e.KeyChar);
            if (!((key >= 48 && key <= 57) || (key > 1 && key <= 26) || key == 8))
            {
                e.Handled = true;
            }
        }

        public static void AcceptOnlyDecimal(KeyPressEventArgs e)
        {
            int key = Convert.ToInt32(e.KeyChar);
            if (!((key >= 48 && key <= 57) || (key > 1 && key <= 26) || key == 8 || key == 46))
            {
                e.Handled = true;
            }
        }
    }
}
