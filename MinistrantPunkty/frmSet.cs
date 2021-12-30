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
    public partial class frmSet : Form
    {
        #region Struktury
        struct Ministrant
        {
            public string Name;
            public string Lastname;
            public string Obow;
        }
        #endregion
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        Ministrant[] ministranci = null;
        int prew = 0;
        #endregion
        #region Funkcje
        void refresh()
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

            ministranci = new Ministrant[count];

            var stm = "SELECT  `Imie`, `Nazwisko`, `Obowiazkowe` FROM `ministranci`";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            int i = 0;
            coxUser.Items.Clear();
            while (rdr.Read())
            {
                coxUser.Items.Add(rdr.GetString(0) + " " + rdr.GetString(1));
                ministranci[i].Name = rdr.GetString(0);
                ministranci[i].Lastname = rdr.GetString(1);
                ministranci[i].Obow = rdr.GetString(2);
                i++;
            }
            con.Close();
        }
        #endregion
        #region Window
        public frmSet()
        {
            InitializeComponent();
        }

        private void frmSet_Load(object sender, EventArgs e)
        {
            refresh();
        }
        #endregion
        #region ComboBox
        private void coxDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (coxDay.SelectedIndex == 6)
            {
                coxHour.Items.Clear();
                coxHour.Items.Add("7:00");
                coxHour.Items.Add("9:00");
                coxHour.Items.Add("10:30");
                coxHour.Items.Add("12:00");
                coxHour.Items.Add("15:00");
                coxHour.Items.Add("17:00");
                coxHour.Items.Add("19:00");
                coxHour.Text = "";
            }
            else 
            {
                coxHour.Items.Clear();
                coxHour.Items.Add("6:30");
                coxHour.Items.Add("7:00");
                coxHour.Items.Add("18:00");
                coxHour.Text = "";
            }
        }

        private void coxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (coxUser.SelectedIndex == -1) { coxUser.SelectedIndex = prew; return; }
            dataGridView1.Rows.Clear();

            string[] arr = ministranci[coxUser.SelectedIndex].Obow.Split('|');

            for (int i = 0; i < arr.Length - 1; i++)
            {
                string[] arr2 = arr[i].Split('-');
                dataGridView1.Rows.Add(arr2[0], arr2[1]);
            }
        }
        #endregion
        #region Buttons
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
            var cmd = new MySqlCommand();

            cmd.Connection = con;

            string cov = coxDay.Text;
            cov += "-";
            cov += coxHour.Text;
            cov += "|";
            cmd.CommandText = "UPDATE `ministranci` SET `Obowiazkowe`='" + ministranci[coxUser.SelectedIndex].Obow + cov + "' WHERE `Imie`='"+ ministranci[coxUser.SelectedIndex].Name + "' AND `Nazwisko`='" + ministranci[coxUser.SelectedIndex].Lastname + "'";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Wykonano polecenie", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            prew = coxUser.SelectedIndex;
            refresh();
            
            coxUser_SelectedIndexChanged(coxUser,e);
        }

        private void btnClear_Click(object sender, EventArgs e)
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
            var cmd = new MySqlCommand();

            cmd.Connection = con;

            cmd.CommandText = "UPDATE `ministranci` SET `Obowiazkowe`='' WHERE `Imie`='" + ministranci[coxUser.SelectedIndex].Name + "' AND `Nazwisko`='" + ministranci[coxUser.SelectedIndex].Lastname + "'";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Wykonano polecenie", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            prew = coxUser.SelectedIndex;
            refresh();

            //coxUser.SelectedIndex = -1;
            coxUser_SelectedIndexChanged(coxUser, e);
        }
        #endregion
    }
}
