namespace FrbaCommerce.Facturar_Publicaciones
{
    partial class Facturar
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
            this.labelComisiones = new System.Windows.Forms.Label();
            this.dropDownFacturar = new System.Windows.Forms.DomainUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.botonFacturar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labelMin = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.labelCostos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelCantidadCostos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelMinimo = new System.Windows.Forms.Label();
            this.labelMaximo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelMonto = new System.Windows.Forms.Label();
            this.labelMontoCalculado = new System.Windows.Forms.Label();
            this.radioButtonEfectivo = new System.Windows.Forms.RadioButton();
            this.radioButtonTarjeta = new System.Windows.Forms.RadioButton();
            this.textBoxNumero = new System.Windows.Forms.TextBox();
            this.labelNumero = new System.Windows.Forms.Label();
            this.labelBanco = new System.Windows.Forms.Label();
            this.textBoxBanco = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelComisiones
            // 
            this.labelComisiones.AutoSize = true;
            this.labelComisiones.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComisiones.Location = new System.Drawing.Point(34, 67);
            this.labelComisiones.Name = "labelComisiones";
            this.labelComisiones.Size = new System.Drawing.Size(216, 17);
            this.labelComisiones.TabIndex = 0;
            this.labelComisiones.Text = "Comisiones por ventas a facturar";
            // 
            // dropDownFacturar
            // 
            this.dropDownFacturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.dropDownFacturar.Location = new System.Drawing.Point(261, 65);
            this.dropDownFacturar.Name = "dropDownFacturar";
            this.dropDownFacturar.ReadOnly = true;
            this.dropDownFacturar.Size = new System.Drawing.Size(55, 23);
            this.dropDownFacturar.TabIndex = 1;
            this.dropDownFacturar.Text = "0";
            this.dropDownFacturar.SelectedItemChanged += new System.EventHandler(this.dropDownFacturar_SelectedItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 269);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "*Recuerde que si acumula 10 ventas sin facturar, será deshabilitado. ";
            // 
            // botonFacturar
            // 
            this.botonFacturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonFacturar.Location = new System.Drawing.Point(238, 219);
            this.botonFacturar.Name = "botonFacturar";
            this.botonFacturar.Size = new System.Drawing.Size(111, 37);
            this.botonFacturar.TabIndex = 3;
            this.botonFacturar.Text = "Facturar";
            this.botonFacturar.UseVisualStyleBackColor = true;
            this.botonFacturar.Click += new System.EventHandler(this.botonFacturar_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(193, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "< Volver al menu principal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMin.Location = new System.Drawing.Point(9, 95);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(567, 13);
            this.labelMin.TabIndex = 5;
            this.labelMin.Text = "La cantidad minima de ventas a facturar corresponden a las ventas sin facturar de" +
                " aquellas publicaciones ya vencidas.";
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(9, 108);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(496, 13);
            this.labelMax.TabIndex = 6;
            this.labelMax.Text = "La cantidad maxima de ventas a facturar corresponden a la cantidad total de venta" +
                "s aún no facturadas.";
            // 
            // labelCostos
            // 
            this.labelCostos.AutoSize = true;
            this.labelCostos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCostos.Location = new System.Drawing.Point(34, 18);
            this.labelCostos.Name = "labelCostos";
            this.labelCostos.Size = new System.Drawing.Size(215, 17);
            this.labelCostos.TabIndex = 7;
            this.labelCostos.Text = "Costos de publicacion a facturar:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(456, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "La cantidad de costos de publicacion a facturar corresponden a las publicaciones " +
                "ya vencidas.";
            // 
            // labelCantidadCostos
            // 
            this.labelCantidadCostos.AutoSize = true;
            this.labelCantidadCostos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCantidadCostos.Location = new System.Drawing.Point(246, 19);
            this.labelCantidadCostos.Name = "labelCantidadCostos";
            this.labelCantidadCostos.Size = new System.Drawing.Size(16, 17);
            this.labelCantidadCostos.TabIndex = 9;
            this.labelCantidadCostos.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(322, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Min:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(321, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Max:";
            // 
            // labelMinimo
            // 
            this.labelMinimo.AutoSize = true;
            this.labelMinimo.Location = new System.Drawing.Point(346, 76);
            this.labelMinimo.Name = "labelMinimo";
            this.labelMinimo.Size = new System.Drawing.Size(13, 13);
            this.labelMinimo.TabIndex = 12;
            this.labelMinimo.Text = "0";
            // 
            // labelMaximo
            // 
            this.labelMaximo.AutoSize = true;
            this.labelMaximo.Location = new System.Drawing.Point(347, 63);
            this.labelMaximo.Name = "labelMaximo";
            this.labelMaximo.Size = new System.Drawing.Size(13, 13);
            this.labelMaximo.TabIndex = 13;
            this.labelMaximo.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(253, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Forma de pago";
            // 
            // labelMonto
            // 
            this.labelMonto.AutoSize = true;
            this.labelMonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMonto.Location = new System.Drawing.Point(54, 157);
            this.labelMonto.Name = "labelMonto";
            this.labelMonto.Size = new System.Drawing.Size(116, 20);
            this.labelMonto.TabIndex = 15;
            this.labelMonto.Text = "Monto a pagar:";
            // 
            // labelMontoCalculado
            // 
            this.labelMontoCalculado.AutoSize = true;
            this.labelMontoCalculado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMontoCalculado.Location = new System.Drawing.Point(163, 158);
            this.labelMontoCalculado.Name = "labelMontoCalculado";
            this.labelMontoCalculado.Size = new System.Drawing.Size(18, 20);
            this.labelMontoCalculado.TabIndex = 16;
            this.labelMontoCalculado.Text = "0";
            // 
            // radioButtonEfectivo
            // 
            this.radioButtonEfectivo.AutoSize = true;
            this.radioButtonEfectivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonEfectivo.Location = new System.Drawing.Point(269, 158);
            this.radioButtonEfectivo.Name = "radioButtonEfectivo";
            this.radioButtonEfectivo.Size = new System.Drawing.Size(67, 19);
            this.radioButtonEfectivo.TabIndex = 17;
            this.radioButtonEfectivo.TabStop = true;
            this.radioButtonEfectivo.Text = "Efectivo";
            this.radioButtonEfectivo.UseVisualStyleBackColor = true;
            this.radioButtonEfectivo.CheckedChanged += new System.EventHandler(this.radioButtonEfectivo_CheckedChanged);
            // 
            // radioButtonTarjeta
            // 
            this.radioButtonTarjeta.AutoSize = true;
            this.radioButtonTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonTarjeta.Location = new System.Drawing.Point(268, 179);
            this.radioButtonTarjeta.Name = "radioButtonTarjeta";
            this.radioButtonTarjeta.Size = new System.Drawing.Size(120, 19);
            this.radioButtonTarjeta.TabIndex = 18;
            this.radioButtonTarjeta.TabStop = true;
            this.radioButtonTarjeta.Text = "Tarjeta de credito";
            this.radioButtonTarjeta.UseVisualStyleBackColor = true;
            this.radioButtonTarjeta.CheckedChanged += new System.EventHandler(this.radioButtonTarjeta_CheckedChanged);
            // 
            // textBoxNumero
            // 
            this.textBoxNumero.Location = new System.Drawing.Point(444, 179);
            this.textBoxNumero.Name = "textBoxNumero";
            this.textBoxNumero.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumero.TabIndex = 19;
            // 
            // labelNumero
            // 
            this.labelNumero.AutoSize = true;
            this.labelNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumero.Location = new System.Drawing.Point(396, 181);
            this.labelNumero.Name = "labelNumero";
            this.labelNumero.Size = new System.Drawing.Size(52, 15);
            this.labelNumero.TabIndex = 20;
            this.labelNumero.Text = "Numero";
            // 
            // labelBanco
            // 
            this.labelBanco.AutoSize = true;
            this.labelBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBanco.Location = new System.Drawing.Point(399, 203);
            this.labelBanco.Name = "labelBanco";
            this.labelBanco.Size = new System.Drawing.Size(42, 15);
            this.labelBanco.TabIndex = 21;
            this.labelBanco.Text = "Banco";
            // 
            // textBoxBanco
            // 
            this.textBoxBanco.Location = new System.Drawing.Point(445, 202);
            this.textBoxBanco.Name = "textBoxBanco";
            this.textBoxBanco.Size = new System.Drawing.Size(100, 20);
            this.textBoxBanco.TabIndex = 22;
            // 
            // Facturar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 348);
            this.Controls.Add(this.textBoxBanco);
            this.Controls.Add(this.labelBanco);
            this.Controls.Add(this.labelNumero);
            this.Controls.Add(this.textBoxNumero);
            this.Controls.Add(this.radioButtonTarjeta);
            this.Controls.Add(this.radioButtonEfectivo);
            this.Controls.Add(this.labelMontoCalculado);
            this.Controls.Add(this.labelMonto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelMaximo);
            this.Controls.Add(this.labelMinimo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelCantidadCostos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelCostos);
            this.Controls.Add(this.labelMax);
            this.Controls.Add(this.labelMin);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.botonFacturar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dropDownFacturar);
            this.Controls.Add(this.labelComisiones);
            this.Name = "Facturar";
            this.Text = "Facturar Publicaciones";
            this.Load += new System.EventHandler(this.Facturar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelComisiones;
        private System.Windows.Forms.DomainUpDown dropDownFacturar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button botonFacturar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.Label labelCostos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelCantidadCostos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelMinimo;
        private System.Windows.Forms.Label labelMaximo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelMonto;
        private System.Windows.Forms.Label labelMontoCalculado;
        private System.Windows.Forms.RadioButton radioButtonEfectivo;
        private System.Windows.Forms.RadioButton radioButtonTarjeta;
        private System.Windows.Forms.TextBox textBoxNumero;
        private System.Windows.Forms.Label labelNumero;
        private System.Windows.Forms.Label labelBanco;
        private System.Windows.Forms.TextBox textBoxBanco;
    }
}