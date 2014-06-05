namespace FrbaCommerce.ABM_Rol
{
    partial class BajaRol
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
     
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.botonDeshabilitar = new System.Windows.Forms.Button();
            this.comboBoxRol = new System.Windows.Forms.ComboBox();
            this.labelRol = new System.Windows.Forms.Label();
            this.botonVolver = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // botonDeshabilitar
            // 
            this.botonDeshabilitar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonDeshabilitar.Location = new System.Drawing.Point(28, 134);
            this.botonDeshabilitar.Name = "botonDeshabilitar";
            this.botonDeshabilitar.Size = new System.Drawing.Size(106, 35);
            this.botonDeshabilitar.TabIndex = 1;
            this.botonDeshabilitar.Text = "Deshabilitar";
            this.botonDeshabilitar.UseVisualStyleBackColor = true;
            this.botonDeshabilitar.Click += new System.EventHandler(this.botonDeshabilitar_Click);
            // 
            // comboBoxRol
            // 
            this.comboBoxRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxRol.FormattingEnabled = true;
            this.comboBoxRol.Location = new System.Drawing.Point(154, 84);
            this.comboBoxRol.Name = "comboBoxRol";
            this.comboBoxRol.Size = new System.Drawing.Size(140, 23);
            this.comboBoxRol.TabIndex = 2;
            this.comboBoxRol.SelectedIndexChanged += new System.EventHandler(this.comboBoxRol_SelectedIndexChanged);
            // 
            // labelRol
            // 
            this.labelRol.AutoSize = true;
            this.labelRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRol.Location = new System.Drawing.Point(101, 84);
            this.labelRol.Name = "labelRol";
            this.labelRol.Size = new System.Drawing.Size(33, 20);
            this.labelRol.TabIndex = 3;
            this.labelRol.Text = "Rol";
            this.labelRol.Click += new System.EventHandler(this.labelRol_Click);
            // 
            // botonVolver
            // 
            this.botonVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonVolver.Location = new System.Drawing.Point(13, 228);
            this.botonVolver.Name = "botonVolver";
            this.botonVolver.Size = new System.Drawing.Size(141, 23);
            this.botonVolver.TabIndex = 4;
            this.botonVolver.Text = "< Volver a menu de rol";
            this.botonVolver.UseVisualStyleBackColor = true;
            this.botonVolver.Click += new System.EventHandler(this.botonVolver_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(154, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 35);
            this.button1.TabIndex = 5;
            this.button1.Text = "Crear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(256, 134);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 35);
            this.button2.TabIndex = 6;
            this.button2.Text = "Editar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BajaRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 285);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.botonVolver);
            this.Controls.Add(this.labelRol);
            this.Controls.Add(this.comboBoxRol);
            this.Controls.Add(this.botonDeshabilitar);
            this.Name = "BajaRol";
            this.Text = "BajaRol";
            this.Load += new System.EventHandler(this.BajaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button botonDeshabilitar;
        private System.Windows.Forms.ComboBox comboBoxRol;
        private System.Windows.Forms.Label labelRol;
        private System.Windows.Forms.Button botonVolver;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}