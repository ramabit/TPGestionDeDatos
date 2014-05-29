namespace FrbaCommerce.ABM_Rol
{
    partial class RolForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.botonBajaRol = new System.Windows.Forms.Button();
            this.botonEditarRol = new System.Windows.Forms.Button();
            this.botonAgregarRol = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(122, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Roles";
            // 
            // botonBajaRol
            // 
            this.botonBajaRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonBajaRol.Location = new System.Drawing.Point(83, 228);
            this.botonBajaRol.Name = "botonBajaRol";
            this.botonBajaRol.Size = new System.Drawing.Size(135, 54);
            this.botonBajaRol.TabIndex = 6;
            this.botonBajaRol.Text = "Eliminar rol";
            this.botonBajaRol.UseVisualStyleBackColor = true;
            this.botonBajaRol.Click += new System.EventHandler(this.botonBajaRol_Click);
            // 
            // botonEditarRol
            // 
            this.botonEditarRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonEditarRol.Location = new System.Drawing.Point(83, 148);
            this.botonEditarRol.Name = "botonEditarRol";
            this.botonEditarRol.Size = new System.Drawing.Size(135, 54);
            this.botonEditarRol.TabIndex = 5;
            this.botonEditarRol.Text = "Editar rol";
            this.botonEditarRol.UseVisualStyleBackColor = true;
            this.botonEditarRol.Click += new System.EventHandler(this.botonEditarRol_Click);
            // 
            // botonAgregarRol
            // 
            this.botonAgregarRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonAgregarRol.Location = new System.Drawing.Point(83, 65);
            this.botonAgregarRol.Name = "botonAgregarRol";
            this.botonAgregarRol.Size = new System.Drawing.Size(135, 54);
            this.botonAgregarRol.TabIndex = 4;
            this.botonAgregarRol.Text = "Agregar rol";
            this.botonAgregarRol.UseVisualStyleBackColor = true;
            this.botonAgregarRol.Click += new System.EventHandler(this.botonAgregarRol_Click);
            // 
            // RolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 311);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.botonBajaRol);
            this.Controls.Add(this.botonEditarRol);
            this.Controls.Add(this.botonAgregarRol);
            this.Name = "RolForm";
            this.Text = "RolForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button botonBajaRol;
        private System.Windows.Forms.Button botonEditarRol;
        private System.Windows.Forms.Button botonAgregarRol;
    }
}