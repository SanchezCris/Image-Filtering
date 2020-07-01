using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filtros_Digitales
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
            Bitmap fondo = new Bitmap(Application.StartupPath + @"\fondoInicio\Venecia.jpg");
            this.BackgroundImage = fondo;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormPrincipal formPrincipal = new FormPrincipal();
            formPrincipal.Show();

        }

        private void FormInicio_Load(object sender, EventArgs e){}
    }
}
