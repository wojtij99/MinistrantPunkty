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
    public partial class frmPoints : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        #endregion

        public frmPoints()
        {
            InitializeComponent();
        }

        private void frmPoints_Load(object sender, EventArgs e)
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

            MySqlCommand licznik_rek = new MySqlCommand("SELECT  COUNT(*) FROM `ministranci`", con);
            int count = Convert.ToInt32(licznik_rek.ExecuteScalar());

            var stm = "SELECT  `Imie`, `Nazwisko`, `Obowiazkowe` FROM `ministranci`";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            coxUser.Items.Clear();
            while (rdr.Read())
            {
                coxUser.Items.Add(rdr.GetString(0) + " " + rdr.GetString(1));
            }
            con.Close();
        }

        private void btnExecute_Click(object sender, EventArgs e)
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

            string[] arr = coxUser.Text.Split(' ');

            var stm = "SELECT `ID` FROM `ministranci` WHERE `Imie`='" + arr[0] + "' AND `Nazwisko`='" + arr[1] + "'";
            var cmd = new MySqlCommand(stm, con);

            Console.WriteLine(stm);

            string id = cmd.ExecuteScalar().ToString();

            cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "UPDATE ministranci SET Punkty=punkty+" + txtPoints.Text + " WHERE `Imie`='" + arr[0] + "' AND `Nazwisko`='" + arr[1] + "'";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO `historia` VALUES (NULL,'" + id + "'," + txtPoints.Text + ",'Admin',now())";
            cmd.ExecuteNonQuery();

            MessageBox.Show("Wykonano", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
