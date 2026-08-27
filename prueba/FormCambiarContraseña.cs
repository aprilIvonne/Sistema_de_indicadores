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
    public partial class FormCambiarContraseña : MaterialForm
    {
        public string usuario;
        public FormCambiarContraseña(string pusuario)
        {
            this.usuario = pusuario;
            InitializeComponent();
        }

        private void FormCambiarContraseña_Load(object sender, EventArgs e)
        {
            lbUsuario.Text = usuario;
        }



        private void btnCambiar_Click(object sender, EventArgs e)
        {
            MySqlConnection conectar = BDComun.ObtenerConexion();
            string comando = "SET SQL_SAFE_UPDATES = 0; Update login set u_contraseña = '"+txtContra.Text+"' where u_usuario = '"+usuario+"'";
            DialogResult result = MessageBox.Show("¿Quiere confirmar para cambiar la contraseña?", "Cambiar", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                MySqlCommand cm = new MySqlCommand(comando, conectar);
                cm.ExecuteNonQuery();
                MessageBox.Show("Se ha modificado la contraseña.", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conectar.Close();
            }
            else if(result == DialogResult.No)
            {
                MessageBox.Show("No se pudo cambiar la contraseña.", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            txtContra.Text = "";
        }
    }
            
}
