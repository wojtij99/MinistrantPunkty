using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MinistrantKatedra
{
    public partial class frmLogin : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        bool off = false;
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

        public frmLogin()
        {
            InitializeComponent();
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
                using (frmAdmin form = new frmAdmin())
                {
                    form.ShowDialog();

                    if (form.OFF) { off = true; this.Close(); }
                }
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
                    using (frmLogged form = new frmLogged(int.Parse(cmd.ExecuteScalar().ToString()))) { form.ShowDialog(); }
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe hasło", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txtPass.Text = "";
            con.Close();
        }

        private void txtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && txtPass.Text != "")
            {
                btnSubmit.PerformClick();
            }
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
            return;
            bool setoff = false;
            DateTime dtsetoff = DateTime.Now;
            stm = "SELECT `V1` FROM `techniczna` WHERE `ID` = 3";
            cmd = new MySqlCommand(stm, con);
            string[] arrset = cmd.ExecuteScalar().ToString().Split('|');
            foreach (string a in arrset)
            {
                if (a == "") continue;
                string[] a_arr = a.Split('.');
                if (dtsetoff.Day == int.Parse(a_arr[0]) && dtsetoff.Month == int.Parse(a_arr[1]) && dtsetoff.Year == int.Parse(a_arr[2])) { setoff = true; break; };
            }

            if (setoff) return;

            string day = "";

            if (!((dt.DayOfWeek.ToString() == "Sunday") && ((dt.Hour == 7 && dt.Minute == 0) || (dt.Hour == 9 && dt.Minute == 0)
                    || (dt.Hour == 10 && dt.Minute == 30) || (dt.Hour == 12 && dt.Minute == 0) || (dt.Hour == 15 && dt.Minute == 0) || (dt.Hour == 17 && dt.Minute == 0)
                    || (dt.Hour == 19 && dt.Minute == 0))) || ((dt.Hour == 6 && dt.Minute == 30) || (dt.Hour == 7 && dt.Minute == 0) || (dt.Hour == 18 && dt.Minute == 0)))
                return;

            day = DayofWeekANG2PL(dt.DayOfWeek.ToString());
            day += "-" + dt.Hour + ":" + dt.Minute;

            string cov = dt.Day.ToString();
            cov += "-";
            cov += dt.Month.ToString();
            if (dt.DayOfWeek.ToString() == "Sunday") cov += "." + dt.Hour + ":" + dt.Minute;

            stm = "SELECT * FROM `ministranci` WHERE `Obowiazkowe` LIKE '%" + day + "%' OR `Wakacyjne` LIKE '%" + cov + "%'";
            cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string stm2 = "SELECT `ID` FROM `historia` WHERE now() - INTERVAL 30 MINUTE < `Data` AND (`Opis` = 'Obowiązkowe' OR  `Opis` = 'Event') AND `ID_ministrant` =" + rdr.GetString(0);
                var cmd2 = new MySqlCommand(stm2, con2);
                if (cmd2.ExecuteScalar() == null)
                {
                    cmd2.CommandText = "INSERT INTO historia VALUES (NULL,'" + rdr.GetString(0) + "',-3,'Nieobecność',now())";
                    cmd2.ExecuteNonQuery();
                }
            }

            rdr.Close();
            con.Close();
            con2.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
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

            var stm = "SELECT COUNT(*) FROM `ministranci`";
            var cmd = new MySqlCommand(stm, con);
            int ministrantsCounts = int.Parse(cmd.ExecuteScalar().ToString());

            stm = "SELECT `V1` FROM `techniczna` WHERE `ID` = 4";
            cmd = new MySqlCommand(stm, con);
            DateTime dt = DateTime.Parse(cmd.ExecuteScalar().ToString());

            stm = "SELECT `V1` FROM `techniczna` WHERE `ID` = 3";
            cmd = new MySqlCommand(stm, con);
            string[] arrset = cmd.ExecuteScalar().ToString().Split('|');
            Array.Resize(ref arrset, arrset.Length - 1);

            DateTime[] dts = { };
            MySqlDataReader rdr;
            for (int i = 1; i <= ministrantsCounts; i++)
            {
                stm = "SELECT `Obowiazkowe` FROM `ministranci` WHERE `ID` =" + i;
                cmd = new MySqlCommand(stm, con);
                string obow = cmd.ExecuteScalar().ToString();
                string[] arr1 = obow.Split('|');
                Array.Resize(ref arr1, arr1.Length - 1);
                if (arr1.Length == 0) continue;

                DateTime dtT = dt;
                stm = "SELECT `historia`.`Data` FROM `techniczna`,`historia`,`ministranci` WHERE `techniczna`.`ID` = 4 AND `historia`.`Data` > `techniczna`.`V1` AND (`historia`.`Opis` = 'Event' OR `historia`.`Opis` = 'Obowiązkowe') AND `ministranci`.`ID` = `historia`.`ID_ministrant` AND `ministranci`.`ID` =" + i;
                cmd = new MySqlCommand(stm, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Array.Resize(ref dts, dts.Length + 1);
                    dts[dts.Length - 1] = new DateTime(rdr.GetDateTime(0).Ticks);
                }
                rdr.Close();

                while (dtT < DateTime.Now)
                {
                    bool setoff = false;
                    foreach (string a in arrset)
                    {
                        if (a == "") continue;
                        string[] a_arr = a.Split('.');
                        if (dtT.Day == int.Parse(a_arr[0]) && dtT.Month == int.Parse(a_arr[1]) && dt.Year == int.Parse(a_arr[2])) { setoff = true; break; }
                    }

                    if (!setoff)
                    {
                        foreach (string a in arr1)
                        {
                            string[] arr2 = a.Split('-');
                            if (DayofWeekANG2PL(dtT.DayOfWeek.ToString()) == arr2[0])
                            {
                                string[] arr3 = arr2[1].Split(':');
                                if (dtT.Hour.ToString() == arr3[0])
                                {
                                    bool byl = false;
                                    foreach (DateTime d in dts)
                                    {
                                        if (d.Year == dtT.Year && d.Month == dtT.Month && d.Day == dtT.Day)
                                        {
                                            if (d >= dtT.AddMinutes(-30) && d <= dtT)
                                            {
                                                byl = true;
                                                break;
                                            } 
                                        }
                                    }

                                    if (!byl)
                                    {
                                        //odejmujemu punkciki ;)
                                        stm = "INSERT INTO historia VALUES (NULL,'" + i + "',-3,'Nieobecność','"+ dtT.ToString("yyyy-MM-dd HH-mm-ss") +"')";
                                        cmd = new MySqlCommand(stm, con);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                break;
                            }
                        }
                    }

                    if (dtT.DayOfWeek.ToString() == "Sunday")
                    {
                        if (dtT.Hour >= 7) dtT = new DateTime(dtT.Year, dtT.Month, dtT.Day, 9, 0, 0);
                        else if (dtT.Hour >= 9) dtT = new DateTime(dtT.Year, dtT.Month, dtT.Day, 10, 30, 0);
                        else if (dtT.Hour >= 10) dtT = new DateTime(dtT.Year, dtT.Month, dtT.Day, 12, 0, 0);
                        else if (dtT.Hour >= 12) dtT = new DateTime(dtT.Year, dtT.Month, dtT.Day, 15, 0, 0);
                        else if (dtT.Hour >= 15) dtT = new DateTime(dtT.Year, dtT.Month, dtT.Day, 17, 0, 0);
                        else if (dtT.Hour >= 17) dtT = new DateTime(dtT.Year, dtT.Month, dtT.Day, 19, 0, 0);
                        else if (dtT.Hour >= 19)
                        {
                            dtT = dtT.AddDays(1);
                            dtT = new DateTime(dtT.Year, dtT.Month, dtT.Day, 6, 30, 0);
                        }
                    }
                    else if (dtT.Hour >= 18)
                    {
                        dtT = dtT.AddDays(1);
                        if (dtT.DayOfWeek.ToString() == "Sunday") dtT = new DateTime(dtT.Year, dtT.Month, dtT.Day, 7, 0, 0);
                        else dtT = new DateTime(dtT.Year, dtT.Month, dtT.Day, 6, 30, 0);
                    }
                    else if (dtT.Hour >= 7) dtT = new DateTime(dtT.Year, dtT.Month, dtT.Day, 18, 00, 0);
                    else if (dtT.Hour >= 6) dtT = new DateTime(dtT.Year, dtT.Month, dtT.Day, 7, 00, 0);
                }

            }

            stm = "UPDATE `techniczna` SET `V1`= now() WHERE `ID` = 4";
            cmd = new MySqlCommand(stm, con);
            cmd.ExecuteNonQuery();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!off) e.Cancel = true;
        }
    }
}
