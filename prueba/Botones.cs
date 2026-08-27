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


namespace hhrrAplication
{
    public partial class rrhh1 : Form
    {

        private int y = 50;
        private int count = 0;

        public rrhh1()
        {
            InitializeComponent();
        }

        static string connect = "SERVER=127.0.0.1;PORT=3306;DATABASE=indices;UID=root;PASSWORDS=;";
        MySqlConnection DB = new MySqlConnection(connect);
        
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
       

        public String hhrrData() {
            DB.Open();
            DataTable dt = new DataTable();
            String modelo = "";
            String SQL = "SHOW COLUMNS FROM indices.rrhh1";
            MySqlCommand cmd = new MySqlCommand(SQL, DB);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            modelo = dataAdapter.ToString();
            DB.Close();
            return modelo;


        }
       
        //Event hanlder
        public void handlerComun_click(object sender, EventArgs e)
        {
            Console.Beep(((Button)sender).Location.Y * 10, 100);
        }


        //Boton de agregar controllers
        private void btn_create_Click(object sender, EventArgs e)
        {
            /* NOTA: 
             * en este proceso puedes agreagar tanto como controles de texto,
             * o etiquetas y darle sus propiedads como
             * como se necesario, las coordenadas en la forma,
             * y finalmente agregarlos a la forma*/
            
            //Creamos la instancia del Boton
            Label ltemp = new Label();
            TextBox ttemp = new TextBox();
            Button btemp = new Button();

            //colocamos las propiedades
            btemp.Height = 23;
            btemp.Width = 150;
            btemp.Location = new Point(50, y);
            y += 25;
            btemp.Name = "btnIndice" + count.ToString();
            btemp.Text = "Indice Nuevo" + count.ToString();
            count++;

            //Add event Handler
            btemp.Click += new EventHandler(handlerComun_click);

            //Add controller to form
            Controls.Add(btemp);
        }
    }
}
