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
        
        public FormPrincipal()
        {
            InitializeComponent();
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
                this.Invalidate(); //Forza el evento paint, redibuja la ventna
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

        private void Digitalizacion(int[,] matrizFiltro)
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

        private void filtroOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            Digitalizacion(matrizFiltroOriginal);
        }

        private void contornoToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            Digitalizacion(matrizFiltroContorno);
        }

        private void difuminadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Trabajando con los pixeles originales
            double[,] matrizFiltroDifuminado = new double[3, 3]; //Declarando matriz del filtro original
            double[,] matrizFactoresDifuminado = new double[3, 3]; //Declarando matriz de factores.
            double[,] matrizTempDifuminado = new double[3, 3]; //Declarando matriz de factores.
            double sumaFactoresDifuminado = 0; //Variable que sumara todos los factores.
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroDifuminado[0, 0] = Convert.ToDouble(0.0625);
            matrizFiltroDifuminado[0, 1] = Convert.ToDouble(0.125);
            matrizFiltroDifuminado[0, 2] = Convert.ToDouble(0.0625);
            matrizFiltroDifuminado[1, 0] = Convert.ToDouble(0.125);
            matrizFiltroDifuminado[1, 1] = Convert.ToDouble(0.25);
            matrizFiltroDifuminado[1, 2] = Convert.ToDouble(0.125);
            matrizFiltroDifuminado[2, 0] = Convert.ToDouble(0.0625);
            matrizFiltroDifuminado[2, 1] = Convert.ToDouble(0.125);
            matrizFiltroDifuminado[2, 2] = Convert.ToDouble(0.0625);
            //Matriz del filtro Contorno llenada exitosamente

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
                                matrizTempDifuminado[h, k] = (imgEscalaGrises.GetPixel(i + h, j + k)).G;
                                matrizFactoresDifuminado[h, k] = matrizTempDifuminado[h, k] * matrizFiltroDifuminado[h, k];
                                sumaFactoresDifuminado = sumaFactoresDifuminado + matrizFactoresDifuminado[h, k];
                            }
                        }
                        
                        if (sumaFactoresDifuminado < 0)
                        {
                            sumaFactoresDifuminado = 0;
                        }
                        if (sumaFactoresDifuminado > 255)
                        {
                            sumaFactoresDifuminado = 255;
                        }
                        
                        //Procesamiento del nuevo color.
                        resultanteColor = Color.FromArgb(Convert.ToInt32(sumaFactoresDifuminado),
                                                         Convert.ToInt32(sumaFactoresDifuminado),
                                                         Convert.ToInt32(sumaFactoresDifuminado));

                        //Colocacion del color en escala de grises
                        imgResultante.SetPixel(i + 1, j + 1, resultanteColor);
                        sumaFactoresDifuminado = 0;
                    }
                }
            }

            pictureBoxImgEditada.Image = imgResultante;
            this.Invalidate(); //Forza el evento paint, redibuja la ventana
        }

        private void realzarToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            Digitalizacion(matrizFiltroRealzar);
        }

        private void afilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            Digitalizacion(matrizFiltroAfilar);
        }

        private void sobelInferiorToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            Digitalizacion(matrizFiltroSobelInferior);
        }

        private void sobelSuperiorToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            Digitalizacion(matrizFiltroSobelSuperior);
        }

        private void sobelDerechoToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            Digitalizacion(matrizFiltroSobelDerecho);
        }

        private void sobelIzquierdoToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            Digitalizacion(matrizFiltroSobelIzquierdo);
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

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            int[,] matrizFiltroPersonalizado = new int[3, 3]; //Declarando matriz del filtro original
            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            matrizFiltroPersonalizado[0, 0] = Convert.ToInt32(textBoxA.Text);
            matrizFiltroPersonalizado[0, 1] = Convert.ToInt32(textBoxB.Text);
            matrizFiltroPersonalizado[0, 2] = Convert.ToInt32(textBoxC.Text);
            matrizFiltroPersonalizado[1, 0] = Convert.ToInt32(textBoxD.Text);
            matrizFiltroPersonalizado[1, 1] = Convert.ToInt32(textBoxE.Text);
            matrizFiltroPersonalizado[1, 2] = Convert.ToInt32(textBoxF.Text);
            matrizFiltroPersonalizado[2, 0] = Convert.ToInt32(textBoxG.Text);
            matrizFiltroPersonalizado[2, 1] = Convert.ToInt32(textBoxH.Text);
            matrizFiltroPersonalizado[2, 2] = Convert.ToInt32(textBoxI.Text);
            //Matriz del filtro Sobel Izquierdo llenada exitosamente
            Digitalizacion(matrizFiltroPersonalizado);
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
    }
}
