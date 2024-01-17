using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EkskulSmecha
{
    public partial class EditUserForm : Form
    {
        private const string ConnectionString = "Host=localhost;Usernaame=postgres;Password=SMKbisa1234;Database=EkskulDatabase";
        private int UserId;

        public EditUserForm(int userId)
        {
            InitializeComponent();
            this.UserId = userId;
            InitializeUI();
            LoadUserData();
        }

        private void InitializeUI()
        {
            Label lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Width = 50;
            lblUsername.Location = new System.Drawing.Point(50, 50);
            this.Controls.Add(lblUsername);

            TextBox txtUsername = new TextBox();
            txtUsername.Location = new System.Drawing.Point(150, 80);
            this.Controls.Add(txtUsername);

            Label lblNama = new Label();
            lblNama.Text = "Nama:";
            lblNama.Width = 50;
            lblNama.Location = new System.Drawing.Point(50, 110);
            this.Controls.Add(lblNama);

            TextBox txtNama = new TextBox();
            txtNama.Location = new System.Drawing.Point(150, 110);
            this.Controls.Add(txtNama);

            Label lblKelas = new Label();
            lblKelas.Text = "Kelas:";
            lblKelas.Width = 50;
            lblKelas.Location = new System.Drawing.Point(50, 140);
            this.Controls.Add(lblKelas);

            TextBox txtKelas = new TextBox();
            txtKelas.Location = new System.Drawing.Point(150, 140);
            this.Controls.Add(txtKelas);

            Label lblProgli = new Label();
            lblProgli.Text = "Nama:";
            lblProgli.Width = 50;
            lblProgli.Location = new System.Drawing.Point(50, 170);
            this.Controls.Add(lblProgli);

            TextBox txtProgli = new TextBox();
            txtProgli.Location = new System.Drawing.Point(150, 170);
            this.Controls.Add(txtProgli);
        }

        private void LoadUserData()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString)) 
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM users WHERE user_id = @userId";
                    command.Parameters.AddWithValue("@userId", UserId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ((TextBox)this.Controls[1]).Text = reader["username"].ToString();
                            ((TextBox)this.Controls[3]).Text = reader["nama"].ToString();
                            ((TextBox)this.Controls[4]).Text = reader["kelas"].ToString();
                            ((TextBox)this.Controls[5]).Text = reader["progli"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("User not Found,");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void SaveChange(string newUsername, string newNAma, string newKelas, string newProgli)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE users SET username = @newUsername, nama = @newNama, kelas = @newKelas, progli = @newProgli WHERE user_id = @userid";
                    command.Parameters.AddWithValue("@newUsername", newUsername);
                    command.Parameters.AddWithValue("@newNama", newNAma);
                    command.Parameters.AddWithValue("@newKelas", newKelas);
                    command.Parameters.AddWithValue("@newProgli", newProgli);
                    command.Parameters.AddWithValue("@userId", UserId);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Changes saved successfully.");
                    this.Close();
                }
            }
        }

        private void EditUserForm_Load (object sender, EventArgs e)
        {

        }
    }
}
