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

namespace MinistrantPunkty
{
    public partial class frmEvents : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        #endregion

        public frmEvents()
        {
            InitializeComponent();

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
        }

        private void frmEvents_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(cs);
            try
            {
                con.Open();
            }
            catch
            {
                MessageBox.Show("Błąd połączenia z bazą MySQL", "Błąd");
                return;
            }

            var stm = "SELECT * FROM `events`";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dataGridView1.Rows.Add(rdr.GetString(2), rdr.GetString(1));
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            DateTime dt = dateTimePicker1.Value;
            string datetime = dt.ToString();

            string[] arr = datetime.Split(' ');
            string[] arr2 = arr[0].Split('.');

            datetime = arr2[2] + "-" + arr2[1] + "-" + arr2[0] + " " + arr[1];

            MySqlConnection con = new MySqlConnection(cs);
            try
            {
                con.Open();
            }
            catch
            {
                MessageBox.Show("Błąd połączenia z bazą MySQL", "Błąd");
                return;
            }

            var cmd = new MySqlCommand();

            cmd.Connection = con;

            cmd.CommandText = "INSERT INTO `events` VALUES (NULL,'" + txtPoints.Text + "','" + datetime + "')";
            cmd.ExecuteNonQuery();

            dataGridView1.Rows.Add(datetime, txtPoints.Text);
            MessageBox.Show("Wykonano polecenie", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
