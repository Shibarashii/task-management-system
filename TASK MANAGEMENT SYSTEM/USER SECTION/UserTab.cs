using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Net.NetworkInformation;

namespace TASK_MANAGEMENT_SYSTEM.UserControls
{
    public partial class UserTab : UserControl
    {
        private MainForm mainForm;
        public UserTab(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            RefreshFlowPanel();
        }

        public void RefreshFlowPanel(string columnName = "id", bool isDescending = false)
        {
            FlowPanel.Controls.Clear();
            count = 0;
            SetUserTasksFinished();
            if (AdminsOnlyButton.Checked)
            {
                GetAllSuperusersOnly(columnName, isDescending);
            }
            else if (UsersOnlyButton.Checked)
            {
                GetAllUsersOnly(columnName, isDescending);
            }
            else
            {
                GetAllUser(columnName, isDescending);
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

        private void SetUserTasksFinished()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string GET_ALL_USERS = "SELECT id FROM users";
                using (MySqlCommand command = new MySqlCommand(GET_ALL_USERS, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();

                            using (MySqlConnection connection2 = new MySqlConnection(Main.ConnectionString))
                            {
                                connection2.Open();
                                string GET_FINISHED_TASKS_COUNT = $"SELECT COUNT(*) FROM task_users WHERE user_id = {id} AND status = 'Finished' AND is_archived = FALSE";
                                using (MySqlCommand command2 = new MySqlCommand(GET_FINISHED_TASKS_COUNT, connection2))
                                {
                                    int count = Convert.ToInt32(command2.ExecuteScalar());
                                    command2.CommandText = $"UPDATE users SET tasks_finished = {count} WHERE id = {id}";
                                    command2.ExecuteNonQuery();
                                }
                                connection2.Close();
                            }
                        }
                        connection.Close();
                    }
                }
            }
        }

        private int count;
        public void GetAllUser(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_USER = $"SELECT * FROM users WHERE is_archived=FALSE AND (LOWER(first_name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(middle_name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(last_name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(email) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
                using (MySqlCommand command = new MySqlCommand(SELECT_USER, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string firstName = reader["first_name"].ToString();
                            string middleName = reader["middle_name"].ToString();
                            string lastName = reader["last_name"].ToString();
                            string email = reader["email"].ToString();
                            int tasksFinished = (int)reader["tasks_finished"];
                            DateTime dateJoined = (DateTime)reader["date_joined"];
                            bool isSuperuser = (bool)reader["is_superuser"];
                            string fullName = $"{firstName} {middleName} {lastName}";
                            CreateControls(id, fullName, email, tasksFinished, dateJoined, isSuperuser);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void GetAllSuperusersOnly(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string sortOrder = isDescending ? "DESC" : "ASC";
                string SELECT_SUPERUSERS = $"SELECT * FROM users WHERE is_superuser=TRUE AND is_archived=FALSE AND (LOWER(first_name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(middle_name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(last_name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(email) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
                using (MySqlCommand command = new MySqlCommand(SELECT_SUPERUSERS, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string firstName = reader["first_name"].ToString();
                            string middleName = reader["middle_name"].ToString();
                            string lastName = reader["last_name"].ToString();
                            string email = reader["email"].ToString();
                            int tasksFinished = (int)reader["tasks_finished"];
                            bool isSuperuser = (bool)reader["is_superuser"];
                            DateTime dateJoined = (DateTime)reader["date_joined"];

                            string fullName = $"{firstName} {middleName} {lastName}";
                            CreateControls(id, fullName, email, tasksFinished, dateJoined, isSuperuser);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void GetAllUsersOnly(string columnName = "id", bool isDescending = false)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                string sortOrder = isDescending ? "DESC" : "ASC";
                connection.Open();
                string SELECT_USERS = $"SELECT * FROM users WHERE is_superuser=FALSE AND is_archived=FALSE AND (LOWER(first_name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(middle_name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(last_name) LIKE LOWER('%{SearchTextbox.Text}%') OR LOWER(email) LIKE LOWER('%{SearchTextbox.Text}%')) ORDER BY {columnName} {sortOrder}";
                using (MySqlCommand command = new MySqlCommand(SELECT_USERS, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string firstName = reader["first_name"].ToString();
                            string middleName = reader["middle_name"].ToString();
                            string lastName = reader["last_name"].ToString();
                            string email = reader["email"].ToString();
                            int tasksFinished = (int)reader["tasks_finished"];
                            bool isSuperuser = (bool)reader["is_superuser"];
                            DateTime dateJoined = (DateTime)reader["date_joined"];

                            string fullName = $"{firstName} {middleName} {lastName}";
                            CreateControls(id, fullName, email, tasksFinished, dateJoined, isSuperuser);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void CreateControls(string id, string fullName, string email, int tasksFinished, DateTime dateJoined, bool isSuperuser)
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
            Label fullNameLabel = Main.CreateLabel(fullName, new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(NameHeader.Location.X, 15), true, true);
            Label emailLabel = Main.CreateLabel(email, new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(EmailHeader.Location.X, 15), true, true);
            Label tasksFinishedLabel = Main.CreateLabel(tasksFinished.ToString(), new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(TasksFinishedHeader.Location.X, 15), true, true);
            Label dateJoinedLabel = Main.CreateLabel(dateJoined.ToString("MM-dd-yyyy"), new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(DateJoinedHeader.Location.X, 15), true, true);

            ActionButtons buttons = new ActionButtons(mainForm, id, this)
            {
                Location = new Point(ActionsFinishedHeader.Location.X, 5),
            };

            if (isSuperuser)
            {
                countLabel.Font = new Font("Arial", 10, FontStyle.Bold);
                fullNameLabel.Font = new Font("Arial", 10, FontStyle.Bold);
                emailLabel.Font = new Font("Arial", 10, FontStyle.Bold);
                tasksFinishedLabel.Font = new Font("Arial", 10, FontStyle.Bold);
                dateJoinedLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            }

            separatorPanel.Controls.Add(countLabel);
            separatorPanel.Controls.Add(fullNameLabel);
            separatorPanel.Controls.Add(emailLabel);
            separatorPanel.Controls.Add(tasksFinishedLabel);
            separatorPanel.Controls.Add(dateJoinedLabel);
            separatorPanel.Controls.Add(buttons);
            FlowPanel.Controls.Add(separatorPanel);
        }

        private void AddNewUser_Click(object sender, EventArgs e)
        {
            AddUserForm addUserForm = new AddUserForm();
            addUserForm.ShowDialog();
        }

        #region SORT BUTTONS
        private void NameSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("first_name", NameSort.Checked);
        }

        private void EmailSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("email", EmailSort.Checked);
        }

        private void TasksFinishedSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("tasks_finished", TasksFinishedSort.Checked);
        }

        private void DateJoinedSort_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel("date_joined", DateJoinedSort.Checked);
        }
        #endregion

        private void SearchTextbox_TextChanged(object sender, EventArgs e)
        {
            RefreshFlowPanel();
        }

        private void AdminsOnlyButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AdminsOnlyButton.Checked)
            {
                UsersOnlyButton.Checked = false;
                RefreshFlowPanel();
                return;
            }

            RefreshFlowPanel();
        }

        private void UsersOnlyButton_CheckedChanged(object sender, EventArgs e)
        {
            if (UsersOnlyButton.Checked)
            {
                AdminsOnlyButton.Checked = false;
                RefreshFlowPanel();
                return;
            }

            RefreshFlowPanel();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            SearchTextbox.Text = string.Empty;
            UsersOnlyButton.Checked = false;
            AdminsOnlyButton.Checked = false;
            if (NameSort.Checked)
            {
                NameSort.Checked = false;

            }
            if (EmailSort.Checked)
            {
                EmailSort.Checked = false;

            }
            if (TasksFinishedSort.Checked)
            {
                TasksFinishedSort.Checked = false;
            }
            if (DateJoinedSort.Checked)
            {
                DateJoinedSort.Checked = false;
            }
            RefreshFlowPanel();
        }
    }
}
