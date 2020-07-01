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
        Color nuevoColor = new Color();

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void importarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogImportarImagen.ShowDialog() == DialogResult.OK)
            {
                imgOriginal = (Bitmap)(Bitmap.FromFile(openFileDialogImportarImagen.FileName));
                pictureBoxImgOriginal.Image = imgOriginal; //Se muestra la imagen importada en el pictureBox

                //Convirtiendo la imagen original a escala de grises
                Color grisColor = new Color();
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
                //imgResultante.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Png);
                imgEscalaGrises.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void filtroOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Trabajando con los pixeles originales
            int[,] matrizTemp = new int[3, 3]; //Declarando matriz temporal de 3x3
            int[,] matrizFiltroOriginal = new int[3, 3]; //Declarando matriz del filtro original
            int[,] matrizFactores = new int[3, 3]; //Declarando matriz de factores.
            int sumaFactores = 0; //Variable que sumara todos los factores.
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

            for (int i = 0; i < imgOriginal.Width; i++)
            {
                for (int j = 0; j < imgOriginal.Height; j++)
                {
                    //valida que h y k no se salga del margen de la matriz de la imagen original.
                    if ((i < (imgOriginal.Width - 2)) && (j < (imgOriginal.Height - 2)))
                    {
                        //Efectuando Algoritmo para la matriz Kernel
                        for (int h = 0; h < 3; h++)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                matrizTemp[h, k] = (imgEscalaGrises.GetPixel(i+h, j+k)).G;
                                matrizFactores[h,k] = matrizTemp[h, k] * matrizFiltroOriginal[h, k];
                                sumaFactores = sumaFactores + matrizFactores[h, k];
                            }
                        }

                        //Lenado de matriz temporal exitosamente
                        
                        //Procesamiento del nuevo color.
                        resultanteColor = Color.FromArgb(sumaFactores, sumaFactores, sumaFactores);
                        
                        //Colocacion del color en escala de grises
                        imgResultante.SetPixel(i+1, j+1, resultanteColor);
                        sumaFactores = 0;
                    }
                }
            }
            pictureBoxImgEditada.Image = imgResultante;
            this.Invalidate(); //Forza el evento paint, redibuja la ventna
        }

        static int[,] MatrizTemporal(int i, int j, Bitmap imgGris)
        {
            Color tempGris = new Color();
            int[,] matrizTemp = new int[3, 3]; //Declarando matriz temporal de 3x3

            for (int h = 0; h < 3; h++)
            {
                for (int k = 0; k < 3; k++)
                {
                    tempGris = imgGris.GetPixel(i+h, j+k);
                    matrizTemp[h, k] = tempGris.G; 
                }
            }
            return matrizTemp;
        }
    }
}
