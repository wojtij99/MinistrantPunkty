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
    public partial class frmLoged : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        int ID;
        DateTime dt = DateTime.Now;
        #endregion
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

        public frmLoged(int id)
        {
            InitializeComponent();
            ID = id;
        }

        private void frmLoged_Load(object sender, EventArgs e)
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

            #endregion

            if (setoff)
            {
                Close();
                return;
            }

            #region Obligatory

            stm = "SELECT `constevents`.pkt, `constevents`.Hour FROM `constevents`, `ministranci` WHERE `constevents`.`DayOfWeek`='" + DayofWeekANG2PL(dt.DayOfWeek.ToString()) + "' AND `constevents`.`Hour`< AddTime('" + dt.ToLongTimeString() + "','01:00:00') AND `constevents`.`Hour`> '" + dt.ToLongTimeString() + "' AND `ministranci`.`Obowiazkowe` LIKE '%" + DayofWeekANG2PL(dt.DayOfWeek.ToString()) + "%' AND `ministranci`.`ID` = " + ID + " LIMIT 1";
            cmd = new MySqlCommand(stm, con);

            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                labPoints.Text = "Przyznano " + rdr.GetInt32(0) + "pkt";
                cmd.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "'," + rdr.GetInt32(0) + ",'Obowiązkowe',now())";
                cmd.ExecuteNonQuery();
                return;
            }
            #endregion

            stm = "SELECT `constevents`.pkt, `constevents`.Hour FROM `constevents` WHERE `constevents`.`DayOfWeek`='" + DayofWeekANG2PL(dt.DayOfWeek.ToString()) + "' AND `constevents`.`Hour`< AddTime('" + dt.ToLongTimeString() + "','01:00:00') AND `constevents`.`Hour`> '" + dt.ToLongTimeString() + "'";
            cmd = new MySqlCommand(stm, con);

            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                labPoints.Text = "Przyznano 2 pkt";
                cmd.CommandText = "INSERT INTO historia VALUES (NULL,'" + ID + "',2,'Nadobowiązkowe',now())";
                cmd.ExecuteNonQuery();
                return;
            }
        }
    }
}
