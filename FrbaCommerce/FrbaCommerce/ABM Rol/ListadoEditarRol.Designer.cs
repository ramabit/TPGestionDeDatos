namespace FrbaCommerce.ABM_Rol
{
    partial class ListadoEditarRol
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
            this.dataGridViewResultadosBusqueda = new System.Windows.Forms.DataGridView();
            this.comboBoxEstadoRoles = new System.Windows.Forms.ComboBox();
            this.labelEstadoRoles = new System.Windows.Forms.Label();
            this.botonCancelar = new System.Windows.Forms.Button();
            this.botonBuscar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResultadosBusqueda)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewResultadosBusqueda
            // 
            this.dataGridViewResultadosBusqueda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResultadosBusqueda.Location = new System.Drawing.Point(9, 108);
            this.dataGridViewResultadosBusqueda.Name = "dataGridViewResultadosBusqueda";
            this.dataGridViewResultadosBusqueda.ReadOnly = true;
            this.dataGridViewResultadosBusqueda.Size = new System.Drawing.Size(394, 235);
            this.dataGridViewResultadosBusqueda.TabIndex = 0;
            // 
            // comboBoxEstadoRoles
            // 
            this.comboBoxEstadoRoles.FormattingEnabled = true;
            this.comboBoxEstadoRoles.Location = new System.Drawing.Point(143, 21);
            this.comboBoxEstadoRoles.Name = "comboBoxEstadoRoles";
            this.comboBoxEstadoRoles.Size = new System.Drawing.Size(142, 21);
            this.comboBoxEstadoRoles.TabIndex = 1;
            this.comboBoxEstadoRoles.SelectedIndexChanged += new System.EventHandler(this.comboBoxEstadoRoles_SelectedIndexChanged);
            // 
            // labelEstadoRoles
            // 
            this.labelEstadoRoles.AutoSize = true;
            this.labelEstadoRoles.Location = new System.Drawing.Point(22, 21);
            this.labelEstadoRoles.Name = "labelEstadoRoles";
            this.labelEstadoRoles.Size = new System.Drawing.Size(101, 13);
            this.labelEstadoRoles.TabIndex = 2;
            this.labelEstadoRoles.Text = "Estado de los Roles";
            // 
            // botonCancelar
            // 
            this.botonCancelar.Location = new System.Drawing.Point(23, 76);
            this.botonCancelar.Name = "botonCancelar";
            this.botonCancelar.Size = new System.Drawing.Size(73, 23);
            this.botonCancelar.TabIndex = 3;
            this.botonCancelar.Text = "Cancelar";
            this.botonCancelar.UseVisualStyleBackColor = true;
            this.botonCancelar.Click += new System.EventHandler(this.botonCancelar_Click);
            // 
            // botonBuscar
            // 
            this.botonBuscar.Location = new System.Drawing.Point(191, 77);
            this.botonBuscar.Name = "botonBuscar";
            this.botonBuscar.Size = new System.Drawing.Size(94, 21);
            this.botonBuscar.TabIndex = 4;
            this.botonBuscar.Text = "Buscar";
            this.botonBuscar.UseVisualStyleBackColor = true;
            this.botonBuscar.Click += new System.EventHandler(this.botonBuscar_Click);
            // 
            // ListadoEditarRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 348);
            this.Controls.Add(this.botonBuscar);
            this.Controls.Add(this.botonCancelar);
            this.Controls.Add(this.labelEstadoRoles);
            this.Controls.Add(this.comboBoxEstadoRoles);
            this.Controls.Add(this.dataGridViewResultadosBusqueda);
            this.Name = "ListadoEditarRol";
            this.Text = "ListadoEditarRol";
            this.Load += new System.EventHandler(this.ListadoEditarRol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResultadosBusqueda)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewResultadosBusqueda;
        private System.Windows.Forms.ComboBox comboBoxEstadoRoles;
        private System.Windows.Forms.Label labelEstadoRoles;
        private System.Windows.Forms.Button botonCancelar;
        private System.Windows.Forms.Button botonBuscar;
    }
}