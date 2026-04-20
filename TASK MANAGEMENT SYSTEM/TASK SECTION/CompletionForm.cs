using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ComponentFactory.Krypton.Toolkit;

namespace TASK_MANAGEMENT_SYSTEM.TASK_SECTION
{
    public partial class CompletionForm : Form
    {
        private static string id;
        public CompletionForm(string id)
        {
            CompletionForm.id = id;
            InitializeComponent();

            if (TaskIsSubmitted(id))
            {
                SetSubmissionDetails();
                SubmitButton.Text = "UPDATE";
                DeleteButton.Show();
            }
            else
            {
                SubmitButton.Text = "SUBMIT";
                DeleteButton.Hide();
            }

            if (MainForm.isSuperuser)
            {
                SubmitButton.Hide();
                BackButton.Text = "BACK";
                BackButton.Location = new Point(350, 485);
                FileNameButton.Location = new Point(48, 379);
                FileNameButton.Size = new Size(95, 45);
                FileNameButton.Font = new Font("Arial", 10, FontStyle.Bold);
                AttachImageButton.Hide();
                CloseFileImageButton.Hide();
                SetSubmissionDetails();
            }
        }

        public static bool TaskIsSubmitted(string id)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string CHECK_TASK = $"SELECT * FROM tasks WHERE id = {id} AND completion_is_submitted = TRUE";
                using (MySqlCommand commnad = new MySqlCommand(CHECK_TASK, connection))
                {
                    int matchRecord = Convert.ToInt32(commnad.ExecuteScalar());
                    if (matchRecord > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        private void SetSubmissionDetails()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string GET_INFO = $"SELECT * FROM tasks WHERE is_archived = FALSE AND id = {id}";
                using (MySqlCommand command = new MySqlCommand(GET_INFO, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        DescriptionTextbox.Text = reader["completion_description"].ToString();
                        if (reader["completion_photo"] != DBNull.Value)
                        {
                            attachedImage = (byte[])reader["completion_photo"];
                            FileNameButton.Show();
                            FileNameButton.Text = "View Image";
                        }
                        if (MainForm.isSuperuser)
                        {
                            DescriptionTextbox.ReadOnly = true;
                        }
                        DescriptionTextbox.StateCommon.Content.Color1 = Color.Black;
                        DescriptionTextbox.StateCommon.Content.Font = new Font("Arial", 14, FontStyle.Regular);
                    }
                }
            }
        }
        private byte[] attachedImage;
        private void AttachImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string selectedFileName = openFileDialog.FileName;
                        string attachedImageName = Path.GetFileName(selectedFileName); // Extract the file name from the path
                        attachedImage = File.ReadAllBytes(selectedFileName);

                        if (attachedImage != null)
                        {
                            CloseFileImageButton.Show();
                            FileNameButton.Show();
                            FileNameButton.Text = attachedImageName;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading the selected image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CloseFileImageButton_Click(object sender, EventArgs e)
        {
            attachedImage = null;
            FileNameButton.Hide();
            CloseFileImageButton.Hide();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        #region PLACEHOLDER EVENT FOR BODY TEXTBOX
        private void DescriptionTextbox_Enter(object sender, EventArgs e)
        {
            if (DescriptionTextbox.Text == "Describe how and what processes you did in finishing the task.")
            {
                DescriptionTextbox.Text = string.Empty;
                DescriptionTextbox.StateCommon.Content.Color1 = Color.Black;
                DescriptionTextbox.StateCommon.Content.Font = new Font("Arial", 14, FontStyle.Regular);
            }
        }
        private void DescriptionTextbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextbox.Text))
            {
                DescriptionTextbox.Text = "Describe how and what processes you did in finishing the task.";
                DescriptionTextbox.StateCommon.Content.Color1 = Color.FromArgb(193, 200, 207);
                DescriptionTextbox.StateCommon.Content.Font = new Font("Arial", 14, FontStyle.Italic);
            }
        }
        private void BodyTextbox_TextChanged(object sender, EventArgs e)
        {
            BodyTextCount.Text = $"{DescriptionTextbox.Text.Length}/300";
        }
        #endregion

        private void FileNameButton_Click(object sender, EventArgs e)
        {
            if (attachedImage != null)
            {
                using (ImagePreviewForm imagePreviewForm = new ImagePreviewForm(attachedImage))
                {
                    imagePreviewForm.ShowDialog();
                }
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextbox.Text) || DescriptionTextbox.Text == "Describe how and what processes you did in finishing the task." || attachedImage == null)
            {
                MessageBox.Show("Please fill in the required fields.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SubmitCompletion();
        }

        private void SubmitCompletion()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string CHECK_SUBMISSION = $"SELECT COUNT(*) FROM tasks WHERE id = {id} AND completion_description != 'N/A'";
                using (MySqlCommand command = new MySqlCommand(CHECK_SUBMISSION, connection))
                {
                    int matchRecord = Convert.ToInt32(command.ExecuteScalar());
                    if (matchRecord > 0)
                    {
                        DialogResult result = MessageBox.Show("This task already has a submitted completion form. Overwrite?", "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            command.CommandText = "UPDATE tasks SET completion_description = @completionDescription, completion_photo = @completionPhoto, completion_is_submitted = TRUE WHERE id = @id";
                            command.Parameters.AddWithValue("@completionDescription", DescriptionTextbox.Text);
                            command.Parameters.AddWithValue("@completionPhoto", attachedImage);
                            command.Parameters.AddWithValue("@id", id);
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Completion updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MainForm.viewTask.RefreshViewTask();
                                Close();
                            }
                        }
                    }

                    else
                    {
                        command.CommandText = "UPDATE tasks SET completion_description = @completionDescription, completion_photo = @completionPhoto, completion_is_submitted = TRUE WHERE id = @id";
                        command.Parameters.AddWithValue("@completionDescription", DescriptionTextbox.Text);
                        command.Parameters.AddWithValue("@completionPhoto", attachedImage);
                        command.Parameters.AddWithValue("@id", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Completion submitted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MainForm.viewTask.RefreshViewTask();
                            Close();
                        }
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string UNSUBMIT_COMPLETION = $"UPDATE tasks SET completion_description = DEFAULT, completion_photo = NULL, completion_is_submitted = FALSE WHERE id = {id}";
                using (MySqlCommand command = new MySqlCommand(UNSUBMIT_COMPLETION, connection))
                {
                    if (MainForm.isSuperuser)
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to remove this submission?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Completion form removed.", "Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MainForm.viewTask.RefreshViewTask();
                                Close();
                            }
                        }
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to unsubmit this completion form?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Completion form unsubmitted.", "Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MainForm.viewTask.RefreshViewTask();
                                Close();
                            }
                        }
                    }
                }
            }
        }
    }
}
