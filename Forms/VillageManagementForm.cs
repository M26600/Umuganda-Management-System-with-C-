
using System;
using System.Windows.Forms;
using UmugandaManagementSystem.Models;
using UmugandaManagementSystem.Repositories;

namespace UmugandaManagementSystem.Forms
{
    public partial class VillageManagementForm : Form
    {
        private VillageRepository villageRepository =
            new VillageRepository();

        private Coordinator loggedInCoordinator;

        public VillageManagementForm()
        {
            InitializeComponent();
            ConfigureForm();
        }

        // Used when opening the form from the dashboard
        public VillageManagementForm(Coordinator coordinator)
            : this()
        {
            loggedInCoordinator = coordinator;
            LoadVillages();
        }

        // Configure the table and connect button events
        private void ConfigureForm()
        {
            dgvVillages.AutoGenerateColumns = true;
            dgvVillages.ReadOnly = true;
            dgvVillages.AllowUserToAddRows = false;

            dgvVillages.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvVillages.MultiSelect = false;

            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;

            dgvVillages.CellClick += dgvVillages_CellClick;
        }

        // Display villages belonging to the logged-in coordinator's cell
        private void LoadVillages()
        {
            try
            {
                dgvVillages.DataSource =
                    villageRepository.GetVillagesByCell(
                        loggedInCoordinator.CellId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load villages: " + ex.Message);
            }
        }

        // Add a new village
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
                return;

            try
            {
                bool result = villageRepository.AddVillage(
                    txtVillageName.Text.Trim(),
                    txtVillageCode.Text.Trim(),
                    txtLeaderName.Text.Trim(),
                    loggedInCoordinator.CellId
                );

                if (result)
                {
                    MessageBox.Show("Village added successfully!");

                    ClearFields();
                    LoadVillages();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not add village: " + ex.Message);
            }
        }

        // Update an existing village
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvVillages.CurrentRow == null)
            {
                MessageBox.Show("Please select a village first.");
                return;
            }

            if (!ValidateFields())
                return;

            try
            {
                int villageId = Convert.ToInt32(
                    dgvVillages.CurrentRow.Cells["VillageId"].Value);

                bool result = villageRepository.UpdateVillage(
                    villageId,
                    txtVillageName.Text.Trim(),
                    txtVillageCode.Text.Trim(),
                    txtLeaderName.Text.Trim(),
                    loggedInCoordinator.CellId
                );

                if (result)
                {
                    MessageBox.Show("Village updated successfully!");

                    ClearFields();
                    LoadVillages();
                }
                else
                {
                    MessageBox.Show(
                        "No changes were made, or the village could not be found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not update village: " + ex.Message);
            }
        }

        // Delete an existing village
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvVillages.CurrentRow == null)
            {
                MessageBox.Show("Please select a village first.");
                return;
            }

            DialogResult answer = MessageBox.Show(
                "Are you sure you want to delete this village?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (answer != DialogResult.Yes)
                return;

            try
            {
                int villageId = Convert.ToInt32(
                    dgvVillages.CurrentRow.Cells["VillageId"].Value);

                bool result = villageRepository.DeleteVillage(
                    villageId,
                    loggedInCoordinator.CellId
                );

                if (result)
                {
                    MessageBox.Show("Village deleted successfully!");

                    ClearFields();
                    LoadVillages();
                }
                else
                {
                    MessageBox.Show("Village could not be found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not delete village. It may already be used by an activity.\n\n"
                    + ex.Message);
            }
        }

        // Load selected village details into the textboxes
        private void dgvVillages_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvVillages.Rows[e.RowIndex];

            txtVillageName.Text =
                row.Cells["VillageName"].Value.ToString();

            txtVillageCode.Text =
                row.Cells["VillageCode"].Value.ToString();

            txtLeaderName.Text =
                row.Cells["LeaderName"].Value.ToString();
        }

        // Validate textboxes
        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtVillageName.Text) ||
                string.IsNullOrWhiteSpace(txtVillageCode.Text) ||
                string.IsNullOrWhiteSpace(txtLeaderName.Text))
            {
                MessageBox.Show(
                    "Please fill in all village details.");

                return false;
            }

            return true;
        }

        // Clear all textboxes and remove the selected row
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtVillageName.Clear();
            txtVillageCode.Clear();
            txtLeaderName.Clear();

            dgvVillages.ClearSelection();
            dgvVillages.CurrentCell = null;
        }

        // Retained in case the Designer still references these events
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void VillageManagementForm_Load(
            object sender, EventArgs e)
        {
        }
    }
}
