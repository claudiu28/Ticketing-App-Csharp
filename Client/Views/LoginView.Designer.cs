namespace Client.Views
{
    partial class LoginView
    {
        private System.ComponentModel.IContainer components = null;

        private Label titleLabel;
        private Label UsernameLabel;
        private TextBox UsernameInput;
        private FlowLayoutPanel usernamePanel;
        private Label PasswordLabel;
        private TextBox PasswordInput;
        private FlowLayoutPanel passwordPanel;
        private Button LogInButton;
        private FlowLayoutPanel container;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            titleLabel = new Label();
            UsernameLabel = new Label();
            UsernameInput = new TextBox();
            PasswordLabel = new Label();
            PasswordInput = new TextBox();
            LogInButton = new Button();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.BackColor = Color.FromArgb(76, 175, 80);
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(0, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(991, 80);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Autentificare";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UsernameLabel
            // 
            UsernameLabel.Font = new Font("Segoe UI", 12F);
            UsernameLabel.Location = new Point(103, 240);
            UsernameLabel.Name = "UsernameLabel";
            UsernameLabel.Size = new Size(142, 45);
            UsernameLabel.TabIndex = 0;
            UsernameLabel.Text = "Utilizator:";
            UsernameLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // UsernameInput
            // 
            UsernameInput.Font = new Font("Segoe UI", 12F);
            UsernameInput.Location = new Point(263, 240);
            UsernameInput.Name = "UsernameInput";
            UsernameInput.PlaceholderText = "Input username...";
            UsernameInput.Size = new Size(672, 45);
            UsernameInput.TabIndex = 1;
            // 
            // PasswordLabel
            // 
            PasswordLabel.Font = new Font("Segoe UI", 12F);
            PasswordLabel.Location = new Point(143, 311);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(102, 42);
            PasswordLabel.TabIndex = 0;
            PasswordLabel.Text = "Parola:";
            PasswordLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // PasswordInput
            // 
            PasswordInput.Font = new Font("Segoe UI", 12F);
            PasswordInput.Location = new Point(263, 310);
            PasswordInput.Name = "PasswordInput";
            PasswordInput.PasswordChar = '*';
            PasswordInput.PlaceholderText = "Input password...";
            PasswordInput.Size = new Size(672, 45);
            PasswordInput.TabIndex = 1;
            PasswordInput.UseSystemPasswordChar = true;
            // 
            // LogInButton
            // 
            LogInButton.BackColor = Color.FromArgb(76, 175, 80);
            LogInButton.FlatStyle = FlatStyle.Flat;
            LogInButton.Font = new Font("Segoe UI", 12F);
            LogInButton.ForeColor = Color.White;
            LogInButton.Location = new Point(387, 465);
            LogInButton.Margin = new Padding(0, 20, 0, 0);
            LogInButton.Name = "LogInButton";
            LogInButton.Size = new Size(231, 80);
            LogInButton.TabIndex = 2;
            LogInButton.Text = "Autentificare";
            LogInButton.UseVisualStyleBackColor = false;
            LogInButton.Click += LogInButton_Click;
            // 
            // LoginController
            // 
            BackColor = Color.White;
            ClientSize = new Size(991, 589);
            Controls.Add(PasswordLabel);
            Controls.Add(UsernameInput);
            Controls.Add(UsernameLabel);
            Controls.Add(titleLabel);
            Controls.Add(LogInButton);
            Controls.Add(PasswordInput);
            Name = "LoginController";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
