namespace FrbaCommerce.Comprar_Ofertar
{
    partial class Ofertar
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
            this.textBoxMonto = new System.Windows.Forms.TextBox();
            this.labelMensaje = new System.Windows.Forms.Label();
            this.botonCancelar = new System.Windows.Forms.Button();
            this.botonOfertar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxMonto
            // 
            this.textBoxMonto.Location = new System.Drawing.Point(105, 104);
            this.textBoxMonto.Name = "textBoxMonto";
            this.textBoxMonto.Size = new System.Drawing.Size(82, 20);
            this.textBoxMonto.TabIndex = 0;
            // 
            // labelMensaje
            // 
            this.labelMensaje.AutoSize = true;
            this.labelMensaje.Location = new System.Drawing.Point(73, 49);
            this.labelMensaje.Name = "labelMensaje";
            this.labelMensaje.Size = new System.Drawing.Size(144, 13);
            this.labelMensaje.TabIndex = 1;
            this.labelMensaje.Text = "Ingrese el monto de su oferta";
            // 
            // botonCancelar
            // 
            this.botonCancelar.Location = new System.Drawing.Point(28, 187);
            this.botonCancelar.Name = "botonCancelar";
            this.botonCancelar.Size = new System.Drawing.Size(85, 37);
            this.botonCancelar.TabIndex = 2;
            this.botonCancelar.Text = "Cancelar";
            this.botonCancelar.UseVisualStyleBackColor = true;
            this.botonCancelar.Click += new System.EventHandler(this.botonCancelar_Click);
            // 
            // botonOfertar
            // 
            this.botonOfertar.Location = new System.Drawing.Point(153, 189);
            this.botonOfertar.Name = "botonOfertar";
            this.botonOfertar.Size = new System.Drawing.Size(99, 34);
            this.botonOfertar.TabIndex = 3;
            this.botonOfertar.Text = "Confirmar Oferta";
            this.botonOfertar.UseVisualStyleBackColor = true;
            this.botonOfertar.Click += new System.EventHandler(this.botonOfertar_Click);
            // 
            // Ofertar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 270);
            this.Controls.Add(this.botonOfertar);
            this.Controls.Add(this.botonCancelar);
            this.Controls.Add(this.labelMensaje);
            this.Controls.Add(this.textBoxMonto);
            this.Name = "Ofertar";
            this.Text = "Ofertar";
            this.Load += new System.EventHandler(this.Ofertar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMonto;
        private System.Windows.Forms.Label labelMensaje;
        private System.Windows.Forms.Button botonCancelar;
        private System.Windows.Forms.Button botonOfertar;
    }
}