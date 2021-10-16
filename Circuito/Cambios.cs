using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circuito
{
    public partial class Cambios : Form
    {
        String mNombrePublicador;

        public Cambios(String pMensaje)
        {
            InitializeComponent();
            mNombrePublicador = pMensaje;

            groupBox1.Text = mNombrePublicador;
            //llenar combo con nombre de las congregaciones
            String ComandoTextoCong = "SELECT * FROM tbl_CONGREGACIONES ORDER BY Nombre ASC";
            String ComandoTextoPub = "SELECT * FROM tbl_PUBLICADORES WHERE Nombre = '" + mNombrePublicador + "'";
            using (SQLiteConnection myconnection2 = new SQLiteConnection("Data Source=C:\\Users\\MARCOS LUQUE\\Downloads\\DATOS\\DATOS;Version=3"))
            {
                myconnection2.Open();
                using (SQLiteCommand cmd2 = new SQLiteCommand(ComandoTextoCong, myconnection2))
                {
                    SQLiteDataReader sdr = cmd2.ExecuteReader();

                    while (sdr.Read())
                    {
                        cmbCongregaciones.Items.Add(sdr["Nombre"].ToString());
                    }
                    sdr.Close();
                }

            }

        }

        private void rBtnSeMudo_a_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtnSeMudo_a.Checked)
            {
                cmbCongregaciones.Enabled = true;
            }
            else
            {
                cmbCongregaciones.Enabled = false;
            }
        }

        private void btnAceptarCambios_Click(object sender, EventArgs e)
        {
            //String mPragma = "PRAGMA foreign_keys = ON;";
            String ComandoTextoBorrar = "PRAGMA foreign_keys = ON; DELETE FROM tbl_PUBLICADORES WHERE Nombre = '" + mNombrePublicador + "'";
            String ComandoTextoInactivo = "UPDATE tbl_PUBLICADORES SET Observaciones = 'INACTIVO' WHERE Nombre = '" + mNombrePublicador + "'";
            String ComandoTextoSeMudo = "UPDATE tbl_PUBLICADORES SET ID_Congregacion = (SELECT ID_Congregacion FROM tbl_CONGREGACIONES WHERE Nombre = '" + cmbCongregaciones.Text + "') WHERE Nombre = '" + mNombrePublicador + "'";

            using (SQLiteConnection myconnection1 = new SQLiteConnection("Data Source=C:\\Users\\MARCOS LUQUE\\Downloads\\DATOS\\DATOS;Version=3"))
            {
                DialogResult dialogResult = MessageBox.Show("De verdad quiere hacer cambios de este tipo en la BD???", "ADVERTENCIA!!!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    myconnection1.Open();
                    if (rBtnSalioDelCirc.Checked == true)
                        using (SQLiteCommand cmd1 = new SQLiteCommand(ComandoTextoBorrar, myconnection1))
                        {
                            cmd1.ExecuteNonQuery();
                        }
                    if (rBtnInactivo.Checked == true)
                        using (SQLiteCommand cmd1 = new SQLiteCommand(ComandoTextoInactivo, myconnection1))
                        {
                            cmd1.ExecuteNonQuery();
                        }
                    if (rBtnSeMudo_a.Checked == true)
                        using (SQLiteCommand cmd1 = new SQLiteCommand(ComandoTextoSeMudo, myconnection1))
                        {
                            cmd1.ExecuteNonQuery();
                        };
                }
            }

            this.Close();
        }

    }
}
