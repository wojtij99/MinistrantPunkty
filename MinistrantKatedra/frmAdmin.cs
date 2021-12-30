using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MinistrantKatedra
{
    public partial class frmAdmin : Form
    {
        public bool OFF
        { get; set; }

        #region Structures
        struct Ministrant
        {
            public string Name;
            public string Lastname;
            public string Obow;
            public string Waka;
        }
        #endregion

        #region Zmienne
        Ministrant[] ministranci = null;

        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        string setoff = "";

        int prew = 0;
        int prew2 = 0;
        uint offset = 0;

        bool[] isEntered = {false, false, false, false, false, false, false };
        #endregion

        #region Functions
        void history2Reload()
        {
            dgvHistory2.Rows.Clear();
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
            string[] arr = coxUserHistory.Text.Split(' ');
            var stm = "SELECT * FROM `historia` WHERE `imie` = " + arr[0] + " `nazwisko` = " + arr[1] + " ORDER BY `historia`.`Data` DESC";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                dgvHistory2.Rows.Add(rdr.GetString(2), rdr.GetString(3), rdr.GetString(4));
            }
            rdr.Close();
            con.Close();
        }

        void historyReload()
        {
            dgvHistory.Rows.Clear();

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

            var stm = "SELECT count(*) FROM `historia` ORDER BY `historia`.`ID` DESC LIMIT 50 OFFSET " + offset * 50;
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if (rdr.GetInt32(0) == 0) return;
            }

            rdr.Close();

            stm = "SELECT * FROM `historia` ORDER BY `historia`.`ID` DESC LIMIT 50 OFFSET " + offset * 50;
            cmd = new MySqlCommand(stm, con);

            rdr = cmd.ExecuteReader();

            int i = 0;
            while (rdr.Read())
            {
                i++;
                string stm2 = "SELECT `Imie` FROM `ministranci` WHERE `ID`=" + rdr.GetInt32(1).ToString();
                var cmd2 = new MySqlCommand(stm2, con2);
                string name = cmd2.ExecuteScalar().ToString();

                string stm3 = "SELECT `Nazwisko` FROM `ministranci` WHERE `ID`=" + rdr.GetInt32(1).ToString();
                var cmd3 = new MySqlCommand(stm3, con2);
                string lastname = cmd3.ExecuteScalar().ToString();

                dgvHistory.Rows.Add(rdr.GetInt32(0).ToString(), name, lastname, rdr.GetInt32(2).ToString(), rdr.GetString(3), rdr.GetString(4));
            }
            if (i == 0) { offset--; historyReload(); }

            con.Close();
            con2.Close();
        }

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

            var stm = "SELECT  `Imie`, `Nazwisko`, `Obowiazkowe`, `Wakacyjne` FROM `ministranci`";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            int i = 0;
            coxUserObligatory.Items.Clear();
            coxUserHistory.Items.Clear();
            while (rdr.Read())
            {
                coxUserObligatory.Items.Add(rdr.GetString(0) + " " + rdr.GetString(1));
                coxUserHistory.Items.Add(rdr.GetString(0) + " " + rdr.GetString(1));
                ministranci[i].Name = rdr.GetString(0);
                ministranci[i].Lastname = rdr.GetString(1);
                ministranci[i].Obow = rdr.GetString(2);
                ministranci[i].Waka = rdr.GetString(3);
                i++;
            }
            con.Close();
        }
        #endregion

        public frmAdmin()
        {
            OFF = false;
            InitializeComponent();

            dtpEvent.Format = DateTimePickerFormat.Custom;
            dtpEvent.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpEvent.Value = DateTime.Now;

            dtpStop.Format = DateTimePickerFormat.Custom;
            dtpStop.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpStop.Value = DateTime.Now;
        }

        #region Buttons
        #region History
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (offset == 0) return;
            offset--;
            historyReload();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            offset++;
            historyReload();
        }
        #endregion

        #region Events
        private void btnExecuteEvent_Click(object sender, EventArgs e)
        {
            DateTime dt = dtpEvent.Value;
            string datetime = dt.ToString();

            string[] arr = datetime.Split(' ');
            string[] arr2 = arr[0].Split('.');

            datetime = arr2[2] + "-" + arr2[1] + "-" + arr2[0] + " " + arr[1];

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

            cmd.CommandText = "INSERT INTO `events` VALUES (NULL,'" + txtPointsEvent.Text + "','" + datetime + "')";
            cmd.ExecuteNonQuery();

            dgvEvents.Rows.Add(datetime, txtPointsEvent.Text);
            MessageBox.Show("Wykonano polecenie", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Stop
        private void btnExecuteStop_Click(object sender, EventArgs e)
        {
            DateTime dt = dtpStop.Value;

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

            dgvStop.Rows.Add(date);
            MessageBox.Show("Wykonano polecenie", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Points
        private void btnExecutePoints_Click(object sender, EventArgs e)
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

            string[] arr = coxUserPoints.Text.Split(' ');

            var stm = "SELECT `ID` FROM `ministranci` WHERE `Imie`='" + arr[0] + "' AND `Nazwisko`='" + arr[1] + "'";
            var cmd = new MySqlCommand(stm, con);

            Console.WriteLine(stm);

            string id = cmd.ExecuteScalar().ToString();

            cmd = new MySqlCommand();
            cmd.Connection = con;

            //cmd.CommandText = "UPDATE ministranci SET Punkty=punkty+" + txtPoints.Text + " WHERE `Imie`='" + arr[0] + "' AND `Nazwisko`='" + arr[1] + "'";
            //cmd.ExecuteNonQuery();
            string Description = txtDescription.Text;
            if (Description == "") Description = "Admin";

            cmd.CommandText = "INSERT INTO `historia` VALUES (NULL,'" + id + "'," + txtPoints.Text + ",'" + Description + "',now())";
            cmd.ExecuteNonQuery();

            MessageBox.Show("Wykonano", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
        }
        #endregion

        #region Obligatory
        private void btnExecuteObligatory_Click(object sender, EventArgs e)
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

            string[] arr = coxUserObligatory.Text.Split(' ');
            string cov = coxDayObligatory.Text;
            cov += "-";
            cov += coxHourObligatory.Text;
            cov += "|";
            cmd.CommandText = "UPDATE `ministranci` SET `Obowiazkowe`= CONCAT(`Obowiazkowe` , '" + cov + "') WHERE `Imie`='" + arr[0] + "' AND `Nazwisko`='" + arr[1] + "'";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Wykonano polecenie", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Console.WriteLine(cmd.CommandText);
            con.Close();
            prew = coxUserObligatory.SelectedIndex;
            refresh();

            coxUserObligatory_SelectedIndexChanged(coxUserObligatory, e);
        }

        private void btnClearObligatory_Click(object sender, EventArgs e)
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

            cmd.CommandText = "UPDATE `ministranci` SET `Obowiazkowe`='' WHERE `Imie`='" + ministranci[coxUserObligatory.SelectedIndex].Name + "' AND `Nazwisko`='" + ministranci[coxUserObligatory.SelectedIndex].Lastname + "'";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Wykonano polecenie", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            prew = coxUserObligatory.SelectedIndex;
            refresh();

            coxUserObligatory_SelectedIndexChanged(coxUserObligatory, e);
        }
        #endregion

        #region Register

        private void btnAddRegister_Click(object sender, EventArgs e)
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

            var stm = "SELECT `ID` FROM `ministranci` WHERE `haslo`='" + txtPassRegister.Text + "'";
            var cmd = new MySqlCommand(stm, con);
            if (txtPassRegister.Text.All(char.IsSymbol))
            {
                MessageBox.Show("Hasło zawiera niedozwolone znaki!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmd.ExecuteScalar() == null)
            {
                cmd = new MySqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "INSERT INTO ministranci VALUES (NULL,'" + txtNameRegister.Text + "','" + txtLastnameRegister.Text + "','" + txtPassRegister.Text + "',0,'','')";
                cmd.ExecuteNonQuery();

                MessageBox.Show("Dodano misnitranta: " + txtNameRegister.Text + " " + txtLastnameRegister.Text + " z hasłem " + txtPassRegister.Text, "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvRegister.Rows.Add(txtNameRegister.Text, txtLastnameRegister.Text, txtPassRegister.Text, 0);
            }
            else
            {
                MessageBox.Show("Takie hasło już istnieje!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtLastnameRegister.Text = "";
            txtNameRegister.Text = "";
            txtPassRegister.Text = "";

            con.Close();
        }

        #endregion

        #region Wacation
        private void btnWacationExecute_Click(object sender, EventArgs e)
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

            string[] arr = coxWacationUsers.Text.Split(' ');
            string cov = dtpWacation.Value.Day.ToString();
            cov += "-";
            cov += dtpWacation.Value.Month.ToString();
            if (coxWacationHour.Text != "Cały dzień") cov += "." + coxWacationHour.Text;
            cov += "|";
            cmd.CommandText = "UPDATE `ministranci` SET `Wakacyjne`= `Wakacyjne` '" + cov + "' WHERE `Imie`='" + arr[0] + "' AND `Nazwisko`='" + arr[1] + "'";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Wykonano polecenie", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            string txt = dtpWacation.Value.Day.ToString() + "." +dtpWacation.Value.Month.ToString();
            if (coxWacationHour.Text != "Cały dzień") txt += "-" + coxWacationHour.Text;
            dgvWacation.Rows.Add(txt);
            prew2 = coxWacationUsers.SelectedIndex;
            refresh();

            coxWacationUsers_SelectedIndexChanged(coxWacationUsers, e);
        }
        #endregion

        #region Advanced
        private void btnOFF_Click(object sender, EventArgs e)
        {
            OFF = true;
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy napewno chcesz dokonać resetu danych?\r\n Wszystkie wydarzenia i historia zostaną usunięte,\r\na wszystkie punkty zostaną ustawione na 0!", "Ostrzeżenie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;
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

            cmd.CommandText = "TRUNCATE `historia`";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "TRUNCATE `events`";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "UPDATE `ministranci` SET `Punkty`=0";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "UPDATE `techniczna` SET `V1`='' WHERE `ID` = 3";
            cmd.ExecuteNonQuery();

            MessageBox.Show("Wykonano polecenie", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #endregion

        #region Combo Boxes

        #region History2
        private void coxUserHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            history2Reload();
        }
        #endregion

        #region Obligatory
        private void coxDayObligatory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (coxDayObligatory.SelectedIndex == 6)
            {
                coxHourObligatory.Items.Clear();
                coxHourObligatory.Items.Add("7:00");
                coxHourObligatory.Items.Add("9:00");
                coxHourObligatory.Items.Add("10:30");
                coxHourObligatory.Items.Add("12:00");
                coxHourObligatory.Items.Add("15:00");
                coxHourObligatory.Items.Add("17:00");
                coxHourObligatory.Items.Add("19:00");
                coxHourObligatory.Text = "";
            }
            else
            {
                coxHourObligatory.Items.Clear();
                coxHourObligatory.Items.Add("6:30");
                coxHourObligatory.Items.Add("7:00");
                coxHourObligatory.Items.Add("18:00");
                coxHourObligatory.Text = "";
            }
        }

        private void coxUserObligatory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (coxUserObligatory.SelectedIndex == -1) { coxUserObligatory.SelectedIndex = prew; return; }
            dgvObligatory.Rows.Clear();

            string[] arr0 = coxUserObligatory.Text.Split(' ');
            int i = -1;
            for (i = 0; i < ministranci.Length; i++) if (ministranci[i].Name == arr0[0] && ministranci[i].Lastname == arr0[1]) break;
            if (i == -1) return;

            string[] arr = ministranci[i].Obow.Split('|');

            for (i = 0; i < arr.Length - 1; i++)
            {
                string[] arr2 = arr[i].Split('-');
                dgvObligatory.Rows.Add(arr2[0], arr2[1]);
            }
        }
        #endregion

        #region Wacation
        private void coxWacationUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (coxWacationUsers.SelectedIndex == -1) { coxWacationUsers.SelectedIndex = prew2; return; }
            dgvWacation.Rows.Clear();

            string[] arr0 = coxUserObligatory.Text.Split(' ');
            int i = -1;
            for (i = 0; i < ministranci.Length; i++) if (ministranci[i].Name == arr0[0] && ministranci[i].Lastname == arr0[1]) break;
            if (i == -1) return;

            string[] arr = ministranci[i].Waka.Split('|');

            for (i = 0; i < arr.Length - 1; i++)
            {
                string[] arr2 = arr[i].Split('.');
                string[] arr3 = arr2[0].Split('-');
                if(arr2.Length == 2) dgvWacation.Rows.Add(arr3[0], arr3[1], arr2[1]);
                else dgvWacation.Rows.Add(arr3[0], arr3[1]);
            }
        }
        #endregion

        #endregion

        #region Tab Pages
        private void tapHistory_Enter(object sender, EventArgs e)
        {
            //if (isEntered[0]) return;
            //offset = 0;
            historyReload();
            //isEntered[0] = true;
        }

        private void tapHistory2_Enter(object sender, EventArgs e)
        {
            if (isEntered[0]) return;
            refresh();
            isEntered[0] = true;
        }

        private void tapEvents_Enter(object sender, EventArgs e)
        {
            if (isEntered[1]) return;

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

            var stm = "SELECT * FROM `events`";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dgvEvents.Rows.Add(rdr.GetString(2), rdr.GetString(1));
            }
            isEntered[1] = true;
        }

        private void tapStop_Enter(object sender, EventArgs e)
        {
            if (isEntered[2]) return;
            
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
                foreach (string a in arr) if(a != "") dgvStop.Rows.Add(a);
            }

            isEntered[2] = true;
        }

        private void tapPoints_Enter(object sender, EventArgs e)
        {
            if (isEntered[3]) return;

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

            //MySqlCommand licznik_rek = new MySqlCommand("SELECT  COUNT(*) FROM `ministranci`", con);
            //int count = Convert.ToInt32(licznik_rek.ExecuteScalar());

            var stm = "SELECT  `Imie`, `Nazwisko`, `Obowiazkowe` FROM `ministranci`";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            coxUserPoints.Items.Clear();
            while (rdr.Read())
            {
                coxUserPoints.Items.Add(rdr.GetString(0) + " " + rdr.GetString(1));
            }
            con.Close();

            isEntered[3] = true;
        }

        private void tapObligatory_Enter(object sender, EventArgs e)
        {
            if (isEntered[4]) return;
            refresh();
            isEntered[4] = true;
        }

        private void tapRegister_Enter(object sender, EventArgs e)
        {
            if (isEntered[5]) return;
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

            var stm = "SELECT * FROM `ministranci`";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dgvRegister.Rows.Add(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetInt32(4).ToString());
            }
            isEntered[5] = true;
        }
        private void tapWacation_Enter(object sender, EventArgs e)
        {
            refresh();
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

            var stm = "SELECT  `Imie`, `Nazwisko`, `Obowiazkowe` FROM `ministranci`";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            coxWacationUsers.Items.Clear();
            while (rdr.Read())
            {
                coxWacationUsers.Items.Add(rdr.GetString(0) + " " + rdr.GetString(1));
            }
            con.Close();
        }
        #endregion
    }
}
