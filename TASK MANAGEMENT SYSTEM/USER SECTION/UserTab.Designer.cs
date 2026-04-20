namespace TASK_MANAGEMENT_SYSTEM.UserControls
{
    partial class UserTab
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserTab));
            this.label6 = new System.Windows.Forms.Label();
            this.gunaLinePanel1 = new Guna.UI.WinForms.GunaLinePanel();
            this.ShadowPanel = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.HeaderPanel = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.CountHeader = new System.Windows.Forms.Label();
            this.DateJoinedSort = new Guna.UI2.WinForms.Guna2ImageCheckBox();
            this.TasksFinishedSort = new Guna.UI2.WinForms.Guna2ImageCheckBox();
            this.EmailSort = new Guna.UI2.WinForms.Guna2ImageCheckBox();
            this.NameSort = new Guna.UI2.WinForms.Guna2ImageCheckBox();
            this.ActionsFinishedHeader = new System.Windows.Forms.Label();
            this.TasksFinishedHeader = new System.Windows.Forms.Label();
            this.DateJoinedHeader = new System.Windows.Forms.Label();
            this.EmailHeader = new System.Windows.Forms.Label();
            this.NameHeader = new System.Windows.Forms.Label();
            this.FlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SearchTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            this.AddNewUser = new Guna.UI2.WinForms.Guna2GradientButton();
            this.AdminsOnlyButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.UsersOnlyButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.RefreshButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.NothingToSeeHerePanel = new Guna.UI2.WinForms.Guna2Panel();
            this.SadMagnifyingGlassPicture = new Guna.UI2.WinForms.Guna2PictureBox();
            this.NothingToSeeHere = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ShadowPanel.SuspendLayout();
            this.HeaderPanel.SuspendLayout();
            this.NothingToSeeHerePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SadMagnifyingGlassPicture)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.label6.Location = new System.Drawing.Point(43, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(381, 111);
            this.label6.TabIndex = 7;
            this.label6.Text = "USERS";
            // 
            // gunaLinePanel1
            // 
            this.gunaLinePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(143)))), ((int)(((byte)(250)))));
            this.gunaLinePanel1.LineColor = System.Drawing.Color.Black;
            this.gunaLinePanel1.LineStyle = System.Windows.Forms.BorderStyle.None;
            this.gunaLinePanel1.Location = new System.Drawing.Point(62, 150);
            this.gunaLinePanel1.Name = "gunaLinePanel1";
            this.gunaLinePanel1.Size = new System.Drawing.Size(1398, 4);
            this.gunaLinePanel1.TabIndex = 8;
            // 
            // ShadowPanel
            // 
            this.ShadowPanel.BackColor = System.Drawing.Color.Transparent;
            this.ShadowPanel.Controls.Add(this.HeaderPanel);
            this.ShadowPanel.Controls.Add(this.FlowPanel);
            this.ShadowPanel.FillColor = System.Drawing.Color.White;
            this.ShadowPanel.Location = new System.Drawing.Point(62, 275);
            this.ShadowPanel.Name = "ShadowPanel";
            this.ShadowPanel.Radius = 5;
            this.ShadowPanel.ShadowColor = System.Drawing.Color.DarkBlue;
            this.ShadowPanel.Size = new System.Drawing.Size(1398, 638);
            this.ShadowPanel.TabIndex = 10;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BorderRadius = 10;
            this.HeaderPanel.Controls.Add(this.CountHeader);
            this.HeaderPanel.Controls.Add(this.DateJoinedSort);
            this.HeaderPanel.Controls.Add(this.TasksFinishedSort);
            this.HeaderPanel.Controls.Add(this.EmailSort);
            this.HeaderPanel.Controls.Add(this.NameSort);
            this.HeaderPanel.Controls.Add(this.ActionsFinishedHeader);
            this.HeaderPanel.Controls.Add(this.TasksFinishedHeader);
            this.HeaderPanel.Controls.Add(this.DateJoinedHeader);
            this.HeaderPanel.Controls.Add(this.EmailHeader);
            this.HeaderPanel.Controls.Add(this.NameHeader);
            this.HeaderPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.HeaderPanel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.HeaderPanel.FillColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.HeaderPanel.FillColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(143)))), ((int)(((byte)(250)))));
            this.HeaderPanel.Location = new System.Drawing.Point(15, 23);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.ShadowDecoration.Parent = this.HeaderPanel;
            this.HeaderPanel.Size = new System.Drawing.Size(1341, 63);
            this.HeaderPanel.TabIndex = 10;
            // 
            // CountHeader
            // 
            this.CountHeader.AutoSize = true;
            this.CountHeader.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountHeader.ForeColor = System.Drawing.Color.White;
            this.CountHeader.Location = new System.Drawing.Point(15, 20);
            this.CountHeader.Name = "CountHeader";
            this.CountHeader.Size = new System.Drawing.Size(22, 24);
            this.CountHeader.TabIndex = 10;
            this.CountHeader.Text = "#";
            // 
            // DateJoinedSort
            // 
            this.DateJoinedSort.BackColor = System.Drawing.Color.Transparent;
            this.DateJoinedSort.CheckedState.ImageSize = new System.Drawing.Size(25, 25);
            this.DateJoinedSort.CheckedState.Parent = this.DateJoinedSort;
            this.DateJoinedSort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DateJoinedSort.HoverState.ImageSize = new System.Drawing.Size(25, 25);
            this.DateJoinedSort.HoverState.Parent = this.DateJoinedSort;
            this.DateJoinedSort.Image = ((System.Drawing.Image)(resources.GetObject("DateJoinedSort.Image")));
            this.DateJoinedSort.ImageSize = new System.Drawing.Size(25, 25);
            this.DateJoinedSort.Location = new System.Drawing.Point(1130, 19);
            this.DateJoinedSort.Name = "DateJoinedSort";
            this.DateJoinedSort.PressedState.Parent = this.DateJoinedSort;
            this.DateJoinedSort.Size = new System.Drawing.Size(25, 25);
            this.DateJoinedSort.TabIndex = 9;
            this.DateJoinedSort.UseTransparentBackground = true;
            this.DateJoinedSort.CheckedChanged += new System.EventHandler(this.DateJoinedSort_CheckedChanged);
            // 
            // TasksFinishedSort
            // 
            this.TasksFinishedSort.BackColor = System.Drawing.Color.Transparent;
            this.TasksFinishedSort.CheckedState.ImageSize = new System.Drawing.Size(25, 25);
            this.TasksFinishedSort.CheckedState.Parent = this.TasksFinishedSort;
            this.TasksFinishedSort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TasksFinishedSort.HoverState.ImageSize = new System.Drawing.Size(25, 25);
            this.TasksFinishedSort.HoverState.Parent = this.TasksFinishedSort;
            this.TasksFinishedSort.Image = ((System.Drawing.Image)(resources.GetObject("TasksFinishedSort.Image")));
            this.TasksFinishedSort.ImageSize = new System.Drawing.Size(25, 25);
            this.TasksFinishedSort.Location = new System.Drawing.Point(846, 22);
            this.TasksFinishedSort.Name = "TasksFinishedSort";
            this.TasksFinishedSort.PressedState.Parent = this.TasksFinishedSort;
            this.TasksFinishedSort.Size = new System.Drawing.Size(25, 25);
            this.TasksFinishedSort.TabIndex = 8;
            this.TasksFinishedSort.UseTransparentBackground = true;
            this.TasksFinishedSort.CheckedChanged += new System.EventHandler(this.TasksFinishedSort_CheckedChanged);
            // 
            // EmailSort
            // 
            this.EmailSort.BackColor = System.Drawing.Color.Transparent;
            this.EmailSort.CheckedState.ImageSize = new System.Drawing.Size(25, 25);
            this.EmailSort.CheckedState.Parent = this.EmailSort;
            this.EmailSort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EmailSort.HoverState.ImageSize = new System.Drawing.Size(25, 25);
            this.EmailSort.HoverState.Parent = this.EmailSort;
            this.EmailSort.Image = ((System.Drawing.Image)(resources.GetObject("EmailSort.Image")));
            this.EmailSort.ImageSize = new System.Drawing.Size(25, 25);
            this.EmailSort.Location = new System.Drawing.Point(462, 19);
            this.EmailSort.Name = "EmailSort";
            this.EmailSort.PressedState.Parent = this.EmailSort;
            this.EmailSort.Size = new System.Drawing.Size(25, 25);
            this.EmailSort.TabIndex = 7;
            this.EmailSort.UseTransparentBackground = true;
            this.EmailSort.CheckedChanged += new System.EventHandler(this.EmailSort_CheckedChanged);
            // 
            // NameSort
            // 
            this.NameSort.BackColor = System.Drawing.Color.Transparent;
            this.NameSort.CheckedState.ImageSize = new System.Drawing.Size(25, 25);
            this.NameSort.CheckedState.Parent = this.NameSort;
            this.NameSort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NameSort.HoverState.ImageSize = new System.Drawing.Size(25, 25);
            this.NameSort.HoverState.Parent = this.NameSort;
            this.NameSort.Image = ((System.Drawing.Image)(resources.GetObject("NameSort.Image")));
            this.NameSort.ImageSize = new System.Drawing.Size(25, 25);
            this.NameSort.Location = new System.Drawing.Point(153, 22);
            this.NameSort.Name = "NameSort";
            this.NameSort.PressedState.Parent = this.NameSort;
            this.NameSort.Size = new System.Drawing.Size(25, 25);
            this.NameSort.TabIndex = 6;
            this.NameSort.UseTransparentBackground = true;
            this.NameSort.CheckedChanged += new System.EventHandler(this.NameSort_CheckedChanged);
            // 
            // ActionsFinishedHeader
            // 
            this.ActionsFinishedHeader.AutoSize = true;
            this.ActionsFinishedHeader.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActionsFinishedHeader.ForeColor = System.Drawing.Color.White;
            this.ActionsFinishedHeader.Location = new System.Drawing.Point(1203, 20);
            this.ActionsFinishedHeader.Name = "ActionsFinishedHeader";
            this.ActionsFinishedHeader.Size = new System.Drawing.Size(87, 24);
            this.ActionsFinishedHeader.TabIndex = 4;
            this.ActionsFinishedHeader.Text = "Actions";
            // 
            // TasksFinishedHeader
            // 
            this.TasksFinishedHeader.AutoSize = true;
            this.TasksFinishedHeader.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TasksFinishedHeader.ForeColor = System.Drawing.Color.White;
            this.TasksFinishedHeader.Location = new System.Drawing.Point(679, 20);
            this.TasksFinishedHeader.Name = "TasksFinishedHeader";
            this.TasksFinishedHeader.Size = new System.Drawing.Size(161, 24);
            this.TasksFinishedHeader.TabIndex = 3;
            this.TasksFinishedHeader.Text = "Tasks Finished";
            // 
            // DateJoinedHeader
            // 
            this.DateJoinedHeader.AutoSize = true;
            this.DateJoinedHeader.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateJoinedHeader.ForeColor = System.Drawing.Color.White;
            this.DateJoinedHeader.Location = new System.Drawing.Point(993, 20);
            this.DateJoinedHeader.Name = "DateJoinedHeader";
            this.DateJoinedHeader.Size = new System.Drawing.Size(131, 24);
            this.DateJoinedHeader.TabIndex = 2;
            this.DateJoinedHeader.Text = "Date Joined";
            // 
            // EmailHeader
            // 
            this.EmailHeader.AutoSize = true;
            this.EmailHeader.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailHeader.ForeColor = System.Drawing.Color.White;
            this.EmailHeader.Location = new System.Drawing.Point(389, 20);
            this.EmailHeader.Name = "EmailHeader";
            this.EmailHeader.Size = new System.Drawing.Size(67, 24);
            this.EmailHeader.TabIndex = 1;
            this.EmailHeader.Text = "Email";
            // 
            // NameHeader
            // 
            this.NameHeader.AutoSize = true;
            this.NameHeader.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameHeader.ForeColor = System.Drawing.Color.White;
            this.NameHeader.Location = new System.Drawing.Point(78, 20);
            this.NameHeader.Name = "NameHeader";
            this.NameHeader.Size = new System.Drawing.Size(69, 24);
            this.NameHeader.TabIndex = 0;
            this.NameHeader.Text = "Name";
            // 
            // FlowPanel
            // 
            this.FlowPanel.AutoScroll = true;
            this.FlowPanel.BackColor = System.Drawing.Color.White;
            this.FlowPanel.Location = new System.Drawing.Point(15, 92);
            this.FlowPanel.Name = "FlowPanel";
            this.FlowPanel.Size = new System.Drawing.Size(1362, 530);
            this.FlowPanel.TabIndex = 9;
            // 
            // SearchTextbox
            // 
            this.SearchTextbox.BorderRadius = 10;
            this.SearchTextbox.BorderThickness = 2;
            this.SearchTextbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchTextbox.DefaultText = "";
            this.SearchTextbox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.SearchTextbox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.SearchTextbox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SearchTextbox.DisabledState.Parent = this.SearchTextbox;
            this.SearchTextbox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.SearchTextbox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SearchTextbox.FocusedState.Parent = this.SearchTextbox;
            this.SearchTextbox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchTextbox.ForeColor = System.Drawing.Color.Black;
            this.SearchTextbox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.SearchTextbox.HoverState.Parent = this.SearchTextbox;
            this.SearchTextbox.IconRight = ((System.Drawing.Image)(resources.GetObject("SearchTextbox.IconRight")));
            this.SearchTextbox.IconRightOffset = new System.Drawing.Point(10, 0);
            this.SearchTextbox.IconRightSize = new System.Drawing.Size(25, 25);
            this.SearchTextbox.Location = new System.Drawing.Point(66, 220);
            this.SearchTextbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SearchTextbox.Name = "SearchTextbox";
            this.SearchTextbox.PasswordChar = '\0';
            this.SearchTextbox.PlaceholderText = "Search User";
            this.SearchTextbox.SelectedText = "";
            this.SearchTextbox.ShadowDecoration.Parent = this.SearchTextbox;
            this.SearchTextbox.Size = new System.Drawing.Size(425, 48);
            this.SearchTextbox.TabIndex = 10;
            this.SearchTextbox.TextChanged += new System.EventHandler(this.SearchTextbox_TextChanged);
            // 
            // AddNewUser
            // 
            this.AddNewUser.Animated = true;
            this.AddNewUser.BorderRadius = 5;
            this.AddNewUser.CheckedState.Parent = this.AddNewUser;
            this.AddNewUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddNewUser.CustomImages.Parent = this.AddNewUser;
            this.AddNewUser.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.AddNewUser.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.AddNewUser.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddNewUser.ForeColor = System.Drawing.Color.White;
            this.AddNewUser.HoverState.Parent = this.AddNewUser;
            this.AddNewUser.Image = ((System.Drawing.Image)(resources.GetObject("AddNewUser.Image")));
            this.AddNewUser.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.AddNewUser.ImageSize = new System.Drawing.Size(25, 25);
            this.AddNewUser.Location = new System.Drawing.Point(1259, 223);
            this.AddNewUser.Name = "AddNewUser";
            this.AddNewUser.ShadowDecoration.Parent = this.AddNewUser;
            this.AddNewUser.Size = new System.Drawing.Size(191, 45);
            this.AddNewUser.TabIndex = 12;
            this.AddNewUser.Text = "Add New User";
            this.AddNewUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.AddNewUser.TextOffset = new System.Drawing.Point(5, 0);
            this.AddNewUser.Click += new System.EventHandler(this.AddNewUser_Click);
            // 
            // AdminsOnlyButton
            // 
            this.AdminsOnlyButton.Animated = true;
            this.AdminsOnlyButton.BorderRadius = 5;
            this.AdminsOnlyButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.ToogleButton;
            this.AdminsOnlyButton.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.AdminsOnlyButton.CheckedState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.AdminsOnlyButton.CheckedState.Parent = this.AdminsOnlyButton;
            this.AdminsOnlyButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AdminsOnlyButton.CustomImages.Parent = this.AdminsOnlyButton;
            this.AdminsOnlyButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.AdminsOnlyButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.AdminsOnlyButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdminsOnlyButton.ForeColor = System.Drawing.Color.White;
            this.AdminsOnlyButton.HoverState.Parent = this.AdminsOnlyButton;
            this.AdminsOnlyButton.Image = ((System.Drawing.Image)(resources.GetObject("AdminsOnlyButton.Image")));
            this.AdminsOnlyButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.AdminsOnlyButton.ImageSize = new System.Drawing.Size(25, 25);
            this.AdminsOnlyButton.Location = new System.Drawing.Point(841, 223);
            this.AdminsOnlyButton.Name = "AdminsOnlyButton";
            this.AdminsOnlyButton.ShadowDecoration.Parent = this.AdminsOnlyButton;
            this.AdminsOnlyButton.Size = new System.Drawing.Size(191, 45);
            this.AdminsOnlyButton.TabIndex = 13;
            this.AdminsOnlyButton.Text = "Admins Only";
            this.AdminsOnlyButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.AdminsOnlyButton.TextOffset = new System.Drawing.Point(10, 0);
            this.AdminsOnlyButton.CheckedChanged += new System.EventHandler(this.AdminsOnlyButton_CheckedChanged);
            // 
            // UsersOnlyButton
            // 
            this.UsersOnlyButton.Animated = true;
            this.UsersOnlyButton.BorderRadius = 5;
            this.UsersOnlyButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.ToogleButton;
            this.UsersOnlyButton.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.UsersOnlyButton.CheckedState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.UsersOnlyButton.CheckedState.Parent = this.UsersOnlyButton;
            this.UsersOnlyButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UsersOnlyButton.CustomImages.Parent = this.UsersOnlyButton;
            this.UsersOnlyButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.UsersOnlyButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.UsersOnlyButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsersOnlyButton.ForeColor = System.Drawing.Color.White;
            this.UsersOnlyButton.HoverState.Parent = this.UsersOnlyButton;
            this.UsersOnlyButton.Image = ((System.Drawing.Image)(resources.GetObject("UsersOnlyButton.Image")));
            this.UsersOnlyButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.UsersOnlyButton.ImageSize = new System.Drawing.Size(25, 25);
            this.UsersOnlyButton.Location = new System.Drawing.Point(1041, 223);
            this.UsersOnlyButton.Name = "UsersOnlyButton";
            this.UsersOnlyButton.ShadowDecoration.Parent = this.UsersOnlyButton;
            this.UsersOnlyButton.Size = new System.Drawing.Size(191, 45);
            this.UsersOnlyButton.TabIndex = 14;
            this.UsersOnlyButton.Text = "Users Only";
            this.UsersOnlyButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.UsersOnlyButton.TextOffset = new System.Drawing.Point(10, 0);
            this.UsersOnlyButton.CheckedChanged += new System.EventHandler(this.UsersOnlyButton_CheckedChanged);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Animated = true;
            this.RefreshButton.BorderRadius = 5;
            this.RefreshButton.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.RefreshButton.CheckedState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.RefreshButton.CheckedState.Parent = this.RefreshButton;
            this.RefreshButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RefreshButton.CustomImages.Parent = this.RefreshButton;
            this.RefreshButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.RefreshButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(4)))), ((int)(((byte)(129)))));
            this.RefreshButton.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshButton.ForeColor = System.Drawing.Color.White;
            this.RefreshButton.HoverState.Parent = this.RefreshButton;
            this.RefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshButton.Image")));
            this.RefreshButton.ImageSize = new System.Drawing.Size(25, 25);
            this.RefreshButton.Location = new System.Drawing.Point(791, 224);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.ShadowDecoration.Parent = this.RefreshButton;
            this.RefreshButton.Size = new System.Drawing.Size(44, 45);
            this.RefreshButton.TabIndex = 15;
            this.RefreshButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.RefreshButton.TextOffset = new System.Drawing.Point(10, 0);
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // NothingToSeeHerePanel
            // 
            this.NothingToSeeHerePanel.BackColor = System.Drawing.Color.Transparent;
            this.NothingToSeeHerePanel.Controls.Add(this.SadMagnifyingGlassPicture);
            this.NothingToSeeHerePanel.Controls.Add(this.NothingToSeeHere);
            this.NothingToSeeHerePanel.Location = new System.Drawing.Point(609, 542);
            this.NothingToSeeHerePanel.Name = "NothingToSeeHerePanel";
            this.NothingToSeeHerePanel.ShadowDecoration.Parent = this.NothingToSeeHerePanel;
            this.NothingToSeeHerePanel.Size = new System.Drawing.Size(311, 73);
            this.NothingToSeeHerePanel.TabIndex = 16;
            this.NothingToSeeHerePanel.UseTransparentBackground = true;
            this.NothingToSeeHerePanel.Visible = false;
            // 
            // SadMagnifyingGlassPicture
            // 
            this.SadMagnifyingGlassPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SadMagnifyingGlassPicture.Image = ((System.Drawing.Image)(resources.GetObject("SadMagnifyingGlassPicture.Image")));
            this.SadMagnifyingGlassPicture.Location = new System.Drawing.Point(21, 13);
            this.SadMagnifyingGlassPicture.Name = "SadMagnifyingGlassPicture";
            this.SadMagnifyingGlassPicture.ShadowDecoration.Parent = this.SadMagnifyingGlassPicture;
            this.SadMagnifyingGlassPicture.Size = new System.Drawing.Size(69, 57);
            this.SadMagnifyingGlassPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SadMagnifyingGlassPicture.TabIndex = 1;
            this.SadMagnifyingGlassPicture.TabStop = false;
            // 
            // NothingToSeeHere
            // 
            this.NothingToSeeHere.AutoSize = true;
            this.NothingToSeeHere.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NothingToSeeHere.ForeColor = System.Drawing.Color.Silver;
            this.NothingToSeeHere.Location = new System.Drawing.Point(108, 26);
            this.NothingToSeeHere.Name = "NothingToSeeHere";
            this.NothingToSeeHere.Size = new System.Drawing.Size(185, 24);
            this.NothingToSeeHere.TabIndex = 0;
            this.NothingToSeeHere.Text = "No users found...";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.NothingToSeeHerePanel);
            this.panel2.Controls.Add(this.RefreshButton);
            this.panel2.Controls.Add(this.UsersOnlyButton);
            this.panel2.Controls.Add(this.AdminsOnlyButton);
            this.panel2.Controls.Add(this.AddNewUser);
            this.panel2.Controls.Add(this.SearchTextbox);
            this.panel2.Controls.Add(this.ShadowPanel);
            this.panel2.Controls.Add(this.gunaLinePanel1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1525, 916);
            this.panel2.TabIndex = 4;
            // 
            // UserTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.Name = "UserTab";
            this.Size = new System.Drawing.Size(1525, 916);
            this.ShadowPanel.ResumeLayout(false);
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            this.NothingToSeeHerePanel.ResumeLayout(false);
            this.NothingToSeeHerePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SadMagnifyingGlassPicture)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private Guna.UI.WinForms.GunaLinePanel gunaLinePanel1;
        private Guna.UI2.WinForms.Guna2ShadowPanel ShadowPanel;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel HeaderPanel;
        private System.Windows.Forms.Label CountHeader;
        private Guna.UI2.WinForms.Guna2ImageCheckBox DateJoinedSort;
        private Guna.UI2.WinForms.Guna2ImageCheckBox TasksFinishedSort;
        private Guna.UI2.WinForms.Guna2ImageCheckBox EmailSort;
        private Guna.UI2.WinForms.Guna2ImageCheckBox NameSort;
        private System.Windows.Forms.Label ActionsFinishedHeader;
        private System.Windows.Forms.Label TasksFinishedHeader;
        private System.Windows.Forms.Label DateJoinedHeader;
        private System.Windows.Forms.Label EmailHeader;
        private System.Windows.Forms.Label NameHeader;
        private System.Windows.Forms.FlowLayoutPanel FlowPanel;
        private Guna.UI2.WinForms.Guna2TextBox SearchTextbox;
        private Guna.UI2.WinForms.Guna2GradientButton AddNewUser;
        private Guna.UI2.WinForms.Guna2GradientButton AdminsOnlyButton;
        private Guna.UI2.WinForms.Guna2GradientButton UsersOnlyButton;
        private Guna.UI2.WinForms.Guna2GradientButton RefreshButton;
        private Guna.UI2.WinForms.Guna2Panel NothingToSeeHerePanel;
        private Guna.UI2.WinForms.Guna2PictureBox SadMagnifyingGlassPicture;
        private System.Windows.Forms.Label NothingToSeeHere;
        private System.Windows.Forms.Panel panel2;
    }
}
