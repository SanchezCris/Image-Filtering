using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filtros_Digitales
{
    class ValidacionNumeros
    {
        public static void NumerosDecimales(KeyPressEventArgs valor)
        {
            if(Char.IsDigit(valor.KeyChar))
            {
                valor.Handled = false;
            }
            else if (Char.IsSeparator(valor.KeyChar))
            {
                valor.Handled = true;
            }
            else if(Char.IsControl(valor.KeyChar))
            {
                valor.Handled = false;
            }
            else if(valor.KeyChar.ToString().Equals("."))
            {
                valor.Handled = false;
            }
            else
            {
                valor.Handled = true;
            }
        }
    }
}
