using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;

namespace prueba
{
    public partial class TablaUsuarios : MaterialForm
    {
        public TablaUsuarios()
        {
            InitializeComponent();
        }

        private void TablaUsuarios_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Consulta();
        }

        public static DataTable Consulta()
        {
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand Llenar = new MySqlCommand("select u_cod as 'Codigo de registro', u_usuario as 'Nombre de usuario', u_contraseña as 'Contraseña', u_tipo as 'Tipo de usuario' from login", conectar);
            MySqlDataAdapter da = new MySqlDataAdapter(Llenar);
            DataTable registro = new DataTable();
            da.Fill(registro);
            conectar.Close();
            return registro;
        }
        public ClaseUsuario seleccion { get; set; }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int linea = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                seleccion = Obtener(linea);
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila!");
            }
        }

        public static ClaseUsuario Obtener(int u_cod)
        {
            ClaseUsuario modelo = new ClaseUsuario();
            MySqlConnection conexion = BDComun.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand(String.Format("select * from login where u_cod='{0}'", u_cod), conexion);
            MySqlDataReader leer = comando.ExecuteReader();
            while (leer.Read())
            {
                modelo.codigo = leer.GetString(0);
                modelo.nom_usuario = leer.GetString(1);
                modelo.contraseña = leer.GetString(2);
                modelo.tipo = leer.GetString(3);

            }
            conexion.Close();
            return modelo;
        }
    }
}
