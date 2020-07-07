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
        private Bitmap imgBrillo;
        private Bitmap imgContraste;
        int brillo = 0, contraste = 0;

        Color originalColor = new Color(); //objeto que guardara el color del pixel de la imagen original
        Color resultanteColor = new Color(); //objeto que guardara el color final del pixel.
        Color grisColor = new Color();

        int[,] matrizTemp = new int[3, 3]; //Matriz temporal para la digitalizacion
        int[,] matrizFactores = new int[3, 3]; //Matriz de factores entre el filtro y la temporal
        int sumaFactores = 0; //Variable que sumara todos los factores.

        int[,] matrizTemp5x5 = new int[5, 5]; //Matriz temporal para la digitalizacion
        int[,] matrizFactores5x5 = new int[5, 5]; //Matriz de factores entre el filtro y la temporal

        double[,] matrizFactoresDouble = new double[3, 3]; //Declarando matriz de factores double.
        double[,] matrizTempDouble = new double[3, 3]; //Declarando matriz de factores double.
        double sumaFactoresDouble = 0; //Variable que sumara todos los factores double.

        public FormPrincipal()
        {
            InitializeComponent();
            Bitmap fondo = new Bitmap(Application.StartupPath + @"\fondoInicio\FondoSecundario.jpg");
            this.BackgroundImage = fondo;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            btnBrillo.Visible = false;
            btnContraste.Visible = false;
            lblBrillo.Visible = false;
            lblContraste.Visible = false;
            title5.Visible = false;
            title6.Visible = false;
            hScrollBar1.Visible = false;
            hScrollBar2.Visible = false;
            OcultarNumericUpDown();
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            System.Windows.Forms.DialogResult respuesta;
            respuesta = MessageBox.Show(this,"¿Desea salir sin guardar los cambios?", "Salir", botones);
            if(respuesta == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void OcultarNumericUpDown()
        {
            buttonAceptar.Visible = false;
            labelTitle4.Visible = false;
            numericUpDownA.Visible = false;
            numericUpDownB.Visible = false;
            numericUpDownC.Visible = false;
            numericUpDownD.Visible = false;
            numericUpDownE.Visible = false;
            numericUpDownF.Visible = false;
            numericUpDownG.Visible = false;
            numericUpDownH.Visible = false;
            numericUpDownI.Visible = false;
        }

        private void importarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogImportarImagen.ShowDialog() == DialogResult.OK)
            {
                Bitmap fondoPictureBox = new Bitmap(Application.StartupPath + @"\fondoInicio\imgFiltro.jpg");
                imgOriginal = (Bitmap)(Bitmap.FromFile(openFileDialogImportarImagen.FileName));

                //Ajuste de dimensiones a los picture Box segun el tamaño de las fotos
                if (imgOriginal.Width >= imgOriginal.Height)
                {
                    pictureBoxImgOriginal.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBoxImgGrises.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBoxImgEditada.SizeMode = PictureBoxSizeMode.Zoom;
                }
                pictureBoxImgOriginal.Image = imgOriginal; //Se muestra la imagen importada en el pictureBox
                pictureBoxImgEditada.Image = fondoPictureBox;
                btnBrillo.Visible = true;
                btnContraste.Visible = true;
                title5.Visible = true;
                title6.Visible = true;
                lblBrillo.Visible = true;
                lblContraste.Visible = true;
                hScrollBar1.Visible = true;
                hScrollBar2.Visible = true;

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
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                if (saveFileDialogImage.ShowDialog() == DialogResult.OK)
                {
                    imgResultante.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            } 
        }

        private void guardarImagenEnEscalaDeGrisesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                if (saveFileDialogImage.ShowDialog() == DialogResult.OK)
                {
                    imgEscalaGrises.Save(saveFileDialogImage.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            for (int i = 0; i < imgOriginal.Width; i++)
            {
                for (int j = 0; j < imgOriginal.Height; j++)
                {
                    //Solucion al problema de los bordes
                    if (i == 0 || i == 1 || j == 0 || j == 1 || i == imgOriginal.Width - 1 ||
                        i == imgOriginal.Width - 2 || j == imgOriginal.Height - 1 || j == imgOriginal.Height - 2)
                    {
                        resultanteColor = Color.FromArgb(255, 255, 255);
                        imgResultante.SetPixel(i, j, resultanteColor);
                    }
                    
                    //valida que h y k no se salga del margen de la matriz de la imagen original.
                    if ((i < (imgOriginal.Width - 4)) && (j < (imgOriginal.Height - 4)))
                    {
                        //Efectuando Algoritmo para la matriz Kernel
                        for (int h = 0; h < 5; h++)
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                matrizTemp5x5[h, k] = (imgEscalaGrises.GetPixel(i + h, j + k)).G;
                                matrizFactores5x5[h, k] = matrizTemp5x5[h, k] * matrizFiltro[h, k];
                                sumaFactores = sumaFactores + matrizFactores5x5[h, k];
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

        //Filtros Basicos:

        private void filtroOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            { 
                //Trabajando con los pixeles originales
                int[,] matrizFiltroOriginal = new int[3, 3]; //Declarando matriz del filtro original
                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if ((i == 1) && (j == 1))
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
        }

        private void contornoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
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
                DigitalizacionInt3x3(matrizFiltroContorno);
            } 
        }

        private void difuminadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
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
        }

        private void realzarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
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
                DigitalizacionInt3x3(matrizFiltroRealzar);
            }
        }

        private void afilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
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
                DigitalizacionInt3x3(matrizFiltroAfilar);
            }
        }

        private void sobelInferiorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
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
                DigitalizacionInt3x3(matrizFiltroSobelInferior);
            }
        }

        private void sobelSuperiorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
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
                DigitalizacionInt3x3(matrizFiltroSobelSuperior);
            }
        }

        private void sobelDerechoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
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
                DigitalizacionInt3x3(matrizFiltroSobelDerecho);
            }
        }

        private void sobelIzquierdoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
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
                DigitalizacionInt3x3(matrizFiltroSobelIzquierdo);
            }
        }

        private void personalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                buttonAceptar.Visible = true;
                labelTitle4.Visible = true;
                numericUpDownA.Visible = true;
                numericUpDownB.Visible = true;
                numericUpDownC.Visible = true;
                numericUpDownD.Visible = true;
                numericUpDownE.Visible = true;
                numericUpDownF.Visible = true;
                numericUpDownG.Visible = true;
                numericUpDownH.Visible = true;
                numericUpDownI.Visible = true;
            }
        }
        //Boton que habilita la opcion de aplicar filtro personalizado
        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            double[,] matrizFiltroPersonalizado = new double[3, 3]; //Declarando matriz del filtro original

            matrizFiltroPersonalizado[0, 0] = Convert.ToDouble(numericUpDownA.Value);
            matrizFiltroPersonalizado[0, 1] = Convert.ToDouble(numericUpDownB.Value);
            matrizFiltroPersonalizado[0, 2] = Convert.ToDouble(numericUpDownC.Value);
            matrizFiltroPersonalizado[1, 0] = Convert.ToDouble(numericUpDownD.Value);
            matrizFiltroPersonalizado[1, 1] = Convert.ToDouble(numericUpDownE.Value);
            matrizFiltroPersonalizado[1, 2] = Convert.ToDouble(numericUpDownF.Value);
            matrizFiltroPersonalizado[2, 0] = Convert.ToDouble(numericUpDownG.Value);
            matrizFiltroPersonalizado[2, 1] = Convert.ToDouble(numericUpDownH.Value);
            matrizFiltroPersonalizado[2, 2] = Convert.ToDouble(numericUpDownI.Value);

            imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);
            DigitalizacionDouble3x3(matrizFiltroPersonalizado);
        }

        //Filtros Extras:

        private void desenfoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
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
        }

        private void detecciónDeBordesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
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
        }

        private void realceDeBordesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                //Trabajando con los pixeles originales
                int[,] matrizFiltroRealceBordes = new int[3, 3]; //Declarando matriz del filtro original
                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i == 1 && j == 0)
                        {
                            matrizFiltroRealceBordes[i, j] = -1;
                        }
                        else if (i == 1 && j == 1)
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
        }

        private void tipoShrapenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
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
        }

        private void norteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
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
        }

        private void esteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
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
        }

        private void esteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
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
        }

        private void oesteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
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
        }

        private void negativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
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

        private void filtroDeTipoGaussToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                int[,] matrizFiltroGauss = new int[5, 5] { {1, 2, 3, 1, 1 },
                                                               {2, 7, 11, 7, 2 },
                                                               {3, 11, 17, 11, 3 },
                                                               {2, 7, 11, 7, 1 },
                                                               {1, 2, 3, 2, 1 },};
                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);
                DigitalizacionInt5x5(matrizFiltroGauss);
            }
        }

        private void negativoColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                for (int i = 0; i < imgOriginal.Width; i++)
                {
                    for (int j = 0; j < imgOriginal.Height; j++)
                    {
                        resultanteColor = Color.FromArgb(255 - imgOriginal.GetPixel(i, j).R,
                                                         255 - imgOriginal.GetPixel(i, j).G,
                                                         255 - imgOriginal.GetPixel(i, j).B);
                        imgResultante.SetPixel(i, j, resultanteColor);
                    }
                }
                pictureBoxImgEditada.Image = imgResultante;
                this.Invalidate(); //Forza el evento paint, redibuja la ventna
            }
        }

        private void rojoToolStripMenuItem1_Click(object sender, EventArgs e) //Evento del Color Rojo
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                for (int i = 0; i < imgOriginal.Width; i++)
                {
                    for (int j = 0; j < imgOriginal.Height; j++)
                    {
                        resultanteColor = Color.FromArgb(imgOriginal.GetPixel(i, j).R, 0, 0);
                        imgResultante.SetPixel(i, j, resultanteColor);
                    }
                }
                pictureBoxImgEditada.Image = imgResultante;
                this.Invalidate(); //Forza el evento paint, redibuja la ventna
            }
        }

        private void verdeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                for (int i = 0; i < imgOriginal.Width; i++)
                {
                    for (int j = 0; j < imgOriginal.Height; j++)
                    {
                        resultanteColor = Color.FromArgb(0, imgOriginal.GetPixel(i, j).G, 0);
                        imgResultante.SetPixel(i, j, resultanteColor);
                    }
                }
                pictureBoxImgEditada.Image = imgResultante;
                this.Invalidate(); //Forza el evento paint, redibuja la ventna
            }
        }

        private void azulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                for (int i = 0; i < imgOriginal.Width; i++)
                {
                    for (int j = 0; j < imgOriginal.Height; j++)
                    {
                        resultanteColor = Color.FromArgb(0, 0, imgOriginal.GetPixel(i, j).B);
                        imgResultante.SetPixel(i, j, resultanteColor);
                    }
                }
                pictureBoxImgEditada.Image = imgResultante;
                this.Invalidate(); //Forza el evento paint, redibuja la ventna
            }
        }

        private void amarilloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                for (int i = 0; i < imgOriginal.Width; i++)
                {
                    for (int j = 0; j < imgOriginal.Height; j++)
                    {
                        resultanteColor = Color.FromArgb(imgEscalaGrises.GetPixel(i, j).R, imgEscalaGrises.GetPixel(i, j).G, 0);
                        imgResultante.SetPixel(i, j, resultanteColor);
                    }
                }
                pictureBoxImgEditada.Image = imgResultante;
                this.Invalidate(); //Forza el evento paint, redibuja la ventna
            }
        }

        private void moradoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                for (int i = 0; i < imgOriginal.Width; i++)
                {
                    for (int j = 0; j < imgOriginal.Height; j++)
                    {
                        resultanteColor = Color.FromArgb(imgEscalaGrises.GetPixel(i, j).R, 0, imgEscalaGrises.GetPixel(i, j).B);
                        imgResultante.SetPixel(i, j, resultanteColor);
                    }
                }
                pictureBoxImgEditada.Image = imgResultante;
                this.Invalidate(); //Forza el evento paint, redibuja la ventna
            }
        }

        private void aberraciónCromáticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                int abCromatica = 5; //Nivel de la aberracion
                int red = 0, green = 0, blue = 0;
                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                for (int i = 0; i < imgOriginal.Width; i++)
                {
                    for (int j = 0; j < imgOriginal.Height; j++)
                    {
                        //Obtencion del color verde
                        green = imgOriginal.GetPixel(i, j).G;
                        
                        //Obtencion del color rojo
                        if((i + abCromatica) < imgOriginal.Width)
                        {
                            red = imgOriginal.GetPixel(i + abCromatica, j).R;
                        }
                        else
                        {
                            red = 0;
                        }

                        //Obtencion del color azul
                        if ((i - abCromatica) >= 0)
                        {
                            blue = imgOriginal.GetPixel(i - abCromatica, j).B;

                        }
                        else
                        {
                            blue = 0;
                        }
                        imgResultante.SetPixel(i, j, Color.FromArgb(red, green, blue));
                    }
                }
                pictureBoxImgEditada.Image = imgResultante;
                this.Invalidate(); //Forza el evento paint, redibuja la ventna
            }
        }

        private void gradientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                int red = 0, green = 0, blue = 0;
                double red1 = 120;
                double green1 = 230;
                double blue1= 120;
                double red2 = 230;
                double green2 = 100;
                double blue2 = 230;

                double degradedRed = (red2 - red1) / imgOriginal.Width;
                double degradedGreen = (green2 - green1) / imgOriginal.Width;
                double degradedBlue = (blue2 - blue1) / imgOriginal.Width;

                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                for (int i = 0; i < imgOriginal.Width; i++)
                {
                    for (int j = 0; j < imgOriginal.Height; j++)
                    {
                        originalColor = imgEscalaGrises.GetPixel(i, j);
                        red = Convert.ToInt32((red1/255.0f)* originalColor.R);
                        green = Convert.ToInt32((green1 / 255.0f) * originalColor.G);
                        blue = Convert.ToInt32((blue1 / 255.0f) * originalColor.B);

                        if (red > 255)
                        {
                            red = 255;
                        }
                        else if(red < 0)
                        {
                            red = 0;
                        }
                        if (green > 255)
                        {
                            green = 255;
                        }
                        else if (green < 0)
                        {
                            green = 0;
                        }
                        if (blue > 255)
                        {
                            blue = 255;
                        }
                        else if (blue < 0)
                        {
                            blue = 0;
                        }
                        imgResultante.SetPixel(i, j, Color.FromArgb(red, green, blue));
                    }
                    red1 = red1 + degradedRed;
                    green1 = green1 + degradedGreen;
                    blue1 = blue1 + degradedBlue;
                }
                pictureBoxImgEditada.Image = imgResultante;
                this.Invalidate(); //Forza el evento paint, redibuja la ventna
            }
        }

        private void brilloContraste_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgResultante == null)
            {
                MessageBox.Show("Porfavor, aplique un filtro a la imagen");
            }
            else
            {
                brillo = hScrollBar1.Value;
                imgBrillo = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                if ((brillo == 0) && (contraste == 0))
                {
                    pictureBoxImgEditada.Image = imgResultante;
                    imgBrillo = null;
                }
                else
                {
                    int red = 0, green = 0, blue = 0;

                    for (int i = 0; i < imgOriginal.Width; i++)
                    {
                        for (int j = 0; j < imgOriginal.Height; j++)
                        {
                            //Aplicando Brillo
                            if (imgContraste == null)
                            {
                                red = imgResultante.GetPixel(i, j).R + brillo;
                                green = imgResultante.GetPixel(i, j).G + brillo;
                                blue = imgResultante.GetPixel(i, j).B + brillo;
                            }
                            else if (imgContraste != null)
                            {
                                red = imgContraste.GetPixel(i, j).R + brillo; ////
                                green = imgContraste.GetPixel(i, j).G + brillo;
                                blue = imgContraste.GetPixel(i, j).B + brillo;
                            }

                            if (red > 255)
                            {
                                red = 255;
                            }
                            else if (red < 0)
                            {
                                red = 0;
                            }
                            if (green > 255)
                            {
                                green = 255;
                            }
                            else if (green < 0)
                            {
                                green = 0;
                            }
                            if (blue > 255)
                            {
                                blue = 255;
                            }
                            else if (blue < 0)
                            {
                                blue = 0;
                            }
                            imgBrillo.SetPixel(i, j, Color.FromArgb(red, green, blue));
                        }
                    }
                    pictureBoxImgEditada.Image = imgBrillo;
                }
                this.Invalidate();
            }
        }

        private void btnContraste_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgResultante == null)
            {
                MessageBox.Show("Porfavor, aplique un filtro a la imagen");
            }
            else
            {
                contraste = hScrollBar2.Value;
                imgContraste = new Bitmap(imgOriginal.Width, imgOriginal.Height);

                if ((brillo == 0) && (contraste == 0))
                {
                    pictureBoxImgEditada.Image = imgResultante;
                    imgContraste = null;
                }
                else
                {
                    double red = 0;
                    double green = 0;
                    double blue = 0;
                    double c = (100.0f + contraste) / 100.0f;
                    c *= c;

                    for (int i = 0; i < imgOriginal.Width; i++)
                    {
                        for (int j = 0; j < imgOriginal.Height; j++)
                        {
                            if (imgBrillo == null)
                            {
                                red = ((((imgResultante.GetPixel(i, j).R / 255.0f) - 0.5f) * c) + 0.5f) * 255;
                                green = ((((imgResultante.GetPixel(i, j).G / 255.0f) - 0.5f) * c) + 0.5f) * 255;
                                blue = ((((imgResultante.GetPixel(i, j).B / 255.0f) - 0.5f) * c) + 0.5f) * 255;
                            }
                            else if (imgBrillo != null)
                            {
                                red = ((((imgBrillo.GetPixel(i, j).R / 255.0f) - 0.5f) * c) + 0.5f) * 255;
                                green = ((((imgBrillo.GetPixel(i, j).G / 255.0f) - 0.5f) * c) + 0.5f) * 255;
                                blue = ((((imgBrillo.GetPixel(i, j).B / 255.0f) - 0.5f) * c) + 0.5f) * 255;
                            }

                            if (red > 255)
                            {
                                red = 255;
                            }
                            else if (red < 0)
                            {
                                red = 0;
                            }
                            if (green > 255)
                            {
                                green = 255;
                            }
                            else if (green < 0)
                            {
                                green = 0;
                            }
                            if (blue > 255)
                            {
                                blue = 255;
                            }
                            else if (blue < 0)
                            {
                                blue = 0;
                            }
                            imgContraste.SetPixel(i, j, Color.FromArgb(Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue)));
                        }
                    }
                    pictureBoxImgEditada.Image = imgContraste;
                }
                this.Invalidate();
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            lblBrillo.Text = Convert.ToString(hScrollBar1.Value);
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            lblContraste.Text = Convert.ToString(hScrollBar2.Value);
        }

        private void originalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcultarNumericUpDown();
            if (imgOriginal == null)
            {
                MessageBox.Show("Porfavor, importe una imagen");
            }
            else
            {
                imgResultante = new Bitmap(imgOriginal.Width, imgOriginal.Height);
                imgResultante = imgOriginal;
                pictureBoxImgEditada.Image = imgResultante;
                this.Invalidate();
            }
        }
    }
}
