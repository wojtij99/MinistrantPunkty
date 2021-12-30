using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WECPOFLogic;

namespace MinistrantPunkty
{
    public partial class frmZalogowano : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        int ID;
        //DateTime dt = new DateTime(2021, 2, 4, 17, 40, 0);
        DateTime dt = DateTime.Now;
        #endregion


        string[] Dt2arr(int h, int m = 0)
        {
            DateTime dtb = new DateTime(dt.Year, dt.Month, dt.Day, h, m, 0);

            string dtc = (dt - dtb).ToString();
            string[] dtarra = dtc.Split('.');
            return  dtarra[0].Split(':');
        }

        void addP(MySqlCommand cmd, int pkt = 2)
        {
            
            labPoints.Text = "Przyznano " + pkt + "pkt";
            cmd.CommandText = "UPDATE ministranci SET Punkty=punkty+" + pkt + " WHERE `ID`='" + ID + "'";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + pkt + ",'Nadbowiązkowe',now())";
            cmd.ExecuteNonQuery();
        }

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

        public frmZalogowano(int id)
        {
            InitializeComponent();
            ID = id;

            if (File.Exists(@"time.t"))
            {
                // rok miesiąc dzień godzina minuta sekunda
                string[] Stime = File.ReadAllLines(@"time.t");
                string[] StimeSplited = Stime[0].Split(' ');
                dt = new DateTime(int.Parse(StimeSplited[0]), int.Parse(StimeSplited[1]), int.Parse(StimeSplited[2]), int.Parse(StimeSplited[3]), int.Parse(StimeSplited[4]), int.Parse(StimeSplited[5]));
            }
        }

        private void frmZalogowano_Load(object sender, EventArgs e)
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

            MySqlConnection con3 = new MySqlConnection(cs);
            try
            {
                con3.Open();
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
            while (rdr.Read())
            {
                labWelcome.Text = "Witaj " + rdr.GetString(1) + "!";
                obow = rdr.GetString(5);
            }
            rdr.Close();

            //zabespieczenie przed farmieniem
            string stm2 = "SELECT `ID` FROM `historia` WHERE now() - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
            var cmd2 = new MySqlCommand(stm2, con);

            if (cmd2.ExecuteScalar() != null && false)
            {       
                this.Close();
                MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
                return;
            }
            else
            {
                //SETOFF
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

                //eventy
                stm = "SELECT * FROM `events` WHERE `data`>now() AND `data` - INTERVAL 30 MINUTE < now()";
                cmd = new MySqlCommand(stm, con);
                cmd2 = new MySqlCommand(stm, con);

                rdr = cmd.ExecuteReader();

                int i = 0;
                int tim = 30;
                string pkt = "";
                DateTime dte = DateTime.Now;

                while (rdr.Read())
                {
                    pkt = rdr.GetInt32(1).ToString();

                    string[] arre = rdr.GetString(2).Split(' ');
                    string[] arre1 = arre[0].Split('.');
                    string[] arre2 = arre[1].Split(':');

                    dte = new DateTime(int.Parse(arre1[2]), int.Parse(arre1[1]), int.Parse(arre1[0]), int.Parse(arre2[0]), int.Parse(arre2[1]), int.Parse(arre2[2]));

                    i++;
                }
                rdr.Close();

                if (i > 0 && dte.Year != 1 && ((dte.DayOfWeek.ToString() == "Sunday") && ((dte.Hour == 7 && dte.Minute == 0) || (dte.Hour == 9 && dte.Minute == 0)
                    || (dte.Hour == 10 && dte.Minute == 30) || (dte.Hour == 12 && dte.Minute == 0) || (dte.Hour == 15 && dte.Minute == 0) || (dte.Hour == 17 && dte.Minute == 0)
                    || (dte.Hour == 19 && dte.Minute == 0))) || ((dte.Hour == 6 && dte.Minute == 30) || (dte.Hour == 7 && dte.Minute == 0) || (dte.Hour == 18 && dte.Minute == 0)))
                {
                    //string[] timearray = Dt2arr(dte.Hour, dte.Minute);
                    //string time = timearray[2] + "-" + timearray[1] + "-" + timearray[0] + " " + timearray[3] + ":" + timearray[4] + ":" + timearray[5];

                    stm2 = "SELECT `ID` FROM `historia` WHERE '" + dte.ToString() + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                    cmd2 = new MySqlCommand(stm2, con);

                    if (cmd2.ExecuteScalar() != null)
                    {
                        this.Close();
                        MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        return;
                    }

                    labPoints.Text = "Przyznano  " + pkt + "pkt";
                    cmd2.CommandText = "UPDATE ministranci SET Punkty=punkty+" + pkt + " WHERE `ID`='" + ID + "'";
                    cmd2.ExecuteNonQuery();

                    cmd2.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + pkt + ",'Event',now())";
                    cmd2.ExecuteNonQuery();
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
                        labPoints.Text = "Przyznano  " + pkt + "pkt";
                        cmd2.CommandText = "UPDATE ministranci SET Punkty=punkty+" + pkt + " WHERE `ID`='" + ID + "'";
                        cmd2.ExecuteNonQuery();

                        cmd2.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + pkt + ",'Event',now())";
                        cmd2.ExecuteNonQuery();
                        return;
                    }
                    else if (result == DialogResult.Ignore)
                    {
                        labPoints.Text = "Przyznano  " + pkt + "pkt";
                        cmd2.CommandText = "UPDATE ministranci SET Punkty=punkty+" + pkt + " WHERE `ID`='" + ID + "'";
                        cmd2.ExecuteNonQuery();

                        cmd2.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + pkt + ",'Event',now())";
                        cmd2.ExecuteNonQuery();
                        tim = 60;
                    }
                    MessageBoxManager.Unregister();
                }

                //Obowiązkowe
                if (setoff)
                {
                    Close();
                    return;
                }
                string[] arr = obow.Split('|');

                for (i = 0; i < arr.Length; i++)
                {
                    string[] arr2 = arr[i].Split('-');
                    if (dt.DayOfWeek.ToString() == DayofWeekPL2ANG(arr2[0]))
                    {
                        string[] arr3 = arr2[1].Split(':');
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, int.Parse(arr3[0]), int.Parse(arr3[1]), 0);

                        string dt3 = (dt - dt2).ToString();
                        string[] dtarr1 = dt3.Split('.');
                        string[] dtarr2 = dtarr1[0].Split(':');

                        if (dtarr2[0] == "-00" && int.Parse(dtarr2[1]) < tim)
                        {
                            string[] predtstring = dt2.ToString().Split(' ');
                            string[] timearray = predtstring[1].Split(':');
                            predtstring = predtstring[0].Split('.');
                            string time = predtstring[2] + "-" + predtstring[1] + "-" + predtstring[0] + " " + timearray[0] + ":" + timearray[1] + ":" + timearray[2];

                            stm2 = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                            cmd2 = new MySqlCommand(stm2, con);

                            if (cmd2.ExecuteScalar() != null)
                            {
                                this.Close();
                                MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                con.Close();
                                return;
                            }

                            if ((int.Parse(arr3[0]) == 18 && arr2[0] != "Sobota") || arr2[0] == "Niedziela")
                            {
                                int ppkt = 1;
                                if (i > 0 && arr2[0] != "Niedziela") ppkt = 5;
                                labPoints.Text = "Przyznano " + ppkt + "pkt";
                                cmd.CommandText = "UPDATE ministranci SET Punkty=punkty+" + ppkt + " WHERE `ID`='" + ID + "'";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + ppkt + ",'Obowiązkowe',now())";
                                cmd.ExecuteNonQuery();
                                return;
                            }
                            else
                            {
                                int ppkt = 3;
                                if (i > 0) ppkt = 5;
                                labPoints.Text = "Przyznano " + ppkt + "pkt";
                                cmd.CommandText = "UPDATE ministranci SET Punkty=punkty+" + ppkt + " WHERE `ID`='" + ID + "'";
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + ppkt + ",'Obowiązkowe',now())";
                                cmd.ExecuteNonQuery();
                                return;
                            }
                        }
                    }
                }

                //Odrabienie
                stm = "SELECT `ID`,`Punkty`,`Data` FROM `historia` WHERE now() - INTERVAL 7 DAY < `Data` AND `Opis` = 'Nieobecność' AND `ID_ministrant` =" + ID;
                cmd = new MySqlCommand(stm, con);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var cmd4 = new MySqlCommand(stm, con3);
                    int ppkt = rdr.GetInt32(1) * -1;
                    labPoints.Text = "Przyznano " + ppkt + "pkt";
                    cmd4.CommandText = "UPDATE ministranci SET Punkty=punkty+" + ppkt + " WHERE `ID`='" + ID + "'";
                    cmd4.ExecuteNonQuery();

                    cmd4.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + ppkt + ",'Odrobione za " + rdr.GetString(2) + "',now())";
                    cmd4.ExecuteNonQuery();

                    cmd4.CommandText = "UPDATE `historia` SET `Opis` = 'Odroniono' WHERE `ID` = " + rdr.GetInt32(0);
                    cmd4.ExecuteNonQuery();
                    return;
                }

                rdr.Close();

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

                stm = "SELECT `ID`,`Punkty`,`Data` FROM `historia` WHERE now() - INTERVAL 7 DAY < `Data` AND `Opis` = 'Nieobecność' AND `ID_ministrant` =" + ID;
                var cmd3 = new MySqlCommand(stm, con2);

                //Nadobowiązkowe
                if (dt.DayOfWeek.ToString() == "Sunday")
                {
                    string[] arrdt = Dt2arr(7);
                    if (arrdt[0] == "-00" && int.Parse(arrdt[1]) < tim)
                    {
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 7, 0, 0);
                        string[] predtstring = dt2.ToString().Split(' ');
                        string[] timearray = predtstring[1].Split(':');
                        predtstring = predtstring[0].Split('.');
                        string time = predtstring[2] + "-" + predtstring[1] + "-" + predtstring[0] + " " + timearray[0] + ":" + timearray[1] + ":" + timearray[2];

                        stm2 = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                        cmd2 = new MySqlCommand(stm2, con);

                        if (cmd2.ExecuteScalar() != null)
                        {
                            this.Close();
                            MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }

                        addP(cmd3);
                        return;
                    }

                    arrdt = Dt2arr(9);
                    if (arrdt[0] == "-00" && int.Parse(arrdt[1]) < tim)
                    {
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 9, 0, 0);
                        string[] predtstring = dt2.ToString().Split(' ');
                        string[] timearray = predtstring[1].Split(':');
                        predtstring = predtstring[0].Split('.');
                        string time = predtstring[2] + "-" + predtstring[1] + "-" + predtstring[0] + " " + timearray[0] + ":" + timearray[1] + ":" + timearray[2];

                        stm2 = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                        cmd2 = new MySqlCommand(stm2, con);

                        if (cmd2.ExecuteScalar() != null)
                        {
                            this.Close();
                            MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }
                        addP(cmd3);
                        return;
                    }

                    arrdt = Dt2arr(10, 30);
                    if (arrdt[0] == "-00" && int.Parse(arrdt[1]) < tim)
                    {
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 10, 30, 0);
                        string[] predtstring = dt2.ToString().Split(' ');
                        string[] timearray = predtstring[1].Split(':');
                        predtstring = predtstring[0].Split('.');
                        string time = predtstring[2] + "-" + predtstring[1] + "-" + predtstring[0] + " " + timearray[0] + ":" + timearray[1] + ":" + timearray[2];

                        stm2 = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                        cmd2 = new MySqlCommand(stm2, con);

                        if (cmd2.ExecuteScalar() != null)
                        {
                            this.Close();
                            MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }

                        addP(cmd3);
                        return;
                    }

                    arrdt = Dt2arr(12);
                    if (arrdt[0] == "-00" && int.Parse(arrdt[1]) < tim)
                    {
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 12, 0, 0);
                        string[] predtstring = dt2.ToString().Split(' ');
                        string[] timearray = predtstring[1].Split(':');
                        predtstring = predtstring[0].Split('.');
                        string time = predtstring[2] + "-" + predtstring[1] + "-" + predtstring[0] + " " + timearray[0] + ":" + timearray[1] + ":" + timearray[2];

                        stm2 = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                        cmd2 = new MySqlCommand(stm2, con);

                        if (cmd2.ExecuteScalar() != null)
                        {
                            this.Close();
                            MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }

                        addP(cmd3);
                        return;
                    }

                    arrdt = Dt2arr(15);
                    if (arrdt[0] == "-00" && int.Parse(arrdt[1]) < tim)
                    {
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 15, 0, 0);
                        string[] predtstring = dt2.ToString().Split(' ');
                        string[] timearray = predtstring[1].Split(':');
                        predtstring = predtstring[0].Split('.');
                        string time = predtstring[2] + "-" + predtstring[1] + "-" + predtstring[0] + " " + timearray[0] + ":" + timearray[1] + ":" + timearray[2];

                        stm2 = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                        cmd2 = new MySqlCommand(stm2, con);

                        if (cmd2.ExecuteScalar() != null)
                        {
                            this.Close();
                            MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }

                        addP(cmd3);
                        return;
                    }

                    arrdt = Dt2arr(17);
                    if (arrdt[0] == "-00" && int.Parse(arrdt[1]) < tim)
                    {
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 17, 0, 0);
                        string[] predtstring = dt2.ToString().Split(' ');
                        string[] timearray = predtstring[1].Split(':');
                        predtstring = predtstring[0].Split('.');
                        string time = predtstring[2] + "-" + predtstring[1] + "-" + predtstring[0] + " " + timearray[0] + ":" + timearray[1] + ":" + timearray[2];

                        stm2 = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                        cmd2 = new MySqlCommand(stm2, con);

                        if (cmd2.ExecuteScalar() != null)
                        {
                            this.Close();
                            MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }

                        addP(cmd3);
                        return;
                    }

                    arrdt = Dt2arr(19);
                    if (arrdt[0] == "-00" && int.Parse(arrdt[1]) < tim)
                    {
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 19, 0, 0);
                        string[] predtstring = dt2.ToString().Split(' ');
                        string[] timearray = predtstring[1].Split(':');
                        predtstring = predtstring[0].Split('.');
                        string time = predtstring[2] + "-" + predtstring[1] + "-" + predtstring[0] + " " + timearray[0] + ":" + timearray[1] + ":" + timearray[2];

                        stm2 = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                        cmd2 = new MySqlCommand(stm2, con);

                        if (cmd2.ExecuteScalar() != null)
                        {
                            this.Close();
                            MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }

                        addP(cmd3);
                        return;
                    }
                }
                else
                {
                    string[] arrdt = Dt2arr(6, 30);
                    if (arrdt[0] == "-00" && int.Parse(arrdt[1]) < tim)
                    {
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 6, 30, 0);
                        string[] predtstring = dt2.ToString().Split(' ');
                        string[] timearray = predtstring[1].Split(':');
                        predtstring = predtstring[0].Split('.');
                        string time = predtstring[2] + "-" + predtstring[1] + "-" + predtstring[0] + " " + timearray[0] + ":" + timearray[1] + ":" + timearray[2];

                        stm2 = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                        cmd2 = new MySqlCommand(stm2, con);

                        if (cmd2.ExecuteScalar() != null)
                        {
                            this.Close();
                            MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }
                        addP(cmd3, 3);
                        return;
                    }

                    arrdt = Dt2arr(7);
                    if (arrdt[0] == "-00" && int.Parse(arrdt[1]) < tim)
                    {
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 7, 0, 0);
                        string[] predtstring = dt2.ToString().Split(' ');
                        string[] timearray = predtstring[1].Split(':');
                        predtstring = predtstring[0].Split('.');
                        string time = predtstring[2] + "-" + predtstring[1] + "-" + predtstring[0] + " " + timearray[0] + ":" + timearray[1] + ":" + timearray[2];

                        stm2 = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                        cmd2 = new MySqlCommand(stm2, con);

                        if (cmd2.ExecuteScalar() != null)
                        {
                            this.Close();
                            MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }

                        addP(cmd3, 3);
                        return;
                    }

                    arrdt = Dt2arr(18);
                    if (arrdt[0] == "-00" && int.Parse(arrdt[1]) < tim)
                    {
                        DateTime dt2 = new DateTime(dt.Year, dt.Month, dt.Day, 18, 0, 0);
                        string[] predtstring = dt2.ToString().Split(' ');
                        string[] timearray = predtstring[1].Split(':');
                        predtstring = predtstring[0].Split('.');
                        string time = predtstring[2] + "-" + predtstring[1] + "-" + predtstring[0] + " " + timearray[0] + ":" + timearray[1] + ":" + timearray[2];

                        stm2 = "SELECT `ID` FROM `historia` WHERE '" + time + "' - INTERVAL 30 MINUTE < `Data` AND `ID_ministrant` =" + ID;
                        cmd2 = new MySqlCommand(stm2, con);

                        if (cmd2.ExecuteScalar() != null)
                        {
                            this.Close();
                            MessageBox.Show("Już się zalogowałeś!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            con.Close();
                            return;
                        }

                        addP(cmd3);
                        return;
                    }
                }
                MessageBox.Show("Zalogować się można tylko 30 min przed mszą bądź nabożeństwem", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Close();
            
        }

        private void frmZalogowano_Shown(object sender, EventArgs e)
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
