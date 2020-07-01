namespace Filtros_Digitales
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarImagenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarImagenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtroOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxImgOriginal = new System.Windows.Forms.PictureBox();
            this.labelTitle1 = new System.Windows.Forms.Label();
            this.labelTitle2 = new System.Windows.Forms.Label();
            this.pictureBoxImgEditada = new System.Windows.Forms.PictureBox();
            this.openFileDialogImportarImagen = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogImage = new System.Windows.Forms.SaveFileDialog();
            this.labelTitle3 = new System.Windows.Forms.Label();
            this.pictureBoxImgGrises = new System.Windows.Forms.PictureBox();
            this.filtrosVariadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contornoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.difuminadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.realzarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobelInferiorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobelSuperiorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobelDerechoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobelIzquierdoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afilarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personalizadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxA = new System.Windows.Forms.TextBox();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.textBoxC = new System.Windows.Forms.TextBox();
            this.textBoxD = new System.Windows.Forms.TextBox();
            this.textBoxE = new System.Windows.Forms.TextBox();
            this.textBoxF = new System.Windows.Forms.TextBox();
            this.textBoxG = new System.Windows.Forms.TextBox();
            this.textBoxH = new System.Windows.Forms.TextBox();
            this.textBoxI = new System.Windows.Forms.TextBox();
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgEditada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgGrises)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.filtrosVariadosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(949, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importarImagenToolStripMenuItem,
            this.guardarImagenToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // importarImagenToolStripMenuItem
            // 
            this.importarImagenToolStripMenuItem.Name = "importarImagenToolStripMenuItem";
            this.importarImagenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importarImagenToolStripMenuItem.Text = "Importar Imagen";
            this.importarImagenToolStripMenuItem.Click += new System.EventHandler(this.importarImagenToolStripMenuItem_Click);
            // 
            // guardarImagenToolStripMenuItem
            // 
            this.guardarImagenToolStripMenuItem.Name = "guardarImagenToolStripMenuItem";
            this.guardarImagenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.guardarImagenToolStripMenuItem.Text = "Guardar Imagen";
            this.guardarImagenToolStripMenuItem.Click += new System.EventHandler(this.guardarImagenToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filtroOriginalToolStripMenuItem,
            this.contornoToolStripMenuItem,
            this.difuminadoToolStripMenuItem,
            this.realzarToolStripMenuItem,
            this.sobelToolStripMenuItem,
            this.afilarToolStripMenuItem,
            this.personalizadoToolStripMenuItem});
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.editarToolStripMenuItem.Text = "Filtros Base";
            // 
            // filtroOriginalToolStripMenuItem
            // 
            this.filtroOriginalToolStripMenuItem.Name = "filtroOriginalToolStripMenuItem";
            this.filtroOriginalToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.filtroOriginalToolStripMenuItem.Text = "Original";
            this.filtroOriginalToolStripMenuItem.Click += new System.EventHandler(this.filtroOriginalToolStripMenuItem_Click);
            // 
            // pictureBoxImgOriginal
            // 
            this.pictureBoxImgOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxImgOriginal.Location = new System.Drawing.Point(12, 269);
            this.pictureBoxImgOriginal.Name = "pictureBoxImgOriginal";
            this.pictureBoxImgOriginal.Size = new System.Drawing.Size(250, 200);
            this.pictureBoxImgOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImgOriginal.TabIndex = 1;
            this.pictureBoxImgOriginal.TabStop = false;
            // 
            // labelTitle1
            // 
            this.labelTitle1.AutoSize = true;
            this.labelTitle1.Location = new System.Drawing.Point(12, 479);
            this.labelTitle1.Name = "labelTitle1";
            this.labelTitle1.Size = new System.Drawing.Size(80, 13);
            this.labelTitle1.TabIndex = 2;
            this.labelTitle1.Text = "Imagen Original";
            // 
            // labelTitle2
            // 
            this.labelTitle2.AutoSize = true;
            this.labelTitle2.Location = new System.Drawing.Point(736, 476);
            this.labelTitle2.Name = "labelTitle2";
            this.labelTitle2.Size = new System.Drawing.Size(81, 13);
            this.labelTitle2.TabIndex = 3;
            this.labelTitle2.Text = "Imagen Editada";
            // 
            // pictureBoxImgEditada
            // 
            this.pictureBoxImgEditada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxImgEditada.Location = new System.Drawing.Point(655, 269);
            this.pictureBoxImgEditada.Name = "pictureBoxImgEditada";
            this.pictureBoxImgEditada.Size = new System.Drawing.Size(250, 200);
            this.pictureBoxImgEditada.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImgEditada.TabIndex = 4;
            this.pictureBoxImgEditada.TabStop = false;
            // 
            // openFileDialogImportarImagen
            // 
            this.openFileDialogImportarImagen.FileName = "openFileDialogImportarImagen";
            this.openFileDialogImportarImagen.Filter = "Archivos de mapa de bits|*.BMP|JPEG|*.JPG|PNG|*.PNG";
            // 
            // saveFileDialogImage
            // 
            this.saveFileDialogImage.Filter = "Archivos de mapa de bits|*.BMP|JPEG|*.JPG|PNG|*.PNG";
            // 
            // labelTitle3
            // 
            this.labelTitle3.AutoSize = true;
            this.labelTitle3.Location = new System.Drawing.Point(344, 479);
            this.labelTitle3.Name = "labelTitle3";
            this.labelTitle3.Size = new System.Drawing.Size(124, 13);
            this.labelTitle3.TabIndex = 5;
            this.labelTitle3.Text = "Imagen Escala de Grises";
            // 
            // pictureBoxImgGrises
            // 
            this.pictureBoxImgGrises.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxImgGrises.Location = new System.Drawing.Point(333, 269);
            this.pictureBoxImgGrises.Name = "pictureBoxImgGrises";
            this.pictureBoxImgGrises.Size = new System.Drawing.Size(250, 200);
            this.pictureBoxImgGrises.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImgGrises.TabIndex = 6;
            this.pictureBoxImgGrises.TabStop = false;
            // 
            // filtrosVariadosToolStripMenuItem
            // 
            this.filtrosVariadosToolStripMenuItem.Name = "filtrosVariadosToolStripMenuItem";
            this.filtrosVariadosToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.filtrosVariadosToolStripMenuItem.Text = "Filtros Variados";
            // 
            // contornoToolStripMenuItem
            // 
            this.contornoToolStripMenuItem.Name = "contornoToolStripMenuItem";
            this.contornoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.contornoToolStripMenuItem.Text = "Contorno";
            this.contornoToolStripMenuItem.Click += new System.EventHandler(this.contornoToolStripMenuItem_Click);
            // 
            // difuminadoToolStripMenuItem
            // 
            this.difuminadoToolStripMenuItem.Name = "difuminadoToolStripMenuItem";
            this.difuminadoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.difuminadoToolStripMenuItem.Text = "Difuminado";
            this.difuminadoToolStripMenuItem.Click += new System.EventHandler(this.difuminadoToolStripMenuItem_Click);
            // 
            // realzarToolStripMenuItem
            // 
            this.realzarToolStripMenuItem.Name = "realzarToolStripMenuItem";
            this.realzarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.realzarToolStripMenuItem.Text = "Realzar";
            this.realzarToolStripMenuItem.Click += new System.EventHandler(this.realzarToolStripMenuItem_Click);
            // 
            // sobelToolStripMenuItem
            // 
            this.sobelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sobelInferiorToolStripMenuItem,
            this.sobelSuperiorToolStripMenuItem,
            this.sobelDerechoToolStripMenuItem,
            this.sobelIzquierdoToolStripMenuItem});
            this.sobelToolStripMenuItem.Name = "sobelToolStripMenuItem";
            this.sobelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sobelToolStripMenuItem.Text = "Sobel ";
            // 
            // sobelInferiorToolStripMenuItem
            // 
            this.sobelInferiorToolStripMenuItem.Name = "sobelInferiorToolStripMenuItem";
            this.sobelInferiorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sobelInferiorToolStripMenuItem.Text = "Sobel Inferior";
            this.sobelInferiorToolStripMenuItem.Click += new System.EventHandler(this.sobelInferiorToolStripMenuItem_Click);
            // 
            // sobelSuperiorToolStripMenuItem
            // 
            this.sobelSuperiorToolStripMenuItem.Name = "sobelSuperiorToolStripMenuItem";
            this.sobelSuperiorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sobelSuperiorToolStripMenuItem.Text = "Sobel Superior";
            this.sobelSuperiorToolStripMenuItem.Click += new System.EventHandler(this.sobelSuperiorToolStripMenuItem_Click);
            // 
            // sobelDerechoToolStripMenuItem
            // 
            this.sobelDerechoToolStripMenuItem.Name = "sobelDerechoToolStripMenuItem";
            this.sobelDerechoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sobelDerechoToolStripMenuItem.Text = "Sobel Derecho";
            this.sobelDerechoToolStripMenuItem.Click += new System.EventHandler(this.sobelDerechoToolStripMenuItem_Click);
            // 
            // sobelIzquierdoToolStripMenuItem
            // 
            this.sobelIzquierdoToolStripMenuItem.Name = "sobelIzquierdoToolStripMenuItem";
            this.sobelIzquierdoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sobelIzquierdoToolStripMenuItem.Text = "Sobel Izquierdo";
            this.sobelIzquierdoToolStripMenuItem.Click += new System.EventHandler(this.sobelIzquierdoToolStripMenuItem_Click);
            // 
            // afilarToolStripMenuItem
            // 
            this.afilarToolStripMenuItem.Name = "afilarToolStripMenuItem";
            this.afilarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.afilarToolStripMenuItem.Text = "Afilar";
            this.afilarToolStripMenuItem.Click += new System.EventHandler(this.afilarToolStripMenuItem_Click);
            // 
            // personalizadoToolStripMenuItem
            // 
            this.personalizadoToolStripMenuItem.Name = "personalizadoToolStripMenuItem";
            this.personalizadoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.personalizadoToolStripMenuItem.Text = "Personalizado";
            this.personalizadoToolStripMenuItem.Click += new System.EventHandler(this.personalizadoToolStripMenuItem_Click);
            // 
            // textBoxA
            // 
            this.textBoxA.Location = new System.Drawing.Point(672, 114);
            this.textBoxA.Name = "textBoxA";
            this.textBoxA.Size = new System.Drawing.Size(70, 20);
            this.textBoxA.TabIndex = 7;
            this.textBoxA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxA_KeyPress);
            // 
            // textBoxB
            // 
            this.textBoxB.Location = new System.Drawing.Point(747, 114);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(70, 20);
            this.textBoxB.TabIndex = 8;
            this.textBoxB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxB_KeyPress);
            // 
            // textBoxC
            // 
            this.textBoxC.Location = new System.Drawing.Point(823, 114);
            this.textBoxC.Name = "textBoxC";
            this.textBoxC.Size = new System.Drawing.Size(70, 20);
            this.textBoxC.TabIndex = 9;
            this.textBoxC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxC_KeyPress);
            // 
            // textBoxD
            // 
            this.textBoxD.Location = new System.Drawing.Point(672, 140);
            this.textBoxD.Name = "textBoxD";
            this.textBoxD.Size = new System.Drawing.Size(70, 20);
            this.textBoxD.TabIndex = 10;
            this.textBoxD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxD_KeyPress);
            // 
            // textBoxE
            // 
            this.textBoxE.Location = new System.Drawing.Point(747, 140);
            this.textBoxE.Name = "textBoxE";
            this.textBoxE.Size = new System.Drawing.Size(70, 20);
            this.textBoxE.TabIndex = 11;
            this.textBoxE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxE_KeyPress);
            // 
            // textBoxF
            // 
            this.textBoxF.Location = new System.Drawing.Point(823, 140);
            this.textBoxF.Name = "textBoxF";
            this.textBoxF.Size = new System.Drawing.Size(70, 20);
            this.textBoxF.TabIndex = 12;
            this.textBoxF.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxF_KeyPress);
            // 
            // textBoxG
            // 
            this.textBoxG.Location = new System.Drawing.Point(672, 166);
            this.textBoxG.Name = "textBoxG";
            this.textBoxG.Size = new System.Drawing.Size(70, 20);
            this.textBoxG.TabIndex = 13;
            this.textBoxG.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxG_KeyPress);
            // 
            // textBoxH
            // 
            this.textBoxH.Location = new System.Drawing.Point(747, 166);
            this.textBoxH.Name = "textBoxH";
            this.textBoxH.Size = new System.Drawing.Size(70, 20);
            this.textBoxH.TabIndex = 14;
            this.textBoxH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxH_KeyPress);
            // 
            // textBoxI
            // 
            this.textBoxI.Location = new System.Drawing.Point(823, 166);
            this.textBoxI.Name = "textBoxI";
            this.textBoxI.Size = new System.Drawing.Size(70, 20);
            this.textBoxI.TabIndex = 15;
            this.textBoxI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxI_KeyPress);
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Location = new System.Drawing.Point(747, 204);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(75, 23);
            this.buttonAceptar.TabIndex = 16;
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.buttonAceptar_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 501);
            this.Controls.Add(this.buttonAceptar);
            this.Controls.Add(this.textBoxI);
            this.Controls.Add(this.textBoxH);
            this.Controls.Add(this.textBoxG);
            this.Controls.Add(this.textBoxF);
            this.Controls.Add(this.textBoxE);
            this.Controls.Add(this.textBoxD);
            this.Controls.Add(this.textBoxC);
            this.Controls.Add(this.textBoxB);
            this.Controls.Add(this.textBoxA);
            this.Controls.Add(this.pictureBoxImgGrises);
            this.Controls.Add(this.labelTitle3);
            this.Controls.Add(this.pictureBoxImgEditada);
            this.Controls.Add(this.labelTitle2);
            this.Controls.Add(this.labelTitle1);
            this.Controls.Add(this.pictureBoxImgOriginal);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.Text = "FormPrincipal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgEditada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgGrises)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importarImagenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarImagenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtroOriginalToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBoxImgOriginal;
        private System.Windows.Forms.Label labelTitle1;
        private System.Windows.Forms.Label labelTitle2;
        private System.Windows.Forms.PictureBox pictureBoxImgEditada;
        private System.Windows.Forms.OpenFileDialog openFileDialogImportarImagen;
        private System.Windows.Forms.SaveFileDialog saveFileDialogImage;
        private System.Windows.Forms.Label labelTitle3;
        private System.Windows.Forms.PictureBox pictureBoxImgGrises;
        private System.Windows.Forms.ToolStripMenuItem filtrosVariadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contornoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem difuminadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realzarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobelInferiorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobelSuperiorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobelDerechoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobelIzquierdoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem afilarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personalizadoToolStripMenuItem;
        public System.Windows.Forms.TextBox textBoxA;
        public System.Windows.Forms.TextBox textBoxB;
        public System.Windows.Forms.TextBox textBoxC;
        public System.Windows.Forms.TextBox textBoxD;
        public System.Windows.Forms.TextBox textBoxE;
        public System.Windows.Forms.TextBox textBoxF;
        public System.Windows.Forms.TextBox textBoxG;
        public System.Windows.Forms.TextBox textBoxH;
        public System.Windows.Forms.TextBox textBoxI;
        public System.Windows.Forms.Button buttonAceptar;
    }
}