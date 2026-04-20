using System;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TASK_MANAGEMENT_SYSTEM.PROJECT_SECTION;
using ZstdSharp.Unsafe;

namespace TASK_MANAGEMENT_SYSTEM.TASK_SECTION
{
    public partial class AddTaskForm : Form
    {
        private readonly bool forEditing;
        private readonly string id;
        private readonly Dictionary<string, string> userIdMapping = new Dictionary<string, string>();
        public static ViewProject viewProject = null;
        public AddTaskForm(bool forEditing = false, string id = null)
        {
            InitializeComponent();
            this.forEditing = forEditing;
            this.id = id;

            PopulateCheckListbox();
            PopulateProjectComboBox();
            SetDateValues();

            if (forEditing)
            {
                SetValuesForEditing();
            }
            else
            {
                InitializeSelectedProject();
            }
        }

        private void PopulateCheckListbox()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_ALL_USERS = "SELECT * FROM users WHERE is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(SELECT_ALL_USERS, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string firstName = reader["first_name"].ToString();
                            string middleName = reader["middle_name"].ToString();
                            string lastName = reader["last_name"].ToString();

                            string fullName = $"{firstName} {middleName} {lastName}";

                            AssignedUsersCheckListbox.Items.Add(fullName);

                            string userId = reader["id"].ToString();
                            userIdMapping.Add(fullName, userId);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void PopulateProjectComboBox()
        {
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
        }

        #region GET PROJECT VALUES
        private void SetDateValues()
        {
            StartDatePicker.Value = GetProjectStartDate();
            DueDatePicker.Value = GetProjectDueDate();
        }

        private DateTime GetProjectDueDate()
        {
            if (ProjectComboBox.SelectedIndex != 0)
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();
                    string SELECT_PROJECT = "SELECT due_date FROM projects WHERE name = @name";
                    using (MySqlCommand command = new MySqlCommand(SELECT_PROJECT, connection))
                    {
                        command.Parameters.AddWithValue("@name", ProjectComboBox.Text);
                        object result = command.ExecuteScalar();

                        return (DateTime)result;
                    }
                }
            }
            return DateTime.Now.Date;
        }

        private DateTime GetProjectStartDate()
        {
            if (ProjectComboBox.SelectedIndex != 0)
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();
                    string SELECT_PROJECT = "SELECT start_date FROM projects WHERE name = @name";
                    using (MySqlCommand command = new MySqlCommand(SELECT_PROJECT, connection))
                    {
                        command.Parameters.AddWithValue("@name", ProjectComboBox.Text);
                        object result = command.ExecuteScalar();

                        return (DateTime)result;
                    }
                }
            }
            return DateTime.Now.Date;
        }

        public string GetProjectID()
        {
            if (ProjectComboBox.SelectedIndex != 0)
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();
                    string SELECT_PROJECT = "SELECT id FROM projects WHERE name = @name";
                    using (MySqlCommand command = new MySqlCommand(SELECT_PROJECT, connection))
                    {
                        command.Parameters.AddWithValue("@name", ProjectComboBox.Text);
                        string result = command.ExecuteScalar().ToString();

                        return result;
                    }
                }
            }
            return string.Empty;
        }
        #endregion

        private void InitializeSelectedProject()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string GET_TASK_NAME = $"SELECT name FROM projects WHERE id = '{id}' AND is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(GET_TASK_NAME, connection))
                {
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        string name = result.ToString();
                        int projectIndex = ProjectComboBox.FindStringExact(name);
                        if (projectIndex != -1)
                        {
                            ProjectComboBox.SelectedIndex = projectIndex;
                        }
                    }
                }
            }
        }


        private void SetValuesForEditing()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string GET_TASK_DATA = $"SELECT * FROM tasks WHERE id='{id} AND is_archived = FALSE'";
                using (MySqlCommand command = new MySqlCommand(GET_TASK_DATA, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        TaskNameTextbox.Text = reader["name"].ToString();
                        TaskDescriptionTextbox.Text = reader["description"].ToString();
                        DueDatePicker.Value = (DateTime)reader["due_date"];
                        StartDatePicker.Value = (DateTime)reader["start_date"];

                        // Pre-select project in ProjectComboBox
                        string projectName = reader["project_name"].ToString();
                        int projectIndex = ProjectComboBox.FindStringExact(projectName);
                        if (projectIndex != -1)
                        {
                            ProjectComboBox.SelectedIndex = projectIndex;
                        }
                    }
                }

                // Pre-select users in AssignedUsersCheckListbox
                string SELECT_ASSIGNED_USERS = $"SELECT user_id FROM task_users WHERE task_id='{id}'";
                using (MySqlCommand userCommand = new MySqlCommand(SELECT_ASSIGNED_USERS, connection))
                {
                    using (MySqlDataReader userReader = userCommand.ExecuteReader())
                    {
                        while (userReader.Read())
                        {
                            string userId = userReader["user_id"].ToString();
                            string fullName = GetFullNameById(userId);

                            int userIndex = AssignedUsersCheckListbox.FindStringExact(fullName);
                            if (userIndex != -1)
                            {
                                AssignedUsersCheckListbox.SetItemChecked(userIndex, true);
                            }
                        }
                    }
                }
            }

            AddLabel.Text = "EDIT TASK";
            AddButton.Text = "UPDATE";
        }

        private string GetFullNameById(string userId)
        {
            foreach (var entry in userIdMapping)
            {
                if (entry.Value == userId)
                {
                    return entry.Key;
                }
            }
            return string.Empty;
        }


        private void XButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (forEditing)
            {
                UpdateTask();
                return;
            }

            AddTask();
        }

        private void AddTask()
        {
            if (string.IsNullOrEmpty(TaskNameTextbox.Text))
            {
                MessageBox.Show("Please fill up all fields.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ProjectComboBox.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a project.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (AssignedUsersCheckListbox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one user.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();
                    string CHECK_TASK_NAME = $"SELECT COUNT(*) FROM tasks WHERE name='{TaskNameTextbox.Text}' AND project_name = '{ProjectComboBox.Text}' AND is_archived = FALSE";
                    using (MySqlCommand command = new MySqlCommand(CHECK_TASK_NAME, connection))
                    {
                        int matchRecord = Convert.ToInt32(command.ExecuteScalar());
                        if (matchRecord > 0)
                        {
                            MessageBox.Show("Task name already exists in this project.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            SaveAssignedUsers();
                            MainForm.taskTab.RefreshFlowPanel();
                            Main.UpdateTaskCount(GetProjectID());
                            viewProject?.RefreshTaskList();
                            ResetValues();
                        }
                    }
                }
            }
        }

        private void UpdateTask()
        {
            string taskId;
            using (MySqlConnection connection1 = new MySqlConnection(Main.ConnectionString))
            {
                connection1.Open();
                string SELECT_TASK_ID = $"SELECT id FROM tasks WHERE id = '{id}'";
                using (MySqlCommand getTaskIdCommand = new MySqlCommand (SELECT_TASK_ID, connection1))
                {
                    taskId = getTaskIdCommand.ExecuteScalar().ToString();
                }
            }
            if (string.IsNullOrWhiteSpace(TaskNameTextbox.Text))
            {
                MessageBox.Show("Please fill up all fields.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ProjectComboBox.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a project.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (AssignedUsersCheckListbox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one user.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();
                    string CHECK_TASK_NAME = $"SELECT COUNT(*) FROM tasks WHERE name='{TaskNameTextbox.Text}' AND project_name = '{ProjectComboBox.Text}' AND is_archived = FALSE";
                    using (MySqlCommand command = new MySqlCommand(CHECK_TASK_NAME, connection))
                    {
                        int matchRecord = Convert.ToInt32(command.ExecuteScalar());
                        if (matchRecord > 0 && taskId != id)
                        {
                            MessageBox.Show("Task name already exists in this project.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            UpdateAssignedUsers();
                            MainForm.taskTab.RefreshFlowPanel();
                            viewProject?.RefreshTaskList();
                            Close();
                        }
                    }
                }
            }
        }

        private void UpdateAssignedUsers()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();

                string INSERT_TASK = "UPDATE tasks SET name=@name, project_id=@projectId, project_name=@projectName, description=@description, start_date=@startDate, due_date=@dueDate WHERE id=@id";
                using (MySqlCommand insertTaskCommand = new MySqlCommand(INSERT_TASK, connection))
                {
                    insertTaskCommand.Parameters.AddWithValue("@name", TaskNameTextbox.Text);
                    insertTaskCommand.Parameters.AddWithValue("@projectId", GetProjectID());
                    insertTaskCommand.Parameters.AddWithValue("@projectName", ProjectComboBox.Text);

                    // Check if TaskDescriptionTextbox is empty
                    if (string.IsNullOrWhiteSpace(TaskDescriptionTextbox.Text))
                    {
                        // If empty, set the default value
                        insertTaskCommand.Parameters.AddWithValue("@description", "No description added");
                    }
                    else
                    {
                        // If not empty, use the provided value
                        insertTaskCommand.Parameters.AddWithValue("@description", TaskDescriptionTextbox.Text);
                    }

                    insertTaskCommand.Parameters.AddWithValue("@startDate", GetProjectStartDate());
                    insertTaskCommand.Parameters.AddWithValue("@dueDate", GetProjectDueDate());

                    insertTaskCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = insertTaskCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        SaveTaskUserAssociations(id);
                    }
                }
                connection.Close();
            }
        }

        private void SaveAssignedUsers()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();

                string INSERT_TASK = "INSERT INTO tasks (name, project_id, project_name, description, start_date, due_date) VALUES (@name, @projectId, @projectName, @description, @startDate, @dueDate)";
                using (MySqlCommand insertTaskCommand = new MySqlCommand(INSERT_TASK, connection))
                {
                    insertTaskCommand.Parameters.AddWithValue("@name", TaskNameTextbox.Text);
                    insertTaskCommand.Parameters.AddWithValue("@projectId", GetProjectID());
                    insertTaskCommand.Parameters.AddWithValue("@projectName", ProjectComboBox.Text);

                    // Check if TaskDescriptionTextbox is empty
                    if (string.IsNullOrEmpty(TaskDescriptionTextbox.Text))
                    {
                        // If empty, set the default value
                        insertTaskCommand.Parameters.AddWithValue("@description", "No description added");
                    }
                    else
                    {
                        // If not empty, use the provided value
                        insertTaskCommand.Parameters.AddWithValue("@description", TaskDescriptionTextbox.Text);
                    }

                    insertTaskCommand.Parameters.AddWithValue("@startDate", GetProjectStartDate());
                    insertTaskCommand.Parameters.AddWithValue("@dueDate", GetProjectDueDate());

                    int rowsAffected = insertTaskCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        string SELECT_LAST_TASK_ID = "SELECT LAST_INSERT_ID()";
                        using (MySqlCommand getLastTaskIdCommand = new MySqlCommand(SELECT_LAST_TASK_ID, connection))
                        {
                            string taskId = getLastTaskIdCommand.ExecuteScalar().ToString();

                            SaveTaskUserAssociations(taskId);
                        }
                    }
                }
                connection.Close();
            }
        }


        private void SaveTaskUserAssociations(string taskId)
        {

            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();

                // Delete existing task-user associations for the task
                string DELETE_TASK_USERS = "DELETE FROM task_users WHERE task_id = @taskId";
                using (MySqlCommand deleteCommand = new MySqlCommand(DELETE_TASK_USERS, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@taskId", taskId);
                    deleteCommand.ExecuteNonQuery();
                }

                // Insert new task-user associations based on the current state of the checklistbox
                string INSERT_TASK_USERS = "INSERT INTO task_users (task_id, user_id) VALUES (@taskId, @userId)";
                int rowsAffected = 0;

                using (MySqlCommand insertCommand = new MySqlCommand(INSERT_TASK_USERS, connection))
                {
                    insertCommand.Parameters.AddWithValue("@taskId", taskId);
                    
                    foreach (string fullName in AssignedUsersCheckListbox.CheckedItems)
                    {
                        if (userIdMapping.TryGetValue(fullName, out string userId))
                        {
                            // Insert records for each checked user
                            insertCommand.Parameters.Clear();
                            insertCommand.Parameters.AddWithValue("@taskId", taskId);
                            insertCommand.Parameters.AddWithValue("@userId", userId);
                            rowsAffected = insertCommand.ExecuteNonQuery();
                        }
                    }
                    if (rowsAffected > 0 && forEditing)
                    {
                        MessageBox.Show("Task successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (rowsAffected > 0 && !forEditing)
                    {
                        MessageBox.Show("Task successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void ProjectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDateValues();
        }

        private void ResetValues()
        {
            TaskNameTextbox.Text = string.Empty;
            TaskDescriptionTextbox.Text = string.Empty;

            for (int i = 0; i < AssignedUsersCheckListbox.Items.Count; i++)
            {
                AssignedUsersCheckListbox.SetItemChecked(i, false);
            }
        }

        private void TaskDescriptionTextbox_TextChanged(object sender, EventArgs e)
        {
            TextCountLabel.Text = $"{TaskDescriptionTextbox.Text.Length}/300";
        }
    }
}
