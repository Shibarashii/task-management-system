using Guna.UI2.WinForms;
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
using TASK_MANAGEMENT_SYSTEM.TASK_SECTION;
using TASK_MANAGEMENT_SYSTEM.UserControls;

namespace TASK_MANAGEMENT_SYSTEM.PROJECT_SECTION
{
    public partial class ViewProject : UserControl
    {
        private readonly string id;
        private readonly MainForm mainForm;
        
        public ViewProject(MainForm mainForm, string id)
        {
            this.id = id;
            this.mainForm = mainForm;

            InitializeComponent();
            if (!MainForm.isSuperuser)
            {
                EditButton.Hide();
                DeleteButton.Hide();
                AddTaskButton.Hide();
            }

            RefreshTaskList();
            AddTaskForm.viewProject = this;
        }

        public void RefreshTaskList()
        {
            TaskListFlowPanel.Controls.Clear();
            count = 0;
            FillTaskList();
            SetProjectLabels();
            Main.UpdateTaskCount(id);
        }

        private void SetProjectLabels()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string GET_PROJECT = $"SELECT * FROM projects WHERE id='{id}' AND is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(GET_PROJECT, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        ProjectNameLabel.Text = reader["name"].ToString();
                        ProjectDescriptionLabel.Text = reader["description"].ToString();
                        StatusLabel.Text = reader["status"].ToString();
                        ProgressLabel.Text = $"{reader["progress"]}%";
                        TotalTasksLabel.Text = $"Total: {reader["total_tasks"]}";
                        DateTime startDate = (DateTime)reader["start_date"];
                        DateTime dueDate = (DateTime)reader["due_date"];

                        StartDateLabel.Text = startDate.ToString("MMMM dd, yyyy");
                        DueDateLabel.Text = dueDate.ToString("MMMM dd, yyyy");

                        switch (StatusLabel.Text)
                        {
                            case "Started":
                                StatusLabel.ForeColor = Color.Blue;
                                StatusCircle.FillColor = Color.Blue;
                                StatusCircle.HoverState.FillColor = Color.Blue;
                                break;
                            case "Ongoing":
                                StatusLabel.ForeColor = Color.Orange;
                                StatusCircle.FillColor = Color.Orange;
                                StatusCircle.HoverState.FillColor = Color.Orange;
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
                }
            }
        }

        private void FillTaskList()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_TASKS_IN_PROJECT = $"SELECT * FROM tasks WHERE project_id = '{id}' AND is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(SELECT_TASKS_IN_PROJECT, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string name = reader["name"].ToString();

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
                Size = new Size(TaskListFlowPanel.Width - 10, 50),
                FillColor = Color.White,
                FillColor2 = Color.LightGray,
                Quality = 10
            };

            Label countLabel = Main.CreateLabel(count.ToString(), new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(CountHeader.Location.X, 15));
            Label taskNameLabel = Main.CreateLabel(name, new Font("Arial", 10, FontStyle.Regular), Color.Black, Color.Transparent, new Point(NameHeader.Location.X, 15), false, true, new Size(150, 20));
            

            ActionButtons buttons = new ActionButtons(mainForm, id, null, null, null, this, id)
            {
                Location = new Point(ActionsFinishedHeader.Location.X, 5),
            };

            separatorPanel.Controls.Add(countLabel);
            separatorPanel.Controls.Add(taskNameLabel);
            separatorPanel.Controls.Add(buttons);
            TaskListFlowPanel.Controls.Add(separatorPanel);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Hide();
            MainForm.projectTab.Show();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            AddProjectForm addProjectForm = new AddProjectForm(true, id);
            addProjectForm.ShowDialog();
            RefreshTaskList();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteProject();
        }
        private void DeleteProject()
        {
            DialogResult resullt = MessageBox.Show("Are you sure you want to delete this project?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resullt == DialogResult.OK)
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();
                    string SET_ARCHIVE = $"UPDATE projects SET is_archived = TRUE WHERE id='{id}'";
                    using (MySqlCommand command = new MySqlCommand(SET_ARCHIVE, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Project successfully deleted.", "Project deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Hide();
                            MainForm.projectTab.RefreshFlowPanel();
                            MainForm.projectTab.Show();
                        }
                    }
                }
            }
        }

        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            AddTaskForm addTaskForm = new AddTaskForm(false, id);
            addTaskForm.ShowDialog();
        }
    }
}
