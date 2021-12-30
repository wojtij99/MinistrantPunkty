using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MinistrantKatedra
{
    public partial class frmRanking : Form
    {
        struct Ministrant
        {
            public string imie;
            public string nazwisko;
            public int[] pkts;
            public int pkt;
        }
        #region Zmienne
        string cs = @"server=localhost;userid=root;password=;database=ministrancikatedra;charset=utf8";
        int[] persran = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int pkt = 0;
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
                pkt += mies;
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

            Ministrant[] ministrant = { };

            while (rdr.Read())
            {
                pkt = 0;
                pranf(rdr.GetInt32(0));

                Array.Resize(ref ministrant, ministrant.Length + 1);
                ministrant[ministrant.Length - 1].imie = rdr.GetString(1);
                ministrant[ministrant.Length - 1].nazwisko = rdr.GetString(2);
                ministrant[ministrant.Length - 1].pkts = new int[] { persran[0], persran[1], persran[2], persran[3], persran[4], persran[5], persran[6], persran[7], persran[8], persran[9], persran[10], persran[11] };
                ministrant[ministrant.Length - 1].pkt = pkt;

                /*dataGridView1.Rows.Add(i, rdr.GetString(1), rdr.GetString(2), persran[0], persran[1], persran[2], persran[3], persran[4], persran[5], persran[6],
                     persran[7], persran[8], persran[9], persran[10], persran[11], pkt);*/
                i++;

            }

            Ministrant temp;

            for (int write = 0; write < ministrant.Length; write++)
            {
                for (int sort = 0; sort < ministrant.Length - 1; sort++)
                {
                    if (ministrant[sort].pkt < ministrant[sort + 1].pkt)
                    {
                        temp = ministrant[sort + 1];
                        ministrant[sort + 1] = ministrant[sort];
                        ministrant[sort] = temp;
                    }
                }
            }

            i = 1;
            foreach (Ministrant m in ministrant)
            {
                dataGridView1.Rows.Add(i, m.imie, m.nazwisko, m.pkts[0], m.pkts[1], m.pkts[2], m.pkts[3], m.pkts[4], m.pkts[5], m.pkts[6], m.pkts[7], m.pkts[8], m.pkts[9], m.pkts[10], m.pkts[11], m.pkt);
                if (m.imie == "Adam" && m.nazwisko == "Sowa")
                {
                    colorID[1] = i - 1;
                }
                else if (m.imie == "Wojciech" && m.nazwisko == "Jędrzejewski")
                {
                    colorID[0] = i - 1;
                }
                else if (m.imie == "Mikołaj" && m.nazwisko == "Walkowicz")
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
