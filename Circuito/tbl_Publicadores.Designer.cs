
namespace Circuito
{
    partial class frmtbl_Publicadores
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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNombrePublicador = new System.Windows.Forms.TextBox();
            this.txtObservacionesPublicador = new System.Windows.Forms.TextBox();
            this.btnAñadirPublicador = new System.Windows.Forms.Button();
            this.cmbSexoPublicador = new System.Windows.Forms.ComboBox();
            this.cmbAnciano = new System.Windows.Forms.ComboBox();
            this.cmbSiervoMinisterial = new System.Windows.Forms.ComboBox();
            this.cmbPrecursorRegular = new System.Windows.Forms.ComboBox();
            this.mtxtFechaNacimientoPublicador = new System.Windows.Forms.MaskedTextBox();
            this.mtxtFechaBautismoPublicador = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NOMBRE:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(345, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "SEXO:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "FECHA DE NACIMIENTO:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(283, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "FECHA DE BAUTISMO:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "ANCIANO:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(136, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "SIERVO MINISTERIAL:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(323, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "PRECURSOR REGULAR:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 158);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "OBSERVACIONES:";
            // 
            // txtNombrePublicador
            // 
            this.txtNombrePublicador.Location = new System.Drawing.Point(81, 22);
            this.txtNombrePublicador.Name = "txtNombrePublicador";
            this.txtNombrePublicador.Size = new System.Drawing.Size(231, 20);
            this.txtNombrePublicador.TabIndex = 1;
            // 
            // txtObservacionesPublicador
            // 
            this.txtObservacionesPublicador.Location = new System.Drawing.Point(125, 158);
            this.txtObservacionesPublicador.Name = "txtObservacionesPublicador";
            this.txtObservacionesPublicador.Size = new System.Drawing.Size(386, 20);
            this.txtObservacionesPublicador.TabIndex = 8;
            // 
            // btnAñadirPublicador
            // 
            this.btnAñadirPublicador.Location = new System.Drawing.Point(205, 203);
            this.btnAñadirPublicador.Name = "btnAñadirPublicador";
            this.btnAñadirPublicador.Size = new System.Drawing.Size(141, 23);
            this.btnAñadirPublicador.TabIndex = 9;
            this.btnAñadirPublicador.Text = "AÑADIR";
            this.btnAñadirPublicador.UseVisualStyleBackColor = true;
            this.btnAñadirPublicador.Click += new System.EventHandler(this.btnAñadirPublicador_Click);
            // 
            // cmbSexoPublicador
            // 
            this.cmbSexoPublicador.FormattingEnabled = true;
            this.cmbSexoPublicador.Items.AddRange(new object[] {
            "Hombre",
            "Mujer"});
            this.cmbSexoPublicador.Location = new System.Drawing.Point(390, 21);
            this.cmbSexoPublicador.Name = "cmbSexoPublicador";
            this.cmbSexoPublicador.Size = new System.Drawing.Size(121, 21);
            this.cmbSexoPublicador.TabIndex = 2;
            this.cmbSexoPublicador.Text = "Hombre";
            // 
            // cmbAnciano
            // 
            this.cmbAnciano.FormattingEnabled = true;
            this.cmbAnciano.Items.AddRange(new object[] {
            "false",
            "true"});
            this.cmbAnciano.Location = new System.Drawing.Point(81, 104);
            this.cmbAnciano.Name = "cmbAnciano";
            this.cmbAnciano.Size = new System.Drawing.Size(49, 21);
            this.cmbAnciano.TabIndex = 5;
            this.cmbAnciano.Text = "false";
            // 
            // cmbSiervoMinisterial
            // 
            this.cmbSiervoMinisterial.FormattingEnabled = true;
            this.cmbSiervoMinisterial.Items.AddRange(new object[] {
            "false",
            "true"});
            this.cmbSiervoMinisterial.Location = new System.Drawing.Point(263, 105);
            this.cmbSiervoMinisterial.Name = "cmbSiervoMinisterial";
            this.cmbSiervoMinisterial.Size = new System.Drawing.Size(49, 21);
            this.cmbSiervoMinisterial.TabIndex = 6;
            this.cmbSiervoMinisterial.Text = "false";
            // 
            // cmbPrecursorRegular
            // 
            this.cmbPrecursorRegular.FormattingEnabled = true;
            this.cmbPrecursorRegular.Items.AddRange(new object[] {
            "false",
            "true"});
            this.cmbPrecursorRegular.Location = new System.Drawing.Point(462, 105);
            this.cmbPrecursorRegular.Name = "cmbPrecursorRegular";
            this.cmbPrecursorRegular.Size = new System.Drawing.Size(49, 21);
            this.cmbPrecursorRegular.TabIndex = 7;
            this.cmbPrecursorRegular.Text = "false";
            // 
            // mtxtFechaNacimientoPublicador
            // 
            this.mtxtFechaNacimientoPublicador.Location = new System.Drawing.Point(157, 63);
            this.mtxtFechaNacimientoPublicador.Mask = "0000-00-00";
            this.mtxtFechaNacimientoPublicador.Name = "mtxtFechaNacimientoPublicador";
            this.mtxtFechaNacimientoPublicador.Size = new System.Drawing.Size(61, 20);
            this.mtxtFechaNacimientoPublicador.TabIndex = 3;
            // 
            // mtxtFechaBautismoPublicador
            // 
            this.mtxtFechaBautismoPublicador.Location = new System.Drawing.Point(411, 63);
            this.mtxtFechaBautismoPublicador.Mask = "0000-00-00";
            this.mtxtFechaBautismoPublicador.Name = "mtxtFechaBautismoPublicador";
            this.mtxtFechaBautismoPublicador.Size = new System.Drawing.Size(61, 20);
            this.mtxtFechaBautismoPublicador.TabIndex = 4;
            // 
            // frmtbl_Publicadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 263);
            this.Controls.Add(this.mtxtFechaBautismoPublicador);
            this.Controls.Add(this.mtxtFechaNacimientoPublicador);
            this.Controls.Add(this.cmbPrecursorRegular);
            this.Controls.Add(this.cmbSiervoMinisterial);
            this.Controls.Add(this.cmbAnciano);
            this.Controls.Add(this.cmbSexoPublicador);
            this.Controls.Add(this.btnAñadirPublicador);
            this.Controls.Add(this.txtObservacionesPublicador);
            this.Controls.Add(this.txtNombrePublicador);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmtbl_Publicadores";
            this.Text = "Publicador";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNombrePublicador;
        private System.Windows.Forms.TextBox txtObservacionesPublicador;
        private System.Windows.Forms.Button btnAñadirPublicador;
        private System.Windows.Forms.ComboBox cmbSexoPublicador;
        private System.Windows.Forms.ComboBox cmbAnciano;
        private System.Windows.Forms.ComboBox cmbSiervoMinisterial;
        private System.Windows.Forms.ComboBox cmbPrecursorRegular;
        private System.Windows.Forms.MaskedTextBox mtxtFechaNacimientoPublicador;
        private System.Windows.Forms.MaskedTextBox mtxtFechaBautismoPublicador;
    }
}