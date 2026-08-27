using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueba
{
    public class ClaseMaestra
    {

        public static Color txtcolor(double resultado, string codigo)
        {
            Color txtResColor = Color.White;
            MySqlConnection conectar = BDComun.ObtenerConexion();
            string ref1 = "";
            string ref2 = "";
            string ref3 = "";
            string ref4 = "";
            string var = "";
            MySqlCommand comand = new MySqlCommand(String.Format("select ref1, ref2, ref3, ref4 from nombreindicadores where cod='{0}';", codigo), conectar);
            MySqlDataReader leer = comand.ExecuteReader();
            while (leer.Read())
            {
                ref1 = leer.GetString(0);
                ref2 = leer.GetString(1);
                ref3 = leer.GetString(2);
                ref4 = leer.GetString(3);
            }
            conectar.Close();
            string parte = "";
            for (int x = 0; x < ref1.Length; x++)
            {
                parte = ref1.Substring(x, 1);
                if (parte == "<")
                {
                    var = "menor que";
                }
                else if (parte == ">")
                {
                    var = "mayor que";
                }
            }
            string referencia1 = "";
            if (ref1.Length >= 3)
            {
                referencia1 = ref1.Substring(1, 2);
            }
            else
            {
                referencia1 = ref1.Substring(1, 1);
            }
            string referencia2 = "";
            if (ref2.Length == 4)
            {
                referencia2 = ref2.Substring(2, 2);
            }
            else if (ref2.Length == 3)
            {
                referencia2 = ref2.Substring(2, 1);
            }
            else if (ref2.Length == 6)
            {
                referencia2 = ref2.Substring(2, 4);
            }
            else
            {
                referencia2 = ref2;
            }
            string referencia3 = "";
            if (ref3 != "" && ref3.Length == 2)
            {
                referencia3 = ref3.Substring(2, 2);
            }
            else if (ref3.Length == 4)
            {
                referencia3 = ref3.Substring(2, 2);
            }
            else if (ref3.Length == 6)
            {
                referencia3 = ref3.Substring(2, 4);
            }
            else if (ref3.Length == 7)
            {
                referencia3 = ref3.Substring(2, 5);
            }
            else
            {
                referencia3 = ref2;
            }
            string referencia4 = "";
            if (ref4.Length >= 4)
            {
                referencia4 = ref4.Substring(2, 2);
            }
            else if (ref4.Length == 3)
            {
                referencia4 = ref4.Substring(2, 1);
            }
            switch (var)
            {
                case "menor que":
                    if (resultado < Convert.ToDouble(referencia1))
                    {
                        txtResColor = Color.Tomato;
                    }
                    else if (resultado >= Convert.ToDouble(referencia2) && resultado <= Convert.ToDouble(referencia3))
                    {
                        txtResColor = Color.Yellow;
                    }
                    else if (resultado >= Convert.ToDouble(referencia4))
                    {
                        txtResColor = Color.LawnGreen;
                    }
                    break;
                case "mayor que":
                    if (resultado > Convert.ToDouble(referencia1))
                    {
                        txtResColor = Color.Tomato;
                    }
                    else if (resultado <= Convert.ToDouble(referencia2) && resultado >= Convert.ToDouble(referencia3))
                    {
                        txtResColor = Color.Yellow;
                    }
                    else if (resultado <= Convert.ToDouble(referencia4))
                    {
                        txtResColor = Color.LawnGreen;
                    }
                    break;
            }
            return txtResColor;
        }

        public static void MesValidacion(string frecuenciaMedicion, Control txt, string mes)
        {
            if (frecuenciaMedicion == "mensual")
            {
                txt.Enabled = true;
            }
            else if (mes == "diciembre" && frecuenciaMedicion == "anual")
            {
                txt.Enabled = true;
            }
            else if ((mes == "junio" || mes == "diciembre") && frecuenciaMedicion == "semestral")
            {
                txt.Enabled = true;
            }

            else if ((mes == "marzo" || mes == "junio" || mes == "septiembre" || mes == "diciembre") && frecuenciaMedicion == "trimestral")
            {
                txt.Enabled = true;
            }
            else
            {
                txt.Enabled = false;
                txt.Text = "";
            }
        }
        public static void ValidarVacios(Control txt)
        {
            if (txt.Enabled == true && txt.Text == "")
            {
                txt.Text = "0";
            }
        }

        public static void ExcesoDeDatos(TextBox name)
        {
            while (name.Text.Length >= 14)
            {
                name.Text = name.Text.Remove(name.Text.Length - 1);
                MessageBox.Show("Sólo se admiten hasta 15 dígitos", "Restricción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static string consultaMes(string nombre)
        {
            string var = "";
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand("select frecuenciaMedicion from nombreindicadores where valor1='" + nombre + "' or valor2='" + nombre + "' and estado ='activo'", conectar);
            MySqlDataReader leer2 = comando.ExecuteReader();
            while (leer2.Read())
            {
                var = leer2.GetString(0);
            }
            conectar.Close();
            return var;
        }
        public static string funcionmes(string area)
        {
            MySqlCommand comand = new MySqlCommand(String.Format("select mes from "+area+" order by cod desc limit 1"), BDComun.ObtenerConexion());
            MySqlDataReader leer = comand.ExecuteReader();
            string var = "";
            while (leer.Read())
            {
                var = leer.GetString(0);
            }
            return var;
        }

        public static string valida(string area)
        {
            string mes = funcionmes(area);
            string retorno = "";
            switch (mes)
            {
                case "enero":
                    retorno = "febrero";
                    break;
                case "febrero":
                    retorno = "marzo";
                    break;
                case "marzo":
                    retorno = "abril";
                    break;
                case "abril":
                    retorno = "mayo";
                    break;
                case "mayo":
                    retorno = "junio";
                    break;
                case "junio":
                    retorno = "julio";
                    break;
                case "julio":
                    retorno = "agosto";
                    break;
                case "agosto":
                    retorno = "septiembre";
                    break;
                case "septiembre":
                    retorno = "octubre";
                    break;
                case "octubre":
                    retorno = "noviembre";
                    break;
                case "noviembre":
                    retorno = "diciembre";
                    break;
                case "diciembre":
                    retorno = "enero";
                    break;

            }
            if (mes == "")
            {
                retorno = "enero";
            }
            return retorno;
        }
        public static void SoloNumeros(KeyPressEventArgs v)
        {
            if (Char.IsDigit(v.KeyChar))
            {
                v.Handled = false;
            }
            else if (Char.IsSeparator(v.KeyChar))
            {
                v.Handled = true;
            }
            else if (Char.IsControl(v.KeyChar))
            {
                v.Handled = false;
            }
            else if(Char.IsPunctuation(v.KeyChar))
            {
                v.Handled = false;
            }
            else
            {
                v.Handled = true;
                MessageBox.Show("Entrada Inválida", "Sólo números", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static string funcionaño(string area)
        {
            MySqlCommand comand = new MySqlCommand(String.Format("select mes, año from "+ area +" order by cod desc limit 1"), BDComun.ObtenerConexion());
            MySqlDataReader leer = comand.ExecuteReader();
            string mes = "";
            string año = "";
            string retorno = "";
            while (leer.Read())
            {
                mes = leer.GetString(0);
                año = leer.GetString(1);
            }
            if (mes == "diciembre")
            {
                int var = Convert.ToInt32(año);
                var++;
                retorno = Convert.ToString(var);
            }
            else if (mes == "")
            {
                retorno = "2022";
            }
            else
            {
                retorno = año;
            }
            return retorno;
        }

        public static double validaDivision(double numerador, double denominador, string codigo)
        {
            ErrorProvider errorObjeto = new ErrorProvider();
            double result = 0;
            string porcentaje = "";
            MySqlCommand comando = new MySqlCommand("select porcentaje from nombreindicadores where cod='" + codigo + "'", BDComun.ObtenerConexion());
            MySqlDataReader leer = comando.ExecuteReader();
            while(leer.Read())
            {
                porcentaje = leer.GetString(0);
            }
            if(porcentaje == "%")
            {
                if (numerador != 0 && denominador == 0)
                {
                    MessageBox.Show("No se permite división entre 0", "Restricción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (numerador == 0 && denominador == 0)
                {
                    result = 0;
                }
                else
                {
                    result = (numerador / denominador) * 100;
                }
            }
            else
            {
                if (numerador != 0 && denominador == 0)
                {
                    MessageBox.Show("No se permite división entre 0", "Restricción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (numerador == 0 && denominador == 0)
                {
                    result = 0;
                }
                else
                {
                    result = numerador / denominador;
                }
            }
            return result;
        }
        public static int Activar(string cod)
        {
            int retorno = 0;

            MySqlConnection conexion = BDComun.ObtenerConexion();

            MySqlCommand comando = new MySqlCommand(String.Format("SET SQL_SAFE_UPDATES=0; Update nombreindicadores set estado='activo' where cod='{0}'", cod), conexion);

            retorno = comando.ExecuteNonQuery();
            conexion.Close();

            return retorno;
        }

        public static int Eliminar(string cod)
        {
            int retorno = 0;
            MySqlConnection conexion = BDComun.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand(String.Format("Update nombreindicadores set estado='inactivo' where cod= '{0}'", cod), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        

    }
}
