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
using MaterialSkin.Controls;

namespace prueba
{
    public partial class FormAgregarUsuarios : MaterialForm
    {
        public FormAgregarUsuarios()
        {
            InitializeComponent();
        }

        bool validar()
        {
            if (String.IsNullOrEmpty(txtUsuario.Text))
            {
                error.SetError(txtUsuario, "Ingrese un nombre de usuario.");
                return false;
            }
            else if (String.IsNullOrEmpty(txtContra.Text))
            {
                error.SetError(txtContra, "Ingrese una contraseña.");
                return false;
            }
            else if (rdAlmacen.Checked == false && rdCompras.Checked == false && rdContabilidad.Checked == false && rdCredito.Checked == false && rdRRHH.Checked == false && rdTecnologia.Checked == false && rdVentas.Checked == false)
            {
                error.SetError(rdAlmacen, "Ingrese un tipo de usuario.");
                return false;
            }
            else if (txtContra.Text != txtRepetir.Text)
            {
                error.SetError(txtRepetir, "Contraseñas no coinciden.");
                return false;
            }
            return true;
        }
       

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MySqlConnection conectar = BDComun.ObtenerConexion();
            if (validar())
            {
                string Tipo = validartipo();
                try
                {
                    if(ExisteUsuario(txtUsuario.Text) || ExisteContraseña(txtContra.Text))
                    {
                        MessageBox.Show("Este usuario o contraseña ya existen");
                    }
                    else
                    {
                        MySqlCommand comando = new MySqlCommand("insert into login(u_usuario, u_contraseña, u_tipo) values('" + txtUsuario.Text + "', '" + txtContra.Text + "', '" + Tipo + "')", conectar);
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Usuario ingresado correctamente");
                        limpiar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            conectar.Close();
        }

        public bool ExisteUsuario(string usuario)
        {
            bool retorno = false;
            string user = "";
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand("select u_usuario from login where u_usuario='" + usuario+"'", conectar);
            MySqlDataReader leer = comando.ExecuteReader();
            while(leer.Read())
            {
                user = leer.GetString(0);
            }
            conectar.Close();
            if(user == usuario)
            {
                return true;
            }
            return retorno;
        }
        bool ExisteContraseña(string contraseña)
        {
            bool retorno = false;
            string password = "";
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand("select u_contraseña from login where u_contraseña='" + contraseña + "'", conectar);
            MySqlDataReader leer = comando.ExecuteReader();
            while (leer.Read())
            {
                password= leer.GetString(0);
            }
            conectar.Close();
            if (password == contraseña)
            {
                return true;
            }
            return retorno;
        }


        string validartipo()
        {
            string tipoUsuario = "";
            if (rdAlmacen.Checked == true)
            {
                tipoUsuario = "almacen";
            }
            else if (rdCompras.Checked == true)
            {
                tipoUsuario = "compras";
            }
            else if (rdContabilidad.Checked == true)
            {
                tipoUsuario = "contabilidad";
            }
            else if (rdCredito.Checked == true)
            {
                tipoUsuario = "credito";
            }
            else if (rdRRHH.Checked == true)
            {
                tipoUsuario = "rrhh";
            }
            else if (rdTecnologia.Checked == true)
            {
                tipoUsuario = "tecnologia";
            }
            else if (rdVentas.Checked == true)
            {
                tipoUsuario = "ventas";
            }
            else if (rdAdmin.Checked == true)
            {
                tipoUsuario = "administrador";
            }
            return tipoUsuario;
        }

        void limpiar()
        {
            rdAlmacen.Checked = false;
            rdTecnologia.Checked = false;
            rdRRHH.Checked = false;
            rdCredito.Checked = false;
            rdContabilidad.Checked = false;
            rdCompras.Checked = false;
            rdVentas.Checked = false;
            txtUsuario.Clear();
            txtRepetir.Clear();
            txtContra.Clear();
            error.Clear();

        }

        public ClaseUsuario actual { get; set; }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            MySqlConnection conectar = BDComun.ObtenerConexion();
            if (validar())
            {
                try
                {
                    string tipo = validartipo();
                    MySqlCommand comando = new MySqlCommand("SET SQL_SAFE_UPDATES=0; update login set u_usuario='" + txtUsuario.Text + "', u_contraseña='" + txtContra.Text + "', u_tipo='" + tipo + "' where u_cod= '" + actual.codigo + "';", conectar);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Usuario actualizado exitosamente!");
                    limpiar();
                    btnAgregar.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            conectar.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            TablaUsuarios buscar = new TablaUsuarios();
            buscar.ShowDialog();

            if (buscar.seleccion != null)
            {
                actual = buscar.seleccion;
                txtUsuario.Text = buscar.seleccion.nom_usuario;
                txtContra.Text = buscar.seleccion.contraseña;
                switch (buscar.seleccion.tipo)
                {
                    case "almacen":
                        rdAlmacen.Checked = true;
                        break;
                    case "compras":
                        rdCompras.Checked = true;
                        break;
                    case "contabilidad":
                        rdContabilidad.Checked = true;
                        break;
                    case "credito":
                        rdCredito.Checked = true;
                        break;
                    case "rrhh":
                        rdRRHH.Checked = true;
                        break;
                    case "tecnologia":
                        rdTecnologia.Checked = true;
                        break;
                    case "ventas":
                        rdVentas.Checked = true;
                        break;
                    case "administrador":
                        rdAdmin.Checked = true;
                        break;
                }
                btnAgregar.Hide();
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if(ExisteUsuario(txtUsuario.Text))
            {
                error.SetError(txtUsuario, "Este usuario ya existe, modifíquelo");
            }
            else
            {
                error.Clear();
            }
        }

        private void txtContra_Leave(object sender, EventArgs e)
        {
            if (ExisteContraseña(txtContra.Text))
            {
                errorProvider1.SetError(txtContra, "Esta contraseña ya existe, modifíquela");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void FormAgregarUsuarios_Load(object sender, EventArgs e)
        {

        }
    }
}
