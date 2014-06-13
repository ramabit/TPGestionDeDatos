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
            this.labelCantCompras = new System.Windows.Forms.Label();
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.botonFacturar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labelMin = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelCantCompras
            // 
            this.labelCantCompras.AutoSize = true;
            this.labelCantCompras.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCantCompras.Location = new System.Drawing.Point(34, 43);
            this.labelCantCompras.Name = "labelCantCompras";
            this.labelCantCompras.Size = new System.Drawing.Size(195, 17);
            this.labelCantCompras.TabIndex = 0;
            this.labelCantCompras.Text = "Cantidad de ventas a facturar";
            // 
            // domainUpDown1
            // 
            this.domainUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.domainUpDown1.Location = new System.Drawing.Point(245, 41);
            this.domainUpDown1.Name = "domainUpDown1";
            this.domainUpDown1.Size = new System.Drawing.Size(120, 23);
            this.domainUpDown1.TabIndex = 1;
            this.domainUpDown1.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "*Recuerde que si acumula 10 compras sin facturar, será deshabilitado ";
            // 
            // botonFacturar
            // 
            this.botonFacturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonFacturar.Location = new System.Drawing.Point(185, 128);
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
            // 
            // labelMin
            // 
            this.labelMin.AutoSize = true;
            this.labelMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMin.Location = new System.Drawing.Point(22, 67);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(443, 13);
            this.labelMin.TabIndex = 5;
            this.labelMin.Text = "La cantidad minima de ventas a facturar corresponden a aquellas publicaciones ya " +
                "vencidas";
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.Location = new System.Drawing.Point(22, 80);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(478, 13);
            this.labelMax.TabIndex = 6;
            this.labelMax.Text = "La cantidad maxima de compras a facturar corresponden a la cantidad de ventas aún" +
                " no facturadas";
            // 
            // Facturar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 269);
            this.Controls.Add(this.labelMax);
            this.Controls.Add(this.labelMin);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.botonFacturar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.domainUpDown1);
            this.Controls.Add(this.labelCantCompras);
            this.Name = "Facturar";
            this.Text = "Facturar Publicaciones";
            this.Load += new System.EventHandler(this.Facturar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCantCompras;
        private System.Windows.Forms.DomainUpDown domainUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button botonFacturar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelMax;
    }
}