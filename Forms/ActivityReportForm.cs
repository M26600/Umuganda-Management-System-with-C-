
using System;
using System.Windows.Forms;
using UmugandaManagementSystem.Models;
using UmugandaManagementSystem.Repositories;

namespace UmugandaManagementSystem.Forms
{
    public partial class ActivityReportForm : Form
    {
        // Repository connection
        private ActivityReportRepository reportRepository =
            new ActivityReportRepository();

        // Currently logged-in coordinator
        private Coordinator loggedInCoordinator;

        public ActivityReportForm()
        {
            InitializeComponent();
            ConfigureForm();
        }

        // Constructor that receives the logged-in coordinator
        public ActivityReportForm(Coordinator coordinator)
            : this()
        {
            loggedInCoordinator = coordinator;
            LoadEligibleActivities();
        }

        private void LoadEligibleActivities()
        {
            if (loggedInCoordinator == null)
                return;

            try
            {
                cmbActivity.DataSource = null;

                cmbActivity.DataSource =
                    reportRepository.GetEligibleActivitiesByCell(
                        loggedInCoordinator.CellId);

                cmbActivity.DisplayMember = "Objective";
                cmbActivity.ValueMember = "ActivityId";

                cmbActivity.SelectedIndex = -1;

                if (cmbActivity.Items.Count == 0)
                {
                    MessageBox.Show(
                        "There are no accomplished activities available for reporting.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load activities: " + ex.Message);
            }
        }

        private void numAttendeeCount_ValueChanged(
            object sender, EventArgs e)
        {
        }

        private void ActivityReportForm_Load(
            object sender, EventArgs e)
        {
        }

        private void ConfigureForm()
        {
            txtWorkDescription.Multiline = true;

            numAttendeeCount.Minimum = 1;
            numAttendeeCount.Maximum = 10000;
            numAttendeeCount.Value = 1;

            cmbActivity.DropDownStyle =
                ComboBoxStyle.DropDownList;
            btnSubmit.Click += btnSubmit_Click;
            btnClear.Click += btnClear_Click;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Check whether a coordinator is logged in
            if (loggedInCoordinator == null)
            {
                MessageBox.Show("Coordinator information is missing.");
                return;
            }

            // Check whether an activity was selected
            if (cmbActivity.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an activity.");
                return;
            }

            // Check the work description
            if (string.IsNullOrWhiteSpace(txtWorkDescription.Text))
            {
                MessageBox.Show("Please enter the work description.");
                return;
            }

            try
            {
                int activityId =
                    Convert.ToInt32(cmbActivity.SelectedValue);

                int attendeeCount =
                    Convert.ToInt32(numAttendeeCount.Value);

                bool saved = reportRepository.AddReport(
                    activityId,
                    loggedInCoordinator.CoordinatorId,
                    loggedInCoordinator.CellId,
                    attendeeCount,
                    txtWorkDescription.Text.Trim()
                );

                if (saved)
                {
                    MessageBox.Show("Activity report submitted successfully!");

                    ClearFields();
                    LoadEligibleActivities();
                }
                else
                {
                    MessageBox.Show(
                        "Report could not be submitted. The activity may already have a report or may no longer be eligible.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not submit report: " + ex.Message);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            cmbActivity.SelectedIndex = -1;
            numAttendeeCount.Value = 1;
            txtWorkDescription.Clear();
        }

    }
}
