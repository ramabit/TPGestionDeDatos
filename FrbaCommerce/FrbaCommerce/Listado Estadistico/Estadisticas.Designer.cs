namespace FrbaCommerce.Listado_Estadistico
{
    partial class Estadisticas
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
            this.button_Buscar = new System.Windows.Forms.Button();
            this.button_Limpiar = new System.Windows.Forms.Button();
            this.button_Cancelar = new System.Windows.Forms.Button();
            this.dataGridView_Estadistica = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Trimestre = new System.Windows.Forms.ComboBox();
            this.comboBox_TipoDeListado = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Anio = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Estadistica)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_Anio);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox_TipoDeListado);
            this.groupBox1.Controls.Add(this.comboBox_Trimestre);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro de busquedad";
            // 
            // button_Buscar
            // 
            this.button_Buscar.Enabled = false;
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
            // dataGridView_Estadistica
            // 
            this.dataGridView_Estadistica.AllowUserToAddRows = false;
            this.dataGridView_Estadistica.AllowUserToDeleteRows = false;
            this.dataGridView_Estadistica.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_Estadistica.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Estadistica.Location = new System.Drawing.Point(12, 155);
            this.dataGridView_Estadistica.Name = "dataGridView_Estadistica";
            this.dataGridView_Estadistica.ReadOnly = true;
            this.dataGridView_Estadistica.Size = new System.Drawing.Size(1068, 199);
            this.dataGridView_Estadistica.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Año";
            // 
            // comboBox_Trimestre
            // 
            this.comboBox_Trimestre.Enabled = false;
            this.comboBox_Trimestre.FormattingEnabled = true;
            this.comboBox_Trimestre.Location = new System.Drawing.Point(102, 40);
            this.comboBox_Trimestre.Name = "comboBox_Trimestre";
            this.comboBox_Trimestre.Size = new System.Drawing.Size(260, 21);
            this.comboBox_Trimestre.TabIndex = 2;
            this.comboBox_Trimestre.SelectedIndex = -1;
            this.comboBox_Trimestre.SelectedIndexChanged += new System.EventHandler(this.comboBox_Trimestre_SelectedIndexChanged);
            // 
            // comboBox_TipoDeListado
            // 
            this.comboBox_TipoDeListado.Enabled = false;
            this.comboBox_TipoDeListado.FormattingEnabled = true;
            this.comboBox_TipoDeListado.Location = new System.Drawing.Point(102, 67);
            this.comboBox_TipoDeListado.Name = "comboBox_TipoDeListado";
            this.comboBox_TipoDeListado.Size = new System.Drawing.Size(260, 21);
            this.comboBox_TipoDeListado.TabIndex = 3;
            this.comboBox_TipoDeListado.SelectedIndex = -1;
            this.comboBox_TipoDeListado.SelectedIndexChanged += new System.EventHandler(this.comboBox_TipoDeListado_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Trimestre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tipo de listado";
            // 
            // textBox_Anio
            // 
            this.textBox_Anio.Location = new System.Drawing.Point(102, 13);
            this.textBox_Anio.Name = "textBox_Anio";
            this.textBox_Anio.Size = new System.Drawing.Size(260, 20);
            this.textBox_Anio.TabIndex = 6;
            this.textBox_Anio.TextChanged += new System.EventHandler(this.textBox_Anio_TextChanged);
            // 
            // Estadisticas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 366);
            this.Controls.Add(this.dataGridView_Estadistica);
            this.Controls.Add(this.button_Cancelar);
            this.Controls.Add(this.button_Limpiar);
            this.Controls.Add(this.button_Buscar);
            this.Controls.Add(this.groupBox1);
            this.Name = "Estadisticas";
            this.Text = "Listado estadistico";
            this.Load += new System.EventHandler(this.Estadisticas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Estadistica)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Buscar;
        private System.Windows.Forms.Button button_Limpiar;
        private System.Windows.Forms.Button button_Cancelar;
        private System.Windows.Forms.DataGridView dataGridView_Estadistica;
        private System.Windows.Forms.TextBox textBox_Anio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_TipoDeListado;
        private System.Windows.Forms.ComboBox comboBox_Trimestre;
        private System.Windows.Forms.Label label1;
    }
}