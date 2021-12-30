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
    public partial class frmHistory : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        #endregion

        public frmHistory()
        {
            InitializeComponent();
        }

        private void frmHistory_Load(object sender, EventArgs e)
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

            MySqlConnection con2 = new MySqlConnection(cs);
            try
            {
                con2.Open();
            }
            catch
            {
                MessageBox.Show("Błąd połączenia z bazą MySQL", "Błąd");
                return;
            }

            var stm = "SELECT * FROM `historia` ORDER BY `historia`.`ID` DESC";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string stm2 = "SELECT `Imie` FROM `ministranci` WHERE `ID`=" + rdr.GetInt32(1).ToString();
                var cmd2 = new MySqlCommand(stm2, con2);
                string name = cmd2.ExecuteScalar().ToString();

                string stm3 = "SELECT `Nazwisko` FROM `ministranci` WHERE `ID`=" + rdr.GetInt32(1).ToString();
                var cmd3 = new MySqlCommand(stm3, con2);
                string lastname = cmd3.ExecuteScalar().ToString();

                dataGridView1.Rows.Add(rdr.GetInt32(0).ToString(), name, lastname, rdr.GetInt32(2).ToString(), rdr.GetString(3), rdr.GetString(4));
            }

            con.Close();
            con2.Close();
        }
    }
}
