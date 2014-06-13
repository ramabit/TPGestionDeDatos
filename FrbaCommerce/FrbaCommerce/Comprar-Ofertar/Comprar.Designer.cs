namespace FrbaCommerce.Comprar_Ofertar
{
    partial class Comprar
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
            this.labelMensaje = new System.Windows.Forms.Label();
            this.textBoxCant = new System.Windows.Forms.TextBox();
            this.botonConfirmarCompra = new System.Windows.Forms.Button();
            this.botonCancelar = new System.Windows.Forms.Button();
            this.groupBoxVendedor = new System.Windows.Forms.GroupBox();
            this.groupBoxDireccion = new System.Windows.Forms.GroupBox();
            this.labelLocalidad = new System.Windows.Forms.Label();
            this.labelPostal = new System.Windows.Forms.Label();
            this.labelDepartamento = new System.Windows.Forms.Label();
            this.labelCalle = new System.Windows.Forms.Label();
            this.labelTelefono = new System.Windows.Forms.Label();
            this.labelMail = new System.Windows.Forms.Label();
            this.labelNombre = new System.Windows.Forms.Label();
            this.groupBoxVendedor.SuspendLayout();
            this.groupBoxDireccion.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelMensaje
            // 
            this.labelMensaje.AutoSize = true;
            this.labelMensaje.Location = new System.Drawing.Point(70, 37);
            this.labelMensaje.Name = "labelMensaje";
            this.labelMensaje.Size = new System.Drawing.Size(191, 13);
            this.labelMensaje.TabIndex = 0;
            this.labelMensaje.Text = "Ingrese la cantidad que desea comprar";
            // 
            // textBoxCant
            // 
            this.textBoxCant.Location = new System.Drawing.Point(107, 78);
            this.textBoxCant.Name = "textBoxCant";
            this.textBoxCant.Size = new System.Drawing.Size(106, 20);
            this.textBoxCant.TabIndex = 1;
            // 
            // botonConfirmarCompra
            // 
            this.botonConfirmarCompra.Location = new System.Drawing.Point(172, 447);
            this.botonConfirmarCompra.Name = "botonConfirmarCompra";
            this.botonConfirmarCompra.Size = new System.Drawing.Size(118, 41);
            this.botonConfirmarCompra.TabIndex = 2;
            this.botonConfirmarCompra.Text = "Confirmar Compra";
            this.botonConfirmarCompra.UseVisualStyleBackColor = true;
            this.botonConfirmarCompra.Click += new System.EventHandler(this.buttonConfirmarCompra_Click);
            // 
            // botonCancelar
            // 
            this.botonCancelar.Location = new System.Drawing.Point(29, 447);
            this.botonCancelar.Name = "botonCancelar";
            this.botonCancelar.Size = new System.Drawing.Size(110, 41);
            this.botonCancelar.TabIndex = 3;
            this.botonCancelar.Text = "Cancelar";
            this.botonCancelar.UseVisualStyleBackColor = true;
            this.botonCancelar.Click += new System.EventHandler(this.botonCancelar_Click);
            // 
            // groupBoxVendedor
            // 
            this.groupBoxVendedor.Controls.Add(this.groupBoxDireccion);
            this.groupBoxVendedor.Controls.Add(this.labelTelefono);
            this.groupBoxVendedor.Controls.Add(this.labelMail);
            this.groupBoxVendedor.Controls.Add(this.labelNombre);
            this.groupBoxVendedor.Location = new System.Drawing.Point(24, 136);
            this.groupBoxVendedor.Name = "groupBoxVendedor";
            this.groupBoxVendedor.Size = new System.Drawing.Size(282, 291);
            this.groupBoxVendedor.TabIndex = 4;
            this.groupBoxVendedor.TabStop = false;
            this.groupBoxVendedor.Text = "Datos del Vendedor";
            // 
            // groupBoxDireccion
            // 
            this.groupBoxDireccion.Controls.Add(this.labelLocalidad);
            this.groupBoxDireccion.Controls.Add(this.labelPostal);
            this.groupBoxDireccion.Controls.Add(this.labelDepartamento);
            this.groupBoxDireccion.Controls.Add(this.labelCalle);
            this.groupBoxDireccion.Location = new System.Drawing.Point(26, 120);
            this.groupBoxDireccion.Name = "groupBoxDireccion";
            this.groupBoxDireccion.Size = new System.Drawing.Size(230, 141);
            this.groupBoxDireccion.TabIndex = 4;
            this.groupBoxDireccion.TabStop = false;
            this.groupBoxDireccion.Text = "Dirección";
            // 
            // labelLocalidad
            // 
            this.labelLocalidad.AutoSize = true;
            this.labelLocalidad.Location = new System.Drawing.Point(21, 102);
            this.labelLocalidad.Name = "labelLocalidad";
            this.labelLocalidad.Size = new System.Drawing.Size(35, 13);
            this.labelLocalidad.TabIndex = 3;
            this.labelLocalidad.Text = "label8";
            // 
            // labelPostal
            // 
            this.labelPostal.AutoSize = true;
            this.labelPostal.Location = new System.Drawing.Point(21, 76);
            this.labelPostal.Name = "labelPostal";
            this.labelPostal.Size = new System.Drawing.Size(35, 13);
            this.labelPostal.TabIndex = 2;
            this.labelPostal.Text = "label7";
            // 
            // labelDepartamento
            // 
            this.labelDepartamento.AutoSize = true;
            this.labelDepartamento.Location = new System.Drawing.Point(20, 51);
            this.labelDepartamento.Name = "labelDepartamento";
            this.labelDepartamento.Size = new System.Drawing.Size(35, 13);
            this.labelDepartamento.TabIndex = 1;
            this.labelDepartamento.Text = "label6";
            // 
            // labelCalle
            // 
            this.labelCalle.AutoSize = true;
            this.labelCalle.Location = new System.Drawing.Point(21, 25);
            this.labelCalle.Name = "labelCalle";
            this.labelCalle.Size = new System.Drawing.Size(35, 13);
            this.labelCalle.TabIndex = 0;
            this.labelCalle.Text = "label5";
            // 
            // labelTelefono
            // 
            this.labelTelefono.AutoSize = true;
            this.labelTelefono.Location = new System.Drawing.Point(23, 69);
            this.labelTelefono.Name = "labelTelefono";
            this.labelTelefono.Size = new System.Drawing.Size(35, 13);
            this.labelTelefono.TabIndex = 2;
            this.labelTelefono.Text = "";
            // 
            // labelMail
            // 
            this.labelMail.AutoSize = true;
            this.labelMail.Location = new System.Drawing.Point(23, 48);
            this.labelMail.Name = "labelMail";
            this.labelMail.Size = new System.Drawing.Size(35, 13);
            this.labelMail.TabIndex = 1;
            this.labelMail.Text = "label2";
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(23, 25);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(35, 13);
            this.labelNombre.TabIndex = 0;
            this.labelNombre.Text = "label1";
            // 
            // Comprar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 500);
            this.Controls.Add(this.groupBoxVendedor);
            this.Controls.Add(this.botonCancelar);
            this.Controls.Add(this.botonConfirmarCompra);
            this.Controls.Add(this.textBoxCant);
            this.Controls.Add(this.labelMensaje);
            this.Name = "Comprar";
            this.Text = "Comprar";
            this.Load += new System.EventHandler(this.Comprar_Load);
            this.groupBoxVendedor.ResumeLayout(false);
            this.groupBoxVendedor.PerformLayout();
            this.groupBoxDireccion.ResumeLayout(false);
            this.groupBoxDireccion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMensaje;
        private System.Windows.Forms.TextBox textBoxCant;
        private System.Windows.Forms.Button botonConfirmarCompra;
        private System.Windows.Forms.Button botonCancelar;
        private System.Windows.Forms.GroupBox groupBoxVendedor;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.Label labelTelefono;
        private System.Windows.Forms.Label labelMail;
        private System.Windows.Forms.GroupBox groupBoxDireccion;
        private System.Windows.Forms.Label labelLocalidad;
        private System.Windows.Forms.Label labelPostal;
        private System.Windows.Forms.Label labelDepartamento;
        private System.Windows.Forms.Label labelCalle;

    }
}