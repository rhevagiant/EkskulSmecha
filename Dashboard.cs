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
    public partial class Dashboard : Form
    {
        private const string ConnectionString = "Host=localhost;Username=postgres;Password=SMKbisa1234;Database=EkskulDatabase";
        public Dashboard()
        {
            InitializeComponent();
            InitializeUI();
            LoadUsers();
        }

        private void InitializeUI()
        {
            DataGridView dgvUsers = new DataGridView();
            dgvUsers.Location = new System.Drawing.Point(50, 50);
            dgvUsers.Size = new System.Drawing.Size(300, 200);
            this.Controls.Add(dgvUsers);

            Button btnEditUser = new Button();
            btnEditUser.Text = "Edit User";
            btnEditUser.Location = new System.Drawing.Point(50, 270);
            btnEditUser.Click += (s, ev) => EditSelectedUser(dgvUsers);
            this.Controls.Add(btnEditUser);
        }

        private void LoadUsers()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM users";

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        ((DataGridView)this.Controls[0]).DataSource = dataTable;
                    }
                }
            }
        }

        private void EditSelectedUser(DataGridView dgvUsers)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells[0].Value);

                EditUserForm editUserForm = new EditUserForm(userId);
                editUserForm.ShowDialog();

                LoadUsers();
            } else
            {
                MessageBox.Show("Please select a user to edit.");
            }
        }
    }
}
