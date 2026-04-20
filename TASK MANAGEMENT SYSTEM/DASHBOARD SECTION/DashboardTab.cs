using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TASK_MANAGEMENT_SYSTEM.UserControls;

namespace TASK_MANAGEMENT_SYSTEM.DASHBOARD_SECTION
{
    public partial class DashboardTab : UserControl
    {
        private readonly MainForm mainForm;
        public DashboardTab(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            RefreshFlowPanel();
        }

        public void RefreshFlowPanel(string columnName = "id", bool isDescending = false)
        {
            InitializeCounts();
            FlowPanel.Controls.Clear();
            count = 0;

            GetAllProjects(columnName, isDescending);

            if (FlowPanel.Controls.Count == 0)
            {
                NothingToSeeHerePanel.Show();
            }
            else
            {
                NothingToSeeHerePanel.Hide();
            }
        }

        private void InitializeCounts()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_PROJECT_COUNT = $"SELECT COUNT(*) FROM projects WHERE is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(SELECT_PROJECT_COUNT, connection))
                {
                    string totalProjects = command.ExecuteScalar().ToString();

                    command.CommandText = "SELECT COUNT(*) FROM projects WHERE status = 'Finished' AND is_archived = FALSE";
                    string finishedProjects = command.ExecuteScalar().ToString();

                    command.CommandText = "SELECT COUNT(*) FROM tasks WHERE is_archived = FALSE";
                    string totalTasks = command.ExecuteScalar().ToString();

                    command.CommandText = "SELECT COUNT(*) FROM tasks WHERE status = 'Finished' AND is_archived = FALSE";
                    string finishedTasks = command.ExecuteScalar().ToString();

                    ProjectCountLabel.Text = $"{finishedProjects}/{totalProjects}";
                    TaskCountLabel.Text = $"{finishedTasks}/{totalTasks}";
                }
            }
        }
        private void GetAllProjects(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_PROJECT_ID = "SELECT id FROM projects WHERE is_archived = FALSE";
                string GET_ALL_PROJECTS = $"SELECT * FROM projects WHERE is_archived = FALSE ORDER BY {columnName} {sortOrder}";
                using (MySqlCommand command = new MySqlCommand(SELECT_PROJECT_ID, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            SetProjectStatus(id);
                        }
                        reader.Close();
                    }

                    command.CommandText = GET_ALL_PROJECTS;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string name = reader["name"].ToString();
                            int progress = Convert.ToInt32(reader["progress"]);
                            double percent = (double)reader["progress"];
                            int taskCount = (int)reader["total_tasks"];
                            DateTime startDate = (DateTime)reader["start_date"];
                            DateTime dueDate = (DateTime)reader["due_date"];
                            string status = SetProjectStatus(id);

                            CreateControls(id, name, progress, percent, taskCount, startDate, dueDate, status);
                        }
                    }
                }
                connection.Close();
            }
        }


        public static string SetProjectStatus(string id)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_TASK_COUNT = $"SELECT COUNT(*) FROM tasks WHERE project_id = {id} AND is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(SELECT_TASK_COUNT, connection))
                {
                    int taskCount = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = $"SELECT COUNT(*) FROM tasks WHERE project_id = {id} AND status = 'Finished' AND is_archived = FALSE";
                    int finishedTasks = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = $"SELECT due_date FROM projects WHERE id = {id} AND is_archived = FALSE";
                    DateTime dueDate = (DateTime)command.ExecuteScalar();

                    double progressPercent = 0.00; // Default value for the case when taskCount is zero

                    if (taskCount > 0)
                    {
                        progressPercent = (Convert.ToDouble(finishedTasks) / Convert.ToDouble(taskCount)) * 100;
                    }

                    if (taskCount == finishedTasks && taskCount > 0 && finishedTasks > 0)
                    {
                        command.CommandText = $"UPDATE projects SET status = 'Finished', progress = {progressPercent}, finished_tasks = {finishedTasks} WHERE id = {id}";
                        command.ExecuteNonQuery();
                        return "Finished";
                    }
                    else if (DateTime.Now >= dueDate)
                    {
                        command.CommandText = $"UPDATE projects SET status = 'Missed', progress = {progressPercent}, finished_tasks = {finishedTasks} WHERE id = {id}";
                        command.ExecuteNonQuery();
                        return "Missed";
                    }
                    else
                    {
                        if (finishedTasks == 0)
                        {
                            command.CommandText = $"UPDATE projects SET status = 'Started', progress = {progressPercent}, finished_tasks = {finishedTasks} WHERE id = {id}";
                            command.ExecuteNonQuery();
                            return "Started";
                        }
                        else if (finishedTasks > 0 && taskCount > finishedTasks)
                        {
                            command.CommandText = $"UPDATE projects SET status = 'Ongoing', progress = {progressPercent}, finished_tasks = {finishedTasks} WHERE id = {id}";
                            command.ExecuteNonQuery();
                            return "Ongoing";
                        }

                        return null;
                    }
                }
            }
        }

        private int count;
        private void CreateControls(string id, string name, int progress, double percent, int taskCount, DateTime startDate, DateTime dueDate, string status)
        {
            count++;
            var separatorPanel = new Guna2CustomGradientPanel
            {
                BorderRadius = 5,
                BorderColor = Color.Gray,
                BorderThickness = 1,
                Size = new Size(FlowPanel.Width - 25, 50),
                FillColor = Color.White,
                FillColor2 = Color.LightGray,
                Quality = 10
            };

            var countLabel = Main.CreateLabel(count.ToString(), new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(CountHeader.Location.X, 15));
            var projectNameLabel = Main.CreateLabel(name, new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(NameHeader.Location.X, 15), true, true);
            var progressBar = new Guna2ProgressBar
            {
                Maximum = 100,
                Value = progress,
                Location = new Point(ProgressHeader.Location.X, 10),
                Size = new Size(250, 15)
            };
            var percentLabel = Main.CreateLabel($"{percent}% Completed", new Font("Arial MT Rounded", 10), Color.Black, Color.Transparent, new Point(ProgressHeader.Location.X, progressBar.Bottom));
            var taskCountLabel = Main.CreateLabel($"Total tasks: {taskCount}", new Font("Arial MT Rounded", 10), Color.Black, Color.Transparent, new Point(ProgressHeader.Location.X + 150, progressBar.Bottom));
            var startDateLabel = Main.CreateLabel(startDate.ToString("MM-dd-yyyy"), new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(StartDateHeader.Location.X, 15), true, true);
            var dueDateLabel = Main.CreateLabel(dueDate.ToString("MM-dd-yyyy"), new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(DueDateHeader.Location.X, 15), true, true);
            var statusLabel = new Guna2Button
            {
                Text = status,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Arial Rounded MT", 10, FontStyle.Bold),
                Size = new Size(100, 30),
                Location = new Point(StatusHeader.Location.X, 10),
                BorderRadius = 5
            };

            switch (statusLabel.Text)
            {
                case "Started":
                    statusLabel.FillColor = Color.Blue;
                    statusLabel.HoverState.FillColor = Color.Blue;
                    statusLabel.PressedColor = Color.Blue;
                    break;
                case "Ongoing":
                    statusLabel.FillColor = Color.Orange;
                    statusLabel.HoverState.FillColor = Color.Orange;
                    statusLabel.PressedColor = Color.Orange;
                    break;
                case "Finished":
                    statusLabel.FillColor = Color.Green;
                    statusLabel.HoverState.FillColor = Color.Green;
                    statusLabel.PressedColor = Color.Green;
                    break;
                case "Missed":
                    statusLabel.FillColor = Color.Red;
                    statusLabel.HoverState.FillColor = Color.Red;
                    statusLabel.PressedColor = Color.Red;
                    break;
            }

            separatorPanel.Controls.Add(countLabel);
            separatorPanel.Controls.Add(projectNameLabel);
            separatorPanel.Controls.Add(progressBar);
            separatorPanel.Controls.Add(percentLabel);
            separatorPanel.Controls.Add(taskCountLabel);
            separatorPanel.Controls.Add(startDateLabel);
            separatorPanel.Controls.Add(dueDateLabel);
            separatorPanel.Controls.Add(statusLabel);
            FlowPanel.Controls.Add(separatorPanel);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            if (NameSort.Checked)
            {
                NameSort.Checked = false;
            }
            if (StartDateSort.Checked)
            {
                StartDateSort.Checked = false;
            }
            if (DueDateSort.Checked)
            {
                DueDateSort.Checked = false;
            }
            if (ProgressSort.Checked)
            {
                ProgressSort.Checked = false;
            }
            if (StatusSort.Checked)
            {
                StatusSort.Checked = true;
            }
            RefreshFlowPanel();
        }

        private void NameSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("name", NameSort.Checked);
        }

        private void ProgressSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("progress", ProgressSort.Checked);
        }

        private void StartDateSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("start_date", StartDateSort.Checked);
        }

        private void DueDateSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("due_date", DueDateSort.Checked);
        }

        private void StatusSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("status", StatusSort.Checked);
        }
    }
}
