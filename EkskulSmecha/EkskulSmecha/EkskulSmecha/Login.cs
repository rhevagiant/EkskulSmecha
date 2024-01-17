﻿using System;
using System.Data;
using System.Windows.Forms;
using EkskulSmecha;
using Npgsql;

namespace EkskulSmecha
{
    public partial class Login : Form
    {
        private const string ConnectionString = "Host=localhost;Username=postgres;Password=SMKbisa1234;Database=EkskulDatabase";

        public Login()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Add labels
            Label lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Location = new System.Drawing.Point(50, 50);
            this.Controls.Add(lblUsername);

            Label lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Location = new System.Drawing.Point(50, 80);
            this.Controls.Add(lblPassword);

            Label lblNama = new Label();
            lblNama.Text = "Nama:";
            lblNama.Location = new System.Drawing.Point(50, 110);
            this.Controls.Add(lblNama);

            Label lblKelas = new Label();
            lblKelas.Text = "Kelas:";
            lblKelas.Location = new System.Drawing.Point(50, 140);
            this.Controls.Add(lblKelas);

            Label lblProgli = new Label();
            lblProgli.Text = "Progli:";
            lblProgli.Location = new System.Drawing.Point(50, 170);
            this.Controls.Add(lblProgli);

            // Add textboxes
            TextBox txtUsername = new TextBox();
            txtUsername.Location = new System.Drawing.Point(150, 50);
            this.Controls.Add(txtUsername);

            TextBox txtPassword = new TextBox();
            txtPassword.Location = new System.Drawing.Point(150, 80);
            txtPassword.PasswordChar = '*'; // Mask the password
            this.Controls.Add(txtPassword);

            TextBox txtNama = new TextBox();
            txtNama.Location = new System.Drawing.Point(150, 110);
            this.Controls.Add(txtNama);

            TextBox txtKelas = new TextBox();
            txtKelas.Location = new System.Drawing.Point(150, 140);
            this.Controls.Add(txtKelas);

            TextBox txtProgli = new TextBox();
            txtProgli.Location = new System.Drawing.Point(150, 170);
            this.Controls.Add(txtProgli);

            // Add a button for login
            Button btnLogin = new Button();
            btnLogin.Text = "Login";
            btnLogin.Location = new System.Drawing.Point(150, 200);
            btnLogin.Click += (s, ev) => AuthenticateUser(txtUsername.Text, txtPassword.Text);
            this.Controls.Add(btnLogin);


            Label lblRegister = new Label();

            lblRegister.Text = "Don't have an account? Click below button to create one:";
            lblRegister.Location = new System.Drawing.Point(150, 230);
            this.Controls.Add(lblRegister);

            // Add a button to go to register page
            Button btnRegister = new Button();
            btnRegister.Text = "Create Account";
            btnRegister.Location = new System.Drawing.Point(150, 260);
            btnRegister.Click += (s, ev) => RedirectRegisterForm();
            this.Controls.Add(btnRegister);
        }

        private void RedirectRegisterForm()
        {
            // Redirect to the Register form
            this.Hide(); // Hide the current form
            Register registerForm = new Register();
            registerForm.Show();
        }


        private void AuthenticateUser(string username, string password)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password); // Hash the password in a real application

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Login successful!");
                        this.Hide(); // Hide the current form
                        //Dashboard dashboard = new Dashboard();
                        //dashboard.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials!");
                    }
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }

}