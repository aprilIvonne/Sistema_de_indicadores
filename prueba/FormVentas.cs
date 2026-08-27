using MySql.Data.MySqlClient;
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

namespace prueba
{
    public partial class FormVentas : Form
    {
        public FormVentas()
        {
            InitializeComponent();
        }
        public string codigo { get; set; }
        string[] arregloCodigos = new string[100];

        private void FormVentas_Load(object sender, EventArgs e)
        {
            interfazConClases();
            string area = "ventas";
            ClaseMaestra.valida(area);
            Button btnAgregar = new Button();
            btnAgregar.Width = 100;
            btnAgregar.Height = 40;
            btnAgregar.Font = new Font(btnAgregar.Font.Name, 12);
            btnAgregar.Location = new Point(20, 570);
            btnAgregar.Text = "Agregar";
            this.Controls.Add(btnAgregar);

            Button btnBuscar = new Button();
            btnBuscar.Width = 100;
            btnBuscar.Height = 40;
            btnBuscar.Font = new Font(btnBuscar.Font.Name, 12);
            btnBuscar.Location = new Point(130, 570);
            btnBuscar.Text = "Ver Datos";
            this.Controls.Add(btnBuscar);

            Button btnCalcular = new Button();
            btnCalcular.Width = 100;
            btnCalcular.Height = 40;
            btnCalcular.Font = new Font(btnCalcular.Font.Name, 12);
            btnCalcular.Location = new Point(240, 570);
            btnCalcular.Text = "Calcular";
            this.Controls.Add(btnCalcular);

            Button btnActualizar = new Button();
            btnActualizar.Width = 100;
            btnActualizar.Height = 40;
            btnActualizar.Font = new Font(btnActualizar.Font.Name, 12);
            btnActualizar.Location = new Point(350, 570);
            btnActualizar.Text = "Actualizar";
            this.Controls.Add(btnActualizar);
            void AgregarHandler(object send, EventArgs eve)
            {
                List<Control> ControlList = new List<Control>();
                foreach (Control c in this.panel1.Controls)
                {
                    if (c is TextBox)
                    {
                        ControlList.Add(c);
                    }
                }
                CalcularHandler(send, eve);
                Agregar(ControlList, area);
                panel1.Controls.Clear();
                FormVentas_Load(sender, e);
            }
            void ActualizarHandler(object send, EventArgs eve)
            {
                List<Control> ControlList = new List<Control>();
                foreach (Control c in this.panel1.Controls)
                {
                    if (c is TextBox)
                    {
                        ControlList.Add(c);
                    }
                }
                CalcularHandler(send, eve);
                Update(ControlList, area, codigo);
                panel1.Controls.Clear();
                FormVentas_Load(sender, e);
            }
            btnCalcular.Click += new EventHandler(CalcularHandler);
            btnBuscar.Click += new EventHandler(BuscarHandler);
            btnAgregar.Click += new EventHandler(AgregarHandler);
            btnActualizar.Click += new EventHandler(ActualizarHandler);
        }

        void BuscarHandler(object send, EventArgs eve)
        {
            TablaVerDatos buscar = new TablaVerDatos("ventas", arregloCodigos);
            buscar.ShowDialog();
            if (buscar.seleccion != null)
            {
                string var = "";
                int cont = 0;
                codigo = buscar.seleccion.ToArray()[0];
                buscar.seleccion.RemoveAt(0);
                foreach (Control txt in this.panel1.Controls)
                {
                    if (txt is TextBox)
                    {
                        if (!txt.Name.StartsWith("result"))
                        {
                            var = ClaseMaestra.consultaMes(txt.Name);
                            ClaseMaestra.MesValidacion(var, txt, buscar.seleccion.ToArray().GetValue(0).ToString());
                        }
                        txt.Text = buscar.seleccion.ToArray().GetValue(cont).ToString();
                        cont++;
                    }
                }
            }
        }

        void CalcularHandler(object send, EventArgs eve)
        {
            double result = 0;
            double num1 = 0;
            double num2 = 0;
            string unValor = "";
            int cont = 0;
            bool vacio = false;
            bool indicadorUnValor = false;
            foreach (Control txt in this.panel1.Controls)
            {
                if (txt is TextBox)
                {
                    if (txt.BackColor == Color.Snow)
                    {
                        if (txt.Text != "")
                        {
                            unValor = txt.Text;
                            indicadorUnValor = true;
                        }
                        else
                        {
                            vacio = true;
                            txt.Text = "";
                        }
                    }
                    else if (txt.BackColor == Color.White)
                    {
                        if (txt.Text != "")
                            num1 = Convert.ToDouble(txt.Text);

                        else
                        {
                            vacio = true;
                            txt.Text = "";
                        }
                    }
                    else if (txt.BackColor == Color.WhiteSmoke)
                    {
                        if (txt.Text != "")
                            num2 = Convert.ToDouble(txt.Text);
                        else
                            txt.Text = "";
                    }
                    else if (txt.Name.StartsWith("result"))
                    {
                        if (indicadorUnValor == true && !vacio)
                        {
                            result = Convert.ToDouble(unValor);
                            txt.Text = String.Format("{0:f2}", result);
                            indicadorUnValor = false;
                        }
                        else if (!vacio)
                        {
                            result = ClaseMaestra.validaDivision(num1, num2, arregloCodigos[cont]);
                            txt.Text = String.Format("{0:f2}", result);
                        }
                        else if (vacio)
                        {
                            result = 0;
                            txt.Text = "";
                            vacio = false;

                        }
                        txt.BackColor = ClaseMaestra.txtcolor(result, arregloCodigos[cont]);
                        cont++;
                    }
                }
            }

        }
        void Agregar(List<Control> ControlList, string area)
        {
            if(codigo == null)
            {
                MySqlConnection conectar = BDComun.ObtenerConexion();
                List<string> lista = new List<string>();
                List<string> lista2 = new List<string>();
                string insert = "insert into " + area + " (";
                MySqlCommand Llenar = new MySqlCommand("show columns from " + area + "", BDComun.ObtenerConexion());
                MySqlDataAdapter da = new MySqlDataAdapter(Llenar);
                DataTable tabla = new DataTable();
                da.Fill(tabla);

                /*foreach (DataRow fila in tabla.Rows)
                {
                    lista.Add(Convert.ToString(fila[0]));
                }
                lista.Remove("cod");*/

                foreach (Control txt in ControlList)
                {
                    if (txt is TextBox)
                    {
                        lista.Add(txt.Name.ToLower().Replace(" ", ""));
                    }
                }

                foreach (Control txt in ControlList)
                {
                    if (txt is TextBox)
                    {
                        ClaseMaestra.ValidarVacios(txt);
                        lista2.Add(txt.Text);
                    }
                }
                foreach (string item in lista)
                {
                    insert = insert + item + ",";
                }
                insert = insert.Remove(insert.Length - 1);
                insert = insert + ") values (";
                foreach (string var in lista2)
                {
                    insert = insert + "'" + var + "', ";
                }
                insert = insert.Remove(insert.Length - 2);
                insert = insert + ")";
                DialogResult result = MessageBox.Show("¿Quiere confirmar para agregar los datos?", "Agregar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MySqlCommand comando = new MySqlCommand(insert, conectar);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Indicadores Guardados Con Exito!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == DialogResult.No)
                {
                    MessageBox.Show("No se pudo guardar el resgistro de indicadores", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                conectar.Close();
            }
            else
            {
                MessageBox.Show("No se pueden agregar datos mientras se actualizan los indicadores", "Actualizando datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        void Update(List<Control> ControlList, string area, string cod)
        {
            string comando = "SET SQL_SAFE_UPDATES=0; Update "+area+" set ";
            List<string> lista = new List<string>();
            List<string> lista2 = new List<string>();
            MySqlConnection conectar = BDComun.ObtenerConexion();
            /*MySqlCommand Llenar = new MySqlCommand("show columns from " + area + "", BDComun.ObtenerConexion());
            MySqlDataReader leer = Llenar.ExecuteReader();
            while (leer.Read())
            {
                lista.Add(leer.GetString(0));
            }
            lista.Remove("cod");*/

            foreach (Control txt in ControlList)
            {
                if (txt is TextBox)
                {
                    lista.Add(txt.Name.ToLower().Replace(" ", ""));
                }
            }
            foreach (Control txt in ControlList)
            {
                if (txt is TextBox)
                {
                    lista2.Add(txt.Text);
                }
            }
            for (int x = 0; x < lista.Count; x++)
            {
                comando = comando + lista.ToArray().GetValue(x) + "='" + lista2.ToArray().GetValue(x) + "', ";
            }
            comando = comando.Remove(comando.Length - 2);
            comando = comando + " where cod ='" + cod + "'";

            if (codigo == null)
            {
                MessageBox.Show("¡Primero debe agregar los datos para poder actualizarlos!", "No existe registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string contra = InputDialog.mostrar("Introduzca la contraseña de Administrador: ");
                if (contra == "1234")
                {
                    MySqlCommand comando2 = new MySqlCommand(comando, conectar);
                    comando2.ExecuteNonQuery();
                    MessageBox.Show("Indicadores actualizados Con Exito!", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    codigo = null;
                }
                else
                {
                    codigo = null;
                    MessageBox.Show("Sólo el usuario que administra puede cambiar los datos", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            conectar.Close();
        }

        void interfazConClases()
        {
            string area = "ventas";
            int coorXNombre = 12, coorYNombre = 60, coorYValor = 90, coorXResultado = 560;
            int cont = 0;
            List<string> listaSeccion = new List<string>();
            MySqlConnection conectar = BDComun.ObtenerConexion();
            MySqlCommand Llenar = new MySqlCommand("select indicadores, ref1, ref2, ref3, ref4, valor1, valor2, frecuenciaMedicion, cod, porcentaje, estado from nombreindicadores where area like '%"+area+"%'", conectar);
            MySqlDataAdapter da = new MySqlDataAdapter(Llenar);
            DataTable tabla = new DataTable();
            da.Fill(tabla);
            Label labelMes = new Label();
            labelMes.Width = 60;
            labelMes.Font = new Font(labelMes.Font.Name, 12);
            labelMes.Location = new Point(12, 20);
            labelMes.Text = "Mes:";
            this.panel1.Controls.Add(labelMes);


            Label labelAño = new Label();
            labelAño.Width = 60;
            labelAño.Font = new Font(labelAño.Font.Name, 12);
            labelAño.Location = new Point(210, 20);
            labelAño.Text = "Año:";
            this.panel1.Controls.Add(labelAño);

            Label result = new Label();
            result.Width = 100;
            result.Font = new Font(result.Font.Name, 12);
            result.Text = "Resultados";
            result.Location = new Point(570, 20);
            this.panel1.Controls.Add(result);

            int xpormejorar = 730, xaceptable = 870, xideal = 1050;
            Label pormejorar = new Label();
            pormejorar.Width = 120;
            pormejorar.Font = new Font(pormejorar.Font.Name, 15);
            pormejorar.Text = "Por mejorar";
            pormejorar.BackColor = Color.Red;
            pormejorar.Location = new Point(xpormejorar, 60);
            this.panel1.Controls.Add(pormejorar);

            Label aceptable = new Label();
            aceptable.Width = 100;
            aceptable.Font = new Font(aceptable.Font.Name, 15);
            aceptable.Text = "Aceptable";
            aceptable.BackColor = Color.Yellow;
            aceptable.Location = new Point(xaceptable, 60);
            this.panel1.Controls.Add(aceptable);

            Label ideal = new Label();
            ideal.Width = 100;
            ideal.Font = new Font(ideal.Font.Name, 15);
            ideal.Text = "Ideal";
            ideal.BackColor = Color.Green;
            ideal.Location = new Point(xideal, 60);
            this.panel1.Controls.Add(ideal);

            int posicionx = 75;
            TextBox txtmes = new TextBox();
            txtmes.Width = 120;
            txtmes.Text = ClaseMaestra.valida(area);
            txtmes.Font = new Font(txtmes.Font.Name, 12);
            txtmes.Location = new Point(posicionx, 20);
            txtmes.Enabled = false;
            txtmes.Name = "mes";
            posicionx = posicionx + 200;
            this.panel1.Controls.Add(txtmes);

            TextBox txtaño = new TextBox();
            txtaño.Width = 120;
            txtaño.Text = ClaseMaestra.funcionaño(area);
            txtaño.Font = new Font(txtaño.Font.Name, 12);
            txtaño.Location = new Point(posicionx, 20);
            txtaño.Enabled = false;
            txtaño.Name = "año";
            posicionx = posicionx + 200;
            this.panel1.Controls.Add(txtaño);

            string varResult = "result";
            int contador = 1;
            foreach (DataRow fila in tabla.Rows)
            {
                string referPorMejorar = Convert.ToString(fila[1]);
                string referAceptable = Convert.ToString(fila[2]) + "  " + Convert.ToString(fila[3]);
                string referIdeal = Convert.ToString(fila[4]);
                string porcentaje = Convert.ToString(fila[9]);
                if (Convert.ToString(fila[6]) == "" && Convert.ToString(fila[10]) == "activo")
                {
                    Label nombreIndicador = new Label();
                    nombreIndicador.Width = 500;
                    nombreIndicador.Font = new Font(nombreIndicador.Font.Name, 15);
                    nombreIndicador.Text = Convert.ToString(fila[0]);
                    nombreIndicador.Location = new Point(coorXNombre, coorYNombre);
                    this.panel1.Controls.Add(nombreIndicador);

                    Label valorIndicador = new Label();
                    valorIndicador.Width = 300;
                    valorIndicador.Font = new Font(valorIndicador.Font.Name, 12);
                    valorIndicador.Text = Convert.ToString(fila[5]);
                    valorIndicador.Location = new Point(coorXNombre, coorYValor);
                    this.panel1.Controls.Add(valorIndicador);

                    TextBox valor = new TextBox();
                    valor.Width = 120;
                    valor.Font = new Font(valor.Font.Name, 12);
                    valor.Location = new Point(400, coorYValor);
                    valor.Name = Convert.ToString(fila[5]);
                    valor.BackColor = Color.Snow;
                    valor.Text = "0";
                    this.panel1.Controls.Add(valor);

                    ClaseMaestra.MesValidacion(Convert.ToString(fila[7]), valor, txtmes.Text);
                    valor.KeyPress += new KeyPressEventHandler(MytxtHandler);
                    void MytxtHandler(object send, KeyPressEventArgs eve)
                    {

                        ClaseMaestra.SoloNumeros(eve);
                        ClaseMaestra.ExcesoDeDatos(valor);
                    }
                    Label referenciasPorMejorar = new Label();
                    referenciasPorMejorar.Width = 100;
                    referenciasPorMejorar.Font = new Font(referenciasPorMejorar.Font.Name, 15);
                    referenciasPorMejorar.Text = referPorMejorar;
                    referenciasPorMejorar.Location = new Point(xpormejorar, coorYValor);
                    this.panel1.Controls.Add(referenciasPorMejorar);

                    Label referenciasAceptable = new Label();
                    referenciasAceptable.Width = 100;
                    referenciasAceptable.Font = new Font(referenciasAceptable.Font.Name, 15);
                    referenciasAceptable.Text = referAceptable;
                    referenciasAceptable.Location = new Point(xaceptable, coorYValor);
                    this.panel1.Controls.Add(referenciasAceptable);

                    Label referenciasIdeal = new Label();
                    referenciasIdeal.Width = 100;
                    referenciasIdeal.Font = new Font(referenciasIdeal.Font.Name, 15);
                    referenciasIdeal.Text = referIdeal;
                    referenciasIdeal.Location = new Point(xideal, coorYValor);
                    this.panel1.Controls.Add(referenciasIdeal);

                    TextBox txtResultado = new TextBox();
                    txtResultado.Width = 120;
                    txtResultado.Font = new Font(valor.Font.Name, 12);
                    txtResultado.Location = new Point(coorXResultado, coorYValor);
                    txtResultado.Name = varResult + Convert.ToString(contador);
                    txtResultado.Enabled = false;
                    this.panel1.Controls.Add(txtResultado);

                    arregloCodigos[cont] = Convert.ToString(fila[8]);

                    cont++;
                    coorYNombre = coorYNombre + 115;
                    coorYValor = coorYNombre + 30;
                }

                else if (Convert.ToString(fila[6]) != "" && Convert.ToString(fila[10]) == "activo")
                {
                    Label nombreIndicador = new Label();
                    nombreIndicador.Width = 500;
                    nombreIndicador.Font = new Font(nombreIndicador.Font.Name, 15);
                    nombreIndicador.Text = Convert.ToString(fila[0]);
                    nombreIndicador.Location = new Point(coorXNombre, coorYNombre);
                    this.panel1.Controls.Add(nombreIndicador);

                    Label valorIndicador = new Label();
                    valorIndicador.Width = 300;
                    valorIndicador.Font = new Font(valorIndicador.Font.Name, 12);
                    valorIndicador.Text = Convert.ToString(fila[5]);
                    valorIndicador.Location = new Point(coorXNombre, coorYValor);
                    this.panel1.Controls.Add(valorIndicador);

                    Label valorIndicador2 = new Label();
                    valorIndicador2.Width = 300;
                    valorIndicador2.Font = new Font(valorIndicador2.Font.Name, 12);
                    valorIndicador2.Text = Convert.ToString(fila[6]);
                    valorIndicador2.Location = new Point(coorXNombre, coorYValor + 20);
                    this.panel1.Controls.Add(valorIndicador2);

                    TextBox valor = new TextBox();
                    valor.Width = 120;
                    valor.Name = Convert.ToString(fila[5]);
                    valor.BackColor = Color.White;
                    valor.Font = new Font(valor.Font.Name, 12);
                    valor.Location = new Point(400, coorYValor);
                    valor.Text = "0";
                    this.panel1.Controls.Add(valor);
                    ClaseMaestra.MesValidacion(Convert.ToString(fila[7]), valor, txtmes.Text);
                    valor.KeyPress += new KeyPressEventHandler(MytxtHandler);

                    void MytxtHandler(object send, KeyPressEventArgs eve)
                    {

                        ClaseMaestra.SoloNumeros(eve);
                        ClaseMaestra.ExcesoDeDatos(valor);
                    }

                    TextBox valor2 = new TextBox();
                    valor2.Width = 120;
                    valor2.Name = Convert.ToString(fila[6]);
                    valor2.BackColor = Color.WhiteSmoke;
                    valor2.Font = new Font(valor2.Font.Name, 12);
                    valor2.Location = new Point(400, coorYValor + 20);
                    valor2.Text = "0";
                    this.panel1.Controls.Add(valor2);
                    ClaseMaestra.MesValidacion(Convert.ToString(fila[7]), valor2, txtmes.Text);
                    valor2.KeyPress += new KeyPressEventHandler(Mytxt2Handler);

                    void Mytxt2Handler(object send, KeyPressEventArgs eve)
                    {

                        ClaseMaestra.SoloNumeros(eve);
                        ClaseMaestra.ExcesoDeDatos(valor2);
                    }

                    Label lbPorcentaje = new Label();
                    lbPorcentaje.Width = 40;
                    lbPorcentaje.Font = new Font(lbPorcentaje.Font.Name, 11);
                    lbPorcentaje.Text = porcentaje;
                    lbPorcentaje.Location = new Point(680, coorYValor);
                    this.panel1.Controls.Add(lbPorcentaje);

                    Label referenciasPorMejorar = new Label();
                    referenciasPorMejorar.Width = 100;
                    referenciasPorMejorar.Font = new Font(referenciasPorMejorar.Font.Name, 15);
                    referenciasPorMejorar.Text = referPorMejorar;
                    referenciasPorMejorar.Location = new Point(xpormejorar, coorYValor);
                    this.panel1.Controls.Add(referenciasPorMejorar);

                    Label referenciasAceptable = new Label();
                    referenciasAceptable.Width = 180;
                    referenciasAceptable.Font = new Font(referenciasAceptable.Font.Name, 15);
                    referenciasAceptable.Text = referAceptable;
                    referenciasAceptable.Location = new Point(xaceptable, coorYValor);
                    this.panel1.Controls.Add(referenciasAceptable);

                    Label referenciasIdeal = new Label();
                    referenciasIdeal.Width = 100;
                    referenciasIdeal.Font = new Font(referenciasIdeal.Font.Name, 15);
                    referenciasIdeal.Text = referIdeal;
                    referenciasIdeal.Location = new Point(xideal, coorYValor);
                    this.panel1.Controls.Add(referenciasIdeal);

                    TextBox txtResultado = new TextBox();
                    txtResultado.Name = varResult + Convert.ToString(contador);
                    txtResultado.Width = 120;
                    txtResultado.Font = new Font(valor.Font.Name, 12);
                    txtResultado.Enabled = false;
                    txtResultado.Location = new Point(coorXResultado, coorYValor);
                    this.panel1.Controls.Add(txtResultado);

                    arregloCodigos[cont] = Convert.ToString(fila[8]);
                    coorYNombre = coorYNombre + 115;
                    coorYValor = coorYNombre + 30;
                    cont++;
                }
                contador++;
            }
            conectar.Close();
        }

        private void Cargar_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            FormVentas_Load(sender, e);
        }
    }
}
