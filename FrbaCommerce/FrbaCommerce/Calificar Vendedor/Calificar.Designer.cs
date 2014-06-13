namespace FrbaCommerce.Calificar_Vendedor
{
    partial class Calificar
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
            this.labelTituloNumero = new System.Windows.Forms.Label();
            this.labelTituloDescripcion = new System.Windows.Forms.Label();
            this.checkBoxPredeterminado = new System.Windows.Forms.CheckBox();
            this.comboBoxDescripciones = new System.Windows.Forms.ComboBox();
            this.textBoxDescripcion = new System.Windows.Forms.TextBox();
            this.botonCalificar = new System.Windows.Forms.Button();
            this.botonCancelar = new System.Windows.Forms.Button();
            this.dropdownCalificacion = new System.Windows.Forms.DomainUpDown();
            this.SuspendLayout();
            // 
            // labelTituloNumero
            // 
            this.labelTituloNumero.AutoSize = true;
            this.labelTituloNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTituloNumero.Location = new System.Drawing.Point(28, 49);
            this.labelTituloNumero.Name = "labelTituloNumero";
            this.labelTituloNumero.Size = new System.Drawing.Size(244, 15);
            this.labelTituloNumero.TabIndex = 10;
            this.labelTituloNumero.Text = "Seleccione cantidad de estrellas del 1 al 10";
            // 
            // labelTituloDescripcion
            // 
            this.labelTituloDescripcion.AutoSize = true;
            this.labelTituloDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTituloDescripcion.Location = new System.Drawing.Point(28, 100);
            this.labelTituloDescripcion.Name = "labelTituloDescripcion";
            this.labelTituloDescripcion.Size = new System.Drawing.Size(138, 15);
            this.labelTituloDescripcion.TabIndex = 11;
            this.labelTituloDescripcion.Text = "Ingrese una descripcion";
            // 
            // checkBoxPredeterminado
            // 
            this.checkBoxPredeterminado.AutoSize = true;
            this.checkBoxPredeterminado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPredeterminado.Location = new System.Drawing.Point(31, 127);
            this.checkBoxPredeterminado.Name = "checkBoxPredeterminado";
            this.checkBoxPredeterminado.Size = new System.Drawing.Size(100, 17);
            this.checkBoxPredeterminado.TabIndex = 12;
            this.checkBoxPredeterminado.Text = "Predeterminada";
            this.checkBoxPredeterminado.UseVisualStyleBackColor = true;
            this.checkBoxPredeterminado.CheckedChanged += new System.EventHandler(this.checkBoxPredeterminado_CheckedChanged);
            // 
            // comboBoxDescripciones
            // 
            this.comboBoxDescripciones.FormattingEnabled = true;
            this.comboBoxDescripciones.Location = new System.Drawing.Point(137, 123);
            this.comboBoxDescripciones.Name = "comboBoxDescripciones";
            this.comboBoxDescripciones.Size = new System.Drawing.Size(264, 21);
            this.comboBoxDescripciones.TabIndex = 13;
            // 
            // textBoxDescripcion
            // 
            this.textBoxDescripcion.Location = new System.Drawing.Point(31, 150);
            this.textBoxDescripcion.Multiline = true;
            this.textBoxDescripcion.Name = "textBoxDescripcion";
            this.textBoxDescripcion.Size = new System.Drawing.Size(370, 47);
            this.textBoxDescripcion.TabIndex = 14;
            // 
            // botonCalificar
            // 
            this.botonCalificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonCalificar.Location = new System.Drawing.Point(226, 213);
            this.botonCalificar.Name = "botonCalificar";
            this.botonCalificar.Size = new System.Drawing.Size(95, 39);
            this.botonCalificar.TabIndex = 15;
            this.botonCalificar.Text = "Calificar";
            this.botonCalificar.UseVisualStyleBackColor = true;
            this.botonCalificar.Click += new System.EventHandler(this.botonCalificar_Click);
            // 
            // botonCancelar
            // 
            this.botonCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonCancelar.Location = new System.Drawing.Point(114, 213);
            this.botonCancelar.Name = "botonCancelar";
            this.botonCancelar.Size = new System.Drawing.Size(96, 39);
            this.botonCancelar.TabIndex = 16;
            this.botonCancelar.Text = "Cancelar";
            this.botonCancelar.UseVisualStyleBackColor = true;
            this.botonCancelar.Click += new System.EventHandler(this.botonCancelar_Click);
            // 
            // dropdownCalificacion
            // 
            this.dropdownCalificacion.Location = new System.Drawing.Point(276, 47);
            this.dropdownCalificacion.Name = "dropdownCalificacion";
            this.dropdownCalificacion.ReadOnly = true;
            this.dropdownCalificacion.Size = new System.Drawing.Size(43, 20);
            this.dropdownCalificacion.TabIndex = 17;
            this.dropdownCalificacion.Text = " ---";
            this.dropdownCalificacion.SelectedItemChanged += new System.EventHandler(this.dropdownCalificacion_SelectedItemChanged);
            // 
            // Calificar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 270);
            this.Controls.Add(this.dropdownCalificacion);
            this.Controls.Add(this.botonCancelar);
            this.Controls.Add(this.botonCalificar);
            this.Controls.Add(this.textBoxDescripcion);
            this.Controls.Add(this.comboBoxDescripciones);
            this.Controls.Add(this.checkBoxPredeterminado);
            this.Controls.Add(this.labelTituloDescripcion);
            this.Controls.Add(this.labelTituloNumero);
            this.Name = "Calificar";
            this.Text = "Calificar";
            this.Load += new System.EventHandler(this.Calificar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTituloNumero;
        private System.Windows.Forms.Label labelTituloDescripcion;
        private System.Windows.Forms.CheckBox checkBoxPredeterminado;
        private System.Windows.Forms.ComboBox comboBoxDescripciones;
        private System.Windows.Forms.TextBox textBoxDescripcion;
        private System.Windows.Forms.Button botonCalificar;
        private System.Windows.Forms.Button botonCancelar;
        private System.Windows.Forms.DomainUpDown dropdownCalificacion;
    }
}