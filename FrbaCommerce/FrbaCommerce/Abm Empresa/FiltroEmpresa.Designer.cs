namespace FrbaCommerce.ABM_Empresa
{
    partial class FiltroEmpresa
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Mail = new System.Windows.Forms.TextBox();
            this.textBox_Cuit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_RazonSocial = new System.Windows.Forms.TextBox();
            this.button_Buscar = new System.Windows.Forms.Button();
            this.button_Limpiar = new System.Windows.Forms.Button();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.dataGridView_Empresa = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Empresa)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_Mail);
            this.groupBox1.Controls.Add(this.textBox_Cuit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_RazonSocial);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro de busquedad";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Mail";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "CUIT";
            // 
            // textBox_Mail
            // 
            this.textBox_Mail.Location = new System.Drawing.Point(77, 71);
            this.textBox_Mail.Name = "textBox_Mail";
            this.textBox_Mail.Size = new System.Drawing.Size(285, 20);
            this.textBox_Mail.TabIndex = 4;
            // 
            // textBox_Cuit
            // 
            this.textBox_Cuit.Location = new System.Drawing.Point(77, 45);
            this.textBox_Cuit.Name = "textBox_Cuit";
            this.textBox_Cuit.Size = new System.Drawing.Size(285, 20);
            this.textBox_Cuit.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Razon Social";
            // 
            // textBox_RazonSocial
            // 
            this.textBox_RazonSocial.Location = new System.Drawing.Point(77, 19);
            this.textBox_RazonSocial.Name = "textBox_RazonSocial";
            this.textBox_RazonSocial.Size = new System.Drawing.Size(285, 20);
            this.textBox_RazonSocial.TabIndex = 0;
            // 
            // button_Buscar
            // 
            this.button_Buscar.Location = new System.Drawing.Point(280, 119);
            this.button_Buscar.Name = "button_Buscar";
            this.button_Buscar.Size = new System.Drawing.Size(100, 30);
            this.button_Buscar.TabIndex = 1;
            this.button_Buscar.Text = "Buscar";
            this.button_Buscar.UseVisualStyleBackColor = true;
            this.button_Buscar.Click += new System.EventHandler(this.button_Buscar_Click);
            // 
            // button_Limpiar
            // 
            this.button_Limpiar.Location = new System.Drawing.Point(175, 119);
            this.button_Limpiar.Name = "button_Limpiar";
            this.button_Limpiar.Size = new System.Drawing.Size(100, 30);
            this.button_Limpiar.TabIndex = 2;
            this.button_Limpiar.Text = "Limpiar";
            this.button_Limpiar.UseVisualStyleBackColor = true;
            this.button_Limpiar.Click += new System.EventHandler(this.button_Limpiar_Click);
            // 
            // button_Cancelar
            // 
            this.button_Cancelar.Location = new System.Drawing.Point(12, 119);
            this.button_Cancelar.Name = "button_Cancelar";
            this.button_Cancelar.Size = new System.Drawing.Size(100, 30);
            this.button_Cancelar.TabIndex = 3;
            this.button_Cancelar.Text = "Cancelar";
            this.button_Cancelar.UseVisualStyleBackColor = true;
            this.button_Cancelar.Click += new System.EventHandler(this.button_Cancelar_Click);
            // 
            // dataGridView_Empresa
            // 
            this.dataGridView_Empresa.AllowUserToAddRows = false;
            this.dataGridView_Empresa.AllowUserToDeleteRows = false;
            this.dataGridView_Empresa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_Empresa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Empresa.Location = new System.Drawing.Point(12, 155);
            this.dataGridView_Empresa.Name = "dataGridView_Empresa";
            this.dataGridView_Empresa.ReadOnly = true;
            this.dataGridView_Empresa.Size = new System.Drawing.Size(1068, 199);
            this.dataGridView_Empresa.TabIndex = 4;
            // 
            // FiltroEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 366);
            this.Controls.Add(this.dataGridView_Empresa);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.button_Limpiar);
            this.Controls.Add(this.button_Buscar);
            this.Controls.Add(this.groupBox1);
            this.Name = "FiltroEmpresa";
            this.Text = "Filtro de empresas";
            this.Load += new System.EventHandler(this.FiltroEmpresa_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Empresa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Mail;
        private System.Windows.Forms.TextBox textBox_Cuit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_RazonSocial;
        private System.Windows.Forms.Button button_Buscar;
        private System.Windows.Forms.Button button_Limpiar;
        private System.Windows.Forms.Button button_Cancelar;
        private System.Windows.Forms.DataGridView dataGridView_Empresa;
    }
}