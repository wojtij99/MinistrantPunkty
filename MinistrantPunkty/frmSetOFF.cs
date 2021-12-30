using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace MinistrantPunkty
{
    public partial class frmSetOFF : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        string setoff = "";
        #endregion

        public frmSetOFF()
        {
            InitializeComponent();

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            DateTime dt = dateTimePicker1.Value;

            string[] arr = dt.ToString().Split(' ');
            string date = arr[0];

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

            setoff += date + "|";

            cmd.CommandText = "UPDATE `techniczna` SET `V1`= '" + setoff + "' WHERE `ID` = 3";
            cmd.ExecuteNonQuery();

            dataGridView1.Rows.Add(date);
            MessageBox.Show("Wykonano polecenie", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmSetOFF_Load(object sender, EventArgs e)
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

            var stm = "SELECT * FROM `techniczna` WHERE `ID` = 3";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                setoff = rdr.GetString(2);
                string[] arr = setoff.Split('|');
                foreach (string a in arr) dataGridView1.Rows.Add(a);
            }
        }
    }
}
