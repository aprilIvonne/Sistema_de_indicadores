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
using Excel = Microsoft.Office.Interop.Excel;
using SpreadsheetLight;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace prueba
{
    public partial class TablaReporteMes : MaterialForm
    {
        public static string[] arregloCodigos = new string[100];
        public static string mes = "";
        public static string año = "";
        public static string area = "";
        public static int contando = 0;
        public TablaReporteMes()
        {
            InitializeComponent();
        }
        public DataTable Consulta()
        {
            List<string> listaNombres = new List<string>();
            List<string> lista = new List<string>();
            string comando = "select ";
            MySqlConnection conectar = BDComun.ObtenerConexion();

            string[] areas = new string[7];
            areas[0] = "rrhh";
            areas[1] = "tecnologias";
            areas[2] = "ventas";
            areas[3] = "compras";
            areas[4] = "almacen";
            areas[5] = "credito";
            areas[6] = "contabilidad";
            if(cmbMES.Text == "")
            {
                DataTable tabla = new DataTable();
                conectar.Close();
                return tabla;
            }
            else
            {
                for (int x = 0; x < 7; x++)
                {
                    comando = comando + ContieneMes(areas[x], mes);
                }
                if(comando == "select ")
                {
                    DataTable tabla1 = new DataTable();
                    conectar.Close();
                    return tabla1;
                }
                else
                {
                    comando = comando.Remove(comando.Length - 2);
                    comando = comando + " from ";
                    for (int p = 0; p < 7; p++)
                    {
                        if (validaMes(areas[p]))
                        {
                            comando = comando + areas[p] + " inner join ";
                        }
                    }
                    comando = comando.Remove(comando.Length - 12);
                    comando = comando + " where ";
                    for (int p = 0; p < 7; p++)
                    {
                        if (validaMes(areas[p]))
                        {
                            comando = comando + " " + areas[p] + ".mes='" + mes + "' and ";
                        }
                    }
                    comando = comando.Remove(comando.Length - 5);
                    comando = comando + " and";
                    for (int p = 0; p < 7; p++)
                    {
                        if (validaMes(areas[p]))
                        {
                            comando = comando + " " + areas[p] + ".año='" + año + "' and ";
                        }
                    }
                    comando = comando.Remove(comando.Length - 5);
                    MySqlCommand final = new MySqlCommand(comando, BDComun.ObtenerConexion());
                    MySqlDataAdapter da = new MySqlDataAdapter(final);
                    DataTable registro = new DataTable();
                    da.Fill(registro);
                    int cont = 0;
                    List<string> Nombres = new List<string>();
                    List<string> Filas = new List<string>();
                    foreach (DataColumn colummn in registro.Columns)
                    {
                        Nombres.Add(Convert.ToString(colummn));
                        cont++;
                    }
                    for (int c = 0; c < registro.Columns.Count; c++)
                    {
                        foreach (DataRow row in registro.Rows)
                        {
                            Filas.Add(Convert.ToString(row[c]));
                        }
                    }

                    DataTable tabla = new DataTable();
                    DataColumn column1 = new DataColumn();
                    column1.ColumnName = "INDICADORES";
                    tabla.Columns.Add(column1);
                    DataColumn column2 = new DataColumn();
                    column2.ColumnName = "VALORES";
                    tabla.Columns.Add(column2);
                    DataColumn column3 = new DataColumn();
                    column3.ColumnName = "EVALUACION";
                    tabla.Columns.Add(column3);
                    DataColumn column4 = new DataColumn();
                    column4.ColumnName = "AREA";
                    tabla.Columns.Add(column4);
                    List<string> evaluacion1 = new List<string>();
                    List<string> area1 = new List<string>();
                    for (int x = 0; x < Filas.Count; x++)
                    {
                        string varia = "";
                        if (Filas.ToArray().GetValue(x).ToString() == "")
                        {
                            MySqlCommand comand = new MySqlCommand(String.Format("select frecuenciaMedicion from nombreindicadores where cod='{0}';", arregloCodigos[x]), conectar);
                            conectar.Close();
                            conectar.Open();
                            MySqlDataReader leyendo1 = comand.ExecuteReader();
                            while (leyendo1.Read())
                            {
                                varia = "Indicador " + leyendo1.GetString(0) + ", no se evalua en este mes";
                            }
                            evaluacion1.Add(varia);
                        }
                        else
                        {
                            evaluacion1.Add(evaluacion(Convert.ToDouble(Filas.ToArray().GetValue(x)), arregloCodigos[x]));
                        }
                        MySqlCommand comand2 = new MySqlCommand(String.Format("select area from nombreindicadores where cod='{0}';", arregloCodigos[x]), conectar);
                        conectar.Close();
                        conectar.Open();
                        MySqlDataReader leyendo2 = comand2.ExecuteReader();
                        leyendo2.Read();
                        area1.Add(leyendo2.GetString(0));
                    }
                    for (int y = 0; y < Filas.Count; y++)
                    {
                        tabla.Rows.Add(Nombres.ToArray().GetValue(y), Filas.ToArray().GetValue(y), evaluacion1.ToArray().GetValue(y), area1.ToArray().GetValue(y));
                    }
                    conectar.Close();
                    return tabla;
                }
                
            }
            
        }
        
        public static string ContieneMes(string area, string mes)
        {
            string retorno = "";
            string month = "";
            int num = 0;
            string[] arreglo = new string[100];
            List<string> listaNombres = new List<string>();
            List<string> lista = new List<string>();
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand("select mes from " + area + " where mes='" + mes + "' and año='"+año+"'", conectar);
            MySqlDataReader leer = comando.ExecuteReader();
            while (leer.Read())
            {
                month = leer.GetString(0);
            }
            conectar.Close();
            conectar.Open();

            if (month != "")
            {
                MySqlCommand comando1 = new MySqlCommand("select indicadores, cod, orden from nombreindicadores where area='" + area + "' and estado='activo'", conectar);
                MySqlDataReader leer1 = comando1.ExecuteReader();
                while (leer1.Read())
                {
                    listaNombres.Add(leer1.GetString(0));
                    arregloCodigos[contando] = leer1.GetString(1);
                    arreglo[num] = leer1.GetString(2);
                    contando++;
                    num++;
                }
                conectar.Close();
                conectar.Open();
                MySqlCommand Llenar = new MySqlCommand("show columns from " + area + ";", conectar);
                MySqlDataReader leer2 = Llenar.ExecuteReader();
                int variable = 0;
                while (leer2.Read())
                {
                    string resultado = "result" + arreglo[variable];
                    if (leer2.GetString(0) == resultado)
                    {
                        lista.Add(leer2.GetString(0));
                        variable++;
                    }
                }
                for (int x = 0; x < listaNombres.Count; x++)
                {
                    retorno = retorno + "" + area + "." + lista.ToArray().GetValue(x) + " as ";
                    retorno = retorno + "'" + listaNombres.ToArray().GetValue(x) + "', ";
                }
            }
            else if(month != mes)
            {
                retorno = retorno + "";
            }
            listaNombres.Clear();
            lista.Clear();
            conectar.Close();
            return retorno;
        }
        public static bool validaMes(string area)
        {
            bool existeMes = false;
            string month = "";
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand("select mes from " + area + " where mes='" + mes + "' and año='"+año+"'", conectar);
            MySqlDataReader leer = comando.ExecuteReader();
            while (leer.Read())
            {
                month = leer.GetString(0);
            }
            if(month == mes)
            {
                existeMes = true;
            }
            conectar.Close();
            return existeMes;
        }
        private void TablaReporteMes_Load(object sender, EventArgs e)
        {
            cmbMES.Items.Add("enero");
            cmbMES.Items.Add("febrero");
            cmbMES.Items.Add("marzo");
            cmbMES.Items.Add("abril");
            cmbMES.Items.Add("mayo");
            cmbMES.Items.Add("junio");
            cmbMES.Items.Add("julio");
            cmbMES.Items.Add("agosto");
            cmbMES.Items.Add("septiembre");
            cmbMES.Items.Add("octubre");
            cmbMES.Items.Add("noviembre");
            cmbMES.Items.Add("diciembre");
            cmbAREA.Items.Add("rrhh");
            cmbAREA.Items.Add("contabilidad");
            cmbAREA.Items.Add("credito");
            cmbAREA.Items.Add("compras");
            cmbAREA.Items.Add("almacen");
            cmbAREA.Items.Add("ventas");
            cmbAREA.Items.Add("tecnologias");
            for(int d = 2022; d < 2100; d++)
            {
                cmbAño.Items.Add(Convert.ToString(d));
            }
            
        }

        private void ExportarDGV(DataGridView grd)
        {
            grd.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            grd.MultiSelect = true;
            grd.SelectAll();
            DataObject dataObj = grd.GetClipboardContent();
            if (dataObj != null) Clipboard.SetDataObject(dataObj);
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Excel.Range rango = (Excel.Range)xlWorkSheet.Cells[1, 1];
            rango.Select();
            xlWorkSheet.Name = "Reporte del Mes de "+ cmbMES.Text.ToUpper();

            xlWorkSheet.PasteSpecial(rango, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            //la primera fila en negrita, centrada y con fondo gris
            Excel.Range fila1 = (Excel.Range)xlWorkSheet.Rows[1];
            fila1.Select();
            fila1.EntireRow.Font.Bold = true;
            fila1.EntireRow.Font.Color = Color.White;
            fila1.EntireRow.HorizontalAlignment = HorizontalAlignment.Center;
            fila1.EntireRow.Interior.Color = Color.Gray;
            //si la primera celda de la primera columna está vacía, elimino la primera columna
            //esto se puede omitir, pero lo dejo para ver cómo se podrían añadir/eliminar datos a posteriori


            Excel.Range c1f1 = (Excel.Range)xlWorkSheet.Cells[1, 1];
            if (c1f1.Text == "")
            {
                Excel.Range columna1 = (Excel.Range)xlWorkSheet.Columns[1];
                columna1.Select();
                columna1.Delete();
            }
            //selecciono la primera celda de la primera columna
            Excel.Range c1 = (Excel.Range)xlWorkSheet.Cells[1, 1];
            c1.Select();
            grd.ClearSelection();
            grd.MultiSelect = false;
        }
        public static string evaluacion(double resultado, string codigo)
        {
            string evaluacion = "";
            MySqlConnection conectar = BDComun.ObtenerConexion();
            string ref1 = "";
            string ref2 = "";
            string ref3 = "";
            string ref4 = "";
            string var = "";
            MySqlCommand comand = new MySqlCommand(String.Format("select ref1, ref2, ref3, ref4, frecuenciaMedicion from nombreindicadores where cod='{0}';", codigo), conectar);
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
                        evaluacion = "Por Mejorar";
                    }
                    else if (resultado >= Convert.ToDouble(referencia2) && resultado <= Convert.ToDouble(referencia3))
                    {
                        evaluacion = "Aceptable";
                    }
                    else if (resultado >= Convert.ToDouble(referencia4))
                    {
                        evaluacion= "Ideal";
                    }
                    break;
                case "mayor que":
                    if (resultado > Convert.ToDouble(referencia1))
                    {
                        evaluacion = "Por Mejorar";
                    }
                    else if (resultado <= Convert.ToDouble(referencia2) && resultado >= Convert.ToDouble(referencia3))
                    {
                        evaluacion= "Aceptable";
                    }
                    else if (resultado <= Convert.ToDouble(referencia4))
                    {
                        evaluacion = "Ideal";
                    }
                    break;
            }
            return evaluacion;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ExportarDGV(dataGridView1);
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            mes = cmbMES.Text;
            año = cmbAño.Text;
            lbMES.Text = "Reporte del Mes de " + cmbMES.Text.ToUpper();
            dataGridView1.DataSource = Consulta();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["EVALUACION"].Value.ToString() == "Por Mejorar")
                {
                    row.Cells["EVALUACION"].Style.BackColor = Color.Red;
                }
                else if (row.Cells["EVALUACION"].Value.ToString() == "Aceptable")
                {
                    row.Cells["EVALUACION"].Style.BackColor = Color.Yellow;
                }
                else if (row.Cells["EVALUACION"].Value.ToString() == "Ideal")
                {
                    row.Cells["EVALUACION"].Style.BackColor = Color.LawnGreen;
                }
            }
            arregloCodigos.ToList().RemoveRange(0, 100);
            contando = 0;
        }

        private void btnIndicadores_Click(object sender, EventArgs e)
        {
            FormGraficos objeto = new FormGraficos(cmbAREA.Text);
            objeto.Show();
        }
    }
}
