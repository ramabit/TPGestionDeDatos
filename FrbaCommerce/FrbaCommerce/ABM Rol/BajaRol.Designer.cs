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
            this.SuspendLayout();
            // 
            // botonDeshabilitar
            // 
            this.botonDeshabilitar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonDeshabilitar.Location = new System.Drawing.Point(91, 135);
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
            this.comboBoxRol.Location = new System.Drawing.Point(110, 83);
            this.comboBoxRol.Name = "comboBoxRol";
            this.comboBoxRol.Size = new System.Drawing.Size(140, 23);
            this.comboBoxRol.TabIndex = 2;
            this.comboBoxRol.SelectedIndexChanged += new System.EventHandler(this.comboBoxRol_SelectedIndexChanged);
            // 
            // labelRol
            // 
            this.labelRol.AutoSize = true;
            this.labelRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRol.Location = new System.Drawing.Point(44, 84);
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
            // BajaRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 270);
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
    }
}