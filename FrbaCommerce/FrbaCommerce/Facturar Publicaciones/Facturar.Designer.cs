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
            this.labelComisiones.Click += new System.EventHandler(this.labelCantCompras_Click);
            // 
            // dropDownFacturar
            // 
            this.dropDownFacturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropDownFacturar.Location = new System.Drawing.Point(261, 65);
            this.dropDownFacturar.Name = "dropDownFacturar";
            this.dropDownFacturar.Size = new System.Drawing.Size(55, 23);
            this.dropDownFacturar.TabIndex = 1;
            this.dropDownFacturar.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "*Recuerde que si acumula 10 compras sin facturar, será deshabilitado ";
            // 
            // botonFacturar
            // 
            this.botonFacturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonFacturar.Location = new System.Drawing.Point(205, 142);
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
            this.button1.Location = new System.Drawing.Point(12, 226);
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
            this.labelMin.Location = new System.Drawing.Point(22, 95);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(443, 13);
            this.labelMin.TabIndex = 5;
            this.labelMin.Text = "La cantidad minima de ventas a facturar corresponden a aquellas publicaciones ya " +
                "vencidas";
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(22, 108);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(478, 13);
            this.labelMax.TabIndex = 6;
            this.labelMax.Text = "La cantidad maxima de compras a facturar corresponden a la cantidad de ventas aún" +
                " no facturadas";
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
            this.label3.Location = new System.Drawing.Point(22, 35);
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
            // Facturar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 269);
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
    }
}