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
    public partial class ProfileForm : Form
    {
        public ProfileForm()
        {
            InitializeComponent();
            ConfigureForm();

        }
        private AuthRepository authRepository = new AuthRepository();

        private Coordinator loggedInCoordinator;
        private CellRepository cellRepository = new CellRepository();
        public ProfileForm(Coordinator coordinator) : this()
        {
            loggedInCoordinator = coordinator;

            if (loggedInCoordinator == null)
            {
                MessageBox.Show("Coordinator information is missing.");
                Close();
                return;
            }

            LoadProfile();
        }
        // Display the coordinator's existing information
        private void LoadProfile()
        {
            txtFirstName.Text = loggedInCoordinator.FirstName;
            txtLastName.Text = loggedInCoordinator.LastName;
            txtPhone.Text = loggedInCoordinator.Phone;

            try
            {
                var cells = cellRepository.GetAllCells();

                foreach (var cell in cells)
                {
                    if (cell.CellId == loggedInCoordinator.CellId)
                    {
                        txtCell.Text = cell.CellName;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load assigned cell: " + ex.Message);
            }
        }
        private void ConfigureForm()
        {
            txtCell.ReadOnly = true;

            txtCurrentPassword.PasswordChar = '*';
            txtNewPassword.PasswordChar = '*';
            txtConfirmPassword.PasswordChar = '*';

            btnSaveProfile.Click += btnSaveProfile_Click;
            btnChangePassword.Click += btnChangePassword_Click;
            btnHome.Click += btnHome_Click;
            btnBack.Click += btnBack_Click;
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {

        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Save changes to personal information
        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please fill in all profile fields.");
                return;
            }

            string phone = txtPhone.Text.Trim();

            if (phone.Length != 10 ||
                !System.Linq.Enumerable.All(phone, char.IsDigit))
            {
                MessageBox.Show(
                    "Telephone number must contain exactly 10 digits.");
                txtPhone.Focus();
                return;
            }

            try
            {
                bool updated = authRepository.UpdateCoordinatorProfile(
                    loggedInCoordinator.CoordinatorId,
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    phone
                );

                if (updated)
                {
                    loggedInCoordinator.FirstName = txtFirstName.Text.Trim();
                    loggedInCoordinator.LastName = txtLastName.Text.Trim();
                    loggedInCoordinator.Phone = phone;

                    MessageBox.Show("Profile updated successfully!");
                }
                else
                {
                    MessageBox.Show("No profile changes were saved.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not update profile: " + ex.Message);
            }
        }
        // Change the coordinator's password
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text) ||
                string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Please complete all password fields.");
                return;
            }

            if (txtNewPassword.Text.Length < 8)
            {
                MessageBox.Show(
                    "New password must contain at least 8 characters.");
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("New passwords do not match.");
                return;
            }

            if (txtNewPassword.Text == txtCurrentPassword.Text)
            {
                MessageBox.Show(
                    "Your new password must be different from your current password.");
                return;
            }

            try
            {
                bool changed = authRepository.ChangePassword(
                    loggedInCoordinator.CoordinatorId,
                    txtCurrentPassword.Text,
                    txtNewPassword.Text
                );

                if (changed)
                {
                    MessageBox.Show("Password changed successfully!");

                    txtCurrentPassword.Clear();
                    txtNewPassword.Clear();
                    txtConfirmPassword.Clear();
                }
                else
                {
                    MessageBox.Show("Current password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not change password: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
