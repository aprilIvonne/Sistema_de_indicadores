using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace prueba
{
    public class BDComun
    {
        public static MySqlConnection ObtenerConexion()
        {
            //MySqlConnection conectar = new MySqlConnection("server=db4free.net; database=indicadoresmysql; Uid=mysqlcajero; password=e63ba6c0; SslMode=none; Port=3306;");
            MySqlConnection conectar = new MySqlConnection("server=your_server; Uid=your_uid; password=your_password; SslMode=none; Port=your_port; database=your_database");
            conectar.Open();
            return conectar;
        }
    }
}
