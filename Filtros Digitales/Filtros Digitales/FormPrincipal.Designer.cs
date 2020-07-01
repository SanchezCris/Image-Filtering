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
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarImagenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarImagenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtroOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxImgOriginal = new System.Windows.Forms.PictureBox();
            this.labelTitle1 = new System.Windows.Forms.Label();
            this.labelTitle2 = new System.Windows.Forms.Label();
            this.pictureBoxImgEditada = new System.Windows.Forms.PictureBox();
            this.openFileDialogImportarImagen = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogImage = new System.Windows.Forms.SaveFileDialog();
            this.labelTitle3 = new System.Windows.Forms.Label();
            this.pictureBoxImgGrises = new System.Windows.Forms.PictureBox();
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
            this.editarToolStripMenuItem});
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
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filtroOriginalToolStripMenuItem});
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.editarToolStripMenuItem.Text = "Editar";
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
            // filtroOriginalToolStripMenuItem
            // 
            this.filtroOriginalToolStripMenuItem.Name = "filtroOriginalToolStripMenuItem";
            this.filtroOriginalToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.filtroOriginalToolStripMenuItem.Text = "Filtro Original";
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
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 501);
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
    }
}