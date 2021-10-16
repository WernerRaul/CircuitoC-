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
    public partial class frmtbl_Publicadores : Form
    {
        String mNombreCongregacion;
        
        
        public frmtbl_Publicadores(String pMensaje)
        {
            InitializeComponent();
            mNombreCongregacion = pMensaje;
        }

        private void btnAñadirPublicador_Click(object sender, EventArgs e)
        {
            
            String mID_Publicador = "(SELECT MAX(ID_Publicador) FROM tbl_PUBLICADORES) + 1";
            String mNombre = txtNombrePublicador.Text;
            String mDireccion = "";
            String mSexo = cmbSexoPublicador.Text;
            String mTelefono = "";
            String mFechaNacimiento = mtxtFechaNacimientoPublicador.Text;
            String mFechaBautismo = mtxtFechaBautismoPublicador.Text;
            String mAnciano = cmbAnciano.Text;
            String mSiervoMinisterial = cmbSiervoMinisterial.Text;
            String mPrecRegular = cmbPrecursorRegular.Text;
            String mObservaciones = txtObservacionesPublicador.Text;
            String mID_Actividad = "(SELECT MAX(ID_Actividad) FROM tbl_ACTIVIDAD) + 1";
            String mID_PublicadorEA = "(SELECT MAX(ID_Publicador) FROM tbl_PUBLICADORES)";

            String ComandoTextoPub = "INSERT INTO tbl_PUBLICADORES (ID_Publicador, Nombre, Direccion, Sexo, Telefono, " +
                                    "FechaNacimiento, FechaBautismo, Anciano, SiervoMinisterial, PrecRegular, ID_Congregacion, Observaciones) " +
                                    "VALUES (" + mID_Publicador + ",'" + mNombre + "','" + mDireccion + "','" + mSexo + "','" + mTelefono +
                                    "','" + mFechaNacimiento + "','" + mFechaBautismo + "','" + mAnciano + "','" + mSiervoMinisterial +
                                    "','" + mPrecRegular + "',(SELECT ID_Congregacion FROM tbl_CONGREGACIONES WHERE Nombre = '" + mNombreCongregacion + "'),'" + mObservaciones + "')";

            String ComandoTextoAct = "INSERT INTO tbl_ACTIVIDAD (ID_Actividad, Horas, Revisitas, Estudios, PAuxiliar, " +
                                    "Observaciones, AñoMes, ID_Publicador) VALUES (" + mID_Actividad + ",'','','','','',''," + mID_PublicadorEA + ")";

            using (SQLiteConnection myconnection1 = new SQLiteConnection("Data Source=C:\\Users\\MARCOS LUQUE\\Downloads\\DATOS\\DATOS;Version=3"))
            {
                myconnection1.Open();
                using (SQLiteCommand cmd1 = new SQLiteCommand(ComandoTextoPub,myconnection1))
                {
                    cmd1.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd1 = new SQLiteCommand(ComandoTextoAct, myconnection1))
                {
                    cmd1.ExecuteNonQuery();
                }
            }
            
            this.Close();
        }
    }
}
