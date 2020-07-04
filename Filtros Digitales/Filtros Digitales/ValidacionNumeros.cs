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
            if(Char.IsDigit(valor.KeyChar)) //Permite que agregue numeros
            {
                valor.Handled = false;
            }
            else if (Char.IsSeparator(valor.KeyChar)) //No permite que agregue espacios entre caracteres
            {
                valor.Handled = true;
            }
            else if(Char.IsControl(valor.KeyChar)) //Permite teclas de control
            {
                valor.Handled = false;
            }
            else if(valor.KeyChar.ToString().Equals(".")) //Permite que agregue punto decimal
            {
                valor.Handled = false;
            }
            else if (valor.KeyChar.ToString().Equals("-")) //Permite que agregue signo menos
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
