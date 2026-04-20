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
using TASK_MANAGEMENT_SYSTEM.PROJECT_SECTION;
using MySql.Data.MySqlClient;
using TASK_MANAGEMENT_SYSTEM.UserControls;

namespace TASK_MANAGEMENT_SYSTEM.TASK_SECTION
{
    public partial class ViewTask : UserControl
    {
        private readonly string id;
        private readonly string projectId;
        private readonly MainForm mainForm;
        private readonly bool fromTask;
        public ViewTask(MainForm mainForm, string id, string projectId, bool fromTask)
        {
            this.id = id;
            this.mainForm = mainForm;
            this.projectId = projectId;
            this.fromTask = fromTask;

            InitializeComponent();
            RefreshViewTask();
        }

        public void RefreshViewTask()
        {
            UserFlowList.Controls.Clear();
            count = 0;
            bool isCurrentUserAssociated = CheckCurrentUserAssociation();  // Check if the current user is associated with the task
            FillUserList();
            SetTaskLabels();
            Main.UpdateTaskCount(id);
            SetButtonState();

            // Hide the CompletionInfoButton if the current user is not associated with the task
            if (!MainForm.isSuperuser)
            {
                CompletionInfoButton.Visible = isCurrentUserAssociated;
                MarkAsFinishedButton.Hide();
                EditButton.Hide();
                DeleteButton.Hide();
                CompletionInfoButton.Location = new Point(550, 648);
                CompletionInfoButton.Text = "SUBMIT COMPLETION";

                if (CompletionForm.TaskIsSubmitted(id))
                {
                    CompletionInfoButton.Text = "CHECK SUBMISSION";
                }
            }
        }

        private bool CheckCurrentUserAssociation()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_USER_ASSOCIATION = $"SELECT COUNT(*) FROM task_users WHERE task_id = '{id}' AND user_id = '{MainForm.id}'";
                using (MySqlCommand command = new MySqlCommand(SELECT_USER_ASSOCIATION, connection))
                {
                    int associationCount = Convert.ToInt32(command.ExecuteScalar());
                    return associationCount > 0;
                }
            }
        }


        private void SetTaskLabels()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string GET_TASK = $"SELECT * FROM tasks WHERE id='{id}' AND is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(GET_TASK, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        TaskNameLabel.Text = reader["name"].ToString();
                        TaskDescriptionLabel.Text = reader["description"].ToString();
                        StatusLabel.Text = reader["status"].ToString();
                        DateTime startDate = (DateTime)reader["start_date"];
                        DateTime dueDate = (DateTime)reader["due_date"];

                        StartDateLabel.Text = startDate.ToString("MMMM dd, yyyy");
                        DueDateLabel.Text = dueDate.ToString("MMMM dd, yyyy");

                        switch (StatusLabel.Text)
                        {
                            case "Pending":
                                StatusLabel.ForeColor = Color.Gray;
                                StatusCircle.FillColor = Color.Gray;
                                StatusCircle.HoverState.FillColor = Color.Gray;
                                break;
                            case "Finished":
                                StatusLabel.ForeColor = Color.Green;
                                StatusCircle.FillColor = Color.Green;
                                StatusCircle.HoverState.FillColor = Color.Green;
                                break;
                            case "Missed":
                                StatusLabel.ForeColor = Color.Red;
                                StatusCircle.FillColor = Color.Red;
                                StatusCircle.HoverState.FillColor = Color.Red;
                                break;
                        }
                    }
                    command.CommandText = $"SELECT COUNT(*) FROM task_users WHERE task_id = {id}";
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    TotalUsersLabel.Text = $"Total: {count}";
                }
            }
        }

        private void FillUserList()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_USERS = $"SELECT user_id FROM task_users WHERE task_id = '{id}'";
                using (MySqlCommand command = new MySqlCommand(SELECT_USERS, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["user_id"].ToString();

                            GetUserName(id);
                        }
                    }
                }
            }
        }

        private void GetUserName(string userId)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_TASKS_IN_PROJECT = $"SELECT * FROM users WHERE id = '{userId}' ";
                using (MySqlCommand command = new MySqlCommand(SELECT_TASKS_IN_PROJECT, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string firstName = reader["first_name"].ToString();
                            string lastName = reader["last_name"].ToString();

                            string name = $"{firstName} {lastName}";
                            CreateControls(id, name);
                        }
                    }
                }
            }
        }

        private int count;
        private void CreateControls(string id, string name)
        {
            count++;
            Guna2CustomGradientPanel separatorPanel = new Guna2CustomGradientPanel
            {
                BorderRadius = 5,
                BorderColor = Color.Gray,
                BorderThickness = 1,
                Size = new Size(UserFlowList.Width - 10, 50),
                FillColor = Color.White,
                FillColor2 = Color.LightGray,
                Quality = 10
            };

            Label countLabel = Main.CreateLabel(count.ToString(), new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(CountHeader.Location.X, 15));
            Label nameLabel = Main.CreateLabel(name, new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(NameHeader.Location.X, 15), false, true, new Size(150, 20));


            ActionButtons buttons = new ActionButtons(mainForm, id, null, null, null, null, id, this)
            {
                Location = new Point(ActionsFinishedHeader.Location.X, 5),
            };

            separatorPanel.Controls.Add(countLabel);
            separatorPanel.Controls.Add(nameLabel);
            separatorPanel.Controls.Add(buttons);
            UserFlowList.Controls.Add(separatorPanel);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (fromTask)
            {
                Hide();
                MainForm.taskTab.Show();
                return;
            }

            Hide();
            MainForm.viewProject.Show();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            AddTaskForm addTaskForm = new AddTaskForm(true, id);
            addTaskForm.ShowDialog();
            RefreshViewTask();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteTask();
        }

        private void DeleteTask()
        {
            DialogResult resullt = MessageBox.Show("Are you sure you want to delete this task?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resullt == DialogResult.OK)
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();
                    string SET_ARCHIVE = $"UPDATE tasks SET is_archived = TRUE WHERE id='{id}'";
                    using (MySqlCommand command = new MySqlCommand(SET_ARCHIVE, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Task successfully deleted.", "Task deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Hide();
                            Main.UpdateTaskCount(projectId);
                            MainForm.taskTab.RefreshFlowPanel();
                            MainForm.taskTab.Show();
                        }
                    }
                }
            }
        }

        private void CompletionInfoButton_Click(object sender, EventArgs e)
        {
            CompletionForm completionForm = new CompletionForm(id);
            completionForm.ShowDialog();
        }

        private void SetButtonState()
        {
            if (StatusLabel.Text != "Finished")
            {
                MarkAsFinishedButton.Text = "MARK AS FINISHED";
                MarkAsFinishedButton.FillColor = Color.Green;
               
            }
            else
            {
                MarkAsFinishedButton.Text = "UNMARK AS FINISHED";
                MarkAsFinishedButton.FillColor = Color.Black;
            }
        }

        private void MarkAsFinishedButton_Click(object sender, EventArgs e)
        {
            if (StatusLabel.Text != "Finished")
            {
                MarkAsFinished();
            }
            else
            {
                UnmarkAsFinished();
            }
        }

        private void MarkAsFinished()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                DialogResult result = MessageBox.Show("Do you want to mark this task as finished?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    string SET_TASK_AS_FINISHED = $"UPDATE tasks SET status = 'Finished' WHERE id = {id}";
                    using (MySqlCommand command = new MySqlCommand(SET_TASK_AS_FINISHED, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Task marked as finished", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MainForm.taskTab.RefreshFlowPanel();
                            MainForm.projectTab.RefreshFlowPanel();
                            MainForm.dashboardTab.RefreshFlowPanel();
                            MainForm.viewProject?.RefreshTaskList();
                            RefreshViewTask();
                        }
                        command.CommandText = $"UPDATE task_users SET status = 'Finished' WHERE task_id = {id}";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void UnmarkAsFinished()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                DialogResult result = MessageBox.Show("Do you want to unmark this task as finished?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    string SET_TASK_AS_FINISHED = $"UPDATE tasks SET status = 'Pending' WHERE id = {id}";
                    using (MySqlCommand command = new MySqlCommand(SET_TASK_AS_FINISHED, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Task unmarked as finished", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MainForm.taskTab.RefreshFlowPanel();
                            MainForm.projectTab.RefreshFlowPanel();
                            MainForm.dashboardTab.RefreshFlowPanel();
                            MainForm.viewProject?.RefreshTaskList();
                            RefreshViewTask();
                        }
                        command.CommandText = $"UPDATE task_users SET status = 'Pending' WHERE task_id = {id}";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
