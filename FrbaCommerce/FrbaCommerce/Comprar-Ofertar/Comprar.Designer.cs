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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // labelMensaje
            // 
            this.labelMensaje.AutoSize = true;
            this.labelMensaje.Location = new System.Drawing.Point(26, 45);
            this.labelMensaje.Name = "labelMensaje";
            this.labelMensaje.Size = new System.Drawing.Size(191, 13);
            this.labelMensaje.TabIndex = 0;
            this.labelMensaje.Text = "Ingrese la cantidad que desea comprar";
            // 
            // textBoxCant
            // 
            this.textBoxCant.Location = new System.Drawing.Point(61, 87);
            this.textBoxCant.Name = "textBoxCant";
            this.textBoxCant.Size = new System.Drawing.Size(106, 20);
            this.textBoxCant.TabIndex = 1;
            // 
            // botonConfirmarCompra
            // 
            this.botonConfirmarCompra.Location = new System.Drawing.Point(147, 332);
            this.botonConfirmarCompra.Name = "botonConfirmarCompra";
            this.botonConfirmarCompra.Size = new System.Drawing.Size(118, 41);
            this.botonConfirmarCompra.TabIndex = 2;
            this.botonConfirmarCompra.Text = "Confirmar Compra";
            this.botonConfirmarCompra.UseVisualStyleBackColor = true;
            this.botonConfirmarCompra.Click += new System.EventHandler(this.buttonConfirmarCompra_Click);
            // 
            // button1
            // 
            this.botonCancelar.Location = new System.Drawing.Point(17, 331);
            this.botonCancelar.Name = "button1";
            this.botonCancelar.Size = new System.Drawing.Size(110, 41);
            this.botonCancelar.TabIndex = 3;
            this.botonCancelar.Text = "Cancelar";
            this.botonCancelar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(24, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 170);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Vendedor";
            // 
            // Comprar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 417);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.botonCancelar);
            this.Controls.Add(this.botonConfirmarCompra);
            this.Controls.Add(this.textBoxCant);
            this.Controls.Add(this.labelMensaje);
            this.Name = "Comprar";
            this.Text = "Comprar";
            this.Load += new System.EventHandler(this.Comprar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMensaje;
        private System.Windows.Forms.TextBox textBoxCant;
        private System.Windows.Forms.Button botonConfirmarCompra;
        private System.Windows.Forms.Button botonCancelar;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}