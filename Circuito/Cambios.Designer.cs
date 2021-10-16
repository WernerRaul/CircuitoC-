
namespace Circuito
{
    partial class Cambios
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
            this.rBtnInactivo = new System.Windows.Forms.RadioButton();
            this.rBtnSalioDelCirc = new System.Windows.Forms.RadioButton();
            this.rBtnSeMudo_a = new System.Windows.Forms.RadioButton();
            this.cmbCongregaciones = new System.Windows.Forms.ComboBox();
            this.btnAceptarCambios = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rBtnInactivo);
            this.groupBox1.Controls.Add(this.rBtnSalioDelCirc);
            this.groupBox1.Controls.Add(this.rBtnSeMudo_a);
            this.groupBox1.Location = new System.Drawing.Point(14, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(111, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cambios";
            // 
            // rBtnInactivo
            // 
            this.rBtnInactivo.AutoSize = true;
            this.rBtnInactivo.Location = new System.Drawing.Point(6, 48);
            this.rBtnInactivo.Name = "rBtnInactivo";
            this.rBtnInactivo.Size = new System.Drawing.Size(63, 17);
            this.rBtnInactivo.TabIndex = 1;
            this.rBtnInactivo.TabStop = true;
            this.rBtnInactivo.Text = "Inactivo";
            this.rBtnInactivo.UseVisualStyleBackColor = true;
            // 
            // rBtnSalioDelCirc
            // 
            this.rBtnSalioDelCirc.AutoSize = true;
            this.rBtnSalioDelCirc.Location = new System.Drawing.Point(6, 24);
            this.rBtnSalioDelCirc.Name = "rBtnSalioDelCirc";
            this.rBtnSalioDelCirc.Size = new System.Drawing.Size(79, 17);
            this.rBtnSalioDelCirc.TabIndex = 0;
            this.rBtnSalioDelCirc.TabStop = true;
            this.rBtnSalioDelCirc.Text = "Sale de BD";
            this.rBtnSalioDelCirc.UseVisualStyleBackColor = true;
            // 
            // rBtnSeMudo_a
            // 
            this.rBtnSeMudo_a.AutoSize = true;
            this.rBtnSeMudo_a.Location = new System.Drawing.Point(6, 71);
            this.rBtnSeMudo_a.Name = "rBtnSeMudo_a";
            this.rBtnSeMudo_a.Size = new System.Drawing.Size(79, 17);
            this.rBtnSeMudo_a.TabIndex = 4;
            this.rBtnSeMudo_a.TabStop = true;
            this.rBtnSeMudo_a.Text = "Se mudó a:";
            this.rBtnSeMudo_a.UseVisualStyleBackColor = true;
            this.rBtnSeMudo_a.CheckedChanged += new System.EventHandler(this.rBtnSeMudo_a_CheckedChanged);
            // 
            // cmbCongregaciones
            // 
            this.cmbCongregaciones.Enabled = false;
            this.cmbCongregaciones.FormattingEnabled = true;
            this.cmbCongregaciones.Location = new System.Drawing.Point(12, 126);
            this.cmbCongregaciones.Name = "cmbCongregaciones";
            this.cmbCongregaciones.Size = new System.Drawing.Size(111, 21);
            this.cmbCongregaciones.TabIndex = 5;
            this.cmbCongregaciones.Text = "Elija congregación:";
            // 
            // btnAceptarCambios
            // 
            this.btnAceptarCambios.Location = new System.Drawing.Point(12, 172);
            this.btnAceptarCambios.Name = "btnAceptarCambios";
            this.btnAceptarCambios.Size = new System.Drawing.Size(111, 33);
            this.btnAceptarCambios.TabIndex = 6;
            this.btnAceptarCambios.Text = "ACTUALIZAR";
            this.btnAceptarCambios.UseVisualStyleBackColor = true;
            this.btnAceptarCambios.Click += new System.EventHandler(this.btnAceptarCambios_Click);
            // 
            // Cambios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(141, 232);
            this.Controls.Add(this.btnAceptarCambios);
            this.Controls.Add(this.cmbCongregaciones);
            this.Controls.Add(this.groupBox1);
            this.Name = "Cambios";
            this.Text = "Cambios";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rBtnInactivo;
        private System.Windows.Forms.RadioButton rBtnSalioDelCirc;
        private System.Windows.Forms.RadioButton rBtnSeMudo_a;
        private System.Windows.Forms.ComboBox cmbCongregaciones;
        private System.Windows.Forms.Button btnAceptarCambios;
    }
}