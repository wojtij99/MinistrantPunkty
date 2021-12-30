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
    public partial class frmRanking : Form
    {
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        int[] persran = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        #endregion

        public frmRanking()
        {
            InitializeComponent();
        }

        void pranf(int id)
        {
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

            for (int i = 1; i <= 12; i++) 
            {
                var stm2 = "SELECT `Punkty` FROM `historia` WHERE MONTH(Data) = " + i + " AND `ID_ministrant` = " + id;
                var cmd2 = new MySqlCommand(stm2, con2);

                MySqlDataReader rdr2 = cmd2.ExecuteReader();
                int mies = 0;
                while (rdr2.Read())
                {
                    mies += rdr2.GetInt32(0);
                }
                persran[i - 1] = mies;
                rdr2.Close();
            }
            con2.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRanking_Load(object sender, EventArgs e)
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

            var stm = "SELECT * FROM `ministranci` ORDER BY `ministranci`.`punkty` DESC";
            var cmd = new MySqlCommand(stm, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            int[] colorID = new int[10];
            for (int x = 0; x < 10; x++)
            {
                colorID[x] = -1;
            }
            int i = 1;
            while (rdr.Read())
            {
                pranf(rdr.GetInt32(0));
                
                dataGridView1.Rows.Add(i,rdr.GetString(1),rdr.GetString(2), persran[0], persran[1], persran[2], persran[3], persran[4], persran[5], persran[6],
                     persran[7], persran[8], persran[9], persran[10], persran[11], rdr.GetInt32(4).ToString());

                if (rdr.GetString(1) == "Adam" && rdr.GetString(2) == "Sowa")
                {
                    colorID[1] = i - 1;
                }
                else if (rdr.GetString(1) == "Wojciech" && rdr.GetString(2) == "Jędrzejewski")
                {
                    colorID[0] = i - 1;
                }
                else if (rdr.GetString(1) == "Mikołaj" && rdr.GetString(2) == "Walkowicz")
                {
                    colorID[2] = i - 1;
                }
                i++;
                
            }

            for (int j = 0; j < colorID.Length; j++)
            {
                if (colorID[j] == -1) continue;
                dataGridView1.Rows[colorID[j]].Cells[1].Style = new DataGridViewCellStyle { ForeColor = Color.FromArgb(255, 140, 0) };
                dataGridView1.Rows[colorID[j]].Cells[2].Style = new DataGridViewCellStyle { ForeColor = Color.FromArgb(255, 140, 0) };
                if (j == 0)
                {
                    dataGridView1.Rows[colorID[j]].Cells[1].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    dataGridView1.Rows[colorID[j]].Cells[2].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                }
                else if (j == 2)
                {
                    dataGridView1.Rows[colorID[j]].Cells[1].Style = new DataGridViewCellStyle { ForeColor = Color.FromArgb(128, 0, 0) };
                    dataGridView1.Rows[colorID[j]].Cells[2].Style = new DataGridViewCellStyle { ForeColor = Color.FromArgb(128, 0, 0) };
                }

            }
            
        }
    }
}
