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
using System.IO;
using System.Media;
using System.Windows.Forms.DataVisualization.Charting;

namespace Circuito
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        String nombrePublicador;
        String nombreCongregacion;
        String nombreCongregacionParaConsultas;
        SQLiteConnection myconnection;
        SQLiteCommand cmd;
        String fecha;
        SoundPlayer Simple;
        int mAsistentes;
        int mEstudios;
        

        private void fConexion()
        {
            myconnection = new SQLiteConnection("Data Source=C:\\Users\\MARCOS LUQUE\\Downloads\\DATOS\\DATOS;Version=3"); ///("Data Source=D:\\DATOS;Version=3"); ////
            myconnection.Open();
            cmd = new SQLiteCommand();
            cmd.Connection = myconnection;

        }

        private String ultimafecha(String nCongregacion)
        {
            fConexion();
            //obtenemos la última fecha de actividad de los publicadores
            cmd.CommandText = "SELECT 	tbl_ACTIVIDAD.AñoMes "
                                + "FROM tbl_PUBLICADORES, tbl_CONGREGACIONES, tbl_ACTIVIDAD "
                                + "WHERE tbl_PUBLICADORES.ID_Congregacion = tbl_CONGREGACIONES.ID_Congregacion "
                                + "AND tbl_PUBLICADORES.ID_Publicador = tbl_ACTIVIDAD.ID_Publicador "
                                + "AND tbl_CONGREGACIONES.Nombre = '" + nCongregacion + "' "
                                + "AND tbl_PUBLICADORES.Observaciones != 'INACTIVO' "
                                + "ORDER BY date(tbl_ACTIVIDAD.AñoMes) "
                                + "DESC Limit 1";
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    fecha = sdr["AñoMes"].ToString();
                }
                sdr.Close();
            }
            return fecha;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            fConexion(); ///Abrimos conexion
            cmd.CommandText = "SELECT * FROM tbl_CONGREGACIONES ORDER BY Nombre ASC";

            SQLiteDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
               {
                listView1.Items.Add(sdr["Nombre"].ToString());
                listView3.Items.Add(sdr["Nombre"].ToString());
                listView4.Items.Add(sdr["Nombre"].ToString());
                listView5.Items.Add(sdr["Nombre"].ToString());
               }

            sdr.Close();
//            listView1.Focus();
        }


        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            nombreCongregacion = listView1.SelectedItems[0].SubItems[0].Text;

            fConexion(); ///Abrimos conexion

            cmd.CommandText = "SELECT * FROM tbl_PUBLICADORES WHERE ID_Congregacion = " +
                "(SELECT ID_Congregacion FROM tbl_CONGREGACIONES WHERE Nombre = '" + nombreCongregacion + "') ORDER BY Nombre ASC";

            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                listView2.Clear();
                while (sdr.Read())
                {
                    listView2.Items.Add(sdr["Nombre"].ToString());
                }

                sdr.Close();


            }

            myconnection.Close();
            //servicio de añadir publicador activado
            buttonAñadirPublicador.Enabled = true;
            //servicio de busqueda de no informados activado
            listBox1.Enabled = true;
            button1.Enabled = true;
            maskedTextBox1.Enabled = true;
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                nombrePublicador = listView2.SelectedItems[0].SubItems[0].Text;

            }
            catch
            {

            }
            fConexion(); ///Abrimos conexion
            cmd.CommandText = "SELECT * FROM tbl_ACTIVIDAD WHERE ID_Publicador = (SELECT ID_Publicador FROM tbl_PUBLICADORES WHERE Nombre = '" + nombrePublicador + "')";
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                //Crear nueva tabla
                DataTable dt = new DataTable();
                //Cargar DataReader dentro de DataTable
                dt.Load(sdr);
                dataGridView1.DataSource = dt;
            }
            
            txtANombre.Enabled = true;
            mtxtAFechaNacimiento.Enabled = true;
            mtxtAFechaBautismo.Enabled = true;
            txtAObservaciones.Enabled = true;
            cmbAAnciano.Enabled = true;
            cmbASiervoMinisterial.Enabled = true;
            cmbAPrecursorRegular.Enabled = true;
            btnAActualizar.Enabled = true;

            //Llenamos las cajas de actualización
            cmd.CommandText = "SELECT * FROM tbl_PUBLICADORES WHERE Nombre = '" + nombrePublicador + "'";
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    txtANombre.Text = sdr["Nombre"].ToString();
                    mtxtAFechaNacimiento.Text = sdr["FechaNacimiento"].ToString();
                    mtxtAFechaBautismo.Text = sdr["FechaBautismo"].ToString();
                    cmbAAnciano.Text = sdr["Anciano"].ToString();
                    cmbASiervoMinisterial.Text = sdr["SiervoMinisterial"].ToString();
                    cmbAPrecursorRegular.Text = sdr["PrecRegular"].ToString();
                    txtAObservaciones.Text = sdr["Observaciones"].ToString();
                }
                sdr.Close();
            }
            myconnection.Close();

            textHoras.Enabled = true;
            textRevisitas.Enabled = true;
            textEstudios.Enabled = true;
            cmbPrecAuxiliar.Enabled = true;
            textObservaciones.Enabled = true;
            mtxtFecha.Enabled = true;
            buttonINGRESAR.Enabled = true;
            buttonActualizar.Enabled = false;
            buttonBorrar.Enabled = false;

            textHoras.Text = "";
            textRevisitas.Text = "";
            textEstudios.Text = "";
            cmbPrecAuxiliar.Text = "false";
            textObservaciones.Text = "";

            textHoras.Focus();

            //presenta el datagrid en la última fila
            int nRowIndex = dataGridView1.Rows.Count - 1;
            dataGridView1.FirstDisplayedScrollingRowIndex = nRowIndex;

            //presenta la fecha a ser editada en el textFecha a partir de la columna AñoFecha de la grid
            int valor = dataGridView1.Rows.Count;
            DateTime ultimaFecha;
            if (valor == 1)
            {
                //ultimaFecha = DateTime.Now;
                mtxtFecha.Text = "";
            }
            else
            {
                ultimaFecha = DateTime.Parse(dataGridView1.Rows[valor - 1].Cells[6].Value.ToString());
                DateTime ultimaFechaAumentada = ultimaFecha.AddMonths(1);
                mtxtFecha.Text = ultimaFechaAumentada.ToString("yyyy-MM-dd");
            }
            //            DateTime ultimaFechaAumentada = ultimaFecha.AddMonths(1);
            //            mtxtFecha.Text = ultimaFechaAumentada.ToString("yyyy-MM-dd");
        }

        private void buttonINGRESAR_Click(object sender, EventArgs e)
        {
            if (mtxtFecha.Text == "" || textHoras.Text == "" || textRevisitas.Text == "" ||
                textEstudios.Text == "" || cmbPrecAuxiliar.Text == "") //Que ningun campo este accidentalmente vacio
            {
                MessageBox.Show("Rellene todos los campos¡¡", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                fConexion(); ///Abrimos conexion

                String id = "(SELECT MAX(ID_Actividad) FROM tbl_ACTIVIDAD) + 1";
                String a = textHoras.Text;
                String b = textRevisitas.Text;
                String c = textEstudios.Text;
                String d = cmbPrecAuxiliar.Text;
                String f = textObservaciones.Text;
                String g = mtxtFecha.Text;

                //Ingresa los nuevos datos de actividad de publicadores
                cmd.CommandText = "INSERT INTO tbl_ACTIVIDAD (ID_Actividad, Horas, Revisitas, Estudios, PAuxiliar, " +
                    "Observaciones, AñoMes, ID_Publicador) VALUES (" + id + ",'" + a + "','" + b + "','" + c + "','" + d +
                    "','" + f + "','" + g + "',(SELECT ID_Publicador FROM tbl_PUBLICADORES WHERE Nombre = '" + nombrePublicador + "'))";
                cmd.ExecuteNonQuery();
                myconnection.Close();

                listView2_DoubleClick(null, null);

                //muestra la ultima fila del grid
                int nRowIndex = dataGridView1.Rows.Count - 1;
                dataGridView1.FirstDisplayedScrollingRowIndex = nRowIndex;
                button1_Click(null, null);
                Simple = new SoundPlayer(@"C:\Users\MARCOS LUQUE\Downloads\DATOS\recycle.wav");
                Simple.Play();
            }
        }

        private void listView3_DoubleClick(object sender, EventArgs e)
        {
            nombreCongregacion = listView3.SelectedItems[0].SubItems[0].Text;

            fConexion(); ///Abrimos conexion

            cmd.CommandText = "SELECT * FROM tbl_REUNIONES WHERE ID_Congregacion = (SELECT ID_Congregacion FROM tbl_CONGREGACIONES WHERE Nombre = '" + nombreCongregacion + "')";

            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                //Crear nueva tabla
                DataTable dt = new DataTable();
                //Cargar DataReader dentro de DataTable
                dt.Load(sdr);
                sdr.Close();
                myconnection.Close();
                dataGridView2.DataSource = dt;

            }

            mtextFechaReunion.Enabled = true;
            textReunionEntreSemana.Enabled = true;
            textReunionFinDeSemana.Enabled = true;
            textObservacionesReunion.Enabled = true;
            buttonIngresarReuniones.Enabled = true;

            textReunionEntreSemana.Text = "";
            textReunionFinDeSemana.Text = "";
            textObservacionesReunion.Text = "";
           
            textReunionEntreSemana.Focus();

            //presenta el datagrid en la última fila
            int nRowIndex = dataGridView2.Rows.Count - 1;
            dataGridView2.FirstDisplayedScrollingRowIndex = nRowIndex;

            //presenta la fecha a ser editada en el textFecha a partir de la columna AñoFecha de la grid
            int valor = dataGridView2.Rows.Count;
            DateTime ultimaFecha = DateTime.Parse(dataGridView2.Rows[valor - 1].Cells[1].Value.ToString());
            DateTime ultimaFechaAumentada = ultimaFecha.AddMonths(1);
            mtextFechaReunion.Text = ultimaFechaAumentada.ToString("yyyy-MM-dd");

        }

        private void buttonIngresarReuniones_Click(object sender, EventArgs e)
        {
            if (mtextFechaReunion.Text == "" || textReunionEntreSemana.Text == "" ||
                textReunionFinDeSemana.Text == "") //Que ningun campo este accidentalmente vacio
            {
                MessageBox.Show("Rellene todos los campos¡¡", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                fConexion(); ///Abrimos conexion

                String id = "(SELECT MAX(ID_Mes) FROM tbl_REUNIONES) + 1";
                String a = mtextFechaReunion.Text;
                String b = textReunionEntreSemana.Text;
                String c = textReunionFinDeSemana.Text;
                String f = textObservacionesReunion.Text;

                cmd.CommandText = "INSERT INTO tbl_REUNIONES (ID_Mes, Mes, ReuEntreSemana, ReuFinSemana, ID_Congregacion, " +
                    "Observaciones) VALUES (" + id + ",'" + a + "','" + b + "','" + c +
                    "',(SELECT ID_Congregacion FROM tbl_CONGREGACIONES WHERE Nombre = '" + nombreCongregacion + "'), '" + f + "')";
                cmd.ExecuteNonQuery();
                myconnection.Close();

                listView3_DoubleClick(null, null);

                textReunionEntreSemana.Text = "";
                textReunionFinDeSemana.Text = "";
                textObservacionesReunion.Text = "";
                textReunionEntreSemana.Focus();

                int nRowIndex = dataGridView2.Rows.Count - 1;
                dataGridView2.FirstDisplayedScrollingRowIndex = nRowIndex;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        
            //Presenta los valores en los cuadros de texto
            mtxtFecha.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            textHoras.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textRevisitas.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textEstudios.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbPrecAuxiliar.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textObservaciones.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

            //Inhabilita boton ingresar y habilita actualizar y borrar
            buttonINGRESAR.Enabled = false;
            buttonActualizar.Enabled = true;
            buttonBorrar.Enabled = true;
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {

            fConexion(); ///Abrimos conexion

            String mID_Actividad = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            String mHoras = textHoras.Text; 
            String mRevisitas = textRevisitas.Text; 
            String mEstudios = textEstudios.Text;
            String mPAuxiliar = cmbPrecAuxiliar.Text;
            String mObservaciones = textObservaciones.Text; 
            String mAñoMes = mtxtFecha.Text; 

            cmd.CommandText = "UPDATE tbl_ACTIVIDAD SET Horas = '" + mHoras + "', Revisitas = '" + mRevisitas 
                + "', Estudios = '" + mEstudios + "', PAuxiliar = '" + mPAuxiliar + "', Observaciones = '" 
                + mObservaciones  + "', AñoMes = '" + mAñoMes + "' WHERE ID_Actividad = '"+ mID_Actividad + "'";
            
            cmd.ExecuteNonQuery();
            myconnection.Close();

            listView2_DoubleClick(null, null);

            buttonINGRESAR.Enabled = true;
            buttonActualizar.Enabled = false;
            buttonBorrar.Enabled = false;

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Presenta los valores en los cuadros de texto
            mtextFechaReunion.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            textReunionEntreSemana.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            textReunionFinDeSemana.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            textObservacionesReunion.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();

            //Inhabilita boton ingresar y habilita actualizar y borrar
            buttonIngresarReuniones.Enabled = false;
            buttonActualizarReuniones.Enabled = true;
            buttonBorrarReuniones.Enabled = true;
        }

        private void buttonActualizarReuniones_Click(object sender, EventArgs e)
        {
            fConexion(); ///Abrimos conexion

            String mID_Mes = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString();
            String mMes = mtextFechaReunion.Text;
            String mReuEntreSemana = textReunionEntreSemana.Text;
            String mReuFinSemana = textReunionFinDeSemana.Text;
            String mID_Congregacion = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[4].Value.ToString();
            String mObservaciones = textObservacionesReunion.Text;

            cmd.CommandText = "UPDATE tbl_REUNIONES SET Mes = '" + mMes + "', ReuEntreSemana = '" + mReuEntreSemana
                + "', ReuFinSemana = '" + mReuFinSemana + "', ID_Congregacion = '" + mID_Congregacion + "', Observaciones = '"
                + mObservaciones + "' WHERE ID_Mes = '" + mID_Mes + "'";

            cmd.ExecuteNonQuery();
            myconnection.Close();

            listView3_DoubleClick(null, null);

            buttonIngresarReuniones.Enabled = true;
            buttonActualizarReuniones.Enabled = false;
            buttonBorrarReuniones.Enabled = false;

        }

        private void buttonBorrar_Click(object sender, EventArgs e)
        {
            fConexion(); ///Abrimos conexion

            String mID_Actividad = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            
            cmd.CommandText = "DELETE FROM tbl_ACTIVIDAD WHERE ID_Actividad = '" + mID_Actividad + "'";

            cmd.ExecuteNonQuery();
            myconnection.Close();

            listView2_DoubleClick(null, null);

            buttonINGRESAR.Enabled = true;
            buttonActualizar.Enabled = false;
            buttonBorrar.Enabled = false;

        }

        private void buttonBorrarReuniones_Click(object sender, EventArgs e)
        {
            fConexion(); ///Abrimos conexion

            String mID_Mes = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString();

            cmd.CommandText = "DELETE FROM tbl_REUNIONES WHERE ID_Mes = '" + mID_Mes + "'";

            cmd.ExecuteNonQuery();
            myconnection.Close();

            listView3_DoubleClick(null, null);

            buttonIngresarReuniones.Enabled = true;
            buttonActualizarReuniones.Enabled = false;
            buttonBorrarReuniones.Enabled = false;

        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Cambios frmCambios = new Cambios(listView2.SelectedItems[0].SubItems[0].Text);
                frmCambios.ShowDialog();
                listView1_DoubleClick(null, null);
            }
        }

        private void buttonAñadirPublicador_Click(object sender, EventArgs e)
        {
            frmtbl_Publicadores frmPublicador = new frmtbl_Publicadores(nombreCongregacion);
            frmPublicador.ShowDialog();
            listView1_DoubleClick(null, null);

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            fConexion(); ///Abrimos conexion

            String mUltimaFecha = ultimafecha(nombreCongregacionParaConsultas);
            dataGridView3.DataSource = ""; //limpiamos los datos previos para mantener el orden 

            if (tabControl2.SelectedIndex == 0)
            {   //Actividad de publicadores activos
                cmd.CommandText = "SELECT tbl_PUBLICADORES.Nombre as 'Publicador Activo', "
                                    + "round(AVG(tbl_ACTIVIDAD.Horas), 1) as x̄Horas, "
                                    + "round(AVG(tbl_ACTIVIDAD.Revisitas), 1) as x̄Revisitas, "
                                    + "round(AVG(tbl_ACTIVIDAD.Estudios), 1) as x̄Estudios, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaNacimiento) as Edad, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaBautismo) as Bautizado, "
                                    + "tbl_PUBLICADORES.Anciano as 'Anciano', "
                                    + "tbl_PUBLICADORES.SiervoMinisterial as 'Ministerial' "
                                    + "FROM tbl_PUBLICADORES, tbl_CONGREGACIONES, tbl_ACTIVIDAD "
                                    + "WHERE tbl_PUBLICADORES.ID_Congregacion = tbl_CONGREGACIONES.ID_Congregacion "
                                    + "AND tbl_ACTIVIDAD.ID_Publicador = tbl_PUBLICADORES.ID_Publicador "
                                    + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                    + "AND tbl_ACTIVIDAD.AñoMes >= date('" + mUltimaFecha + "', 'start of month', '-5 months') "
                                    + "AND tbl_PUBLICADORES.PrecRegular = 'false' "
                                    + "AND tbl_PUBLICADORES.Observaciones != 'INACTIVO' "
                                    + "GROUP BY tbl_PUBLICADORES.Nombre, "
                                    + "tbl_PUBLICADORES.Anciano, "
                                    + "tbl_PUBLICADORES.SiervoMinisterial, "
                                    + "tbl_PUBLICADORES.PrecRegular, "
                                    + "tbl_PUBLICADORES.FechaNacimiento, "
                                    + "tbl_PUBLICADORES.FechaBautismo, tbl_PUBLICADORES.Sexo "
                                    + "ORDER BY AVG(tbl_ACTIVIDAD.Horas) DESC";
            }
            if (tabControl2.SelectedIndex == 1)
            {   //Consulta de actividad de regulares
                cmd.CommandText = "SELECT tbl_PUBLICADORES.Nombre as 'Precursor Regular', "
                                    + "round(AVG(tbl_ACTIVIDAD.Horas), 1) AS 'x̄Horas', "
                                    + "round(AVG(tbl_ACTIVIDAD.Revisitas), 1) AS 'x̄Revisitas', "
                                    + "round(AVG(tbl_ACTIVIDAD.Estudios), 1) AS 'x̄Estudios', "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaNacimiento) as Edad, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaBautismo) as Bautizado, "
                                    + "tbl_PUBLICADORES.Anciano as 'Anciano', "
                                    + "tbl_PUBLICADORES.SiervoMinisterial as 'Ministerial' "
                                    + "FROM tbl_CONGREGACIONES, tbl_PUBLICADORES, tbl_ACTIVIDAD "
                                    + "WHERE tbl_CONGREGACIONES.ID_Congregacion = tbl_PUBLICADORES.ID_Congregacion "
                                    + "AND tbl_PUBLICADORES.ID_Publicador = tbl_ACTIVIDAD.ID_Publicador "
                                    + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                    + "AND tbl_PUBLICADORES.PrecRegular = 'true' "
                                    + "AND tbl_ACTIVIDAD.AñoMes >= date('" + mUltimaFecha + "', 'start of month', '-5 months') "
                                    + "GROUP BY tbl_PUBLICADORES.ID_Publicador, tbl_PUBLICADORES.Nombre "
                                    + "ORDER BY round(AVG(tbl_ACTIVIDAD.Horas ),1) ASC";
            }
            if (tabControl2.SelectedIndex == 2)
            {   //Consulta de Actividad de Varones
                cmd.CommandText = "SELECT tbl_PUBLICADORES.Nombre as 'Publicador Varón', "
                                    + "round(AVG(tbl_ACTIVIDAD.Horas), 1) as x̄Horas, "
                                    + "round(AVG(tbl_ACTIVIDAD.Revisitas), 1) as x̄Revisitas, "
                                    + "round(AVG(tbl_ACTIVIDAD.Estudios), 1) as x̄Estudios, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaNacimiento) as Edad, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaBautismo) as Bautizado, "
                                    + "tbl_PUBLICADORES.Anciano as 'Anciano', "
                                    + "tbl_PUBLICADORES.SiervoMinisterial as 'Ministerial', "
                                    + "tbl_PUBLICADORES.PrecRegular as 'P.Regular' "
                                    + "FROM tbl_PUBLICADORES, tbl_CONGREGACIONES, tbl_ACTIVIDAD "
                                    + "WHERE tbl_PUBLICADORES.ID_Congregacion = tbl_CONGREGACIONES.ID_Congregacion "
                                    + "AND tbl_ACTIVIDAD.ID_Publicador = tbl_PUBLICADORES.ID_Publicador "
                                    + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "'"
                                    + "AND tbl_PUBLICADORES.Sexo = 'Hombre' "
                                    + "AND tbl_ACTIVIDAD.AñoMes >= date('" + mUltimaFecha + "', 'start of month', '-5 months') "
                                    + "AND tbl_PUBLICADORES.Observaciones != 'INACTIVO' "
                                    + "GROUP BY tbl_PUBLICADORES.Nombre, "
                                    + "tbl_PUBLICADORES.Anciano, "
                                    + "tbl_PUBLICADORES.SiervoMinisterial, "
                                    + "tbl_PUBLICADORES.PrecRegular, "
                                    + "tbl_PUBLICADORES.FechaNacimiento, "
                                    + "tbl_PUBLICADORES.FechaBautismo, tbl_PUBLICADORES.Sexo "
                                    + "ORDER BY AVG(tbl_ACTIVIDAD.Horas) DESC";
            }
            if (tabControl2.SelectedIndex == 3)
            {    //Consulta de actividad de ancianos
                cmd.CommandText = "SELECT tbl_PUBLICADORES.Nombre as Anciano, "
                                    + "round(AVG(tbl_ACTIVIDAD.Horas), 1) as x̄Horas, "
                                    + "round(AVG(tbl_ACTIVIDAD.Revisitas), 1) as x̄Revisitas, "
                                    + "round(AVG(tbl_ACTIVIDAD.Estudios), 1) as x̄Estudios, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaNacimiento) as Edad, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaBautismo) as Bautizado, "
                                    + "tbl_PUBLICADORES.PrecRegular as 'P.Regular' "
                                    + "FROM tbl_PUBLICADORES, tbl_CONGREGACIONES, tbl_ACTIVIDAD "
                                    + "WHERE tbl_PUBLICADORES.ID_Congregacion = tbl_CONGREGACIONES.ID_Congregacion "
                                    + "AND tbl_ACTIVIDAD.ID_Publicador = tbl_PUBLICADORES.ID_Publicador "
                                    + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                    + "AND tbl_PUBLICADORES.Anciano = 'true' "
                                    + "AND tbl_ACTIVIDAD.AñoMes >= date('" + mUltimaFecha + "', 'start of month', '-5 months') "
                                    + "GROUP BY tbl_PUBLICADORES.Nombre, "
                                    + "tbl_PUBLICADORES.Anciano, "
                                    + "tbl_PUBLICADORES.SiervoMinisterial, "
                                    + "tbl_PUBLICADORES.PrecRegular, "
                                    + "tbl_PUBLICADORES.FechaNacimiento, "
                                    + "tbl_PUBLICADORES.FechaBautismo, tbl_PUBLICADORES.Sexo "
                                    + "ORDER BY AVG(tbl_ACTIVIDAD.Horas) DESC";
            }
            if (tabControl2.SelectedIndex == 4)
            {    //Consulta de actividad de siervos ministeriales
                cmd.CommandText = "SELECT tbl_PUBLICADORES.Nombre as 'Siervo Ministerial', "
                                    + "round(AVG(tbl_ACTIVIDAD.Horas), 1) as x̄Horas, "
                                    + "round(AVG(tbl_ACTIVIDAD.Revisitas), 1) as x̄Revisitas, "
                                    + "round(AVG(tbl_ACTIVIDAD.Estudios), 1) as x̄Estudios, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaNacimiento) as Edad, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaBautismo) as Bautizado, "
                                    + "tbl_PUBLICADORES.PrecRegular as 'P.Regular' "
                                    + "FROM tbl_PUBLICADORES, tbl_CONGREGACIONES, tbl_ACTIVIDAD "
                                    + "WHERE tbl_PUBLICADORES.ID_Congregacion = tbl_CONGREGACIONES.ID_Congregacion "
                                    + "AND tbl_ACTIVIDAD.ID_Publicador = tbl_PUBLICADORES.ID_Publicador "
                                    + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                    + "AND tbl_PUBLICADORES.SiervoMinisterial = 'true' "
                                    + "AND tbl_ACTIVIDAD.AñoMes >= date('" + mUltimaFecha + "', 'start of month', '-5 months') "
                                    + "GROUP BY tbl_PUBLICADORES.Nombre, "
                                    + "tbl_PUBLICADORES.Anciano, "
                                    + "tbl_PUBLICADORES.SiervoMinisterial, "
                                    + "tbl_PUBLICADORES.PrecRegular, "
                                    + "tbl_PUBLICADORES.FechaNacimiento, "
                                    + "tbl_PUBLICADORES.FechaBautismo, tbl_PUBLICADORES.Sexo "
                                    + "ORDER BY AVG(tbl_ACTIVIDAD.Horas) DESC";
            }
            if (tabControl2.SelectedIndex == 5)
            {   //Consulta de cuantas veces el publicador tomo el auxiliar
                cmd.CommandText = "SELECT tbl_PUBLICADORES.Nombre AS 'Precursor Auxiliar', "
                                    + "COUNT(tbl_PUBLICADORES.Nombre) AS 'N° de Veces', "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaNacimiento) as Edad, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaBautismo) as Bautizado "
                                    + "FROM tbl_CONGREGACIONES, tbl_PUBLICADORES, tbl_ACTIVIDAD "
                                    + "WHERE tbl_CONGREGACIONES.ID_Congregacion = tbl_PUBLICADORES.ID_Congregacion "
                                    + "AND tbl_PUBLICADORES.ID_Publicador = tbl_ACTIVIDAD.ID_Publicador "
                                    + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                    + "AND tbl_ACTIVIDAD.PAuxiliar = 'true' "
                                    + "AND tbl_ACTIVIDAD.AñoMes >= date('" + mUltimaFecha + "', 'start of month', '-5 months') "
                                    + "GROUP BY tbl_PUBLICADORES.Nombre";
            }
            if (tabControl2.SelectedIndex == 6)
            {  //Consulta de baja actividad
                cmd.CommandText = "SELECT tbl_PUBLICADORES.Nombre as Publicador, "
                                    + "round(AVG(tbl_ACTIVIDAD.Horas), 1) AS 'Promedio', "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaNacimiento) as Edad, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaBautismo) as Bautizado "
                                    + "FROM tbl_PUBLICADORES, tbl_CONGREGACIONES, tbl_ACTIVIDAD "
                                    + "WHERE tbl_PUBLICADORES.ID_Congregacion = tbl_CONGREGACIONES.ID_Congregacion "
                                    + "AND tbl_ACTIVIDAD.ID_Publicador = tbl_PUBLICADORES.ID_Publicador "
                                    + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                    + "AND tbl_ACTIVIDAD.AñoMes >= date('" + mUltimaFecha + "', 'start of month', '-5 months') "
                                    + "AND tbl_PUBLICADORES.Observaciones != 'INACTIVO' "
                                    + "GROUP BY tbl_PUBLICADORES.Nombre "
                                    + "HAVING((AVG(tbl_ACTIVIDAD.Horas) < 8)) "
                                    + "ORDER BY round(AVG(tbl_ACTIVIDAD.Horas), 1) ASC";
            }
            if (tabControl2.SelectedIndex == 7)
            {  //Consulta de Irregulares
                cmd.CommandText = "SELECT tbl_PUBLICADORES.Nombre as Publicador, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaNacimiento) as Edad, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaBautismo) as Bautizado "
                                    + "FROM tbl_PUBLICADORES, tbl_CONGREGACIONES, tbl_ACTIVIDAD "
                                    + "WHERE tbl_PUBLICADORES.ID_Congregacion = tbl_CONGREGACIONES.ID_Congregacion "
                                    + "AND tbl_ACTIVIDAD.ID_Publicador = tbl_PUBLICADORES.ID_Publicador "
                                    + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                    + "AND tbl_ACTIVIDAD.AñoMes >= date('" + mUltimaFecha + "', 'start of month', '-5 months') "
                                    + "AND tbl_PUBLICADORES.Observaciones != 'INACTIVO' "
                                    + "AND tbl_ACTIVIDAD.Horas = 0 "
                                    + "GROUP BY tbl_PUBLICADORES.Nombre";                
            }
            if (tabControl2.SelectedIndex == 8)
            {   //Consulta de inactivos
                cmd.CommandText = "SELECT tbl_PUBLICADORES.Nombre as Publicador, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaNacimiento) as Edad, "
                                    + "strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaBautismo) as Bautizado "
                                    + "FROM tbl_PUBLICADORES, tbl_CONGREGACIONES "
                                    + "WHERE tbl_PUBLICADORES.ID_Congregacion = tbl_CONGREGACIONES.ID_Congregacion "
                                    + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                    + "AND tbl_PUBLICADORES.Observaciones = 'INACTIVO' "
                                    + "GROUP BY tbl_PUBLICADORES.Nombre";
            }
            if (tabControl2.SelectedIndex == 9)
            {   //Consulta de promedios
                try
                {
                    DateTime ultimaFecha;
                    ultimaFecha = DateTime.Parse(mUltimaFecha);
                    DateTime ultimaFechaDeHaceMeses = ultimaFecha.AddMonths(-5);
                    String a = mUltimaFecha;
                    String b = ultimaFechaDeHaceMeses.ToString("yyyy-MM-01");
                    cmd.CommandText = "SELECT round(avg(tbl_ACTIVIDAD.Horas), 1) AS 'Horas', "
                                        + "round(avg(tbl_ACTIVIDAD.Revisitas), 1) AS 'Revisitas', "
                                        + "round(avg(tbl_ACTIVIDAD.Estudios), 1) AS 'Estudios', "
                                        + "'" + b + "' AS 'desde...', "
                                        + "'" + a + "' AS 'hasta...' "
                                        + "FROM tbl_PUBLICADORES, tbl_CONGREGACIONES, tbl_ACTIVIDAD "
                                        + "WHERE tbl_PUBLICADORES.ID_Congregacion = tbl_CONGREGACIONES.ID_Congregacion "
                                        + "AND tbl_PUBLICADORES.PrecRegular = 'false' "
                                        + "AND tbl_PUBLICADORES.Observaciones != 'INACTIVO' "
                                        + "AND tbl_ACTIVIDAD.ID_Publicador = tbl_PUBLICADORES.ID_Publicador "
                                        + "AND tbl_ACTIVIDAD.PAuxiliar = 'false' "
                                        + "AND tbl_ACTIVIDAD.AñoMes between '" + b + "' and '" + a + "' "
                                        + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "'";
                }
                catch
                {

                }
            };

            dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                //Crear nueva tabla
                DataTable dt = new DataTable();
                //Cargar DataReader dentro de DataTable
                dt.Load(sdr);
                dataGridView3.DataSource = dt;
            }
            //numero de registros creados en el datagridview
            lblNumeroRegistros.Text= "N° de Registros: " + dataGridView3.Rows.Count.ToString();

            //Informacion sobre las edades de los hermanos
            cmd.CommandText = "SELECT strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaNacimiento) "
                                + "FROM tbl_PUBLICADORES, tbl_CONGREGACIONES "
                                + "WHERE tbl_PUBLICADORES.ID_Congregacion = tbl_CONGREGACIONES.ID_Congregacion "
                                + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                + "AND tbl_PUBLICADORES.Observaciones != 'INACTIVO'";
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                int a = 0;
                int b = 0;
                int c = 0;
                int d = 0;
                int f = 0;
                lblNumeroNiños.Text = "No hay niños publicadores en la congregación";
                lblNumeroAdolescentes.Text = "No hay adolescentes publicadores en la congregación";
                lblNumeroAdultos.Text = "No hay adultos publicadores en la congregación";
                lblNumeroAdultosMayores.Text = "No hay adultos mayores publicadores en la congregación";
                lblNoFechaNacimiento.Text = "Fechas de nacimiento llenados";

                while (sdr.Read())
                {   
                    if (sdr.GetValue(0).ToString() != "")
                    {
                        if (Convert.ToInt32(sdr.GetValue(0)) <= 12)
                        {
                            a = a + 1;
                            lblNumeroNiños.Text = a.ToString() + " niños publicadores en la congregación";
                        }
                        if (Convert.ToInt32(sdr.GetValue(0)) > 12 && Convert.ToInt32(sdr.GetValue(0)) < 21)
                        {
                            b = b + 1;
                            lblNumeroAdolescentes.Text = b.ToString() + " adolescentes publicadores en la congregación";
                        }
                        if (Convert.ToInt32(sdr.GetValue(0)) >= 21 && Convert.ToInt32(sdr.GetValue(0)) < 60)
                        {
                            c = c + 1;
                            lblNumeroAdultos.Text = c.ToString() + " adultos publicadores en la congregación";
                        }
                        if (Convert.ToInt32(sdr.GetValue(0)) >= 60)
                        {
                            d = d + 1;
                            lblNumeroAdultosMayores.Text = d.ToString() + " adultos mayores publicadores en la \n congregación";
                        }
                    }
                    else
                    {
                        f = f + 1;
                        lblNoFechaNacimiento.Text = f.ToString() + " sin fecha de nacimiento";
                    }
                }
                sdr.Close();
            }
            myconnection.Close();
        }

        private void listView4_DoubleClick(object sender, EventArgs e)
        {
            //Que congregacion desea consultar
            nombreCongregacionParaConsultas = listView4.SelectedItems[0].SubItems[0].Text;
            tabControl2_SelectedIndexChanged(null, null);
            DateTime ultimaFecha;
            ultimaFecha = DateTime.Parse(ultimafecha(nombreCongregacionParaConsultas));
            DateTime ultimaFechaDeHaceMeses = ultimaFecha.AddMonths(-5);
            String a = ultimafecha(nombreCongregacionParaConsultas);
            String b = ultimaFechaDeHaceMeses.ToString("yyyy-MM-01");
            lblConsultaCongregacion.Text = "Consulta de " + nombreCongregacionParaConsultas + "\n"
                + "desde " + b + " hasta " + a;
        }

        private void btnAActualizar_Click(object sender, EventArgs e)
        {
            String ComandoTextoActualizarDatos = "UPDATE tbl_PUBLICADORES SET Nombre = '" + txtANombre.Text
                + "', FechaNacimiento = '" + mtxtAFechaNacimiento.Text + "', FechaBautismo = '" + mtxtAFechaBautismo.Text
                + "', Anciano = '" + cmbAAnciano.Text + "', SiervoMinisterial = '" + cmbASiervoMinisterial.Text
                + "', PrecRegular = '" + cmbAPrecursorRegular.Text + "', Observaciones = '" + txtAObservaciones.Text
                + "' WHERE Nombre = '" + nombrePublicador + "'";

            fConexion(); ///Abrimos conexion

            using (SQLiteCommand cmd = new SQLiteCommand(ComandoTextoActualizarDatos, myconnection))
            {
                int nFilasActualizadas = cmd.ExecuteNonQuery();
                MessageBox.Show(nFilasActualizadas + " fila actualizada. Vuelva a entrar a Congregaciones...", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            myconnection.Close();
            //actualizar la lista de publicadores con sus nombres correctos
            listView1_DoubleClick(null, null);
            //actualizamos la variable para que pueda ser usada por botón INGRESAR
            nombrePublicador = txtANombre.Text;
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try{
                fConexion(); ///Abrimos conexion
                cmd.CommandText = "SELECT tbl_ACTIVIDAD.AñoMes, "
                    + "tbl_ACTIVIDAD.Horas, tbl_ACTIVIDAD.Revisitas, "
                    + "tbl_ACTIVIDAD.Estudios, tbl_ACTIVIDAD.PAuxiliar, "
                    + "tbl_ACTIVIDAD.Observaciones "
                    + "FROM tbl_ACTIVIDAD WHERE ID_Publicador = "
                    + "(SELECT ID_Publicador FROM tbl_PUBLICADORES WHERE Nombre = '" + 
                    dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString() + "')";
                using (SQLiteDataReader sdr = cmd.ExecuteReader())
                {
                    //Crear nueva tabla
                    DataTable dt = new DataTable();
                    //Cargar DataReader dentro de DataTable
                    dt.Load(sdr);
                    dataGridView4.DataSource = dt;
                }
                lblNombreAExaminar.Text = "Actividad de " + dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
                //presenta el datagrid en la última fila
                int nRowIndex = dataGridView4.Rows.Count - 1;
                dataGridView4.FirstDisplayedScrollingRowIndex = nRowIndex;
                dataGridView4.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);

            }
            catch
            {
                MessageBox.Show("Se equivocó de tab ... error ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fConexion(); ///Abrimos conexion
            //Llenar los arrays con los nombres de los publicadores de la congregación
            cmd.CommandText = "SELECT tbl_PUBLICADORES.Nombre "
                                                + "from tbl_CONGREGACIONES, tbl_PUBLICADORES "
                                                + "where tbl_CONGREGACIONES.ID_Congregacion = tbl_PUBLICADORES.ID_Congregacion "
                                                + "and tbl_CONGREGACIONES.Nombre = '" + nombreCongregacion + "'";
            List<String> listTotalPublic = new List<string>();
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    listTotalPublic.Add(sdr["Nombre"].ToString());
                }
                sdr.Close();
            }
            //obtenemos los nombres de los publicadores que han informado hasta la ultima fecha llenada
            String mUltimaFecha = ultimafecha(nombreCongregacion);
            cmd.CommandText = "SELECT tbl_PUBLICADORES.Nombre "
                                + "from tbl_CONGREGACIONES, tbl_PUBLICADORES, tbl_ACTIVIDAD "
                                + "where tbl_CONGREGACIONES.ID_Congregacion = tbl_PUBLICADORES.ID_Congregacion "
                                + "and tbl_PUBLICADORES.ID_Publicador = tbl_ACTIVIDAD.ID_Publicador "
                                + "and tbl_CONGREGACIONES.Nombre = '" + nombreCongregacion + "' "
                                + "and tbl_ACTIVIDAD.AñoMes = '" + mUltimaFecha + "'";
            List<String> listParcialPublic = new List<string>();
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    listParcialPublic.Add(sdr["Nombre"].ToString());
                }
                sdr.Close();
            }

            //comparamos los dos arrays y sacamos los que no han informado
            List<String> listPublicadoresNoInformados = new List<String>();
            listPublicadoresNoInformados = listTotalPublic.Except(listParcialPublic).ToList();
            listBox1.Items.Clear();
            foreach (var item in listPublicadoresNoInformados)
            {
                listBox1.Items.Add(item);
            }

            //presentamos la ultima fecha de actividad de los publicadores
            maskedTextBox1.Text = ultimafecha(nombreCongregacion);
            lblFaltantes.Text = "Faltan " + listBox1.Items.Count.ToString() + " informes";
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Left)) // Por si presiona la tecla FLECHA IZQUIERDA
            {
                SendKeys.Send("+{TAB}");
                // Envío datos indicando que se pulsaron las teclas Shift + TAB

                e.Handled = true;
                // Aquí indico que me hice cargo del evento...
            }
        }

        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                listView1_DoubleClick(null, null);
                listView2.Focus();
            }
        }

        private void listView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                listView2_DoubleClick(null, null);
            }
        }

        private void listView3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                listView3_DoubleClick(null, null);
            }
        }

        private void listView4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                listView4_DoubleClick(null, null);
            }
        }

        private void btnBorrarActividad_Click(object sender, EventArgs e)
        {
            lblNombreAExaminar.Text = "Actividad";
            dataGridView4.DataSource = "";
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
                    //Que congregacion desea consultar
                    //nombreCongregacionParaConsultas = listView5.SelectedItems[0].SubItems[0].Text;
                    //tabControl2_SelectedIndexChanged(null, null);
                   
        }

        private void listView5_DoubleClick(object sender, EventArgs e)
        {
            fConexion();
            //Que congregacion desea consultar
            nombreCongregacionParaConsultas = listView5.SelectedItems[0].SubItems[0].Text;
            
            //Informacion sobre las edades de los hermanos
            cmd.CommandText = "SELECT strftime('%Y', 'now') - strftime('%Y', tbl_PUBLICADORES.FechaNacimiento) "
                                    + "FROM tbl_PUBLICADORES, tbl_CONGREGACIONES "
                                    + "WHERE tbl_PUBLICADORES.ID_Congregacion = tbl_CONGREGACIONES.ID_Congregacion "
                                    + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                    + "AND tbl_PUBLICADORES.Observaciones != 'INACTIVO'";

            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            int f = 0;
            int aChart = 0;
            int bChart = 0;
            int cChart = 0;
            int dChart = 0;
            int fChart = 0;

            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    if (sdr.GetValue(0).ToString() != "")
                    {
                        if (Convert.ToInt32(sdr.GetValue(0)) <= 12)
                        {
                            a = a + 1;
                            aChart = a;
                        }
                        if (Convert.ToInt32(sdr.GetValue(0)) > 12 && Convert.ToInt32(sdr.GetValue(0)) < 21)
                        {
                            b = b + 1;
                            bChart = b;
                        }
                        if (Convert.ToInt32(sdr.GetValue(0)) >= 21 && Convert.ToInt32(sdr.GetValue(0)) < 60)
                        {
                            c = c + 1;
                            cChart = c;
                        }
                        if (Convert.ToInt32(sdr.GetValue(0)) >= 60)
                        {
                            d = d + 1;
                            dChart = d;
                        }
                    }
                }
                sdr.Close();
            }

            chart1.Series["s1"].Points.Clear();
            chart1.Series["s1"].IsValueShownAsLabel = true;
            chart1.Legends["Legend1"].Title = "Publicadores y Precursores de la Congregación " + nombreCongregacionParaConsultas;
            chart1.Series["s1"].SetCustomProperty("PieLabelStyle", "outside");
            chart1.Series["s1"].Points.AddXY(aChart.ToString() + " Niños Publicadores", aChart.ToString());
            chart1.Series["s1"].Points.AddXY(bChart.ToString() + " Adolescentes Publicadores", bChart.ToString());
            chart1.Series["s1"].Points.AddXY(cChart.ToString() + " Adultos Publicadores", cChart.ToString());
            chart1.Series["s1"].Points.AddXY(dChart.ToString() + " Adultos Mayores Publicadores", dChart.ToString());

            lblTotalPublicadores.Text = "Total de Publicadores: " + (aChart + bChart + cChart + dChart);

            //Informacion sobre cantidad de invitados
            String mUltimaFecha = ultimafecha(nombreCongregacionParaConsultas);
            cmd.CommandText = "select round(AVG(tbl_REUNIONES.ReuFinSemana)) "
                                    + "from tbl_REUNIONES, tbl_CONGREGACIONES "
                                    + "where tbl_CONGREGACIONES.ID_Congregacion = tbl_REUNIONES.ID_Congregacion "
                                    + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                    + "AND tbl_REUNIONES.Mes >= date('" + mUltimaFecha + "', 'start of month', '-5 months') ";


            int mPublicadores = 0;
            int mInvitados = 0;
            mAsistentes = 0;

            using (SQLiteDataReader sdr1 = cmd.ExecuteReader())
            {
                try
                {
                    while (sdr1.Read())
                    {
                        mAsistentes = Convert.ToInt32(sdr1.GetValue(0));
                    }
                }
                catch
                {

                }
                sdr1.Close();
            }

            mPublicadores = aChart+bChart+cChart+dChart;
            mInvitados = mAsistentes - mPublicadores;
            
            chart2.Series["s1"].Points.Clear();
            chart2.Series["s1"].IsValueShownAsLabel = true;
            chart2.Legends["Legend1"].Title = "Asistencia a las Reuniones de la Congregación " + nombreCongregacionParaConsultas;
            chart2.Series["s1"].SetCustomProperty("PieLabelStyle", "outside");
            chart2.Series["s1"].Points.AddXY(mPublicadores.ToString() + " Publicadores", mPublicadores.ToString());
            chart2.Series["s1"].Points.AddXY(mInvitados.ToString() + " Invitados", mInvitados.ToString());

            lblTotalAsistentes.Text = "Total de Asistentes a la Reunión de Fin de Semana: " + mAsistentes;

            //Estudiantes que asisten a las reuniones
            cmd.CommandText = "select round(avg(tbl_ACTIVIDAD.Estudios)) "
                                + "from tbl_PUBLICADORES, tbl_CONGREGACIONES, tbl_ACTIVIDAD "
                                + "where tbl_CONGREGACIONES.ID_Congregacion = tbl_PUBLICADORES.ID_Congregacion "
                                + "AND tbl_PUBLICADORES.ID_Publicador = tbl_ACTIVIDAD.ID_Publicador "
                                + "AND tbl_CONGREGACIONES.Nombre = '" + nombreCongregacionParaConsultas + "' "
                                + "AND tbl_ACTIVIDAD.AñoMes >= date('" + mUltimaFecha + "', 'start of month', '-5 months') "
                                + "GROUP BY tbl_PUBLICADORES.Nombre";
            mEstudios = 0;
            using (SQLiteDataReader sdr2 = cmd.ExecuteReader())
            {
                try
                {
                    while (sdr2.Read())
                    {
                        mEstudios = mEstudios + Convert.ToInt32(sdr2.GetValue(0));
                    }
                }
                catch
                {

                }
                sdr2.Close();
            }

            chart3.Series["s1"].Points.Clear();
            chart3.Series["s1"].IsValueShownAsLabel = true;
            chart3.Legends["Legend1"].Title = "Total de Estudios Bíblicos de la Congregación " + nombreCongregacionParaConsultas;
            chart3.Series["s1"].SetCustomProperty("PieLabelStyle", "outside");
            chart3.Series["s1"].Points.AddXY(mInvitados.ToString() + " van a las reuniones", mInvitados.ToString());
            chart3.Series["s1"].Points.AddXY((mEstudios - mInvitados).ToString() + " solo estudian", (mEstudios-mInvitados).ToString());

            lblTotalEstudiantes.Text = "Total de Estudiantes en la congregación: " + mEstudios;
        }

        private void lblConsultaPersonasNombreCong_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalEstudiantes_Click(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void lblNumeroAdolescentes_Click(object sender, EventArgs e)
        {

        }
    }
}
