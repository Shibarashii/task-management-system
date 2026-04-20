using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Windows.Forms;

namespace TASK_MANAGEMENT_SYSTEM
{
    public partial class AddUserForm : Form
    {
        private readonly string id;
        private readonly bool forEditing;
        public AddUserForm(string id = null, bool forEditing = false)
        {
            this.id = id;
            this.forEditing = forEditing;
            InitializeComponent();

            if (forEditing)
            {
                AddUserButton.Text = "UPDATE USER";
                SetValuesForEditing();
                CheckEmailState();
            }

            if (!MainForm.isSuperuser)
            {
                AdminCheckbox.Hide();
                label1.Hide();
            }
        }

        private void XButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddUserButton_Click(object sender, EventArgs e)
        {
            if (forEditing)
            {
                EditUser();
                return;
            }
            AddUser();
        }

        private string currentFirstName;
        private string currentMiddleName;
        private string currentLastName;
        private string currentEmail;
        private void SetValuesForEditing()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string SELECT_ALL_USERS = $"SELECT * FROM users WHERE id = {id} AND is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(SELECT_ALL_USERS, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FirstNameTextbox.Text = reader["first_name"].ToString();
                            MiddleNameTextbox.Text = reader["middle_name"].ToString();
                            LastNameTextbox.Text = reader["last_name"].ToString();
                            EmailTextbox.Text = reader["email"].ToString();
                            PasswordTextbox.Text = reader["password"].ToString();
                            ConfirmPasswordTextbox.Text = reader["password"].ToString();
                            AdminCheckbox.Checked = (bool)reader["is_superuser"];

                            currentFirstName = reader["first_name"].ToString();
                            currentMiddleName = reader["middle_name"].ToString();
                            currentLastName = reader["last_name"].ToString();
                            currentEmail = reader["email"].ToString();
                        }
                    }
                }
            }
        }

        private void EditUser()
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextbox.Text) ||
                        string.IsNullOrWhiteSpace(MiddleNameTextbox.Text) ||
                        string.IsNullOrWhiteSpace(LastNameTextbox.Text) ||
                        string.IsNullOrWhiteSpace(EmailTextbox.Text) ||
                        string.IsNullOrWhiteSpace(PasswordTextbox.Text) ||
                        string.IsNullOrWhiteSpace(ConfirmPasswordTextbox.Text))
            {
                MessageBox.Show("Please fill up all fields", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!emailIsAvailable && EmailTextbox.Text != currentEmail)
            {
                MessageBox.Show("Email is already taken", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!Regex.IsMatch(EmailTextbox.Text, @"^[a-zA-Z0-9._%+-]+@gmail\.com$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Please enter a valid email", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!passwordMatches)
            {
                MessageBox.Show("Passwords do not match", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();
                    string CHECK_IF_USER_ALREADY_EXISTS = $"SELECT * FROM users WHERE first_name='{FirstNameTextbox.Text}' AND middle_name='{MiddleNameTextbox.Text}' AND last_name='{LastNameTextbox.Text}' AND is_archived = FALSE";
                    using (MySqlCommand command = new MySqlCommand(CHECK_IF_USER_ALREADY_EXISTS, connection))
                    {
                        int matchRecord = Convert.ToInt32(command.ExecuteScalar());
                        if (matchRecord > 0 && (FirstNameTextbox.Text != currentFirstName || MiddleNameTextbox.Text != currentMiddleName || LastNameTextbox.Text != currentLastName))
                        {
                            MessageBox.Show($"Account already with the name {FirstNameTextbox.Text} {MiddleNameTextbox.Text} {LastNameTextbox.Text}", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        else
                        {
                            command.CommandText = $"UPDATE users SET first_name = '{FirstNameTextbox.Text}', middle_name = '{MiddleNameTextbox.Text}', last_name = '{LastNameTextbox.Text}', email = '{EmailTextbox.Text}', password = '{PasswordTextbox.Text}', is_superuser = {AdminCheckbox.Checked} WHERE id = {id}";
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("User successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MainForm.userTab.RefreshFlowPanel();
                                    connection.Close();
                                    Close();
                                }
                            }
                        }
                    }
                }
            }
        }


        private void AddUser()
        {
            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string CHECK_IF_USER_ALREADY_EXISTS = $"SELECT * FROM users WHERE first_name='{FirstNameTextbox.Text}' AND middle_name='{MiddleNameTextbox.Text}' AND last_name='{LastNameTextbox.Text}' AND is_archived = FALSE";
                using (MySqlCommand command = new MySqlCommand(CHECK_IF_USER_ALREADY_EXISTS, connection))
                {
                    int match = Convert.ToInt32(command.ExecuteScalar());
                    int matchRecord = Convert.ToInt32(command.ExecuteScalar());
                    if (string.IsNullOrWhiteSpace(FirstNameTextbox.Text) ||
                        string.IsNullOrWhiteSpace(MiddleNameTextbox.Text) ||
                        string.IsNullOrWhiteSpace(LastNameTextbox.Text) ||
                        string.IsNullOrWhiteSpace(EmailTextbox.Text) ||
                        string.IsNullOrWhiteSpace(PasswordTextbox.Text) ||
                        string.IsNullOrWhiteSpace(ConfirmPasswordTextbox.Text))
                    {
                        MessageBox.Show("Please fill up all fields", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!emailIsAvailable)
                    {
                        MessageBox.Show("Email is already taken", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!Regex.IsMatch(EmailTextbox.Text, @"^[a-zA-Z0-9._%+-]+@gmail\.com$", RegexOptions.IgnoreCase))
                    {
                        MessageBox.Show("Please enter a valid email", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!passwordMatches)
                    {
                        MessageBox.Show("Passwords do not match", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (matchRecord > 0)
                    {
                        MessageBox.Show($"Account already with the name {FirstNameTextbox.Text} {MiddleNameTextbox.Text} {LastNameTextbox.Text}", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string INSERT_USER = $"INSERT INTO users (first_name, middle_name, last_name, email, password, date_joined, is_superuser) VALUES (@firstName, @middleName, @lastName, @email, @password, @dateJoined, @isSuperuser)";
                        using (MySqlCommand command2 = new MySqlCommand(INSERT_USER, connection))
                        {
                            command2.Parameters.AddWithValue("@firstName", FirstNameTextbox.Text);
                            command2.Parameters.AddWithValue("@middleName", MiddleNameTextbox.Text);
                            command2.Parameters.AddWithValue("@lastName", LastNameTextbox.Text);
                            command2.Parameters.AddWithValue("@email", EmailTextbox.Text);
                            command2.Parameters.AddWithValue("@password", PasswordTextbox.Text);
                            command2.Parameters.AddWithValue("@dateJoined", DateTime.Now);
                            command2.Parameters.AddWithValue("@isSuperuser", AdminCheckbox.Checked);

                            int rowsAffected = command2.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("User successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MainForm.userTab.RefreshFlowPanel();
                                connection.Close();
                                Close();
                            }
                        }
                    }
                }
            }
        }

        bool emailIsAvailable;
        private void EmailTextbox_TextChanged(object sender, EventArgs e)
        {
            CheckEmailState();
        }

        private void CheckEmailState()
        {
            if (forEditing)
            {
                using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
                {
                    connection.Open();
                    string CHECK_EMAIL = $"SELECT * FROM users WHERE email='{EmailTextbox.Text}'";
                    using (MySqlCommand command = new MySqlCommand(CHECK_EMAIL, connection))
                    {
                        int match = Convert.ToInt32(command.ExecuteScalar());
                        if (match > 0 && EmailTextbox.Text != currentEmail)
                        {
                            EmailTakenLabel.Visible = true;
                            emailIsAvailable = false;
                        }
                        else
                        {
                            EmailTakenLabel.Visible = false;
                            emailIsAvailable = true;
                        }
                    }
                    connection.Close();
                    return;
                }
            }

            using (MySqlConnection connection = new MySqlConnection(Main.ConnectionString))
            {
                connection.Open();
                string CHECK_EMAIL = $"SELECT * FROM users WHERE email='{EmailTextbox.Text}'";
                using (MySqlCommand command = new MySqlCommand(CHECK_EMAIL, connection))
                {
                    int match = Convert.ToInt32(command.ExecuteScalar());
                    if (match > 0)
                    {
                        EmailTakenLabel.Visible = true;
                        emailIsAvailable = false;
                    }
                    else
                    {
                        EmailTakenLabel.Visible = false;
                        emailIsAvailable = true;
                    }
                }
                connection.Close();
            }
        }
        bool passwordMatches;
        private void PasswordTextbox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordTextbox.Text) || string.IsNullOrWhiteSpace(ConfirmPasswordTextbox.Text))
            {
                PasswordMatchLabel.Visible = false;
            }

            else if (PasswordTextbox.Text != ConfirmPasswordTextbox.Text)
            {
                PasswordMatchLabel.Visible = true;
                PasswordMatchLabel.Text = "Passwords do not match";
                PasswordMatchLabel.ForeColor = Color.Red;
                passwordMatches = false;
            }
            else if (PasswordTextbox.Text == ConfirmPasswordTextbox.Text)
            {
                PasswordMatchLabel.Visible = true;
                PasswordMatchLabel.Text = "Password matches";
                PasswordMatchLabel.ForeColor = Color.LimeGreen;
                passwordMatches = true;
            }
        }

        private void Password2Textbox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordTextbox.Text) || string.IsNullOrWhiteSpace(ConfirmPasswordTextbox.Text))
            {
                PasswordMatchLabel.Visible = false;
            }

            else if (PasswordTextbox.Text != ConfirmPasswordTextbox.Text)
            {
                PasswordMatchLabel.Visible = true;
                PasswordMatchLabel.Text = "Passwords do not match";
                PasswordMatchLabel.ForeColor = Color.Red;
                passwordMatches = false;
            }
            else if (PasswordTextbox.Text == ConfirmPasswordTextbox.Text)
            {
                PasswordMatchLabel.Visible = true;
                PasswordMatchLabel.Text = "Password matches";
                PasswordMatchLabel.ForeColor = Color.LimeGreen;
                passwordMatches = true;
            }
        }

        private void ShowPasswordImageCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPasswordImageCheckbox.Checked)
            {
                PasswordTextbox.PasswordChar = '\0';
            }
            else if (!ShowPasswordImageCheckbox.Checked)
            {
                PasswordTextbox.PasswordChar = '●';
            }
        }

        private void ShowConfirmPasswordCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowConfirmPasswordCheckbox.Checked)
            {
                ConfirmPasswordTextbox.PasswordChar = '\0';
            }
            else if (!ShowConfirmPasswordCheckbox.Checked)
            {
                ConfirmPasswordTextbox.PasswordChar = '●';
            }
        }
    }
}
