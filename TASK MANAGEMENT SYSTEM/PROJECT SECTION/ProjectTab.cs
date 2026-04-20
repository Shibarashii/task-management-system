using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using TASK_MANAGEMENT_SYSTEM.UserControls;
using System.Runtime.InteropServices;
using TASK_MANAGEMENT_SYSTEM.DASHBOARD_SECTION;

namespace TASK_MANAGEMENT_SYSTEM.PROJECT_SECTION
{
    public partial class ProjectTab : UserControl
    {
        private readonly MainForm mainForm;
        public ProjectTab(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            
            RefreshFlowPanel();
        }

        private int count;

        public void RefreshFlowPanel(string columnName = "id", bool isDescending = false)
        {
            FlowPanel.Controls.Clear();
            count = 0;

            switch (FilterComboBox.SelectedIndex)
            {
                case 0:
                    GetAllProjects(columnName, isDescending); break;
                case 1:
                    GetStartedProjects(columnName, isDescending); break;
                case 2:
                    GetOngoingProjects(columnName, isDescending); break;
                case 3:
                    GetFinishedProjects(columnName, isDescending); break;
                case 4:
                    GetMissedProjects(columnName, isDescending); break;
            }

            if (FlowPanel.Controls.Count == 0)
            {
                NothingToSeeHerePanel.Show();
            }
            else
            {
                NothingToSeeHerePanel.Hide();
            }
        }

        private void GetAllProjects(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_PROJECT_ID = "SELECT id FROM projects WHERE is_archived = FALSE";

                string SELECT_PROJECT = $"SELECT * FROM projects WHERE is_archived=FALSE AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
                using (MySqlCommand command = new MySqlCommand(SELECT_PROJECT_ID, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            DashboardTab.SetProjectStatus(id);
                        }
                        reader.Close();
                    }

                    command.CommandText = SELECT_PROJECT;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string name = reader["name"].ToString();
                            DateTime startDate = (DateTime)reader["start_date"];
                            DateTime dueDate = (DateTime)reader["due_date"];
                            string status = reader["status"].ToString();

                            CreateControls(id, name, startDate, dueDate, status);
                            Main.UpdateTaskCount(id);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void GetStartedProjects(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_PROJECT = $"SELECT * FROM projects WHERE is_archived=FALSE AND status='Started' AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
                using (MySqlCommand command = new MySqlCommand(SELECT_PROJECT, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string name = reader["name"].ToString();
                            DateTime startDate = (DateTime)reader["start_date"];
                            DateTime dueDate = (DateTime)reader["due_date"];
                            string status = reader["status"].ToString();

                            CreateControls(id, name, startDate, dueDate, status);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void GetOngoingProjects(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_PROJECT = $"SELECT * FROM projects WHERE is_archived=FALSE AND status='Ongoing' AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
                using (MySqlCommand command = new MySqlCommand(SELECT_PROJECT, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string name = reader["name"].ToString();
                            DateTime startDate = (DateTime)reader["start_date"];
                            DateTime dueDate = (DateTime)reader["due_date"];
                            string status = reader["status"].ToString();

                            CreateControls(id, name, startDate, dueDate, status);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void GetFinishedProjects(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_PROJECT = $"SELECT * FROM projects WHERE is_archived=FALSE AND status='Finished' AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
                using (MySqlCommand command = new MySqlCommand(SELECT_PROJECT, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string name = reader["name"].ToString();
                            DateTime startDate = (DateTime)reader["start_date"];
                            DateTime dueDate = (DateTime)reader["due_date"];
                            string status = reader["status"].ToString();

                            CreateControls(id, name, startDate, dueDate, status);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void GetMissedProjects(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_PROJECT = $"SELECT * FROM projects WHERE is_archived=FALSE AND status='Missed' AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
                using (MySqlCommand command = new MySqlCommand(SELECT_PROJECT, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string name = reader["name"].ToString();
                            DateTime startDate = (DateTime)reader["start_date"];
                            DateTime dueDate = (DateTime)reader["due_date"];
                            string status = reader["status"].ToString();

                            CreateControls(id, name, startDate, dueDate, status);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void CreateControls(string id, string name, DateTime startDate, DateTime dueDate, string status)
        {
            count++;
            Guna2CustomGradientPanel separatorPanel = new Guna2CustomGradientPanel
            {
                BorderRadius = 5,
                BorderColor = Color.Gray,
                BorderThickness = 1,
                Size = new Size(FlowPanel.Width - 25, 50),
                FillColor = Color.White,
                FillColor2 = Color.LightGray,
                Quality = 10
            };

            Label countLabel = Main.CreateLabel(count.ToString(), new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(CountHeader.Location.X, 15));
            Label projectNameLabel = Main.CreateLabel(name, new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(NameHeader.Location.X, 15), true, true);
            Label startDateLabel = Main.CreateLabel(startDate.ToString("MM-dd-yyyy"), new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(StartDateHeader.Location.X, 15), true, true);
            Label dueDateLabel = Main.CreateLabel(dueDate.ToString("MM-dd-yyyy"), new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(DueDateHeader.Location.X, 15), true, true);
            Guna2Button statusLabel = new Guna2Button
            {
                Text = status,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Arial Rounded MT", 10, FontStyle.Bold),
                Size = new Size(100, 30),
                Location = new Point(StatusHeader.Location.X, 10),
                BorderRadius = 5
            };

            ActionButtons buttons = new ActionButtons(mainForm, id, null, this)
            {
                Location = new Point(ActionsFinishedHeader.Location.X, 5),
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
            separatorPanel.Controls.Add(startDateLabel);
            separatorPanel.Controls.Add(dueDateLabel);
            separatorPanel.Controls.Add(statusLabel);
            separatorPanel.Controls.Add(buttons);
            FlowPanel.Controls.Add(separatorPanel);
        }

        private void NameSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("name", NameSort.Checked);
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

        private void SearchTextbox_TextChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel();
        }

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            SearchTextbox.Text = string.Empty;
            FilterComboBox.SelectedIndex = 0;
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
            if (!StatusSort.Checked)
            {
                StatusSort.Checked = true;
            }

            RefreshFlowPanel();
        }

        private void AddNewButton_Click(object sender, EventArgs e)
        {
            AddProjectForm addProjectForm = new AddProjectForm();
            addProjectForm.ShowDialog();
        }
    }
}
