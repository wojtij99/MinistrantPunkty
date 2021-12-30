using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinistrantPunkty
{
    public partial class Logowanie : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        #endregion

        string DayofWeekANG2PL(string ang)
        {

            switch (ang)
            {
                case "Monday":
                    return "Poniedziałek";
                case "Tuesday":
                    return "Wtorek";
                case "Wednesday":
                    return "Środa";
                case "Thursday":
                    return "Czwartek";
                case "Friday":
                    return "Piątek";
                case "Saturday":
                    return "Sobota";
                case "Sunday":
                    return "Niedziela";
            }
            return null;
        }

        public Logowanie()
        {
            InitializeComponent();

            DateTime dt = DateTime.Now;
            //Console.WriteLine("<>"+ dt.ToString());
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            MySqlConnection con = new MySqlConnection(cs);
            try
            {
                con.Open();
            }
            catch
            {
                MessageBox.Show("Błąd połączenia z bazą MySQL", "Błąd");
                txtPass.Text = "";
                return;
            }

            var stm = "SELECT `V1` FROM `techniczna` WHERE `ID`= 2";
            var cmd = new MySqlCommand(stm, con);

            Console.WriteLine();
            string haslo = cmd.ExecuteScalar().ToString();
            if (txtPass.Text == haslo)
            {
                using (frmAdmin form = new frmAdmin())
                {
                    form.ShowDialog();
                }
            }
            else
            {
                stm = "SELECT `ID` FROM `ministranci` WHERE `haslo`='" + txtPass.Text + "'";
                cmd = new MySqlCommand(stm, con);

                if (cmd.ExecuteScalar() != null)
                {
                    using (frmZalogowano form = new frmZalogowano(int.Parse(cmd.ExecuteScalar().ToString())))
                    {
                        form.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe hasło", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txtPass.Text = "";
            con.Close();
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            using (frmRanking form = new frmRanking())
            {
                form.ShowDialog();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine("D");
            DateTime dt = DateTime.Now;

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
            var stm = "SELECT `V1` FROM `techniczna` WHERE `ID` = 1";
            var cmd = new MySqlCommand(stm, con);

            //Console.WriteLine(dt.AddDays(-1).Month);
            if (dt.Month != dt.AddDays(-1).Month && dt.Month != int.Parse(cmd.ExecuteScalar().ToString()))
            {
                var stm2 = "SELECT `ID`, `Obowiazkowe` FROM `ministranci`";
                var cmd2 = new MySqlCommand(stm2, con);

                MySqlDataReader rdr2 = cmd2.ExecuteReader();

                while (rdr2.Read())
                {
                    string obw = rdr2.GetString(1);
                    if (obw.Contains("Niedziela-7:00"))
                    {
                        obw = obw.Replace("Niedziela-7:00", "Niedziela-9:00");

                        var stm3 = "UPDATE `ministranci` SET `Obowiazkowe` ='" + obw + "' WHERE ID =" + rdr2.GetString(0);
                        var cmd3 = new MySqlCommand(stm3, con2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (obw.Contains("Niedziela-9:00"))
                    {
                        obw = obw.Replace("Niedziela-9:00", "Niedziela-10:30");

                        var stm3 = "UPDATE `ministranci` SET `Obowiazkowe` ='" + obw + "' WHERE ID =" + rdr2.GetString(0);
                        var cmd3 = new MySqlCommand(stm3, con2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (obw.Contains("Niedziela-10:30"))
                    {
                        obw = obw.Replace("Niedziela-10:30", "Niedziela-12:00");

                        var stm3 = "UPDATE `ministranci` SET `Obowiazkowe` ='" + obw + "' WHERE ID =" + rdr2.GetString(0);
                        var cmd3 = new MySqlCommand(stm3, con2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (obw.Contains("Niedziela-12:00"))
                    {
                        obw = obw.Replace("Niedziela-12:00", "Niedziela-15:00");

                        var stm3 = "UPDATE `ministranci` SET `Obowiazkowe` ='" + obw + "' WHERE ID =" + rdr2.GetString(0);
                        var cmd3 = new MySqlCommand(stm3, con2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (obw.Contains("Niedziela-15:00"))
                    {
                        obw = obw.Replace("Niedziela-15:00", "Niedziela-17:00");

                        var stm3 = "UPDATE `ministranci` SET `Obowiazkowe` ='" + obw + "' WHERE ID =" + rdr2.GetString(0);
                        var cmd3 = new MySqlCommand(stm3, con2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (obw.Contains("Niedziela-17:00"))
                    {
                        obw = obw.Replace("Niedziela-17:00", "Niedziela-19:00");

                        var stm3 = "UPDATE `ministranci` SET `Obowiazkowe` ='"+ obw + "' WHERE ID =" + rdr2.GetString(0);
                        var cmd3 = new MySqlCommand(stm3,con2);
                        cmd3.ExecuteNonQuery();
                    }
                    else if (obw.Contains("Niedziela-19:00"))
                    {
                        obw = obw.Replace("Niedziela-19:00", "Niedziela-7:00");

                        var stm3 = "UPDATE `ministranci` SET `Obowiazkowe` ='" + obw + "' WHERE ID =" + rdr2.GetString(0);
                        var cmd3 = new MySqlCommand(stm3, con2);
                        cmd3.ExecuteNonQuery();
                    }
                }

                var stm4 = "UPDATE `techniczna` SET `V1` ='" + dt.Month + "' WHERE ID = 1";
                var cmd4 = new MySqlCommand(stm4, con2);
                cmd4.ExecuteNonQuery();
                return;
            }

            bool setoff = false;
            DateTime dtsetoff = DateTime.Now;
            stm = "SELECT * FROM `techniczna` WHERE `ID`=3";
            cmd = new MySqlCommand(stm, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string[] arrset = rdr.GetString(2).Split('|');
                foreach (string a in arrset)
                {
                    if (a == "") continue;
                    string[] a_arr = a.Split('.');
                    if (dtsetoff.Day == int.Parse(a_arr[0]) && dtsetoff.Month == int.Parse(a_arr[1]) && dtsetoff.Year == int.Parse(a_arr[2])) setoff = true;
                }
            }
            rdr.Close();

            if (setoff) return;

            string day = "";

            day = DayofWeekANG2PL(dt.DayOfWeek.ToString());
            day += "-" + dt.Hour + ":" + dt.Minute;

            stm = "SELECT * FROM `ministranci` WHERE `Obowiazkowe` LIKE '%" + day + "%'";
            cmd = new MySqlCommand(stm, con);

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string stm2 = "SELECT `ID` FROM `historia` WHERE now() - INTERVAL 30 MINUTE < `Data` AND `Opis` = 'Obowiązkowe' AND `ID_ministrant` =" + rdr.GetString(0);
                var cmd2 = new MySqlCommand(stm2, con2);
                if (cmd2.ExecuteScalar() == null)
                {
                    cmd2.CommandText = "UPDATE ministranci SET Punkty=punkty-3 WHERE `ID` =" + rdr.GetString(0);
                    cmd2.ExecuteNonQuery();

                    cmd2.CommandText = "INSERT INTO historia VALUES (NULL,'" + rdr.GetString(0) + "',-3,'Nieobecność',now())";
                    cmd2.ExecuteNonQuery();
                }
            }

            con.Close();
            con2.Close();
            
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnSubmit.PerformClick();
            }
        }
    }
}
