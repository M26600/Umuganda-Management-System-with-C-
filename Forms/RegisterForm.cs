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
    public partial class RegisterForm : Form
    {
        private AuthRepository authRepository = new AuthRepository();
        private CellRepository cellRepository = new CellRepository();
        public RegisterForm()
        {
            InitializeComponent();
            LoadCells();
        }
        private void LoadCells()
        {
            try
            {
                cmbCell.DataSource = cellRepository.GetAllCells();
                cmbCell.DisplayMember = "CellName";
                cmbCell.ValueMember = "CellId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load cells: " + ex.Message);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
        string.IsNullOrWhiteSpace(txtLastName.Text) ||
        string.IsNullOrWhiteSpace(txtPhone.Text) ||
        string.IsNullOrWhiteSpace(txtUsername.Text) ||
        string.IsNullOrWhiteSpace(txtPassword.Text) ||
        string.IsNullOrWhiteSpace(txtConfirmPassword.Text) ||
        cmbCell.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            string phone = txtPhone.Text.Trim();

            if (phone.Length != 10 || !phone.All(char.IsDigit))
            {
                MessageBox.Show(
                    "Telephone number must contain exactly 10 digits.",
                    "Invalid Telephone Number",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPhone.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must contain at least 8 characters.");
                return;
            }

            try
            {
                int cellId = Convert.ToInt32(cmbCell.SelectedValue);

                bool registered = authRepository.RegisterCoordinator(
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    txtPhone.Text.Trim(),
                    cellId,
                    txtUsername.Text.Trim(),
                    txtPassword.Text
                );

                if (registered)
                {
                    MessageBox.Show("Registration successful!");

                    LoginForm login = new LoginForm();
                    login.Show();

                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration failed: " + ex.Message);
            }
        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();

            this.Hide();
        }
    }
}
