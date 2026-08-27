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
    public partial class TablaIndicadores : MaterialForm
    {
        DataTable tabla;
        public TablaIndicadores(DataTable tb)
        {
            this.tabla = tb;
            InitializeComponent();
        }

        private void TablaIndicadores_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tabla;
        }


        private void btnDesactivar_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string linea = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                if (ClaseMaestra.Eliminar(linea) > 0)
                {
                    MessageBox.Show("Indicador Desactivado Correctamente", "Desactivar Indicador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TablaIndicadores_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("No se pudo desactivar el indicador", "Error al desactivar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila");
            }
        }

        private void btnActivar_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string linea = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                if (ClaseMaestra.Activar(linea) > 0)
                {
                    MessageBox.Show("Indicador Activado Correctamente", "Activar Indicador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo activar el indicador", "Error al Activar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila");
            }
            dataGridView1.DataSource = tabla;
        }
        
        

       
    }
}
