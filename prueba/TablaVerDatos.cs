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
using Microsoft.Reporting.WinForms;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;


namespace prueba
{
    public partial class TablaVerDatos : MaterialForm
    {
        public string area1 = "";
        string[] arregloCodigos = new string[100];
        public TablaVerDatos(string parea, string[] arreglo)
        {
            this.area1 = parea;
            this.arregloCodigos = arreglo;
            InitializeComponent();
        }
        private void TablaVerDatos_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Consulta();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int cont = 0;
                for (int x = 3; x < dataGridView1.Columns.Count; x++)
                {
                    if(row.Cells[x].Value.ToString() == "")
                    {
                        row.Cells[x].Style.BackColor = Color.White;
                        cont++;
                    }
                    else
                    {
                        row.Cells[x].Style.BackColor = ClaseMaestra.txtcolor(Convert.ToDouble(row.Cells[x].Value), arregloCodigos[cont]);
                        cont++;
                    }
                }
            }
        }
        public DataTable Consulta()
        {
            List<string> listaNombres = new List<string>();
            List<string> lista = new List<string>();
            string[] arreglo = new string[100];
            int num = 1;
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand comando1 = new MySqlCommand("select indicadores, orden from nombreindicadores where area='"+area1+"' and estado='activo'", conectar);
            MySqlDataReader leer1 = comando1.ExecuteReader();
            while (leer1.Read())
            {
                listaNombres.Add(leer1.GetString(0));
                arreglo[num] = leer1.GetString(1);
                num++;
            }
            conectar.Close();
            string comando = "select cod as 'Codigo de registro', mes as 'Mes', año as 'Año', ";
            conectar.Open();
            MySqlCommand Llenar = new MySqlCommand("show columns from "+ area1 +"", conectar);
            MySqlDataReader leer = Llenar.ExecuteReader();
            int variable = 1;
            while(leer.Read())
            {
                string resultado = "result" + arreglo[variable];
                if(leer.GetString(0) == resultado)
                {
                    lista.Add(leer.GetString(0));
                    variable++;
                }
            }
            for(int x = 0; x < lista.Count; x++)
            {
                comando = comando + lista.ToArray().GetValue(x) + " as ";
                comando = comando + "'" + listaNombres.ToArray().GetValue(x) + "', ";
            }
            comando = comando.Remove(comando.Length - 2);
            comando = comando + "from "+area1+"";
            MySqlCommand Llenar2 = new MySqlCommand(comando, BDComun.ObtenerConexion());
            MySqlDataAdapter da = new MySqlDataAdapter(Llenar2);
            DataTable registro = new DataTable();
            da.Fill(registro);
            conectar.Close();
            return registro;
        }
        public List<string> Obtener(string cod)
        {
            List<string> lista = new List<string>();
            MySqlConnection conexion = BDComun.ObtenerConexion();

            string select = "select cod, mes, año, ";
            MySqlCommand com = new MySqlCommand("select orden, valor1, valor2, estado from nombreindicadores where area='"+area1+"'", conexion);
            MySqlDataReader leyendo = com.ExecuteReader();
            while(leyendo.Read())
            {
                if(leyendo.GetString(3) == "activo" && leyendo.GetString(2) != "")
                {
                    select = select + leyendo.GetString(1).ToLower().Replace(" ", "") + "," + leyendo.GetString(2).ToLower().Replace(" ", "") + "," + "result" + leyendo.GetString(0) + ",";
                }
                else if(leyendo.GetString(3) == "activo" && leyendo.GetString(2) == "")
                {
                    select = select + leyendo.GetString(1).ToLower().Replace(" ", "") + "," + "result" + leyendo.GetString(0) + ",";
                }
            }
            select = select.Remove(select.Length - 1);
            select = select + " from " + area1 + " where cod='"+cod+"';";
            conexion.Close();
            conexion.Open();
            MySqlCommand comando = new MySqlCommand(select, conexion);
            MySqlDataReader leer = comando.ExecuteReader();
            for (int x = 0; x < leer.FieldCount; x++)
            {
                leer.Read();
                if (leer.GetValue(x) == null)
                {
                    lista.Add("");
                }
                else
                {
                    lista.Add(leer.GetString(x));
                }
            }
            /*MySqlCommand comando = new MySqlCommand("select * from " + area1 + " where cod='" + cod + "'", conexion);
            MySqlDataReader leer = comando.ExecuteReader();
            for (int x = 0; x < leer.FieldCount; x++)
            {
                leer.Read();
                if(leer.GetValue(x) == null)
                {
                    lista.Add("");
                }
                else
                {
                    lista.Add(leer.GetString(x));
                }
            }*/
            conexion.Close();
            return lista;
        }
        public List<string> seleccion { get; set; }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string linea = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
                seleccion = Obtener(linea);
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila!");
            }
        }
        private void btnReporte_Click(object sender, EventArgs e)
        {
            ExportarDGV(dataGridView1);
        }
        private void ExportarDGV(DataGridView grd)
        {
            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridView1.MultiSelect = true;
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
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
            dataGridView1.ClearSelection();
            dataGridView1.MultiSelect = false;
        }
    }
}
