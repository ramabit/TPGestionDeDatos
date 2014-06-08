namespace FrbaCommerce.ABM_Rol
{
    partial class AgregarRol
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
            this.botonVolver = new System.Windows.Forms.Button();
            this.checkedListBoxFuncionalidades = new System.Windows.Forms.CheckedListBox();
            this.labelRol = new System.Windows.Forms.Label();
            this.textBoxRol = new System.Windows.Forms.TextBox();
            this.labelFuncionalidades = new System.Windows.Forms.Label();
            this.botonGuardar = new System.Windows.Forms.Button();
            this.botonLimpiar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // botonVolver
            // 
            this.botonVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonVolver.Location = new System.Drawing.Point(12, 334);
            this.botonVolver.Name = "botonVolver";
            this.botonVolver.Size = new System.Drawing.Size(141, 23);
            this.botonVolver.TabIndex = 0;
            this.botonVolver.Text = "< Volver a menu de rol";
            this.botonVolver.UseVisualStyleBackColor = true;
            this.botonVolver.Click += new System.EventHandler(this.botonVolver_Click);
            // 
            // checkedListBoxFuncionalidades
            // 
            this.checkedListBoxFuncionalidades.FormattingEnabled = true;
            this.checkedListBoxFuncionalidades.Location = new System.Drawing.Point(132, 77);
            this.checkedListBoxFuncionalidades.Name = "checkedListBox1";
            this.checkedListBoxFuncionalidades.Size = new System.Drawing.Size(193, 199);
            this.checkedListBoxFuncionalidades.TabIndex = 1;
            this.checkedListBoxFuncionalidades.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxFuncionalidades_SelectedIndexChanged);
            // 
            // labelRol
            // 
            this.labelRol.AutoSize = true;
            this.labelRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRol.Location = new System.Drawing.Point(80, 28);
            this.labelRol.Name = "label1";
            this.labelRol.Size = new System.Drawing.Size(33, 20);
            this.labelRol.TabIndex = 2;
            this.labelRol.Text = "Rol";
            // 
            // textBox1
            // 
            this.textBoxRol.Location = new System.Drawing.Point(132, 28);
            this.textBoxRol.Name = "textBox1";
            this.textBoxRol.Size = new System.Drawing.Size(94, 20);
            this.textBoxRol.TabIndex = 3;
            // 
            // label2
            // 
            this.labelFuncionalidades.AutoSize = true;
            this.labelFuncionalidades.Location = new System.Drawing.Point(29, 77);
            this.labelFuncionalidades.Name = "label2";
            this.labelFuncionalidades.Size = new System.Drawing.Size(84, 13);
            this.labelFuncionalidades.TabIndex = 4;
            this.labelFuncionalidades.Text = "Funcionalidades";
            // 
            // button2
            // 
            this.botonGuardar.Location = new System.Drawing.Point(229, 334);
            this.botonGuardar.Name = "button2";
            this.botonGuardar.Size = new System.Drawing.Size(96, 30);
            this.botonGuardar.TabIndex = 5;
            this.botonGuardar.Text = "Guardar";
            this.botonGuardar.UseVisualStyleBackColor = true;
            this.botonGuardar.Click += new System.EventHandler(this.botonGuardar_Click);
            // 
            // button3
            // 
            this.botonLimpiar.Location = new System.Drawing.Point(230, 293);
            this.botonLimpiar.Name = "button3";
            this.botonLimpiar.Size = new System.Drawing.Size(95, 26);
            this.botonLimpiar.TabIndex = 6;
            this.botonLimpiar.Text = "Limpiar";
            this.botonLimpiar.UseVisualStyleBackColor = true;
            this.botonLimpiar.Click += new System.EventHandler(this.botonLimpiar_Click);
            // 
            // AgregarRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 376);
            this.Controls.Add(this.botonLimpiar);
            this.Controls.Add(this.botonGuardar);
            this.Controls.Add(this.labelFuncionalidades);
            this.Controls.Add(this.textBoxRol);
            this.Controls.Add(this.labelRol);
            this.Controls.Add(this.checkedListBoxFuncionalidades);
            this.Controls.Add(this.botonVolver);
            this.Name = "AgregarRol";
            this.Text = "AgregarRol";
            this.Load += new System.EventHandler(this.AgregarRol_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button botonVolver;
        private System.Windows.Forms.CheckedListBox checkedListBoxFuncionalidades;
        private System.Windows.Forms.Label labelRol;
        private System.Windows.Forms.TextBox textBoxRol;
        private System.Windows.Forms.Label labelFuncionalidades;
        private System.Windows.Forms.Button botonGuardar;
        private System.Windows.Forms.Button botonLimpiar;
    }
}