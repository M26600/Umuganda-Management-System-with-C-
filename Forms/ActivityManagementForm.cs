using System;
using System.Windows.Forms;
using UmugandaManagementSystem.Models;
using UmugandaManagementSystem.Repositories;

namespace UmugandaManagementSystem.Forms
{
    public partial class ActivityManagementForm : Form
    {
        private Coordinator loggedInCoordinator;

        private VillageRepository villageRepository =
            new VillageRepository();

        private ActivityTypeRepository activityTypeRepository =
            new ActivityTypeRepository();

        private ActivityRepository activityRepository =
            new ActivityRepository();

        // Used by the Visual Studio Designer
        public ActivityManagementForm()
        {
            InitializeComponent();
            ConfigureForm();
        }

        // Used when opening the form after login
        public ActivityManagementForm(Coordinator coordinator)
            : this()
        {
            loggedInCoordinator = coordinator;
            LoadFormData();
        }

        private void ConfigureForm()
        {
            cmbVillage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbActivityType.DropDownStyle = ComboBoxStyle.DropDownList;

            dgvActivities.AutoGenerateColumns = true;
            dgvActivities.ReadOnly = true;
            dgvActivities.AllowUserToAddRows = false;
            dgvActivities.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvActivities.MultiSelect = false;
            dgvActivities.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            btnAddActivity.Click += btnAddActivity_Click;
            btnClear.Click += btnClear_Click;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            dtpNewDate.MinDate = DateTime.Today.AddDays(1);
            dgvActivities.CellClick += dgvActivities_CellClick;
            btnUpdateStatus.Click += btnUpdateStatus_Click;
            // Configure activity filter controls
            cmbFilterVillage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterActivityType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            dtpFilterDate.ShowCheckBox = true;
            dtpFilterDate.Checked = false;

            btnApplyFilters.Click += btnApplyFilters_Click;
            btnClearFilters.Click += btnClearFilters_Click;
            btnBack.Click += btnBack_Click;
            btnLogOut.Click += btnLogout_Click;
            btnUpdateActivity.Click += btnUpdateActivity_Click;
            btnDeleteActivity.Click += btnDeleteActivity_Click;

        }

        private void LoadFormData()
        {
            if (loggedInCoordinator == null)
                return;

            // Load only villages belonging to this coordinator's cell
            cmbVillage.DataSource =
                villageRepository.GetVillagesByCell(
                    loggedInCoordinator.CellId);

            cmbVillage.DisplayMember = "VillageName";
            cmbVillage.ValueMember = "VillageId";
            cmbVillage.SelectedIndex = -1;

            // Load predefined activity types
            cmbActivityType.DataSource =
                activityTypeRepository.GetAllActivityTypes();

            cmbActivityType.DisplayMember = "TypeName";
            cmbActivityType.ValueMember = "ActivityTypeId";
            cmbActivityType.SelectedIndex = -1;

            // Load villages for filtering
            cmbFilterVillage.DataSource =
                villageRepository.GetVillagesByCell(
                    loggedInCoordinator.CellId);

            cmbFilterVillage.DisplayMember = "VillageName";
            cmbFilterVillage.ValueMember = "VillageId";
            cmbFilterVillage.SelectedIndex = -1;

            // Load activity types for filtering
            cmbFilterActivityType.DataSource =
                activityTypeRepository.GetAllActivityTypes();

            cmbFilterActivityType.DisplayMember = "TypeName";
            cmbFilterActivityType.ValueMember = "ActivityTypeId";
            cmbFilterActivityType.SelectedIndex = -1;

            // Load statuses for filtering
            cmbFilterStatus.Items.Clear();

            foreach (ActivityStatus status in Enum.GetValues(typeof(ActivityStatus)))
            {
                cmbFilterStatus.Items.Add(status);
            }

            cmbFilterStatus.SelectedIndex = -1;

            LoadActivities();
        }

        private void LoadActivities()
        {
            if (loggedInCoordinator == null)
                return;

            dgvActivities.DataSource = null;

            dgvActivities.DataSource =
                activityRepository.GetActivitiesByCell(
                    loggedInCoordinator.CellId);

            // Hide technical IDs from the user
            if (dgvActivities.Columns["ActivityId"] != null)
                dgvActivities.Columns["ActivityId"].Visible = false;

            if (dgvActivities.Columns["VillageId"] != null)
                dgvActivities.Columns["VillageId"].Visible = false;

            if (dgvActivities.Columns["ActivityTypeId"] != null)
                dgvActivities.Columns["ActivityTypeId"].Visible = false;

            if (dgvActivities.Columns["CoordinatorId"] != null)
                dgvActivities.Columns["CoordinatorId"].Visible = false;
            cmbStatus.Items.Clear();
            cmbStatus.SelectedIndex = -1;
        }

        private void btnAddActivity_Click(object sender, EventArgs e)
        {
            if (loggedInCoordinator == null)
            {
                MessageBox.Show("Coordinator information is missing.");
                return;
            }

            if (cmbVillage.SelectedIndex == -1 ||
                cmbActivityType.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtObjective.Text))
            {
                MessageBox.Show(
                    "Please select a village, activity type, and enter an objective.");

                return;
            }

            if (dtpActivityDate.Value.Date <= DateTime.Today)
            {
                MessageBox.Show(
                    "Please select a future date for the activity.");

                return;
            }

            try
            {
                bool saved = activityRepository.AddActivity(
                    dtpActivityDate.Value,
                    txtObjective.Text.Trim(),
                    Convert.ToInt32(cmbVillage.SelectedValue),
                    Convert.ToInt32(cmbActivityType.SelectedValue),
                    loggedInCoordinator.CoordinatorId,
                    loggedInCoordinator.CellId
                );

                if (saved)
                {
                    MessageBox.Show("Activity planned successfully!");

                    LoadActivities();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show(
                        "Activity could not be saved. Check your selections.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not plan activity: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            cmbVillage.SelectedIndex = -1;
            cmbActivityType.SelectedIndex = -1;
            dtpActivityDate.Value = DateTime.Today;
            txtObjective.Clear();
        }

        private void ActivityManagementForm_Load(object sender, EventArgs e)
        {

        }
        private void LoadAllowedStatuses(ActivityStatus currentStatus)
        {
            cmbStatus.Items.Clear();

            if (currentStatus == ActivityStatus.PLANNED)
            {
                cmbStatus.Items.Add(ActivityStatus.ONGOING);
                cmbStatus.Items.Add(ActivityStatus.POSTPONED);
            }
            else if (currentStatus == ActivityStatus.ONGOING)
            {
                cmbStatus.Items.Add(ActivityStatus.ACCOMPLISHED);
                cmbStatus.Items.Add(ActivityStatus.POSTPONED);
            }
            else if (currentStatus == ActivityStatus.POSTPONED)
            {
                cmbStatus.Items.Add(ActivityStatus.PLANNED);
            }

            cmbStatus.SelectedIndex = -1;
        }
        private void dgvActivities_CellClick(
    object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvActivities.Rows[e.RowIndex];

            string currentStatus =
                row.Cells["Status"].Value.ToString();

            ActivityStatus status =
                (ActivityStatus)Enum.Parse(
                    typeof(ActivityStatus), currentStatus);

            LoadAllowedStatuses(status);

            if (status == ActivityStatus.ACCOMPLISHED)
            {
                MessageBox.Show(
                    "This activity is already accomplished. Its status cannot be changed.");
            }
        }
        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (loggedInCoordinator == null)
            {
                MessageBox.Show("Coordinator information is missing.");
                return;
            }

            if (dgvActivities.CurrentRow == null)
            {
                MessageBox.Show("Please select an activity first.");
                return;
            }

            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a new status.");
                return;
            }

            try
            {
                int activityId = Convert.ToInt32(
                    dgvActivities.CurrentRow.Cells["ActivityId"].Value);

                ActivityStatus newStatus =
                    (ActivityStatus)cmbStatus.SelectedItem;

                bool updated = activityRepository.UpdateActivityStatus(
                    activityId,
                    newStatus,
                    dtpNewDate.Value,
                    loggedInCoordinator.CoordinatorId,
                    loggedInCoordinator.CellId
                );

                if (updated)
                {
                    MessageBox.Show("Activity status updated successfully!");

                    LoadActivities();
                    cmbStatus.Items.Clear();
                    cmbStatus.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show(
                        "Status could not be updated. Please check the selected activity.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not update activity status: " + ex.Message);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        // Get activities belonging to a cell using optional filters
        
        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            if (loggedInCoordinator == null)
            {
                MessageBox.Show("Coordinator information is missing.");
                return;
            }

            // Get the selected village, or null if none was selected
            int? villageId = null;

            if (cmbFilterVillage.SelectedIndex != -1)
            {
                villageId = Convert.ToInt32(
                    cmbFilterVillage.SelectedValue);
            }

            // Get the selected activity type, or null
            int? activityTypeId = null;

            if (cmbFilterActivityType.SelectedIndex != -1)
            {
                activityTypeId = Convert.ToInt32(
                    cmbFilterActivityType.SelectedValue);
            }

            // Get the selected date, or null if the checkbox is unchecked
            DateTime? activityDate = null;

            if (dtpFilterDate.Checked)
            {
                activityDate = dtpFilterDate.Value.Date;
            }

            // Get the selected status, or null
            ActivityStatus? status = null;

            if (cmbFilterStatus.SelectedIndex != -1)
            {
                status = (ActivityStatus)cmbFilterStatus.SelectedItem;
            }

            try
            {
                dgvActivities.DataSource = null;

                dgvActivities.DataSource =
                    activityRepository.GetFilteredActivitiesByCell(
                        loggedInCoordinator.CellId,
                        villageId,
                        activityTypeId,
                        activityDate,
                        status
                    );

                // Hide technical IDs
                if (dgvActivities.Columns["ActivityId"] != null)
                    dgvActivities.Columns["ActivityId"].Visible = false;

                if (dgvActivities.Columns["VillageId"] != null)
                    dgvActivities.Columns["VillageId"].Visible = false;

                if (dgvActivities.Columns["ActivityTypeId"] != null)
                    dgvActivities.Columns["ActivityTypeId"].Visible = false;

                if (dgvActivities.Columns["CoordinatorId"] != null)
                    dgvActivities.Columns["CoordinatorId"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not filter activities: " + ex.Message);
            }
        }
        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            cmbFilterVillage.SelectedIndex = -1;
            cmbFilterActivityType.SelectedIndex = -1;
            cmbFilterStatus.SelectedIndex = -1;

            dtpFilterDate.Checked = false;

            // Display all activities belonging to the coordinator's cell
            LoadActivities();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.Hide();

                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();

                this.Close();
            }

        }
        private void btnManageActivities_Click(object sender, EventArgs e)
        {
            using (ActivityManagementForm form =
                new ActivityManagementForm(loggedInCoordinator))
            {
                form.ShowDialog();
            }
        }
        private int? GetSelectedActivityId()
        {
            if (dgvActivities.CurrentRow == null)
                return null;

            return Convert.ToInt32(
                dgvActivities.CurrentRow.Cells["ActivityId"].Value);
        }
        private void btnUpdateActivity_Click(object sender, EventArgs e)
        {
            if (loggedInCoordinator == null)
            {
                MessageBox.Show("Coordinator information is missing.");
                return;
            }

            int? activityId = GetSelectedActivityId();

            if (activityId == null)
            {
                MessageBox.Show("Please select an activity first.");
                return;
            }

            if (cmbVillage.SelectedIndex == -1 ||
                cmbActivityType.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtObjective.Text))
            {
                MessageBox.Show(
                    "Please select a village, activity type, and enter an objective.");
                return;
            }

            if (dtpActivityDate.Value.Date <= DateTime.Today)
            {
                MessageBox.Show("Please select a future date.");
                return;
            }

            try
            {
                bool updated = activityRepository.UpdatePlannedActivity(
                    activityId.Value,
                    dtpActivityDate.Value,
                    txtObjective.Text.Trim(),
                    Convert.ToInt32(cmbVillage.SelectedValue),
                    Convert.ToInt32(cmbActivityType.SelectedValue),
                    loggedInCoordinator.CoordinatorId,
                    loggedInCoordinator.CellId
                );

                if (updated)
                {
                    MessageBox.Show("Activity updated successfully!");
                    LoadActivities();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show(
                        "Activity could not be updated. It may no longer be PLANNED.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not update activity: " + ex.Message);
            }
        }
        private void btnDeleteActivity_Click(object sender, EventArgs e)
        {
            if (loggedInCoordinator == null)
            {
                MessageBox.Show("Coordinator information is missing.");
                return;
            }

            int? activityId = GetSelectedActivityId();

            if (activityId == null)
            {
                MessageBox.Show("Please select an activity first.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this planned activity?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            try
            {
                bool deleted = activityRepository.DeletePlannedActivity(
                    activityId.Value,
                    loggedInCoordinator.CellId
                );

                if (deleted)
                {
                    MessageBox.Show("Activity deleted successfully!");
                    LoadActivities();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show(
                        "Activity could not be deleted. Only PLANNED activities can be deleted.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not delete activity: " + ex.Message);
            }
        }



    }
}