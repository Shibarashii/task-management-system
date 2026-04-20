using System;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using TASK_MANAGEMENT_SYSTEM.UserControls;
using TASK_MANAGEMENT_SYSTEM.PROJECT_SECTION;
using TASK_MANAGEMENT_SYSTEM.TASK_SECTION;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TASK_MANAGEMENT_SYSTEM.DASHBOARD_SECTION;

namespace TASK_MANAGEMENT_SYSTEM
{
    public partial class MainForm : KryptonForm
    {
        public static UserTab userTab;
        public static ProjectTab projectTab;
        public static TaskTab taskTab;
        public static ViewProject viewProject;
        public static ViewTask viewTask;
        public static DashboardTab dashboardTab;
        public static string id;
        public static bool isSuperuser;
        public MainForm(string id, bool isSuperuser)
        {
            InitializeComponent();
            MainForm.isSuperuser = isSuperuser;
            MainForm.id = id;
            if (!isSuperuser )
            {
                UsersButton.Hide();
            }

            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_USER = $"SELECT * FROM users WHERE id='{id}'";
                using (MySqlCommand command = new MySqlCommand(SELECT_USER, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        NameLabel.Text = reader["first_name"].ToString();
                    }
                }
            }

            ClientSize = new Size(800, 600);
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // Initializing the User Controls

            dashboardTab = new DashboardTab(this);
            dashboardTab.Location = new Point(363, 36);
            Controls.Add(dashboardTab);

            userTab = new UserTab(this);
            userTab.Location = new Point(363, 36);
            userTab.Hide();
            Controls.Add(userTab);

            projectTab = new ProjectTab(this);
            projectTab.Location = new Point(363, 36);
            projectTab.Hide();
            Controls.Add(projectTab);

            taskTab = new TaskTab(this);
            taskTab.Location = new Point(363, 36);
            taskTab.Hide();
            Controls.Add(taskTab);
        }

        private void CloseViewing()
        {
            viewProject?.Hide();
            viewTask?.Hide();
        }

        private void DashboardButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DashboardButton.Checked)
            {
                dashboardTab.Show();
                dashboardTab.RefreshFlowPanel();
                CloseViewing();
                return;
            }
            CloseViewing();
            dashboardTab.Hide();
        }

        private void ProjectsButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ProjectButton.Checked)
            {
                projectTab.RefreshFlowPanel();
                projectTab.Show();
                CloseViewing();
                return;
            }
            CloseViewing();
            projectTab.Hide();
        }

        private void TaskButton_CheckedChanged(object sender, EventArgs e)
        {
            if (TaskButton.Checked)
            {
                taskTab.RefreshFlowPanel();
                taskTab.Show();
                CloseViewing();
                return;
            }
            CloseViewing();
            taskTab.Hide();
        }

        private void UsersButton_CheckedChanged(object sender, EventArgs e)
        {
            if (UsersButton.Checked)
            {
                userTab.RefreshFlowPanel();
                userTab.Show();
                CloseViewing();
                return;
            }
            CloseViewing();
            userTab.Hide();
        }

        private void SignOutButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to sign out?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                LoginForm loginForm = new LoginForm();
                Hide();
                loginForm.Show();
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            AddUserForm addUserForm = new AddUserForm(id, true);
            addUserForm.ShowDialog();
        }
    }
}
