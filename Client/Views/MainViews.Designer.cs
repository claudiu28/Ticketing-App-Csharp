using System.Windows.Forms;
using System.Drawing;

namespace Client.Views
{
    partial class MainViews
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
        private Button logoutButton;

        private Label matchesLabel;
        private DataGridView matchesDataGridView;
        private Button sellTicketButton;
        private Button findMatchButton;


        private void InitializeComponent()
        {
            topPanel = new Panel();
            titleLabel = new Label();
            logoutButton = new Button();
            matchesLabel = new Label();
            matchesDataGridView = new DataGridView();
            sellTicketButton = new Button();
            findMatchButton = new Button();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)matchesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.FromArgb(76, 175, 80);
            topPanel.Controls.Add(titleLabel);
            topPanel.Controls.Add(logoutButton);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Margin = new Padding(5, 6, 5, 6);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1383, 100);
            topPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(26, 24);
            titleLabel.Margin = new Padding(5, 0, 5, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(263, 45);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Manage Tickets";
            // 
            // logoutButton
            // 
            logoutButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            logoutButton.BackColor = Color.FromArgb(143, 0, 0);
            logoutButton.FlatAppearance.BorderSize = 0;
            logoutButton.FlatStyle = FlatStyle.Flat;
            logoutButton.ForeColor = Color.White;
            logoutButton.Location = new Point(1221, 20);
            logoutButton.Margin = new Padding(5, 6, 5, 6);
            logoutButton.Name = "logoutButton";
            logoutButton.Size = new Size(137, 60);
            logoutButton.TabIndex = 1;
            logoutButton.Text = "Logout";
            logoutButton.UseVisualStyleBackColor = false;
            logoutButton.Click += LogoutButton_Click;
            // 
            // matchesLabel
            // 
            matchesLabel.AutoSize = true;
            matchesLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            matchesLabel.Location = new Point(26, 130);
            matchesLabel.Margin = new Padding(5, 0, 5, 0);
            matchesLabel.Name = "matchesLabel";
            matchesLabel.Size = new Size(127, 38);
            matchesLabel.TabIndex = 2;
            matchesLabel.Text = "Matches";
            // 
            // matchesDataGridView
            // 
            matchesDataGridView.AllowUserToAddRows = false;
            matchesDataGridView.AllowUserToDeleteRows = false;
            matchesDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            matchesDataGridView.BackgroundColor = Color.White;
            matchesDataGridView.BorderStyle = BorderStyle.Fixed3D;
            matchesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            matchesDataGridView.Location = new Point(26, 190);
            matchesDataGridView.Margin = new Padding(5, 6, 5, 6);
            matchesDataGridView.Name = "matchesDataGridView";
            matchesDataGridView.ReadOnly = true;
            matchesDataGridView.RowHeadersWidth = 72;
            matchesDataGridView.RowTemplate.Height = 25;
            matchesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            matchesDataGridView.Size = new Size(1332, 615);
            matchesDataGridView.TabIndex = 3;
            matchesDataGridView.CellFormatting += ModifyCell;

            // 
            // sellTicketButton
            // 
            sellTicketButton.BackColor = Color.FromArgb(76, 175, 80);
            sellTicketButton.FlatAppearance.BorderSize = 0;
            sellTicketButton.FlatStyle = FlatStyle.Flat;
            sellTicketButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            sellTicketButton.ForeColor = Color.White;
            sellTicketButton.Location = new Point(390, 893);
            sellTicketButton.Margin = new Padding(5, 6, 5, 6);
            sellTicketButton.Name = "sellTicketButton";
            sellTicketButton.Size = new Size(257, 80);
            sellTicketButton.TabIndex = 0;
            sellTicketButton.Text = "Sell Ticket";
            sellTicketButton.UseVisualStyleBackColor = false;
            sellTicketButton.Click += SellTicketButton_Click;
            // 
            // findMatchButton
            // 
            findMatchButton.BackColor = Color.FromArgb(33, 150, 243);
            findMatchButton.FlatAppearance.BorderSize = 0;
            findMatchButton.FlatStyle = FlatStyle.Flat;
            findMatchButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            findMatchButton.ForeColor = Color.White;
            findMatchButton.Location = new Point(682, 893);
            findMatchButton.Margin = new Padding(5, 6, 5, 6);
            findMatchButton.Name = "findMatchButton";
            findMatchButton.Size = new Size(257, 80);
            findMatchButton.TabIndex = 1;
            findMatchButton.Text = "Find Match by Client";
            findMatchButton.UseVisualStyleBackColor = false;
            findMatchButton.Click += FindMatchButton_Click;
            // 
            // MainViews
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1383, 1055);
            Controls.Add(sellTicketButton);
            Controls.Add(findMatchButton);
            Controls.Add(matchesDataGridView);
            Controls.Add(matchesLabel);
            Controls.Add(topPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(5, 6, 5, 6);
            MaximizeBox = false;
            Name = "MainViews";
            StartPosition = FormStartPosition.CenterScreen;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)matchesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}