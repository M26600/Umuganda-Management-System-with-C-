
using System;
using System.Windows.Forms;
using UmugandaManagementSystem.Models;
using UmugandaManagementSystem.Repositories;

namespace UmugandaManagementSystem.Forms
{
    public partial class DashboardForm : Form
    {
        private DashboardRepository dashboardRepository = new DashboardRepository();
        private Coordinator loggedInCoordinator;

        public DashboardForm(Coordinator coordinator)
        {
            InitializeComponent();

            loggedInCoordinator = coordinator;

            if (loggedInCoordinator == null)
            {
                MessageBox.Show("Coordinator information is missing.");
                Close();
                return;
            }
            LoadDashboardSummary();

            this.Activated += DashboardForm_Activated;

            lblWelcome.Text =
                "Welcome, " + loggedInCoordinator.FirstName +
                " " + loggedInCoordinator.LastName +
                " | Cell ID: " + loggedInCoordinator.CellId;

            // Connect dashboard buttons
            btnManageVillages.Click += btnManageVillages_Click;
            btnMyProfile.Click += btnMyProfile_Click;


            // Improve button labels
            btnManageVillages.Text = "Manage Villages";
            button1.Text = "Manage Activities";
        }

        // Open Village Management
        private void btnManageVillages_Click(object sender, EventArgs e)
        {
            using (VillageManagementForm villageForm =
                new VillageManagementForm(loggedInCoordinator))
            {
                villageForm.ShowDialog(this);
            }
        }

        // Open Activity Management
        private void button1_Click(object sender, EventArgs e)
        {
            using (ActivityManagementForm activityForm =
                new ActivityManagementForm(loggedInCoordinator))
            {
                activityForm.ShowDialog(this);
            }
        }
        // Retained in case the Designer still references this event
        private void DashboardForm_Load(object sender, EventArgs e)
        {
        }

        // Retained in case the Designer still references this event
        private void btnManageVillages_Click_1(object sender, EventArgs e)
        {
        }
        

        private void btnManageReports_Click_1(object sender, EventArgs e)
        {
            using (ActivityReportForm reportForm =
                new ActivityReportForm(loggedInCoordinator))
            {
                reportForm.ShowDialog(this);
            }

        }
        private void LoadDashboardSummary()
        {
            if (loggedInCoordinator == null)
                return;

            try
            {
                DashboardSummary summary =
                    dashboardRepository.GetDashboardSummary(
                        loggedInCoordinator.CellId);

                lblTotalVillages.Text =
                    summary.TotalVillages.ToString();

                lblTotalActivities.Text =
                    summary.TotalActivities.ToString();

                lblPlanned.Text =
                    summary.PlannedActivities.ToString();

                lblOngoing.Text =
                    summary.OngoingActivities.ToString();

                lblAccomplished.Text =
                    summary.AccomplishedActivities.ToString();

                lblPostponed.Text =
                    summary.PostponedActivities.ToString();

                lblUpcoming.Text =
                    summary.UpcomingPlannedActivities.ToString();

                lblReports.Text =
                    summary.SubmittedReports.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load dashboard statistics: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void DashboardForm_Activated(object sender, EventArgs e)
        {
            LoadDashboardSummary();
        }

        private void lblReports_Click(object sender, EventArgs e)
        {

        }
        // Open Coordinator Profile
        private void btnMyProfile_Click(object sender, EventArgs e)
        {
            using (ProfileForm profileForm =
                new ProfileForm(loggedInCoordinator))
            {
                profileForm.ShowDialog(this);
            }

            // Refresh the welcome message in case the name changed
            lblWelcome.Text =
                "Welcome, " + loggedInCoordinator.FirstName +
                " " + loggedInCoordinator.LastName +
                " | Cell ID: " + loggedInCoordinator.CellId;
        }
    }
}
