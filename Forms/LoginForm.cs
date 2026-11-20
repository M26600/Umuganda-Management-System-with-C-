using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmugandaManagementSystem.Models;
using UmugandaManagementSystem.Repositories;

namespace UmugandaManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        private AuthRepository authRepository = new AuthRepository();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
       string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter your username and password.");
                return;
            }

            try
            {
                Coordinator coordinator = authRepository.Login(
                    txtUsername.Text.Trim(),
                    txtPassword.Text
                );

                if (coordinator != null)
                {
                    MessageBox.Show(
                        "Welcome, " + coordinator.FirstName + "!"
                    );

                    DashboardForm dashboard = new DashboardForm(coordinator);
                    dashboard.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed: " + ex.Message);
            }
        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.Show();

            this.Hide();
        }
    }
}
