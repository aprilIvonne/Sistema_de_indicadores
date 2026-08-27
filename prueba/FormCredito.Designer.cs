
namespace prueba
{
    partial class FormCredito
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
            this.Cargar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Cargar
            // 
            this.Cargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cargar.Location = new System.Drawing.Point(1105, 571);
            this.Cargar.Name = "Cargar";
            this.Cargar.Size = new System.Drawing.Size(105, 50);
            this.Cargar.TabIndex = 9;
            this.Cargar.Text = "cargar formulario";
            this.Cargar.UseVisualStyleBackColor = true;
            this.Cargar.Click += new System.EventHandler(this.Cargar_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(2, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1231, 551);
            this.panel1.TabIndex = 7;
            // 
            // FormCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 626);
            this.Controls.Add(this.Cargar);
            this.Controls.Add(this.panel1);
            this.Name = "FormCredito";
            this.Text = "Formulario del Área de Crédito";
            this.Load += new System.EventHandler(this.FormCredito_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Cargar;
        private System.Windows.Forms.Panel panel1;
    }
}