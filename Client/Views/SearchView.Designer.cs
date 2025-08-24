using System.Windows.Forms;
using System.Drawing;

namespace Client.Views
{
    partial class SearchView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private Panel topPanel;
        private Label titleLabel;

        private GroupBox searchingGroupBox;
        private Label firstNameLabel;
        private TextBox firstNameField;
        private Label lastNameLabel;
        private TextBox lastNameField;
        private Label addressLabel;
        private TextBox addressField;
        private Button searchButton;
        private Button deleteButton;

        private Label resultsLabel;
        private DataGridView resultsDataGridView;

        private void InitializeComponent()
        {
            topPanel = new Panel();
            titleLabel = new Label();
            searchingGroupBox = new GroupBox();
            firstNameLabel = new Label();
            firstNameField = new TextBox();
            lastNameLabel = new Label();
            lastNameField = new TextBox();
            addressLabel = new Label();
            addressField = new TextBox();
            searchButton = new Button();
            deleteButton = new Button();
            resultsLabel = new Label();
            resultsDataGridView = new DataGridView();
            closeButton = new Button();
            topPanel.SuspendLayout();
            searchingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)resultsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.FromArgb(76, 175, 80);
            topPanel.Controls.Add(titleLabel);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Margin = new Padding(5, 6, 5, 6);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1440, 120);
            topPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.Anchor = AnchorStyles.None;
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(603, 30);
            titleLabel.Margin = new Padding(5, 0, 5, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(274, 51);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Search Tickets";
            // 
            // searchingGroupBox
            // 
            searchingGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            searchingGroupBox.Controls.Add(firstNameLabel);
            searchingGroupBox.Controls.Add(firstNameField);
            searchingGroupBox.Controls.Add(lastNameLabel);
            searchingGroupBox.Controls.Add(lastNameField);
            searchingGroupBox.Controls.Add(addressLabel);
            searchingGroupBox.Controls.Add(addressField);
            searchingGroupBox.Controls.Add(searchButton);
            searchingGroupBox.Controls.Add(deleteButton);
            searchingGroupBox.Location = new Point(34, 160);
            searchingGroupBox.Margin = new Padding(5, 6, 5, 6);
            searchingGroupBox.Name = "searchingGroupBox";
            searchingGroupBox.Padding = new Padding(5, 6, 5, 6);
            searchingGroupBox.Size = new Size(1371, 300);
            searchingGroupBox.TabIndex = 1;
            searchingGroupBox.TabStop = false;
            searchingGroupBox.Text = "Searching";
            // 
            // firstNameLabel
            // 
            firstNameLabel.AutoSize = true;
            firstNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            firstNameLabel.Location = new Point(34, 60);
            firstNameLabel.Margin = new Padding(5, 0, 5, 0);
            firstNameLabel.Name = "firstNameLabel";
            firstNameLabel.Size = new Size(125, 30);
            firstNameLabel.TabIndex = 0;
            firstNameLabel.Text = "First Name:";
            // 
            // firstNameField
            // 
            firstNameField.Location = new Point(206, 54);
            firstNameField.Margin = new Padding(5, 6, 5, 6);
            firstNameField.Name = "firstNameField";
            firstNameField.PlaceholderText = "Client first name...";
            firstNameField.Size = new Size(374, 35);
            firstNameField.TabIndex = 1;
            // 
            // lastNameLabel
            // 
            lastNameLabel.AutoSize = true;
            lastNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lastNameLabel.Location = new Point(651, 60);
            lastNameLabel.Margin = new Padding(5, 0, 5, 0);
            lastNameLabel.Name = "lastNameLabel";
            lastNameLabel.Size = new Size(118, 30);
            lastNameLabel.TabIndex = 2;
            lastNameLabel.Text = "Last name:";
            // 
            // lastNameField
            // 
            lastNameField.Location = new Point(823, 54);
            lastNameField.Margin = new Padding(5, 6, 5, 6);
            lastNameField.Name = "lastNameField";
            lastNameField.PlaceholderText = "Client last name";
            lastNameField.Size = new Size(374, 35);
            lastNameField.TabIndex = 3;
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            addressLabel.Location = new Point(34, 140);
            addressLabel.Margin = new Padding(5, 0, 5, 0);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new Size(97, 30);
            addressLabel.TabIndex = 4;
            addressLabel.Text = "Address:";
            // 
            // addressField
            // 
            addressField.Location = new Point(206, 134);
            addressField.Margin = new Padding(5, 6, 5, 6);
            addressField.Name = "addressField";
            addressField.PlaceholderText = "Client address";
            addressField.Size = new Size(374, 35);
            addressField.TabIndex = 5;
            // 
            // searchButton
            // 
            searchButton.BackColor = Color.FromArgb(2, 151, 95);
            searchButton.FlatAppearance.BorderSize = 0;
            searchButton.FlatStyle = FlatStyle.Flat;
            searchButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            searchButton.ForeColor = Color.White;
            searchButton.Location = new Point(206, 220);
            searchButton.Margin = new Padding(5, 6, 5, 6);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(137, 60);
            searchButton.TabIndex = 6;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = false;
            searchButton.Click += SearchButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.BackColor = Color.FromArgb(209, 46, 46);
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.ForeColor = Color.White;
            deleteButton.Location = new Point(360, 220);
            deleteButton.Margin = new Padding(5, 6, 5, 6);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(137, 60);
            deleteButton.TabIndex = 7;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += DeleteButton_Click;
            // 
            // resultsLabel
            // 
            resultsLabel.AutoSize = true;
            resultsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            resultsLabel.Location = new Point(34, 490);
            resultsLabel.Margin = new Padding(5, 0, 5, 0);
            resultsLabel.Name = "resultsLabel";
            resultsLabel.Size = new Size(240, 38);
            resultsLabel.TabIndex = 2;
            resultsLabel.Text = "Searching results";
            // 
            // resultsDataGridView
            // 
            resultsDataGridView.AllowUserToAddRows = false;
            resultsDataGridView.AllowUserToDeleteRows = false;
            resultsDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            resultsDataGridView.BackgroundColor = Color.White;
            resultsDataGridView.BorderStyle = BorderStyle.Fixed3D;
            resultsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resultsDataGridView.Location = new Point(34, 550);
            resultsDataGridView.Margin = new Padding(5, 6, 5, 6);
            resultsDataGridView.Name = "resultsDataGridView";
            resultsDataGridView.ReadOnly = true;
            resultsDataGridView.RowHeadersWidth = 72;
            resultsDataGridView.RowTemplate.Height = 25;
            resultsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            resultsDataGridView.Size = new Size(1371, 480);
            resultsDataGridView.TabIndex = 3;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.None;
            closeButton.BackColor = Color.FromArgb(143, 0, 0);
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Font = new Font("Segoe UI", 10F);
            closeButton.ForeColor = Color.White;
            closeButton.Location = new Point(616, 1090);
            closeButton.Margin = new Padding(5, 6, 5, 6);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(206, 80);
            closeButton.TabIndex = 0;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += CloseButton_Click;
            // 
            // SearchView
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1440, 1200);
            Controls.Add(closeButton);
            Controls.Add(resultsDataGridView);
            Controls.Add(resultsLabel);
            Controls.Add(searchingGroupBox);
            Controls.Add(topPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(5, 6, 5, 6);
            MaximizeBox = false;
            Name = "SearchView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Search Tickets";
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            searchingGroupBox.ResumeLayout(false);
            searchingGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)resultsDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button closeButton;
    }
}