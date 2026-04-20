using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using TASK_MANAGEMENT_SYSTEM.TASK_SECTION;

namespace TASK_MANAGEMENT_SYSTEM.PROJECT_SECTION
{
    public partial class AddProjectForm : Form
    {
        private readonly bool forEditing;
        private readonly string id;
        public AddProjectForm(bool forEditing = false, string id = null)
        {
            InitializeComponent();
            this.forEditing = forEditing;
            this.id = id;
            DueDatePicker.Value = DateTime.Now;
            StartDatePicker.Value = DateTime.Now;

            if (forEditing)
            {
                AddLabel.Text = "EDIT PROJECT";
                AddButton.Text = "UPDATE";
                SetValuesForEditing();
            }

        }

        #region EVENTS

        private void ProjectDescriptionTextbox_TextChanged(object sender, EventArgs e)
        {
            TextCountLabel.Text = $"{ProjectDescriptionTextbox.Text.Length}/300";
        }

        private void DueDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (DueDatePicker.Value < DateTime.Today && !forEditing)
            {
                DueDatePicker.Value = DateTime.Today;
                MessageBox.Show("Please select a fututre date.", "Cannot pick date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (StartDatePicker.Value < DateTime.Today && !forEditing)
            {
                StartDatePicker.Value = DateTime.Today;
                MessageBox.Show("Please select a fututre date.", "Cannot pick date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        private void XButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetValuesForEditing()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string GET_PROJECT_DATA = $"SELECT * FROM projects WHERE id='{id}'";
                using (MySqlCommand command = new MySqlCommand(GET_PROJECT_DATA, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        ProjectNameTextbox.Text = reader["name"].ToString();
                        ProjectDescriptionTextbox.Text = reader["description"].ToString();
                        DueDatePicker.Value = (DateTime)reader["due_date"];
                        StartDatePicker.Value = (DateTime)reader["start_date"];
                    }
                }
                connection.Close();
            }
        }

        private void EditProject()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string CHECK_PROJECT_NAME = "SELECT * FROM projects WHERE name=@name";

                using (MySqlCommand command = new MySqlCommand(CHECK_PROJECT_NAME, connection))
                {
                    command.Parameters.AddWithValue("@name", ProjectNameTextbox.Text);

                    int? matchRecord = Convert.ToInt32(command.ExecuteScalar());
                    string projectId = null;

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            projectId = reader["id"] as string;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(ProjectNameTextbox.Text) ||
                        string.IsNullOrWhiteSpace(DueDatePicker.Text) ||
                        string.IsNullOrWhiteSpace(ProjectDescriptionTextbox.Text))
                    {
                        MessageBox.Show("Please fill up all fields", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (matchRecord > 0 && projectId != null && projectId != id)
                    {
                        MessageBox.Show("Project name already exists", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string UPDATE_PROJECT = $"UPDATE projects SET name=@newName, start_date=@startDate, due_date=@dueDate, description=@description WHERE id={id}";

                        command.CommandText = UPDATE_PROJECT;
                        command.Parameters.Clear();

                        command.Parameters.AddWithValue("@newName", ProjectNameTextbox.Text);
                        command.Parameters.AddWithValue("@startDate", StartDatePicker.Value);
                        command.Parameters.AddWithValue("@dueDate", DueDatePicker.Value);
                        command.Parameters.AddWithValue("@description", ProjectDescriptionTextbox.Text);

                        try
                        {
                            int rowsAffected = Convert.ToInt32(command.ExecuteNonQuery());
                            if (rowsAffected > 0)
                            {
                                // Update the start_date and due_date of tasks associated with the project
                                UpdateTaskDates(id, StartDatePicker.Value, DueDatePicker.Value);

                                MessageBox.Show("Project successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MainForm.projectTab.RefreshFlowPanel();
                                connection.Close();
                                Close();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Project name already exists", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void UpdateTaskDates(string projectId, DateTime newStartDate, DateTime newDueDate)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();

                // Update the start_date and due_date of tasks associated with the project
                string UPDATE_TASK_DATES = $"UPDATE tasks SET start_date=@newStartDate, due_date=@newDueDate WHERE project_id='{projectId}' AND is_archived = FALSE";

                using (MySqlCommand command = new MySqlCommand(UPDATE_TASK_DATES, connection))
                {
                    command.Parameters.AddWithValue("@newStartDate", newStartDate);
                    command.Parameters.AddWithValue("@newDueDate", newDueDate);
                    command.ExecuteNonQuery();
                }
            }
        }


        private void AddProject()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string CHECK_PROJECT_NAME = "SELECT * FROM projects WHERE name=@name";
                using (MySqlCommand command = new MySqlCommand(CHECK_PROJECT_NAME, connection))
                {
                    command.Parameters.AddWithValue("@name", ProjectNameTextbox.Text);

                    int matchRecord = Convert.ToInt32(command.ExecuteScalar());
                    if (string.IsNullOrWhiteSpace(ProjectNameTextbox.Text) ||
                        string.IsNullOrWhiteSpace(DueDatePicker.Text) ||
                        string.IsNullOrWhiteSpace(ProjectDescriptionTextbox.Text))
                    {
                        MessageBox.Show("Please fill up all fields", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (matchRecord > 0)
                    {
                        MessageBox.Show("Project name already exists", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string INSERT_PROJECT = "INSERT INTO projects (name, description, start_date, due_date, status) VALUES (@name, @description, @startDate, @dueDate, @status)";
                        using (MySqlCommand command2 = new MySqlCommand(INSERT_PROJECT, connection))
                        {
                            command2.Parameters.AddWithValue("@name", ProjectNameTextbox.Text);
                            command2.Parameters.AddWithValue("@description", ProjectDescriptionTextbox.Text);
                            command2.Parameters.AddWithValue("@startDate", StartDatePicker.Value);
                            command2.Parameters.AddWithValue("@dueDate", DueDatePicker.Value);
                            command2.Parameters.AddWithValue("@status", "Started");

                            int rowsAffected = command2.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MainForm.projectTab.RefreshFlowPanel();
                                DialogResult result = MessageBox.Show("Project successfully added! Do you want to add a task?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                if (result == DialogResult.OK)
                                {
                                    AddTaskForm addTaskForm = new AddTaskForm(false, id);
                                    addTaskForm.ShowDialog();
                                }
                                connection.Close();
                                Close();
                            }
                        }
                    }
                }
            }
        }
        private void AddProjectButton_Click(object sender, EventArgs e)
        {
            if (forEditing)
            {
                EditProject();
                return;
            }

            AddProject();
        }
    }
}
