namespace ConexionSQL
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.NombreInstancia = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ListHC = new System.Windows.Forms.CheckedListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // NombreInstancia
            // 
            this.NombreInstancia.Location = new System.Drawing.Point(127, 349);
            this.NombreInstancia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NombreInstancia.Name = "NombreInstancia";
            this.NombreInstancia.Size = new System.Drawing.Size(148, 26);
            this.NombreInstancia.TabIndex = 0;
            this.NombreInstancia.Text = "LOCALHOST";
            this.NombreInstancia.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(163, 385);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ejecutar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 349);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Instancia";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ListHC
            // 
            this.ListHC.BackColor = System.Drawing.Color.Lavender;
            this.ListHC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListHC.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListHC.FormattingEnabled = true;
            this.ListHC.Items.AddRange(new object[] {
            "Propiedades Instancia",
            "PropiedadesServidor",
            "servicesInfo",
            "contadores",
            "infoHardware",
            "propiedadesBD",
            "AutoCrecimiento",
            "UsoCPUBD",
            "UsoDiscoBD",
            "estadisticasBD",
            "waits",
            "sysadmin"});
            this.ListHC.Location = new System.Drawing.Point(-1, -1);
            this.ListHC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ListHC.Name = "ListHC";
            this.ListHC.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ListHC.Size = new System.Drawing.Size(290, 300);
            this.ListHC.TabIndex = 7;
            this.ListHC.SelectedIndexChanged += new System.EventHandler(this.ListHC_SelectedIndexChanged);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Items.AddRange(new object[] {
            "Autenticacion Windows",
            "Autenticacion SQL"});
            this.listBox1.Location = new System.Drawing.Point(-1, 309);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(290, 24);
            this.listBox1.TabIndex = 11;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AccessibleName = "hhh";
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(861, 438);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.ListHC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.NombreInstancia);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "https://henrytroncosovalencia.com/";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NombreInstancia;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox ListHC;
        private System.Windows.Forms.ListBox listBox1;
    }
}

