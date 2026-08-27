using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MaterialSkin.Controls;
using System.Windows.Forms.DataVisualization.Charting;

namespace prueba
{
    public partial class FormGraficos : MaterialForm
    {
        public string area = "";
        public string año = "";
        public FormGraficos(string parea)
        {
            area = parea;
            InitializeComponent();
        }

        private void FormGraficos_Load(object sender, EventArgs e)
        {
            List<string> lista = new List<string>();
            string[] arreglo = new string[100];
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand Llenar = new MySqlCommand("select indicadores from nombreindicadores where area='"+area+"' and estado='activo'", conectar);
            MySqlDataReader leer = Llenar.ExecuteReader();
            while(leer.Read())
            {
                lista.Add(leer.GetString(0));
            }
            conectar.Close();
            foreach (string item in lista)
            {
                cmbINDICADOR.Items.Add(item.ToString());
            }
            for (int d = 2022; d < 2100; d++)
            {
                cmbAño.Items.Add(Convert.ToString(d));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chartIndicador.ResetAutoValues();
            chartIndicador.Series.Clear();
            año = cmbAño.Text;
            string[] series = new string[50];
            double[] puntos = new double[50];
            List<string> listaNombres = new List<string>();
            List<string> listaValores = new List<string>();
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand com = new MySqlCommand("select orden from nombreindicadores where indicadores='" + cmbINDICADOR.Text + "'", conectar);
            MySqlDataReader leido = com.ExecuteReader();
            leido.Read();
            string result = leido.GetString(0);
            conectar.Close();
            string comando = "select result"+result+", mes from "+area+" where año='"+año+"'";
            conectar.Open();
            MySqlCommand Llenar = new MySqlCommand(comando, conectar);
            MySqlDataReader leer = Llenar.ExecuteReader();
            while (leer.Read())
            {
                listaNombres.Add(leer.GetString(1));
                listaValores.Add(leer.GetString(0));
            }
            conectar.Close();
            for(int x = 0; x < listaNombres.Count; x++)
            {
                series[x] = listaNombres.ToArray().GetValue(x).ToString();
                if(listaValores.ToArray().GetValue(x).ToString() == "")
                {
                    puntos[x] = 0;
                }
                else if(listaValores.ToArray().GetValue(x).ToString() != "")
                {
                    puntos[x] = Convert.ToDouble(listaValores.ToArray().GetValue(x));
                }
            }
            for(int i = 0; i < listaNombres.Count; i++)
            {
                Series serie = chartIndicador.Series.Add(series[i]);
                serie.Font = new Font(label1.Font.Name, 15);
                serie.Label = puntos[i].ToString();
                serie.Points.Add(puntos[i]);
            }

        }

        private void chartIndicador_Click(object sender, EventArgs e)
        {

        }
    }
}
