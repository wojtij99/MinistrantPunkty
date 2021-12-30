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
    public partial class frmAdmin : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        #endregion

        public frmAdmin()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frmAdd form = new frmAdd())
            {
                form.ShowDialog();
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            using (frmSet form = new frmSet())
            {
                form.ShowDialog();
            }
        }

        private void btnPoints_Click(object sender, EventArgs e)
        {
            using (frmPoints form = new frmPoints())
            {
                form.ShowDialog();
            }
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            using (frmEvents form = new frmEvents())
            {
                form.ShowDialog();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
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

        private void btnHistory_Click(object sender, EventArgs e)
        {
            using (frmHistory form = new frmHistory())
            {
                form.ShowDialog();
            }
        }

        private void btnSetOFF_Click(object sender, EventArgs e)
        {
            using (frmSetOFF form = new frmSetOFF())
            {
                form.ShowDialog();
            }
        }
    }
}
