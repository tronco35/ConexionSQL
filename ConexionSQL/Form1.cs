using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//libreria SQL
using System.IO;

namespace ConexionSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TODO Configurar autenticacion de LOGIN SQL y windows
        
            string cadena1 = "Data Source=" + NombreInstancia.Text;//localhost ";
            string cadena2 = ";Initial Catalog=master"; // Base
            string cadena3 = ";Integrated Security=True";
            string con = cadena1 + cadena2 + cadena3;

            //archivos Planos
            string fileName = "propiedadesInstancia.csv";
            string fileNamePS = "propiedadesServidor.csv";
            string fNservicesInfo = "servicesInfo.csv";
            string fNcontadores = "contadores.csv";
            string fNinfoHardware = "infoHardware.csv";
            string fNpropiedadesBD = "propiedadesBD.csv";
            string fNautoCrecimiento = "autoCrecimiento.csv";
            string fNUsoCPUBD = "UsoCPUBD.csv";
            string fNUsoDiscoBD = "UsoDiscoBD.csv";
            string fNestadisticasBD = "estadisticasBD.csv";
            string fNwaits = "waits.csv";
            string fNsysadmin = "sysadmin.csv";

            if (ListHC.CheckedItems.Count != 0)
            {     
                //propiedades servidor check
                if (ListHC.GetItemChecked(1) == true)
                {


                    try
                    {

                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL


                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.PropiedadesServidor, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fileNamePS);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }

                                lee.Close();
                                sw.Close();
                                tsql.Clone();



                            }
                            Conectarbd.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    }
                //propiedades instancia
                if (ListHC.GetItemChecked(0) == true)
                {


                    try
                    {

                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL


                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.propiedadesInstancia, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fileName);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }

                                lee.Close();
                                sw.Close();
                                tsql.Clone();



                            }
                            Conectarbd.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                //servicesInfo
                if (ListHC.GetItemChecked(2) == true)
                {
                    try
                    {
                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL

                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.servicesInfo, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fNservicesInfo);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }
                                lee.Close();
                                sw.Close();
                                tsql.Clone();
                            }
                            Conectarbd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                //contadores
                if (ListHC.GetItemChecked(3) == true)
                {
                    try
                    {
                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL

                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.contadores, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fNcontadores);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }
                                lee.Close();
                                sw.Close();
                                tsql.Clone();
                            }
                            Conectarbd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                //infoHardware
                if (ListHC.GetItemChecked(4) == true)
                {
                    try
                    {
                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL

                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.infoHardware, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fNinfoHardware);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }
                                lee.Close();
                                sw.Close();
                                tsql.Clone();
                            }
                            Conectarbd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                //propiedadesBD
                if (ListHC.GetItemChecked(5) == true)
                {
                    try
                    {
                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL

                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.propiedadesBD, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fNpropiedadesBD);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }
                                lee.Close();
                                sw.Close();
                                tsql.Clone();
                            }
                            Conectarbd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                //Crecimiento
                if (ListHC.GetItemChecked(6) == true)
                {
                    try
                    {
                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL

                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.autoCrecimiento, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fNautoCrecimiento);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }
                                lee.Close();
                                sw.Close();
                                tsql.Clone();
                            }
                            Conectarbd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                //UsoCPUBD
                if (ListHC.GetItemChecked(7) == true)
                {
                    try
                    {
                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL

                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.UsoCPUBD, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fNUsoCPUBD);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }
                                lee.Close();
                                sw.Close();
                                tsql.Clone();
                            }
                            Conectarbd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                //UsoDiscoBD
                if (ListHC.GetItemChecked(8) == true)
                {
                    try
                    {
                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL

                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.UsoDiscoBD, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fNUsoDiscoBD);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }
                                lee.Close();
                                sw.Close();
                                tsql.Clone();
                            }
                            Conectarbd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                //estadisticasBD
                if (ListHC.GetItemChecked(9) == true)
                {
                    try
                    {
                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL

                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.estadisticasBD, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fNestadisticasBD);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }
                                lee.Close();
                                sw.Close();
                                tsql.Clone();
                            }
                            Conectarbd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                //waits
                if (ListHC.GetItemChecked(10) == true)
                {
                    try
                    {
                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL

                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.waits, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fNwaits);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }
                                lee.Close();
                                sw.Close();
                                tsql.Clone();
                            }
                            Conectarbd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                //sysadmin
                if (ListHC.GetItemChecked(11) == true)
                {
                    try
                    {
                        SentenciasSQL pIns = new SentenciasSQL(); //inicialicia clase sentenciasSQL

                        using (SqlConnection Conectarbd = new SqlConnection(con))
                        {
                            Conectarbd.Open(); //inicializa
                            using (SqlCommand tsql = new SqlCommand(pIns.sysadmin, Conectarbd))
                            {

                                SqlDataReader lee = tsql.ExecuteReader();
                                StreamWriter sw = new StreamWriter(fNsysadmin);
                                object[] salida = new object[lee.FieldCount];
                                for (int i = 0; i < lee.FieldCount; i++)

                                    salida[i] = lee.GetName(i);

                                sw.WriteLine(string.Join(";", salida));
                                while (lee.Read())
                                {
                                    lee.GetValues(salida);
                                    sw.WriteLine(string.Join(";", salida));
                                }
                                lee.Close();
                                sw.Close();
                                tsql.Clone();
                            }
                            Conectarbd.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            else

            { MessageBox.Show("Selecccione una opcion "); }




        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
             string Instancia = NombreInstancia.Text;//retorna el valor del nombreservidor
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
       


        public void ListHC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
