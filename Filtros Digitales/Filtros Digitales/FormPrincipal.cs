using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filtros_Digitales
{
    public partial class FormPrincipal : Form
    {
        private Bitmap imgOriginal; //imagen original
        private Bitmap imgEscalaGrises; //imagen escalaGrises
        private Bitmap imgResultante; //imagen editada

        Color originalColor = new Color(); //objeto que guardara el color del pixel de la imagen original
        Color resultanteColor = new Color(); //objeto que guardara el color final del pixel.
        Color grisColor = new Color();

        int[,] matrizTemp = new int[3, 3]; //Matriz temporal para la digitalizacion
        int[,] matrizFactores = new int[3, 3]; //Matriz de factores entre el filtro y la temporal
        int sumaFactores = 0; //Variable que sumara todos los factores.

        double[,] matrizFactoresDouble = new double[3, 3]; //Declarando matriz de factores double.
        double[,] matrizTempDouble = new double[3, 3]; //Declarando matriz de factores double.
        double sumaFactoresDouble = 0; //Variable que sumara todos los factores double.

        public FormPrincipal()
        {
            InitializeComponent();
            OcultarTextBox();
        }

        private void OcultarTextBox()
        {
            buttonAceptar.Visible = false;
            textBoxA.Visible = false;
            textBoxB.Visible = false;
            textBoxC.Visible = false;
            textBoxD.Visible = false;
            textBoxE.Visible = false;
            textBoxF.Visible = false;
            textBoxG.Visible = false;
            textBoxH.Visible = false;
            textBoxI.Visible = false;
        }

        private void importarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogImportarImagen.ShowDialog() == DialogResult.OK)
            {
                imgOriginal = (Bitmap)(Bitmap.FromFile(openFileDialogImportarImagen.FileName));
                pictureBoxImgOriginal.Image = imgOriginal; //Se muestra la imagen importada en el pictureBox

                //Convirtiendo la imagen original a escala de grises
                
                imgEscalaGrises = new Bitmap(imgOriginal.Width, imgOriginal.Height);
                int intensidad = 0;

                for (int i = 0; i < imgOriginal.Width; i++)
                {
                    for (int j = 0; j < imgOriginal.Height; j++)
                    {
                        originalColor = imgOriginal.GetPixel(i,j); //Obtencion del color del pixel
                        intensidad = (originalColor.R + originalColor.G + originalColor.B)/3;
                        //Procesamiento del nuevo color.
                        grisColor = Color.FromArgb(intensidad, intensidad, intensidad);
                        //Colocacion del color en escala de grises
                        imgEscalaGrises.SetPixel(i, j, grisColor);
                    }
                }
                pictureBoxImgGrises.Image = imgEscalaGrises;
                this.Invalidate(); //Forza el evento paint, redibuja la ventana
            }
        }

        private void guardarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                imgResultante.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Png);
                //imgEscalaGrises.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Algoritmo para enteros, double de 3x3 y 5x5

        private void DigitalizacionInt3x3(int[,] matrizFiltro)
        {
            for (int i = 0; i < imgOriginal.Width; i++)
            {
                for (int j = 0; j < imgOriginal.Height; j++)
                {
                    //Solucion al problema de los bordes
                    if (i == 0 || j == 0 || i == imgOriginal.Width - 1 || j == imgOriginal.Height - 1)
                    {
                        imgResultante.SetPixel(i, j, imgEscalaGrises.GetPixel(i, j));
                    }

                    //valida que h y k no se salga del margen de la matriz de la imagen original.
                    if ((i < (imgOriginal.Width - 2)) && (j < (imgOriginal.Height - 2)))
                    {
                        //Efectuando Algoritmo para la matriz Kernel
                        for (int h = 0; h < 3; h++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                matrizTemp[h, k] = (imgEscalaGrises.GetPixel(i + h, j + k)).G;
                                matrizFactores[h, k] = matrizTemp[h, k] * matrizFiltro[h, k];
                                sumaFactores = sumaFactores + matrizFactores[h, k];
                            }
                        }
                        if (sumaFactores < 0)
                        {
                            sumaFactores = 0;
                        }
                        if (sumaFactores > 255)
                        {
                            sumaFactores = 255;
                        }
                        //Procesamiento del nuevo color.
                        resultanteColor = Color.FromArgb(sumaFactores, sumaFactores, sumaFactores);

                        //Colocacion del color en escala de grises
                        imgResultante.SetPixel(i + 1, j + 1, resultanteColor);
                        sumaFactores = 0;
                    }
                }
            }
            pictureBoxImgEditada.Image = imgResultante;
            this.Invalidate(); //Forza el evento paint, redibuja la ventna
        }

        private void DigitalizacionDouble3x3(double[,] matrizFiltro)
        {
            for (int i = 0; i < imgOriginal.Width; i++)
            {
                for (int j = 0; j < imgOriginal.Height; j++)
                {
                    //Solucion al problema de los bordes
                    if (i == 0 || j == 0 || i == imgOriginal.Width - 1 || j == imgOriginal.Height - 1)
                    {
                        imgResultante.SetPixel(i, j, imgEscalaGrises.GetPixel(i, j));
                    }
                    //valida que h y k no se salga del borde de la imagen original.
                    if ((i < (imgOriginal.Width - 2)) && (j < (imgOriginal.Height - 2)))
                    {
                        for (int h = 0; h < 3; h++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                matrizTempDouble[h, k] = (imgEscalaGrises.GetPixel(i + h, j + k)).G;
                                matrizFactoresDouble[h, k] = matrizTempDouble[h, k] * matrizFiltro[h, k];
                                sumaFactoresDouble = sumaFactoresDouble + matrizFactoresDouble[h, k];
                            }
                        }
                        if (sumaFactoresDouble < 0)
                        {
                            sumaFactoresDouble = 0;
                        }
                        if (sumaFactoresDouble > 255)
                        {
                            sumaFactoresDouble = 255;
                        }
                        resultanteColor = Color.FromArgb(Convert.ToInt32(sumaFactoresDouble),
                                                         Convert.ToInt32(sumaFactoresDouble),
                                                         Convert.ToInt32(sumaFactoresDouble));
                        imgResultante.SetPixel(i + 1, j + 1, resultanteColor);
                        sumaFactoresDouble = 0;
                    }
                }
            }
            pictureBoxImgEditada.Image = imgResultante;
            this.Invalidate();
        }

        private void DigitalizacionInt5x5(int[,] matrizFiltro)
        {

        }

        //Filtros Basicos:

        private void filtroOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            //Trabajando con los pixeles originales
            int[,] matrizFiltroOriginal = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if((i==1) && (j==1))
                    {
                        matrizFiltroOriginal[i, j] = 1;
                    }
                    else
                    {
                        matrizFiltroOriginal[i, j] = 0;
                    }
                }
            }
            //Matriz del filtro Original llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroOriginal);
        }

        private void contornoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            //Trabajando con los pixeles originales
            int[,] matrizFiltroContorno = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((i == 1) && (j == 1))
                    {
                        matrizFiltroContorno[i, j] = 8;
                    }
                    else
                    {
                        matrizFiltroContorno[i, j] = -1;
                    }
                }
            }
            //Matriz del filtro Contorno llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroContorno);
        }

        private void difuminadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            //Trabajando con los pixeles originales
            double[,] matrizFiltroDifuminado = new double[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroDifuminado[0, 0] = 0.0625;
            matrizFiltroDifuminado[0, 1] = 0.125;
            matrizFiltroDifuminado[0, 2] = 0.0625;
            matrizFiltroDifuminado[1, 0] = 0.125;
            matrizFiltroDifuminado[1, 1] = 0.25;
            matrizFiltroDifuminado[1, 2] = 0.125;
            matrizFiltroDifuminado[2, 0] = 0.0625;
            matrizFiltroDifuminado[2, 1] = 0.125;
            matrizFiltroDifuminado[2, 2] = 0.0625;
            //Matriz del filtro Difuminado llenada exitosamente

            DigitalizacionDouble3x3(matrizFiltroDifuminado); 
        }

        private void realzarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroRealzar = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroRealzar[0, 0] = -2;
            matrizFiltroRealzar[0, 1] = -1;
            matrizFiltroRealzar[0, 2] = 0;
            matrizFiltroRealzar[1, 0] = -1;
            matrizFiltroRealzar[1, 1] = 1;
            matrizFiltroRealzar[1, 2] = 1;
            matrizFiltroRealzar[2, 0] = 0;
            matrizFiltroRealzar[2, 1] = 1;
            matrizFiltroRealzar[2, 2] = 2;
            //Matriz del filtro Realzar llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroRealzar);
        }

        private void afilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroAfilar = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroAfilar[0, 0] = 0;
            matrizFiltroAfilar[0, 1] = -1;
            matrizFiltroAfilar[0, 2] = 0;
            matrizFiltroAfilar[1, 0] = -1;
            matrizFiltroAfilar[1, 1] = 5;
            matrizFiltroAfilar[1, 2] = -1;
            matrizFiltroAfilar[2, 0] = 0;
            matrizFiltroAfilar[2, 1] = -1;
            matrizFiltroAfilar[2, 2] = 0;
            //Matriz del filtro Afilar llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroAfilar);
        }

        private void sobelInferiorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroSobelInferior = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroSobelInferior[0, 0] = -1;
            matrizFiltroSobelInferior[0, 1] = -2;
            matrizFiltroSobelInferior[0, 2] = -1;
            matrizFiltroSobelInferior[1, 0] = 0;
            matrizFiltroSobelInferior[1, 1] = 0;
            matrizFiltroSobelInferior[1, 2] = 0;
            matrizFiltroSobelInferior[2, 0] = 1;
            matrizFiltroSobelInferior[2, 1] = 2;
            matrizFiltroSobelInferior[2, 2] = 1;
            //Matriz del filtro Sobel Inferior llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroSobelInferior);
        }

        private void sobelSuperiorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroSobelSuperior = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroSobelSuperior[0, 0] = 1;
            matrizFiltroSobelSuperior[0, 1] = 2;
            matrizFiltroSobelSuperior[0, 2] = 1;
            matrizFiltroSobelSuperior[1, 0] = 0;
            matrizFiltroSobelSuperior[1, 1] = 0;
            matrizFiltroSobelSuperior[1, 2] = 0;
            matrizFiltroSobelSuperior[2, 0] = -1;
            matrizFiltroSobelSuperior[2, 1] = -2;
            matrizFiltroSobelSuperior[2, 2] = -1;
            //Matriz del filtro Sobel Superior llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroSobelSuperior);
        }

        private void sobelDerechoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroSobelDerecho = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroSobelDerecho[0, 0] = -1;
            matrizFiltroSobelDerecho[0, 1] = 0;
            matrizFiltroSobelDerecho[0, 2] = 1;
            matrizFiltroSobelDerecho[1, 0] = -2;
            matrizFiltroSobelDerecho[1, 1] = 0;
            matrizFiltroSobelDerecho[1, 2] = 2;
            matrizFiltroSobelDerecho[2, 0] = -1;
            matrizFiltroSobelDerecho[2, 1] = 0;
            matrizFiltroSobelDerecho[2, 2] = 1;
            //Matriz del filtro Sobel Derecho llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroSobelDerecho);
        }

        private void sobelIzquierdoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroSobelIzquierdo = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroSobelIzquierdo[0, 0] = 1;
            matrizFiltroSobelIzquierdo[0, 1] = 0;
            matrizFiltroSobelIzquierdo[0, 2] = -1;
            matrizFiltroSobelIzquierdo[1, 0] = 2;
            matrizFiltroSobelIzquierdo[1, 1] = 0;
            matrizFiltroSobelIzquierdo[1, 2] = -2;
            matrizFiltroSobelIzquierdo[2, 0] = 1;
            matrizFiltroSobelIzquierdo[2, 1] = 0;
            matrizFiltroSobelIzquierdo[2, 2] = -1;
            //Matriz del filtro Sobel Izquierdo llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroSobelIzquierdo);
        }

        private void personalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonAceptar.Visible = true;
            textBoxA.Visible = true;
            textBoxB.Visible = true;
            textBoxC.Visible = true;
            textBoxD.Visible = true;
            textBoxE.Visible = true;
            textBoxF.Visible = true;
            textBoxG.Visible = true;
            textBoxH.Visible = true;
            textBoxI.Visible = true;
        }

        //Boton que habilita la opcion de aplicar filtro personalizado
        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            double [,] matrizFiltroPersonalizado = new double [3, 3]; //Declarando matriz del filtro original
            bool errorA = true, errorB = true, errorC = true;
            bool errorD = true, errorE = true, errorF = true;
            bool errorG = true, errorH = true, errorI = true;

            errorA = ValidarSignoNegativo(textBoxA.Text);
            errorB = ValidarSignoNegativo(textBoxB.Text);
            errorC = ValidarSignoNegativo(textBoxC.Text);
            errorD = ValidarSignoNegativo(textBoxD.Text);
            errorE = ValidarSignoNegativo(textBoxE.Text);
            errorF = ValidarSignoNegativo(textBoxF.Text);
            errorG = ValidarSignoNegativo(textBoxG.Text);
            errorH = ValidarSignoNegativo(textBoxH.Text);
            errorI = ValidarSignoNegativo(textBoxI.Text);

            if (errorA == false && errorB == false && errorC == false &&
                errorD == false && errorE == false && errorF == false &&
                errorG == false && errorH == false && errorI == false)
            {
                matrizFiltroPersonalizado[0, 0] = Convert.ToDouble(textBoxA.Text);
                matrizFiltroPersonalizado[0, 1] = Convert.ToDouble(textBoxB.Text);
                matrizFiltroPersonalizado[0, 2] = Convert.ToDouble(textBoxC.Text);
                matrizFiltroPersonalizado[1, 0] = Convert.ToDouble(textBoxD.Text);
                matrizFiltroPersonalizado[1, 1] = Convert.ToDouble(textBoxE.Text);
                matrizFiltroPersonalizado[1, 2] = Convert.ToDouble(textBoxF.Text);
                matrizFiltroPersonalizado[2, 0] = Convert.ToDouble(textBoxG.Text);
                matrizFiltroPersonalizado[2, 1] = Convert.ToDouble(textBoxH.Text);
                matrizFiltroPersonalizado[2, 2] = Convert.ToDouble(textBoxI.Text);

                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);
                DigitalizacionDouble3x3(matrizFiltroPersonalizado);
            }
        }

        //Metodo que valida el signo negativo en la matriz personalizada
        static bool ValidarSignoNegativo(string cadena)
        {
            if(cadena == "")
            {
                return true;
            }
            else
            {
                for (int i = 0; i < cadena.Length; i++)
                {
                    if (i > 0)
                    {
                        if (cadena.Substring(i, 1) == "-")
                        {
                            MessageBox.Show("Matriz incorrecta");
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        // Metodos que validan que solo se ingresen numeros a la matriz personalizada por el usuario.
        private void textBoxA_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros.NumerosDecimales(e);
        }

        private void textBoxB_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros.NumerosDecimales(e);
        }

        private void textBoxC_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros.NumerosDecimales(e);
        }

        private void textBoxD_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros.NumerosDecimales(e);
        }

        private void textBoxE_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros.NumerosDecimales(e);
        }

        private void textBoxF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros.NumerosDecimales(e);
        }

        private void textBoxG_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros.NumerosDecimales(e);
        }

        private void textBoxH_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros.NumerosDecimales(e);
        }

        private void textBoxI_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionNumeros.NumerosDecimales(e);
        }

        //Filtros Extras:

        private void desenfoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            //Trabajando con los pixeles originales
            int[,] matrizFiltroDesenfoque = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrizFiltroDesenfoque[i, j] = 1;
                }
            }
            //Matriz del filtro Desenfoque llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroDesenfoque);
        }

        private void detecciónDeBordesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroDeteccionBordes = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroDeteccionBordes[0, 0] = 0;
            matrizFiltroDeteccionBordes[0, 1] = 1;
            matrizFiltroDeteccionBordes[0, 2] = 0;
            matrizFiltroDeteccionBordes[1, 0] = 1;
            matrizFiltroDeteccionBordes[1, 1] = -4;
            matrizFiltroDeteccionBordes[1, 2] = 1;
            matrizFiltroDeteccionBordes[2, 0] = 0;
            matrizFiltroDeteccionBordes[2, 1] = 1;
            matrizFiltroDeteccionBordes[2, 2] = 0;
            //Matriz del filtro Deteccion de bordes llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroDeteccionBordes);
        }

        private void realceDeBordesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            //Trabajando con los pixeles originales
            int[,] matrizFiltroRealceBordes = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(i == 1 && j ==0)
                    {
                        matrizFiltroRealceBordes[i, j] = -1;
                    }
                    else if(i==1 && j ==1)
                    {
                        matrizFiltroRealceBordes[i, j] = 1;
                    }
                    else
                    {
                        matrizFiltroRealceBordes[i, j] = 0;
                    }
                    
                }
            }
            //Matriz del filtro Realce de bordes llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroRealceBordes);
        }

        private void tipoShrapenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroSharpen = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroSharpen[0, 0] = 1;
            matrizFiltroSharpen[0, 1] = -2;
            matrizFiltroSharpen[0, 2] = 1;
            matrizFiltroSharpen[1, 0] = -2;
            matrizFiltroSharpen[1, 1] = 5;
            matrizFiltroSharpen[1, 2] = -2;
            matrizFiltroSharpen[2, 0] = 1;
            matrizFiltroSharpen[2, 1] = -2;
            matrizFiltroSharpen[2, 2] = 1;
            //Matriz del filtro Sharpen llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroSharpen);
        }

        private void norteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroNorte = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroNorte[0, 0] = 1;
            matrizFiltroNorte[0, 1] = 1;
            matrizFiltroNorte[0, 2] = 1;
            matrizFiltroNorte[1, 0] = 1;
            matrizFiltroNorte[1, 1] = -2;
            matrizFiltroNorte[1, 2] = 1;
            matrizFiltroNorte[2, 0] = -1;
            matrizFiltroNorte[2, 1] = -1;
            matrizFiltroNorte[2, 2] = -1;
            //Matriz del filtro Norte llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroNorte);
        }

        private void esteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroSur = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroSur[0, 0] = -1;
            matrizFiltroSur[0, 1] = -1;
            matrizFiltroSur[0, 2] = -1;
            matrizFiltroSur[1, 0] = 1;
            matrizFiltroSur[1, 1] = -2;
            matrizFiltroSur[1, 2] = 1;
            matrizFiltroSur[2, 0] = 1;
            matrizFiltroSur[2, 1] = 1;
            matrizFiltroSur[2, 2] = 1;
            //Matriz del filtro Sur llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroSur);
        }

        private void esteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroEste = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroEste[0, 0] = -1;
            matrizFiltroEste[0, 1] = 1;
            matrizFiltroEste[0, 2] = 1;
            matrizFiltroEste[1, 0] = -1;
            matrizFiltroEste[1, 1] = -2;
            matrizFiltroEste[1, 2] = 1;
            matrizFiltroEste[2, 0] = -1;
            matrizFiltroEste[2, 1] = 1;
            matrizFiltroEste[2, 2] = 1;
            //Matriz del filtro Sur llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroEste);
        }

        private void oesteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            int[,] matrizFiltroOeste = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroOeste[0, 0] = 1;
            matrizFiltroOeste[0, 1] = 1;
            matrizFiltroOeste[0, 2] = -1;
            matrizFiltroOeste[1, 0] = 1;
            matrizFiltroOeste[1, 1] = -2;
            matrizFiltroOeste[1, 2] = -1;
            matrizFiltroOeste[2, 0] = 1;
            matrizFiltroOeste[2, 1] = 1;
            matrizFiltroOeste[2, 2] = -1;
            //Matriz del filtro Sur llenada exitosamente
            DigitalizacionInt3x3(matrizFiltroOeste);
        }

        private void negativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarTextBox();
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            for (int i = 0; i < imgOriginal.Width; i++)
            {
                for (int j = 0; j < imgOriginal.Height; j++)
                {
                    grisColor = imgEscalaGrises.GetPixel(i, j); //Obtencion del color del pixel
                    //Procesamiento del nuevo color.
                    resultanteColor = Color.FromArgb(255 - grisColor.R,
                                                     255 - grisColor.G,
                                                     255 - grisColor.B);
                    //Colocacion del color en escala de grises
                    imgResultante.SetPixel(i, j, resultanteColor);
                }
            }
            pictureBoxImgEditada.Image = imgResultante;
            this.Invalidate(); //Forza el evento paint, redibuja la ventna
        }
    }
}
