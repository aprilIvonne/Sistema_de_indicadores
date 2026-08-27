using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InputKey;
using MySql.Data.MySqlClient;

namespace prueba
{
    public partial class FormCrearIndicadores : Form
    {
        public string area = "";
        public FormCrearIndicadores()
        {
            InitializeComponent();
            
        }
        private void FormCrearIndicadores_Load(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
        }
        
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
           int numero = 0;
            string frecuenciaMedicion = "";
            string resultado = "result";
            string var = txtValor1.Text;
            string nombre = "";
            for (int x = 0; x < var.Length; x++)
            {
                string parte = var.Substring(x, 1);
                if(parte == " ")
                {
                    nombre = nombre + "";
                }
                else
                {
                    nombre = nombre + parte;
                }
            }
            if(rdMensual.Checked == true)
            {
                frecuenciaMedicion = "mensual";
            }
            if (rdTrimestral.Checked == true)
            {
                frecuenciaMedicion = "trimestral";
            }
            if (rdSemestral.Checked == true)
            {
                frecuenciaMedicion = "semestral";
            }
            if (rdAnual.Checked == true)
            {
                frecuenciaMedicion = "anual";
            }

            if(opcion1.Checked == true)
            {
                ind1Ref1 = ">" + Ind1Ref1.Text;
                ind1Ref2 = Ind1Ref2.Text;
                ind1Ref3 = "<=" + Ind1Ref3.Text;
            }
            if(opcion2.Checked == true)
            {
                ind1Ref1 = "<" + textBox6.Text;
                ind1Ref2 = textBox5.Text;
                ind1Ref3 = ">=" + textBox4.Text;
            }
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand comando3 = new MySqlCommand(String.Format("select count(*) from  nombreindicadores where area like '%"+area+"%'"), conectar);
            MySqlDataReader leer = comando3.ExecuteReader();
            while (leer.Read())
            {
                numero = leer.GetInt32(0);
            }
            numero++;
            conectar.Close();
            conectar.Open();
            resultado = resultado + Convert.ToString(numero);
            string contra = InputDialog.mostrar("Al crear nuevos indicadores la base de datos se modifica por lo que necesita la contraseña de Administrador: ");
            if (contra == "1234")
            {
                try
                {
                    MySqlCommand comando = new MySqlCommand(String.Format("insert into nombreindicadores(orden, indicadores, area, ref1, ref2, ref3, ref4, frecuenciaMedicion, valor1, valor2, porcentaje, estado, creacion) values ('" + Convert.ToString(numero) + "','" + txtNombreInd1.Text + "', '" + area + "', '" + ind1Ref1 + "', '" + ind1Ref2 + "', '', '" + ind1Ref3 + "', '" + frecuenciaMedicion + "', '" + txtValor1.Text + "', '', '', 'activo', 'creado en " + dateTimePicker1.Text + "');"), conectar);
                    MySqlCommand comando2 = new MySqlCommand(String.Format("alter table " + area + " add column " + nombre.ToLower() + " varchar(15) not null default '', add column " + resultado + " varchar(15) not null default '';"), conectar);
                    comando.ExecuteNonQuery();
                    comando2.ExecuteNonQuery();
                    MessageBox.Show("Indicador ingresado correctamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta");
            }
            conectar.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        string ind1Ref1 = "", ind1Ref2 = "", ind1Ref3 = "";
        string ind2Ref1 = "", ind2Ref2 = "", ind2Ref3 = "", ind2Ref4 = "";
        
        public string varPorcentaje { get; set; }

        private void porcentaje_CheckedChanged(object sender, EventArgs e)
        {
            varPorcentaje = "%";
        }

        private void sinPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            varPorcentaje = "";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Ind1Ref1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Ind1Ref2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Ind1Ref1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(Ind1Ref1);
        }

        private void Ind1Ref2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(Ind1Ref2);
        }

        private void Ind1Ref3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(Ind1Ref3);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(textBox6);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(textBox5);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(textBox4);
        }

        private void ind2ref1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(ind2ref1);
        }

        private void ind2ref2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(ind2ref2);
        }

        private void ind2ref3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(ind2ref3);
        }

        private void ind2ref4_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(ind2ref4);
        }

        private void indDosref1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(indDosref1);
        }

        private void indDosref2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(indDosref2);
        }

        private void indDosref3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(indDosref3);
        }

        private void indDosref4_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClaseMaestra.SoloNumeros(e);
            ExcesoDeDatos(indDosref4);
        }

        private void PrimeraOpcion_CheckedChanged(object sender, EventArgs e)
        {
            ind2ref1.Enabled = true;
            ind2ref2.Enabled = true;
            ind2ref3.Enabled = true;
            ind2ref4.Enabled = true;
            indDosref1.Enabled = false;
            indDosref2.Enabled = false;
            indDosref3.Enabled = false;
            indDosref4.Enabled = false;

        }

        private void btnBuscarIndicadores_Click(object sender, EventArgs e)
        {
            
            MySqlConnection conectar = BDComun.ObtenerConexion();
            try
            {
                MySqlCommand comando = new MySqlCommand(String.Format("select cod, indicadores, area, ref1, ref2, ref3, ref4, frecuenciaMedicion, valor1, valor2, porcentaje, creacion, estado from nombreindicadores where area='"+area+"'"), conectar);
                MySqlDataAdapter ada = new MySqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                ada.Fill(tabla);
                TablaIndicadores buscar = new TablaIndicadores(tabla);
                buscar.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conectar.Close();
            
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void rdAlmacen_CheckedChanged(object sender, EventArgs e)
        {
            area = "almacen";
            groupBox6.Enabled = true;
        }

        private void rdRRHH_CheckedChanged(object sender, EventArgs e)
        {
            area = "rrhh";
            groupBox6.Enabled = true;
        }

        private void rdContabilidad_CheckedChanged(object sender, EventArgs e)
        {
            area = "contabilidad";
            groupBox6.Enabled = true;
        }

        private void rdCredito_CheckedChanged(object sender, EventArgs e)
        {
            area = "credito";
            groupBox6.Enabled = true;
        }

        private void rdCompras_CheckedChanged(object sender, EventArgs e)
        {
            area = "compras";
            groupBox6.Enabled = true;
        }

        private void rdTecnologias_CheckedChanged(object sender, EventArgs e)
        {
            area = "tecnologias";
            groupBox6.Enabled = true;
        }

        private void rdVentas_CheckedChanged(object sender, EventArgs e)
        {
            area = "ventas";
            groupBox6.Enabled = true;
        }

        private void indDosref3_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void SegundaOpcion_CheckedChanged(object sender, EventArgs e)
        {
            ind2ref1.Enabled = false;
            ind2ref2.Enabled = false;
            ind2ref3.Enabled = false;
            ind2ref4.Enabled = false;
            indDosref1.Enabled = true;
            indDosref2.Enabled = true;
            indDosref3.Enabled = true;
            indDosref4.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int numero = 0;
            string frecuenciaMedicion = "";
            string resultado = "result";
            string var = txtInd2Valor1.Text;
            string nombre = "";
            string var2 = txtInd2Valor2.Text;
            string nombre2 = "";
            for (int x = 0; x < var.Length; x++)
            {
                string parte = var.Substring(x, 1);
                if (parte == " ")
                {
                    nombre = nombre + "";
                }
                else
                {
                    nombre = nombre + parte;
                }
            }
            for (int x = 0; x < var2.Length; x++)
            {
                string parte = var2.Substring(x, 1);
                if (parte == " ")
                {
                    nombre2 = nombre2 + "";
                }
                else
                {
                    nombre2 = nombre2 + parte;
                }
            }
            if (rd2Mensual.Checked == true)
            {
                frecuenciaMedicion = "mensual";
            }
            if (rd2Trimestral.Checked == true)
            {
                frecuenciaMedicion = "trimestral";
            }
            if (rd2Semestral.Checked == true)
            {
                frecuenciaMedicion = "semestral";
            }
            if (rd2Anual.Checked == true)
            {
                frecuenciaMedicion = "anual";
            }
            if (PrimeraOpcion.Checked == true)
            {
                ind2Ref1 = ">" + ind2ref1.Text;
                ind2Ref2 = "<=" + ind2ref2.Text;
                ind2Ref3 = ">=" + ind2ref3.Text;
                ind2Ref4 = "<=" + ind2ref4.Text;
            }
            if (SegundaOpcion.Checked == true)
            {
                ind2Ref1 = "<" + indDosref1.Text;
                ind2Ref2 = ">=" + indDosref2.Text;
                ind2Ref3 = "<=" + indDosref3.Text;
                ind2Ref4 = ">=" + indDosref4.Text;
            }
            MySqlConnection conectar = BDComun.ObtenerConexion();

            MySqlCommand comando3 = new MySqlCommand(String.Format("select count(*) from  nombreindicadores where area like '%" + area + "%';"), conectar);
            MySqlDataReader leer = comando3.ExecuteReader();
            while (leer.Read())
            {
                numero = leer.GetInt32(0);
            }
            numero++;
            resultado = resultado + Convert.ToString(numero);
            string contra = InputDialog.mostrar("Al crear nuevos indicadores la base de datos se modifica por lo que necesita la contraseña de Administrador: ");
            if (contra == "1234")
            {
                try
                {
                    MySqlCommand comando = new MySqlCommand(String.Format("insert into nombreindicadores(orden, indicadores, area, ref1, ref2, ref3, ref4, frecuenciaMedicion, valor1, valor2, porcentaje, estado, creacion) values ('" + numero.ToString() + "','" + txtInd2Nombre.Text + "', '" + area + "', '" + ind2Ref1 + "', '" + ind2Ref2 + "', '" + ind2Ref3 + "', '" + ind2Ref4 + "', '" + frecuenciaMedicion + "', '" + txtInd2Valor1.Text + "', '" + txtInd2Valor2.Text + "', '" + varPorcentaje + "', 'activo', 'creado en " + dateTimePicker1.Text + "');"), conectar);
                    conectar.Close();
                    conectar.Open();
                    MySqlCommand comando2 = new MySqlCommand(String.Format("alter table " + area + " add column " + nombre.ToLower() + " varchar(15) not null default '', add column " + nombre2.ToLower() + " varchar(15) not null default '', add column " + resultado + " varchar(15) not null default '';"), conectar);
                    comando.ExecuteNonQuery();
                    comando2.ExecuteNonQuery();
                    MessageBox.Show("Indicador ingresado correctamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta");
            }
            conectar.Close();
        }

        private void opcion1_CheckedChanged(object sender, EventArgs e)
        {
            Ind1Ref1.Enabled = true;
            Ind1Ref2.Enabled = true;
            Ind1Ref3.Enabled = true;
            textBox6.Enabled = false;
            textBox5.Enabled = false;
            textBox4.Enabled = false;
        }

        private void opcion2_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = true;
            textBox5.Enabled = true;
            textBox4.Enabled = true;
            Ind1Ref1.Enabled = false;
            Ind1Ref2.Enabled = false;
            Ind1Ref3.Enabled = false;
        }

        public void ExcesoDeDatos(TextBox name)
        {
            while (name.Text.Length >= 4)
            {
                name.Text = name.Text.Remove(name.Text.Length - 1);
                MessageBox.Show("Sólo se admiten hasta 3 dígitos", "Restricción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
