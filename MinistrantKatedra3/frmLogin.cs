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

namespace MinistrantKatedra3
{
    public partial class frmLogin : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        bool off = false;
        #endregion

        public frmLogin()
        {
            InitializeComponent();
        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && txtPass.Text != "") btnSubmit.PerformClick();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!off) e.Cancel = true;
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            using (frmRanking form = new frmRanking())
            {
                form.ShowDialog();
            }
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
            if (haslo.All(char.IsSymbol))
            {
                MessageBox.Show("Nieprawidłowe hasło", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txtPass.Text == haslo)
            {
                /*using (frmAdmin form = new frmAdmin())
                {
                    form.ShowDialog();

                    if (form.OFF) { off = true; this.Close(); }
                }*/
            }
            else if (txtPass.Text == "Paweł Wójcik")
            {
                using (frmEsterEgg form = new frmEsterEgg(0))
                {
                    form.ShowDialog();
                }
            }
            else if (txtPass.Text == "Mikrut")
            {
                using (frmEsterEgg form = new frmEsterEgg(1))
                {
                    form.ShowDialog();
                }
            }
            else if (txtPass.Text == "Ciasteczka")
            {
                using (frmEsterEgg form = new frmEsterEgg(2))
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
                   using (frmLoged form = new frmLoged(int.Parse(cmd.ExecuteScalar().ToString()))) { form.ShowDialog(); }
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe hasło", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txtPass.Text = "";
            con.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
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

                        var stm3 = "UPDATE `ministranci` SET `Obowiazkowe` ='" + obw + "' WHERE ID =" + rdr2.GetString(0);
                        var cmd3 = new MySqlCommand(stm3, con2);
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

            //rdr.Close();
            con.Close();
            con2.Close();
        }
    }
}
