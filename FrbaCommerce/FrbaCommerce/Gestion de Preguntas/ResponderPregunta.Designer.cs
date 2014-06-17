namespace FrbaCommerce.Gestion_de_Preguntas
{
    partial class ResponderPregunta
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
            this.textBoxRespuesta = new System.Windows.Forms.TextBox();
            this.botonVolver = new System.Windows.Forms.Button();
            this.botonEnviar = new System.Windows.Forms.Button();
            this.labelPregunta = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxRespuesta
            // 
            this.textBoxRespuesta.Location = new System.Drawing.Point(291, 65);
            this.textBoxRespuesta.MaxLength = 255;
            this.textBoxRespuesta.Multiline = true;
            this.textBoxRespuesta.Name = "textBoxRespuesta";
            this.textBoxRespuesta.Size = new System.Drawing.Size(229, 121);
            this.textBoxRespuesta.TabIndex = 0;
            // 
            // botonVolver
            // 
            this.botonVolver.Location = new System.Drawing.Point(111, 219);
            this.botonVolver.Name = "botonVolver";
            this.botonVolver.Size = new System.Drawing.Size(87, 38);
            this.botonVolver.TabIndex = 1;
            this.botonVolver.Text = "Cancelar";
            this.botonVolver.UseVisualStyleBackColor = true;
            this.botonVolver.Click += new System.EventHandler(this.botonVolver_Click);
            // 
            // botonEnviar
            // 
            this.botonEnviar.Location = new System.Drawing.Point(354, 219);
            this.botonEnviar.Name = "botonEnviar";
            this.botonEnviar.Size = new System.Drawing.Size(93, 38);
            this.botonEnviar.TabIndex = 2;
            this.botonEnviar.Text = "Enviar";
            this.botonEnviar.UseVisualStyleBackColor = true;
            this.botonEnviar.Click += new System.EventHandler(this.botonEnviar_Click);
            // 
            // labelPregunta
            // 
            this.labelPregunta.Location = new System.Drawing.Point(26, 65);
            this.labelPregunta.Name = "labelPregunta";
            this.labelPregunta.Size = new System.Drawing.Size(232, 121);
            this.labelPregunta.TabIndex = 3;
            this.labelPregunta.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(288, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 18);
            this.label2.TabIndex = 12;
            this.label2.Text = "Respuesta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "Pregunta";
            // 
            // ResponderPregunta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 291);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelPregunta);
            this.Controls.Add(this.botonEnviar);
            this.Controls.Add(this.botonVolver);
            this.Controls.Add(this.textBoxRespuesta);
            this.Name = "ResponderPregunta";
            this.Text = "ResponderPreguntas";
            this.Load += new System.EventHandler(this.ResponderPregunta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRespuesta;
        private System.Windows.Forms.Button botonVolver;
        private System.Windows.Forms.Button botonEnviar;
        private System.Windows.Forms.Label labelPregunta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}