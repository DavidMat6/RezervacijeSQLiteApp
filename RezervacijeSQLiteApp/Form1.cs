using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RezervacijeSQLiteApp
{

    public partial class Form1 : Form
    {
        private List<Rezervacija> rezervacije = new List<Rezervacija>();
        private string connectionString = "Data Source=rezervacije.db";

        public Form1()
        {
            InitializeComponent();
            LoadRezervacije();
        }

        private void LoadRezervacije()
        {
            rezervacije.Clear();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Rezervacije", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rezervacije.Add(new Rezervacija(
                            Convert.ToInt32(reader["ID"]),
                            Convert.ToInt32(reader["KlijentID"]),
                            Convert.ToInt32(reader["UslugaID"]),
                            DateTime.Parse(reader["Datum"].ToString()),
                            TimeSpan.Parse(reader["Vrijeme"].ToString())
                        ));
                    }
                }
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = rezervacije;
            dataGridView1.Columns["Datum"].Visible = false;
            dataGridView1.Columns["Vrijeme"].Visible = false;
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            var nova = new Rezervacija(0,
            int.Parse(txtKlijentID.Text),
            int.Parse(txtUslugaID.Text),
            dateTimePicker1.Value,
            dateTimePicker2.Value.TimeOfDay);

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand("INSERT INTO Rezervacije (KlijentID, UslugaID, Datum, Vrijeme) VALUES (@k, @u, @d, @v)", conn);
                cmd.Parameters.AddWithValue("@k", nova.KlijentID);
                cmd.Parameters.AddWithValue("@u", nova.UslugaID);
                cmd.Parameters.AddWithValue("@d", nova.Datum.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@v", nova.Vrijeme.ToString());
                cmd.ExecuteNonQuery();
            }

            LoadRezervacije();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var r = (Rezervacija)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                MessageBox.Show($"Klijent ID: {r.KlijentID}\nUsluga ID: {r.UslugaID}\nDatum: {r.Datum:d}\nVrijeme: {r.Vrijeme}");
            }
        }
    }
}
