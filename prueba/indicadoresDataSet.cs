using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace prueba
{


    partial class indicadoresDataSet
    {

    }
}

namespace prueba.indicadoresDataSetTableAdapters {
    
    
    public partial class rrhhTableAdapter {
        
        public DataTable Datos()
        {
            List<string> listaNombres = new List<string>();
            List<string> lista = new List<string>();
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand comando1 = new MySqlCommand("select indicadores from nombreindicadores where area='rrhh'", conectar);
            MySqlDataReader leer1 = comando1.ExecuteReader();
            while (leer1.Read())
            {
                listaNombres.Add(leer1.GetString(0));
            }
            conectar.Close();
            string comando = "select cod as 'Codigo de registro', mes as 'Mes', año as 'Año', ";
            conectar.Open();
            MySqlCommand Llenar = new MySqlCommand("show columns from rrhh", conectar);
            MySqlDataReader leer = Llenar.ExecuteReader();
            int limite = leer.FieldCount;
            int variable = 1;
            while (leer.Read())
            {
                string resultado = "result" + Convert.ToString(variable);
                if (leer.GetString(0) == resultado)
                {
                    lista.Add(leer.GetString(0));
                    variable++;
                }
            }

            for (int x = 0; x < lista.Count; x++)
            {
                comando = comando + lista.ToArray().GetValue(x) + " as ";
                comando = comando + "'" + listaNombres.ToArray().GetValue(x) + "', ";
                //rrhh.Columns.Add(Convert.ToString(listaNombres.ToArray().GetValue(x)), typeof(string));
            }
            comando = comando.Remove(comando.Length - 2);
            comando = comando + "from rrhh";


            MySqlCommand Llenar2 = new MySqlCommand(comando, BDComun.ObtenerConexion());
            MySqlDataAdapter da = new MySqlDataAdapter(Llenar2);
            DataTable registro = new DataTable();
            da.Fill(registro);

            return registro;
        }

    }
}
