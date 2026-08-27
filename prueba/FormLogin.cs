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

namespace prueba
{
    public partial class FormLogin : MaterialForm
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usuario.Text))

            {
                errorProvider1.SetError(usuario, "Ingrese un usuario");
            }
            else if (string.IsNullOrEmpty(contraseña.Text))
            {
                errorProvider1.SetError(contraseña, "Ingrese una contraseña");
            }
            else
            {

                MySqlConnection conectar = BDComun.ObtenerConexion();
                DataTable tabla = new DataTable();
                MySqlCommand comando = new MySqlCommand(string.Format("select u_contraseña, u_tipo from login where u_usuario='{0}' and u_contraseña='{1}'", usuario.Text, contraseña.Text), conectar);
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                adaptador.Fill(tabla);
                conectar.Close();
                if (tabla.Rows.Count == 1)
                {
                    if (tabla.Rows[0][1].ToString() == "administrador")
                    {
                        Form menu = new Portada("admin");
                        menu.Show();
                    }
                    else if (tabla.Rows[0][1].ToString() == "almacen")
                    {
                        Form menu = new Portada("almacen");
                        menu.Show();
                    }
                    else if (tabla.Rows[0][1].ToString() == "rrhh")
                    {
                        Form menu = new Portada("rrhh");
                        menu.Show();
                    }
                    else if (tabla.Rows[0][1].ToString() == "ventas")
                    {
                        Form menu = new Portada("ventas");
                        menu.Show();
                    }
                    else if (tabla.Rows[0][1].ToString() == "compras")
                    {
                        Form menu = new Portada("compras");
                        menu.Show();
                    }
                    else if (tabla.Rows[0][1].ToString() == "tecnologia")
                    {
                        Form menu = new Portada("tecnologia");
                        menu.Show();
                    }
                    else if (tabla.Rows[0][1].ToString() == "credito")
                    {
                        Form menu = new Portada("credito");
                        menu.Show();
                    }
                    else if (tabla.Rows[0][1].ToString() == "contabilidad")
                    {
                        Form menu = new Portada("contabilidad");
                        menu.Show();
                    }
                    usuario.Clear();
                    contraseña.Clear();
                    errorProvider1.Clear();
                }
                else
                {
                    MessageBox.Show("Datos incorrectos", "Intente de nuevo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    usuario.Text = "";
                    contraseña.Text = "";
                    errorProvider1.Clear();

                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAgregarUsuarios objeto = new FormAgregarUsuarios();
            bool ExUsuario = objeto.ExisteUsuario(usuario.Text);
            if (usuario.Text == "")
            {
                errorProvider1.SetError(usuario, "Escriba el usuario del cual quiere cambiar la contraseña");
            }
            else if(ExUsuario == false)
            {
                errorProvider1.SetError(usuario, "Este usuario no existe, intente corregir o escribir otro usuario.");
            }
            else if(usuario.Text != "")
            {
                errorProvider1.Clear();
                FormCambiarContraseña nuevo = new FormCambiarContraseña(usuario.Text);
                nuevo.ShowDialog();
            }
        }

        private void usuario_Leave(object sender, EventArgs e)
        {
            FormAgregarUsuarios objeto = new FormAgregarUsuarios();
            bool ExUsuario = objeto.ExisteUsuario(usuario.Text);
            if(ExUsuario == false)
            {
                errorProvider1.SetError(usuario, "Este usuario no existe, intente corregir o escribir otro usuario.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
