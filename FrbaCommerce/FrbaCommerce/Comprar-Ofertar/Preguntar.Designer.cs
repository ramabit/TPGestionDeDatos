namespace FrbaCommerce.Comprar_Ofertar
{
    partial class Preguntar
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
            this.textBoxPregunta = new System.Windows.Forms.TextBox();
            this.labelMensaje = new System.Windows.Forms.Label();
            this.botonVolver = new System.Windows.Forms.Button();
            this.botonPreguntar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPregunta
            // 
            this.textBoxPregunta.Location = new System.Drawing.Point(23, 81);
            this.textBoxPregunta.MaxLength = 255;
            this.textBoxPregunta.Name = "textBoxPregunta";
            this.textBoxPregunta.Size = new System.Drawing.Size(235, 20);
            this.textBoxPregunta.TabIndex = 0;
            // 
            // labelMensaje
            // 
            this.labelMensaje.AutoSize = true;
            this.labelMensaje.Location = new System.Drawing.Point(30, 39);
            this.labelMensaje.Name = "labelMensaje";
            this.labelMensaje.Size = new System.Drawing.Size(90, 13);
            this.labelMensaje.TabIndex = 1;
            this.labelMensaje.Text = "Ingresar pregunta";
            // 
            // botonVolver
            // 
            this.botonVolver.Location = new System.Drawing.Point(25, 197);
            this.botonVolver.Name = "botonVolver";
            this.botonVolver.Size = new System.Drawing.Size(86, 27);
            this.botonVolver.TabIndex = 2;
            this.botonVolver.Text = "Cancelar";
            this.botonVolver.UseVisualStyleBackColor = true;
            this.botonVolver.Click += new System.EventHandler(this.botonVolver_Click);
            // 
            // botonPreguntar
            // 
            this.botonPreguntar.Location = new System.Drawing.Point(138, 198);
            this.botonPreguntar.Name = "botonPreguntar";
            this.botonPreguntar.Size = new System.Drawing.Size(98, 25);
            this.botonPreguntar.TabIndex = 3;
            this.botonPreguntar.Text = "Enviar";
            this.botonPreguntar.UseVisualStyleBackColor = true;
            this.botonPreguntar.Click += new System.EventHandler(this.botonPreguntar_Click);
            // 
            // Preguntar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 270);
            this.Controls.Add(this.botonPreguntar);
            this.Controls.Add(this.botonVolver);
            this.Controls.Add(this.labelMensaje);
            this.Controls.Add(this.textBoxPregunta);
            this.Name = "Preguntar";
            this.Text = "Preguntar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPregunta;
        private System.Windows.Forms.Label labelMensaje;
        private System.Windows.Forms.Button botonVolver;
        private System.Windows.Forms.Button botonPreguntar;
    }
}