using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using WECPOFLogic;

namespace MinistrantKatedra
{
    public partial class frmLogged : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        int ID;
        DateTime dt = DateTime.Now;
        int tim = 30;
        #endregion

        #region Functions
        string DayofWeekPL2ANG(string pl)
        {
            switch (pl)
            {
                case "Poniedziałek":
                    return "Monday";
                case "Wtorek":
                    return "Tuesday";
                case "Środa":
                    return "Wednesday";
                case "Czwartek":
                    return "Thursday";
                case "Piątek":
                    return "Friday";
                case "Sobota":
                    return "Saturday";
                case "Niedziela":
                    return "Sunday";
            }
            return null;
        }

        void addP(MySqlCommand cmd, int pkt = 2)
        {

            labPoints.Text = "Przyznano " + pkt + "pkt";
            //cmd.CommandText = "UPDATE `ministranci` SET Punkty=punkty+" + pkt + " WHERE `ID`='" + ID + "'";
            //cmd.ExecuteNonQuery();

            string sqlFormattedDate = dt.ToString("yyyy-MM-dd HH:mm:ss");

            cmd.CommandText = "INSERT INTO `historia` VALUES (NULL,'" + ID + "'," + pkt + ",'Nadbowiązkowe','" + sqlFormattedDate + "')";
            cmd.ExecuteNonQuery();
        }

        bool[] Optional(int pkt,int H,int M = 0)
        {
            MySqlConnection con = new MySqlConnection(cs);
            try
            {
                con.Open();
            }
            catch
            {
                MessageBox.Show("Błąd połączenia z bazą MySQL", "Błąd");
                bool[] re = { false, false };
                return re;
            }

            DateTime dtStop = new DateTime(dt.Year, dt.Month, dt.Day, H, M, 0);
            DateTime dtStart = dtStop.Subtract(new TimeSpan(0, tim, 0));

            if (dt > dtStart && dt < dtStop)
            {
                string time = dtStop.AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss");

                var stm = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                var cmd = new MySqlCommand(stm, con);

                if (cmd.ExecuteScalar() != null)
                {
                    this.Close();
                    MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    bool[] re1 = { true, false };
                    return re1;
                }

                addP(cmd,pkt);
                bool[] re2 = { true, true };
                return re2;
            }

            bool[] re3 = { false, false };
            return re3;
        }
        #endregion

        public frmLogged(int id)
        {
            ID = id;
            InitializeComponent();
        }

        private void frmLogged_Load(object sender, EventArgs e)
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

            var stm = "SELECT * FROM `ministranci` WHERE `ID`=" + ID;
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            string obow = "";
            string waka = "";
            while (rdr.Read())
            {
                labWelcome.Text = "Witaj " + rdr.GetString(1) + "!";
                obow = rdr.GetString(5);
                waka = rdr.GetString(6);
            }
            rdr.Close();

            #region SetOff

            bool setoff = false;
            DateTime dtsetoff = DateTime.Now;
            stm = "SELECT * FROM `techniczna` WHERE `ID`=3";
            cmd = new MySqlCommand(stm, con);
            rdr = cmd.ExecuteReader();
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

            #endregion  

            #region Event
            stm = "SELECT * FROM `events` WHERE `data`>now() AND `data` - INTERVAL 30 MINUTE < now()";
            cmd = new MySqlCommand(stm, con);

            rdr = cmd.ExecuteReader();

            int i = 0;
            string pkt = "";
            string evTime = "";
            DateTime dte = DateTime.Now;

            while (rdr.Read())
            {
                pkt = rdr.GetInt32(1).ToString();
                evTime = rdr.GetString(2);
                string[] arre = evTime.Split(' ');
                string[] arre1 = arre[0].Split('.');
                string[] arre2 = arre[1].Split(':');
                evTime = arre1[2] + "-" + arre1[1] + "-" + arre1[0] + " " + arre2[0] + ":" + arre2[1] + ":" + arre2[2];
                evTime = evTime.Replace('.', '-');

                dte = new DateTime(int.Parse(arre1[2]), int.Parse(arre1[1]), int.Parse(arre1[0]), int.Parse(arre2[0]), int.Parse(arre2[1]), int.Parse(arre2[2]));
                Console.WriteLine(dte.ToString());
                i++;
            }
            rdr.Close();

            if (i > 0 && dte.Year != 1 && ((dte.DayOfWeek.ToString() == "Sunday") && ((dte.Hour == 7 && dte.Minute == 0) || (dte.Hour == 9 && dte.Minute == 0)
                    || (dte.Hour == 10 && dte.Minute == 30) || (dte.Hour == 12 && dte.Minute == 0) || (dte.Hour == 15 && dte.Minute == 0) || (dte.Hour == 17 && dte.Minute == 0)
                    || (dte.Hour == 19 && dte.Minute == 0))) || ((dte.Hour == 6 && dte.Minute == 30) || (dte.Hour == 7 && dte.Minute == 0) || (dte.Hour == 18 && dte.Minute == 0)))
            {
                stm = "SELECT `ID` FROM `historia` WHERE '" + evTime + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                cmd = new MySqlCommand(stm, con);

                if (cmd.ExecuteScalar() != null)
                {
                    this.Close();
                    MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                    return;
                }

                labPoints.Text = "Przyznano  " + pkt + "pkt";
                //cmd.CommandText = "UPDATE ministranci SET Punkty=punkty+" + pkt + " WHERE `ID`='" + ID + "'";
                //cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + pkt + ",'Event',now())";
                cmd.ExecuteNonQuery();
                return;
            }
            else if (i > 0)
            {
                MessageBoxManager.Abort = "Msza";
                MessageBoxManager.Retry = "Wydarzenie";
                MessageBoxManager.Ignore = "Oba";
                MessageBoxManager.Register();

                var result = MessageBox.Show("Wybierz odpowiednie", "Wybierz", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Information);

                if (result == DialogResult.Abort)
                {
                    tim = 60;
                }
                else if (result == DialogResult.Retry)
                {
                    stm = "SELECT `ID` FROM `historia` WHERE '" + evTime + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                    cmd = new MySqlCommand(stm, con);

                    if (cmd.ExecuteScalar() != null)
                    {
                        this.Close();
                        MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        MessageBoxManager.Unregister();
                        return;
                    }

                    labPoints.Text = "Przyznano  " + pkt + "pkt";
                    //cmd.CommandText = "UPDATE ministranci SET Punkty=punkty+" + pkt + " WHERE `ID`='" + ID + "'";
                    //cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + pkt + ",'Event',now())";
                    cmd.ExecuteNonQuery();
                    MessageBoxManager.Unregister();
                    return;
                }
                else if (result == DialogResult.Ignore)
                {
                    stm = "SELECT `ID` FROM `historia` WHERE '" + evTime + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                    cmd = new MySqlCommand(stm, con);

                    if (cmd.ExecuteScalar() != null)
                    {
                        this.Close();
                        MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        MessageBoxManager.Unregister();
                        return;
                    }

                    labPoints.Text = "Przyznano  " + pkt + "pkt";
                    //cmd.CommandText = "UPDATE ministranci SET Punkty=punkty+" + pkt + " WHERE `ID`='" + ID + "'";
                    //cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + pkt + ",'Event',now())";
                    cmd.ExecuteNonQuery();
                    tim = 60;
                }
                MessageBoxManager.Unregister();
            }
            #endregion

            #region Wacation

            #endregion

            if (setoff)
            {
                Close();
                return;
            }

            #region Obligatory
            string[] arr = obow.Split('|');

            for (i = 0; i < arr.Length; i++)
            {
                string[] arr2 = arr[i].Split('-');
                if (dt.DayOfWeek.ToString() == DayofWeekPL2ANG(arr2[0]) || waka.Contains(dt.Day.ToString() + "-" + dt.Month.ToString()))
                {
                    string[] arr3 = arr2[1].Split(':');
                    if (waka.Contains(","))
                    {
                        string[] Warr = obow.Split('|');
                        string[] Warr2 = arr[i].Split(',');
                        arr3 = arr2[1].Split(':');
                    }
                    DateTime dtStop = new DateTime(dt.Year, dt.Month, dt.Day, int.Parse(arr3[0]), int.Parse(arr3[1]), 0);
                    DateTime dtStart = dtStop.Subtract(new TimeSpan(0, tim, 0));

                    if (dt > dtStart && dt < dtStop || (waka.Contains(dt.Day.ToString() + "-" + dt.Month.ToString()) && !waka.Contains(",")))
                    {
                        string time = dtStop.AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss");
                        stm = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                        cmd = new MySqlCommand(stm, con);

                        if (cmd.ExecuteScalar() != null)
                        {
                            this.Close();
                            MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }

                        if ((int.Parse(arr3[0]) == 18 && arr2[0] != "Sobota") || arr2[0] == "Niedziela")
                        {
                            int ppkt = 2;
                            if (waka.Contains(dt.Day.ToString() + "-" + dt.Month.ToString())) ppkt = 5;
                            else if (arr2[0] == "Niedziela") ppkt = 5;
                            else if (arr2[0] == "Wtorek") ppkt = 1;

                            labPoints.Text = "Przyznano " + ppkt + "pkt";
                            //cmd.CommandText = "UPDATE ministranci SET Punkty=punkty+" + ppkt + " WHERE `ID`='" + ID + "'";
                            //cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + ppkt + ",'Obowiązkowe',now())";
                            cmd.ExecuteNonQuery();
                            return;
                        }
                        else
                        {
                            int ppkt = 3;
                            if (i > 0 || waka.Contains(dt.Day.ToString() + "-" + dt.Month.ToString())) ppkt = 5;
                            labPoints.Text = "Przyznano " + ppkt + "pkt";
                            //cmd.CommandText = "UPDATE ministranci SET Punkty=punkty+" + ppkt + " WHERE `ID`='" + ID + "'";
                            //cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + ppkt + ",'Obowiązkowe','"+dt.ToString("yyyy-MM-dd HH-mm-ss")+"')";
                            cmd.ExecuteNonQuery();

                            if (int.Parse(arr3[0]) == 6 && int.Parse(arr3[1]) == 30)
                            {
                                MessageBoxManager.Yes = "6:30";
                                MessageBoxManager.No = "6:30 i 7:00";
                                MessageBoxManager.Register();

                                var result = MessageBox.Show("Wybierz odpowiednie", "Wybierz", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                if (result == DialogResult.Yes)
                                {
                                    MessageBoxManager.Unregister();
                                    return;
                                }
                                else if (result == DialogResult.No)
                                {
                                    MessageBoxManager.Unregister();
                                    tim = 60;
                                    i = 0;
                                    dt = new DateTime(dt.Year,dt.Month,dt.Day,6,59,0);
                                    frmLogged_Load(sender,e);
                                    return;
                                }
                            }

                            return;
                        }
                    }
                   
                }
            }
            #endregion

            #region Made up
            stm = "SELECT `ID`,`Punkty`,`Data` FROM `historia` WHERE now() - INTERVAL 7 DAY < `Data` AND `Opis` = 'Nieobecność' AND `ID_ministrant` =" + ID;
            cmd = new MySqlCommand(stm, con);

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var cmd2 = new MySqlCommand(stm, con2);
                int ppkt = rdr.GetInt32(1) * -1;
                labPoints.Text = "Przyznano " + ppkt + "pkt";
                //cmd2.CommandText = "UPDATE ministranci SET Punkty=punkty+" + ppkt + " WHERE `ID`='" + ID + "'";
                //cmd2.ExecuteNonQuery();

                cmd2.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + ppkt + ",'Odrobione za " + rdr.GetString(2) + "',now())";
                cmd2.ExecuteNonQuery();

                cmd2.CommandText = "UPDATE `historia` SET `Opis` = 'Odrobiono' WHERE `ID` = " + rdr.GetInt32(0);
                cmd2.ExecuteNonQuery();
                return;
            }

            rdr.Close();
            #endregion

            #region Optional

            if (dt.DayOfWeek.ToString() == "Sunday")
            {
                if (Optional(3, 7)[0]) return;
                else if (Optional(3, 9)[0]) return;
                else if (Optional(3, 10, 30)[0]) return;
                else if (Optional(3, 12)[0]) return;
                else if (Optional(3, 15)[0]) return;
                else if (Optional(3, 17)[0]) return;
                else if (Optional(3, 19)[0]) return;
            }
            else
            {
                bool[] op = Optional(3, 6, 30);
                if (op[0])
                {
                    if (!op[1]) return;
                    MessageBoxManager.Yes = "6:30";
                    MessageBoxManager.No = "6:30 i 7:00";
                    MessageBoxManager.Register();

                    var result = MessageBox.Show("Wybierz odpowiednie", "Wybierz", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        MessageBoxManager.Unregister();
                        return;
                    }
                    else if (result == DialogResult.No)
                    {
                        MessageBoxManager.Unregister();
                        tim = 60;
                        i = 0;
                        dt = new DateTime(dt.Year, dt.Month, dt.Day, 6, 59, 0);
                        frmLogged_Load(sender, e);
                        return;
                    }
                }
                else if (Optional(3, 7)[0]) return;
                else if (dt.DayOfWeek.ToString() == "Saturday") if(Optional(3, 18)[0]) return;
                if (Optional(2, 18)[0]) return;
            }
            #endregion
            MessageBox.Show("Zalogować się można tylko 30 min przed mszą bądź nabożeństwem", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
        }

        private void frmLogged_Shown(object sender, EventArgs e)
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
            var stm = "SELECT * FROM `historia` WHERE `ID_ministrant` = " + ID + " ORDER BY `historia`.`Data` DESC";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                dataGridView1.Rows.Add(rdr.GetString(2), rdr.GetString(3), rdr.GetString(4));
            }
            rdr.Close();
            con.Close();
        }
    }
}
