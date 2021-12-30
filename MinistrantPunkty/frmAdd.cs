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
    public partial class frmAdd : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        #endregion

        public frmAdd()
        {
            InitializeComponent();
        }

        private void frmAdd_Load(object sender, EventArgs e)
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

            var stm = "SELECT * FROM `ministranci`";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dataGridView1.Rows.Add(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetInt32(4).ToString());
            }
            rdr.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
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

            var stm = "SELECT `ID` FROM `ministranci` WHERE `haslo`='" + txtPass.Text+"'";
            var cmd = new MySqlCommand(stm, con);
            if (cmd.ExecuteScalar() == null)
            {
                cmd = new MySqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "INSERT INTO ministranci VALUES (NULL,'" + txtName.Text + "','" + txtLastname.Text + "','" + txtPass.Text + "',0,'')";
                cmd.ExecuteNonQuery();

                MessageBox.Show("Dodano misnitranta: " + txtName.Text + " " + txtLastname.Text + " z hasłem " + txtPass.Text, "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.Rows.Add(txtName.Text, txtLastname.Text, txtPass.Text, 0);
            }
            else 
            {
                MessageBox.Show("Takie hasło już istnieje!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txtLastname.Text = "";
            txtName.Text = "";
            txtPass.Text = "";

            con.Close();
        }
    }
}
