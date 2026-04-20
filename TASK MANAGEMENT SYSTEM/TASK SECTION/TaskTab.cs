using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TASK_MANAGEMENT_SYSTEM.UserControls;
using Microsoft.VisualBasic.ApplicationServices;

namespace TASK_MANAGEMENT_SYSTEM.TASK_SECTION
{
    public partial class TaskTab : UserControl
    {
        private readonly MainForm mainForm;
        public TaskTab(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            RefreshFlowPanel();
        }

        private int count;

        public void RefreshFlowPanel(string columnName = "id", bool isDescending = false)
        {
            PopulateProjectComboBox();
            FlowPanel.Controls.Clear();
            count = 0;

            switch (FilterComboBox.SelectedIndex)
            {
                case 0:
                    GetAllTasks(columnName, isDescending); break;
                case 1:
                    GetPendingTasks(columnName, isDescending); break;
                case 2:
                    GetFinishedTasks(columnName, isDescending); break;
                case 3:
                    GetMissedTasks(columnName, isDescending); break;
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

        private void SetTaskStatus(string id)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_TASK = $"SELECT * FROM tasks WHERE id = {id} AND is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(SELECT_TASK, connection))
                {
                    string status;
                    DateTime dueDate;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        status = reader["status"].ToString();
                        dueDate = (DateTime)reader["due_date"];
                    }
                    
                    if (DateTime.Now >= dueDate && status != "Finished")
                    {
                        command.CommandText = $"UPDATE tasks SET status = 'Missed' WHERE id = {id} AND is_archived = FALSE";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void GetAllTasks(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_ID = "SELECT id FROM tasks WHERE is_archived = FALSE";

                string SELECT_USER;
                if (MainForm.isSuperuser)
                {
                    SELECT_USER = $@"
                SELECT tasks.* 
                FROM tasks
                WHERE tasks.is_archived = FALSE 
                AND (LOWER(tasks.name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(tasks.project_name) LIKE LOWER('%{SearchTextbox.Text}%')) 
                ORDER BY {columnName} {sortOrder}";
                }
                else
                {
                    SELECT_USER = $@"
                SELECT tasks.* 
                FROM tasks 
                JOIN task_users ON tasks.id = task_users.task_id
                WHERE tasks.is_archived = FALSE 
                AND task_users.user_id = {MainForm.id}
                AND (LOWER(tasks.name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(tasks.project_name) LIKE LOWER('%{SearchTextbox.Text}%')) 
                ORDER BY {columnName} {sortOrder}";
                }

                using (MySqlCommand command = new MySqlCommand(SELECT_ID, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            SetTaskStatus(id);
                        }
                        reader.Close();
                    }

                    command.CommandText = SELECT_USER;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string projectId = reader["project_id"].ToString();
                            string projectName = reader["project_name"].ToString();
                            string name = reader["name"].ToString();
                            DateTime startDate = (DateTime)reader["start_date"];
                            DateTime dueDate = (DateTime)reader["due_date"];
                            string status = reader["status"].ToString();

                            CreateControls(id, projectId, projectName, name, startDate, dueDate, status);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void GetPendingTasks(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_USER;
                if (MainForm.isSuperuser)
                {
                    SELECT_USER = $@"
                SELECT * 
                FROM tasks
                WHERE is_archived=FALSE 
                AND status='Pending' 
                AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(project_name) LIKE LOWER('%{SearchTextbox.Text}%')) 
                ORDER BY {columnName} {sortOrder}";
                }
                else
                {
                    SELECT_USER = $@"
                SELECT tasks.* 
                FROM tasks 
                JOIN task_users ON tasks.id = task_users.task_id
                WHERE tasks.is_archived = FALSE 
                AND task_users.user_id = {MainForm.id}
                AND tasks.status='Pending' 
                AND (LOWER(tasks.name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(tasks.project_name) LIKE LOWER('%{SearchTextbox.Text}%')) 
                ORDER BY {columnName} {sortOrder}";
                }

                using (MySqlCommand command = new MySqlCommand(SELECT_USER, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string projectId = reader["project_id"].ToString();
                            string projectName = reader["project_name"].ToString();
                            string name = reader["name"].ToString();
                            DateTime startDate = (DateTime)reader["start_date"];
                            DateTime dueDate = (DateTime)reader["due_date"];
                            string status = reader["status"].ToString();

                            CreateControls(id, projectId, projectName, name, startDate, dueDate, status);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void GetFinishedTasks(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_USER;
                if (MainForm.isSuperuser)
                {
                    SELECT_USER = $@"
                SELECT * 
                FROM tasks
                WHERE is_archived=FALSE 
                AND status='Finished' 
                AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(project_name) LIKE LOWER('%{SearchTextbox.Text}%')) 
                ORDER BY {columnName} {sortOrder}";
                }
                else
                {
                    SELECT_USER = $@"
                SELECT tasks.* 
                FROM tasks 
                JOIN task_users ON tasks.id = task_users.task_id
                WHERE tasks.is_archived = FALSE 
                AND task_users.user_id = {MainForm.id}
                AND tasks.status='Finished' 
                AND (LOWER(tasks.name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(tasks.project_name) LIKE LOWER('%{SearchTextbox.Text}%')) 
                ORDER BY {columnName} {sortOrder}";
                }

                using (MySqlCommand command = new MySqlCommand(SELECT_USER, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string projectId = reader["project_id"].ToString();
                            string projectName = reader["project_name"].ToString();
                            string name = reader["name"].ToString();
                            DateTime startDate = (DateTime)reader["start_date"];
                            DateTime dueDate = (DateTime)reader["due_date"];
                            string status = reader["status"].ToString();

                            CreateControls(id, projectId, projectName, name, startDate, dueDate, status);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void GetMissedTasks(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_USER;
                if (MainForm.isSuperuser)
                {
                    SELECT_USER = $@"
                SELECT * 
                FROM tasks
                WHERE is_archived=FALSE 
                AND status='Missed' 
                AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(project_name) LIKE LOWER('%{SearchTextbox.Text}%')) 
                ORDER BY {columnName} {sortOrder}";
                }
                else
                {
                    SELECT_USER = $@"
                SELECT tasks.* 
                FROM tasks 
                JOIN task_users ON tasks.id = task_users.task_id
                WHERE tasks.is_archived = FALSE 
                AND task_users.user_id = {MainForm.id}
                AND tasks.status='Missed' 
                AND (LOWER(tasks.name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(tasks.project_name) LIKE LOWER('%{SearchTextbox.Text}%')) 
                ORDER BY {columnName} {sortOrder}";
                }

                using (MySqlCommand command = new MySqlCommand(SELECT_USER, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string projectId = reader["project_id"].ToString();
                            string projectName = reader["project_name"].ToString();
                            string name = reader["name"].ToString();
                            DateTime startDate = (DateTime)reader["start_date"];
                            DateTime dueDate = (DateTime)reader["due_date"];
                            string status = reader["status"].ToString();

                            CreateControls(id, projectId, projectName, name, startDate, dueDate, status);
                        }
                    }
                }
                connection.Close();
            }
        }

        //private void GetAllTasks(string columnName = "id", bool isDescending = false)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
        //    {
        //        connection.Open();
        //        string sortOrder = isDescending ? "DESC" : "ASC";
        //        string SELECT_ID = "SELECT id FROM tasks WHERE is_archived = FALSE";
        //        string SELECT_USER = $"SELECT * FROM tasks WHERE is_archived=FALSE AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(project_name) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
        //        using (MySqlCommand command = new MySqlCommand(SELECT_ID, connection))
        //        {
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    string id = reader["id"].ToString();
        //                    SetTaskStatus(id);
        //                }
        //                reader.Close();
        //            }

        //            command.CommandText = SELECT_USER;
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    string id = reader["id"].ToString();
        //                    string projectId = reader["project_id"].ToString();
        //                    string projectName = reader["project_name"].ToString();
        //                    string name = reader["name"].ToString();
        //                    DateTime startDate = (DateTime)reader["start_date"];
        //                    DateTime dueDate = (DateTime)reader["due_date"];
        //                    string status = reader["status"].ToString();

        //                    CreateControls(id, projectId, projectName, name, startDate, dueDate, status);
        //                }
        //            }
        //        }
        //        connection.Close();
        //    }
        //}

        //private void GetPendingTasks(string columnName = "id", bool isDescending = false)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
        //    {
        //        connection.Open();
        //        string sortOrder = isDescending ? "DESC" : "ASC";
        //        string SELECT_USER = $"SELECT * FROM tasks WHERE is_archived=FALSE AND status='Pending' AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(project_name) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
        //        using (MySqlCommand command = new MySqlCommand(SELECT_USER, connection))
        //        {
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    string id = reader["id"].ToString();
        //                    string projectId = reader["project_id"].ToString();
        //                    string projectName = reader["project_name"].ToString();
        //                    string name = reader["name"].ToString();
        //                    DateTime startDate = (DateTime)reader["start_date"];
        //                    DateTime dueDate = (DateTime)reader["due_date"];
        //                    string status = reader["status"].ToString();

        //                    CreateControls(id, projectId, projectName, name, startDate, dueDate, status);
        //                }
        //            }
        //        }
        //        connection.Close();
        //    }
        //}

        //private void GetFinishedTasks(string columnName = "id", bool isDescending = false)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
        //    {
        //        connection.Open();
        //        string sortOrder = isDescending ? "DESC" : "ASC";
        //        string SELECT_USER = $"SELECT * FROM tasks WHERE is_archived=FALSE AND status='Finished' AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(project_name) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
        //        using (MySqlCommand command = new MySqlCommand(SELECT_USER, connection))
        //        {
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    string id = reader["id"].ToString();
        //                    string projectId = reader["project_id"].ToString();
        //                    string projectName = reader["project_name"].ToString();
        //                    string name = reader["name"].ToString();
        //                    DateTime startDate = (DateTime)reader["start_date"];
        //                    DateTime dueDate = (DateTime)reader["due_date"];
        //                    string status = reader["status"].ToString();

        //                    CreateControls(id, projectId, projectName, name, startDate, dueDate, status);
        //                }
        //            }
        //        }
        //        connection.Close();
        //    }
        //}

        //private void GetMissedTasks(string columnName = "id", bool isDescending = false)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
        //    {
        //        connection.Open();
        //        string sortOrder = isDescending ? "DESC" : "ASC";
        //        string SELECT_USER = $"SELECT * FROM tasks WHERE is_archived=FALSE AND status='Missed' AND (LOWER(name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(project_name) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
        //        using (MySqlCommand command = new MySqlCommand(SELECT_USER, connection))
        //        {
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    string id = reader["id"].ToString();
        //                    string projectId = reader["project_id"].ToString();
        //                    string projectName = reader["project_name"].ToString();
        //                    string name = reader["name"].ToString();
        //                    DateTime startDate = (DateTime)reader["start_date"];
        //                    DateTime dueDate = (DateTime)reader["due_date"];
        //                    string status = reader["status"].ToString();

        //                    CreateControls(id, projectId, projectName, name, startDate, dueDate, status);
        //                }
        //            }
        //        }
        //        connection.Close();
        //    }
        //}

        private void CreateControls(string id, string projectId, string projectName, string name, DateTime startDate, DateTime dueDate, string status)
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
            Label taskNameLabel = Main.CreateLabel(name, new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(NameHeader.Location.X, 15));
            Label projectNameLabel = Main.CreateLabel(projectName, new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(ProjectNameHeader.Location.X, 15), true, true);
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

            ActionButtons buttons = new ActionButtons(mainForm, id, null, null, this, null, projectId)
            {
                Location = new Point(ActionsFinishedHeader.Location.X, 5),
            };

            switch (statusLabel.Text)
            {
                case "Pending":
                    statusLabel.FillColor = Color.Gray;
                    statusLabel.HoverState.FillColor = Color.Gray;
                    statusLabel.PressedColor = Color.Gray;
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
            separatorPanel.Controls.Add(taskNameLabel);
            separatorPanel.Controls.Add(projectNameLabel);
            separatorPanel.Controls.Add(startDateLabel);
            separatorPanel.Controls.Add(dueDateLabel);
            separatorPanel.Controls.Add(statusLabel);
            separatorPanel.Controls.Add(buttons);
            FlowPanel.Controls.Add(separatorPanel);
        }

        private void SearchTextbox_TextChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel();
        }

        private void NameSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("name", NameSort.Checked);
        }

        private void ProjectSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("project_name", ProjectSort.Checked);
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
        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            ProjectComboBox.SelectedIndex = 0;
            SearchTextbox.Text = string.Empty;
            FilterComboBox.SelectedIndex = 0;
            if (NameSort.Checked)
            {
                NameSort.Checked = false;
            }
            if (ProjectSort.Checked)
            {
                ProjectSort.Checked = false;
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
            AddTaskForm addTaskForm = new AddTaskForm();
            addTaskForm.ShowDialog();
        }

        private bool isPopulating = false;
        private void PopulateProjectComboBox()
        {
            isPopulating = true;
            string selectedProject = ProjectComboBox.SelectedItem?.ToString();
            string defaultProject = "--ALL PROJECTS--";
            ProjectComboBox.Items.Clear();
            ProjectComboBox.Items.Add(defaultProject);

            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_PROJECT = "SELECT * FROM projects WHERE is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(SELECT_PROJECT, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProjectComboBox.Items.Add(reader["name"].ToString());
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(selectedProject) && ProjectComboBox.Items.Contains(selectedProject))
            {
                ProjectComboBox.SelectedItem = selectedProject;
            }
            else
            {
                ProjectComboBox.SelectedIndex = 0;
            }
            isPopulating = false;
        }


        private void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isPopulating) return;

            if (ProjectComboBox.SelectedIndex != 0)
            {
                SearchTextbox.Enabled = false; 
                SearchTextbox.Text = ProjectComboBox.SelectedItem.ToString();
            }
            else
            {
                SearchTextbox.Enabled = true; 
                SearchTextbox.Text = string.Empty;
            }
        }

    }
}
