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

namespace prueba
{
    public partial class Portada : MaterialForm
    {
        string usuario = "";
        public Portada(string pusuario)
        {
            this.MinimizeBox = true;
            InitializeComponent();
            this.usuario = pusuario;
            this.usuario = pusuario;
            if (usuario == "almacen")
            {
                btnAgregar_u.Hide();
                btnContabilidad.Hide();
                btnInformatica.Hide();
                btnCompras.Hide();
                btnCredito.Hide();
                btnVentas.Hide();
                rrhh.Hide();
                btnReporte.Hide();
                btnCrearIndi.Hide();

            }
            else if (usuario == "compras")
            {
                btnAgregar_u.Hide();
                btnContabilidad.Hide();
                btnInformatica.Hide();
                btnAlmacen.Hide();
                btnCredito.Hide();
                btnVentas.Hide();
                rrhh.Hide();
                btnReporte.Hide();
                btnCrearIndi.Hide();

            }
            else if (usuario == "ventas")
            {
                btnAgregar_u.Hide();
                btnContabilidad.Hide();
                btnInformatica.Hide();
                btnAlmacen.Hide();
                btnCompras.Hide();
                btnCredito.Hide();
                rrhh.Hide();
                btnReporte.Hide();
                btnCrearIndi.Hide();
            }

            else if (usuario == "contabilidad")
            {
                btnAgregar_u.Hide();
                btnVentas.Hide();
                btnInformatica.Hide();
                btnAlmacen.Hide();
                btnCompras.Hide();
                btnCredito.Hide();
                rrhh.Hide();
                btnReporte.Hide();
                btnCrearIndi.Hide();
            }
            else if (usuario == "credito")
            {
                btnAgregar_u.Hide();
                btnContabilidad.Hide();
                btnInformatica.Hide();
                btnAlmacen.Hide();
                btnCompras.Hide();
                btnVentas.Hide();
                rrhh.Hide();
                btnReporte.Hide();
                btnCrearIndi.Hide();
            }
            else if (usuario == "tecnologia")
            {
                btnAgregar_u.Hide();
                btnContabilidad.Hide();
                btnVentas.Hide();
                btnAlmacen.Hide();
                btnCompras.Hide();
                btnCredito.Hide();
                rrhh.Hide();
                btnReporte.Hide();
                btnCrearIndi.Hide();
            }
            else if (usuario == "rrhh")
            {
                btnAgregar_u.Hide();
                btnContabilidad.Hide();
                btnInformatica.Hide();
                btnAlmacen.Hide();
                btnCompras.Hide();
                btnCredito.Hide();
                btnVentas.Hide();
                btnReporte.Hide();
                btnCrearIndi.Hide();
            }
        }

        private void Portada_Load(object sender, EventArgs e)
        {
        }

        private void btnAgregar_u_Click(object sender, EventArgs e)
        {
            FormAgregarUsuarios boton = new FormAgregarUsuarios();
            boton.Show();
        }

        private void btnCredito_Click(object sender, EventArgs e)
        {
            FormCredito boton = new FormCredito();
            boton.Show();
            
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            FormVentas nuevo = new FormVentas();
            nuevo.Show();
        }

        private void rrhh_Click(object sender, EventArgs e)
        {
            FormRRHH nuevo = new FormRRHH();
            nuevo.Show();
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            FormCompras nuevo = new FormCompras();
            nuevo.Show();
        }

        private void btnAlmacen_Click(object sender, EventArgs e)
        {
            FormAlmacen nuevo = new FormAlmacen();
            nuevo.Show();
        }

        private void btnContabilidad_Click(object sender, EventArgs e)
        {
            FormContabilidad nuevo = new FormContabilidad();
            nuevo.Show();
        }

        private void btnInformatica_Click(object sender, EventArgs e)
        {
            FormTecnologia nuevo = new FormTecnologia();
            nuevo.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TablaReporteMes nuevo = new TablaReporteMes();
            nuevo.ShowDialog();
        }

        private void btnCrearIndi_Click(object sender, EventArgs e)
        {
            FormCrearIndicadores nuevo = new FormCrearIndicadores();
            nuevo.Show();
        }
    }
}
