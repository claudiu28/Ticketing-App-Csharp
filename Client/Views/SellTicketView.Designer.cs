using System.Windows.Forms;
using System.Drawing;

namespace Client.Views
{
    partial class SellTicketView
    {
        private System.ComponentModel.IContainer components = null;

        // Panoul de sus:
        private Panel topPanel;
        private Label titleLabel;

        // GroupBox pt. Info Match
        private GroupBox matchInfoGroupBox;
        private Label matchTeamsLabelText;
        private Label matchTeamsLabel;
        private Label matchTypeLabelText;
        private Label matchTypeLabel;
        private Label matchPriceLabelText;
        private Label matchPriceLabel;
        private Label matchSeatsLabelText;
        private Label matchSeatsLabel;

        // GroupBox pt. Info Client
        private GroupBox clientInfoGroupBox;
        private Label firstNameLabel;
        private TextBox firstNameField;
        private Label lastNameLabel;
        private TextBox lastNameField;
        private Label addressLabel;
        private TextBox addressField;
        private Label numberOfSeatsLabel;
        private NumericUpDown seatsNumericUpDown;
        private Label maxAvailableLabel;
        private Button sellButton;
        private Button cancelButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            topPanel = new Panel();
            titleLabel = new Label();
            matchInfoGroupBox = new GroupBox();
            matchTeamsLabelText = new Label();
            matchTeamsLabel = new Label();
            matchTypeLabelText = new Label();
            matchTypeLabel = new Label();
            matchPriceLabelText = new Label();
            matchPriceLabel = new Label();
            matchSeatsLabelText = new Label();
            matchSeatsLabel = new Label();
            clientInfoGroupBox = new GroupBox();
            firstNameLabel = new Label();
            firstNameField = new TextBox();
            lastNameLabel = new Label();
            lastNameField = new TextBox();
            addressLabel = new Label();
            addressField = new TextBox();
            numberOfSeatsLabel = new Label();
            seatsNumericUpDown = new NumericUpDown();
            maxAvailableLabel = new Label();
            sellButton = new Button();
            cancelButton = new Button();
            topPanel.SuspendLayout();
            matchInfoGroupBox.SuspendLayout();
            clientInfoGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)seatsNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.FromArgb(76, 175, 80);
            topPanel.Controls.Add(titleLabel);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Margin = new Padding(4);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(814, 90);
            topPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.Anchor = AnchorStyles.Top;
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(308, 18);
            titleLabel.Margin = new Padding(4, 0, 4, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(203, 51);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Sell Ticket";
            // 
            // matchInfoGroupBox
            // 
            matchInfoGroupBox.Controls.Add(matchTeamsLabelText);
            matchInfoGroupBox.Controls.Add(matchTeamsLabel);
            matchInfoGroupBox.Controls.Add(matchTypeLabelText);
            matchInfoGroupBox.Controls.Add(matchTypeLabel);
            matchInfoGroupBox.Controls.Add(matchPriceLabelText);
            matchInfoGroupBox.Controls.Add(matchPriceLabel);
            matchInfoGroupBox.Controls.Add(matchSeatsLabelText);
            matchInfoGroupBox.Controls.Add(matchSeatsLabel);
            matchInfoGroupBox.Location = new Point(27, 121);
            matchInfoGroupBox.Margin = new Padding(4);
            matchInfoGroupBox.Name = "matchInfoGroupBox";
            matchInfoGroupBox.Padding = new Padding(4);
            matchInfoGroupBox.Size = new Size(747, 224);
            matchInfoGroupBox.TabIndex = 1;
            matchInfoGroupBox.TabStop = false;
            matchInfoGroupBox.Text = "Info Match";
            // 
            // matchTeamsLabelText
            // 
            matchTeamsLabelText.AutoSize = true;
            matchTeamsLabelText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            matchTeamsLabelText.Location = new Point(27, 45);
            matchTeamsLabelText.Margin = new Padding(4, 0, 4, 0);
            matchTeamsLabelText.Name = "matchTeamsLabelText";
            matchTeamsLabelText.Size = new Size(81, 30);
            matchTeamsLabelText.TabIndex = 0;
            matchTeamsLabelText.Text = "Match:";
            // 
            // matchTeamsLabel
            // 
            matchTeamsLabel.AutoSize = true;
            matchTeamsLabel.Location = new Point(173, 45);
            matchTeamsLabel.Margin = new Padding(4, 0, 4, 0);
            matchTeamsLabel.Name = "matchTeamsLabel";
            matchTeamsLabel.Size = new Size(150, 30);
            matchTeamsLabel.TabIndex = 1;
            matchTeamsLabel.Text = "Home vs Away";
            // 
            // matchTypeLabelText
            // 
            matchTypeLabelText.AutoSize = true;
            matchTypeLabelText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            matchTypeLabelText.Location = new Point(27, 90);
            matchTypeLabelText.Margin = new Padding(4, 0, 4, 0);
            matchTypeLabelText.Name = "matchTypeLabelText";
            matchTypeLabelText.Size = new Size(136, 30);
            matchTypeLabelText.TabIndex = 2;
            matchTypeLabelText.Text = "MatchTypes:";
            // 
            // matchTypeLabel
            // 
            matchTypeLabel.AutoSize = true;
            matchTypeLabel.Location = new Point(173, 90);
            matchTypeLabel.Margin = new Padding(4, 0, 4, 0);
            matchTypeLabel.Name = "matchTypeLabel";
            matchTypeLabel.Size = new Size(115, 30);
            matchTypeLabel.TabIndex = 3;
            matchTypeLabel.Text = "TypeMatch";
            // 
            // matchPriceLabelText
            // 
            matchPriceLabelText.AutoSize = true;
            matchPriceLabelText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            matchPriceLabelText.Location = new Point(27, 135);
            matchPriceLabelText.Margin = new Padding(4, 0, 4, 0);
            matchPriceLabelText.Name = "matchPriceLabelText";
            matchPriceLabelText.Size = new Size(67, 30);
            matchPriceLabelText.TabIndex = 4;
            matchPriceLabelText.Text = "Price:";
            // 
            // matchPriceLabel
            // 
            matchPriceLabel.AutoSize = true;
            matchPriceLabel.Location = new Point(173, 135);
            matchPriceLabel.Margin = new Padding(4, 0, 4, 0);
            matchPriceLabel.Name = "matchPriceLabel";
            matchPriceLabel.Size = new Size(58, 30);
            matchPriceLabel.TabIndex = 5;
            matchPriceLabel.Text = "Price";
            // 
            // matchSeatsLabelText
            // 
            matchSeatsLabelText.AutoSize = true;
            matchSeatsLabelText.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            matchSeatsLabelText.Location = new Point(27, 180);
            matchSeatsLabelText.Margin = new Padding(4, 0, 4, 0);
            matchSeatsLabelText.Name = "matchSeatsLabelText";
            matchSeatsLabelText.Size = new Size(70, 30);
            matchSeatsLabelText.TabIndex = 6;
            matchSeatsLabelText.Text = "Seats:";
            // 
            // matchSeatsLabel
            // 
            matchSeatsLabel.AutoSize = true;
            matchSeatsLabel.Location = new Point(173, 180);
            matchSeatsLabel.Margin = new Padding(4, 0, 4, 0);
            matchSeatsLabel.Name = "matchSeatsLabel";
            matchSeatsLabel.Size = new Size(152, 30);
            matchSeatsLabel.TabIndex = 7;
            matchSeatsLabel.Text = "Seats Available";
            // 
            // clientInfoGroupBox
            // 
            clientInfoGroupBox.Controls.Add(firstNameLabel);
            clientInfoGroupBox.Controls.Add(firstNameField);
            clientInfoGroupBox.Controls.Add(lastNameLabel);
            clientInfoGroupBox.Controls.Add(lastNameField);
            clientInfoGroupBox.Controls.Add(addressLabel);
            clientInfoGroupBox.Controls.Add(addressField);
            clientInfoGroupBox.Controls.Add(numberOfSeatsLabel);
            clientInfoGroupBox.Controls.Add(seatsNumericUpDown);
            clientInfoGroupBox.Controls.Add(maxAvailableLabel);
            clientInfoGroupBox.Location = new Point(27, 371);
            clientInfoGroupBox.Margin = new Padding(4);
            clientInfoGroupBox.Name = "clientInfoGroupBox";
            clientInfoGroupBox.Padding = new Padding(4);
            clientInfoGroupBox.Size = new Size(747, 270);
            clientInfoGroupBox.TabIndex = 2;
            clientInfoGroupBox.TabStop = false;
            clientInfoGroupBox.Text = "Info Client";
            // 
            // firstNameLabel
            // 
            firstNameLabel.AutoSize = true;
            firstNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            firstNameLabel.Location = new Point(27, 45);
            firstNameLabel.Margin = new Padding(4, 0, 4, 0);
            firstNameLabel.Name = "firstNameLabel";
            firstNameLabel.Size = new Size(125, 30);
            firstNameLabel.TabIndex = 0;
            firstNameLabel.Text = "First Name:";
            // 
            // firstNameField
            // 
            firstNameField.Location = new Point(173, 40);
            firstNameField.Margin = new Padding(4);
            firstNameField.Name = "firstNameField";
            firstNameField.PlaceholderText = "Client first name...";
            firstNameField.Size = new Size(332, 35);
            firstNameField.TabIndex = 1;
            // 
            // lastNameLabel
            // 
            lastNameLabel.AutoSize = true;
            lastNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lastNameLabel.Location = new Point(27, 90);
            lastNameLabel.Margin = new Padding(4, 0, 4, 0);
            lastNameLabel.Name = "lastNameLabel";
            lastNameLabel.Size = new Size(122, 30);
            lastNameLabel.TabIndex = 2;
            lastNameLabel.Text = "Last Name:";
            // 
            // lastNameField
            // 
            lastNameField.Location = new Point(173, 86);
            lastNameField.Margin = new Padding(4);
            lastNameField.Name = "lastNameField";
            lastNameField.PlaceholderText = "Client last name...";
            lastNameField.Size = new Size(332, 35);
            lastNameField.TabIndex = 3;
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            addressLabel.Location = new Point(27, 135);
            addressLabel.Margin = new Padding(4, 0, 4, 0);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new Size(97, 30);
            addressLabel.TabIndex = 4;
            addressLabel.Text = "Address:";
            // 
            // addressField
            // 
            addressField.Location = new Point(173, 130);
            addressField.Margin = new Padding(4);
            addressField.Name = "addressField";
            addressField.PlaceholderText = "Client address...";
            addressField.Size = new Size(332, 35);
            addressField.TabIndex = 5;
            // 
            // numberOfSeatsLabel
            // 
            numberOfSeatsLabel.AutoSize = true;
            numberOfSeatsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            numberOfSeatsLabel.Location = new Point(27, 180);
            numberOfSeatsLabel.Margin = new Padding(4, 0, 4, 0);
            numberOfSeatsLabel.Name = "numberOfSeatsLabel";
            numberOfSeatsLabel.Size = new Size(181, 30);
            numberOfSeatsLabel.TabIndex = 6;
            numberOfSeatsLabel.Text = "Number of seats:";
            // 
            // seatsNumericUpDown
            // 
            seatsNumericUpDown.Location = new Point(218, 180);
            seatsNumericUpDown.Margin = new Padding(4);
            seatsNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            seatsNumericUpDown.Name = "seatsNumericUpDown";
            seatsNumericUpDown.Size = new Size(288, 35);
            seatsNumericUpDown.TabIndex = 7;
            seatsNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // maxAvailableLabel
            // 
            maxAvailableLabel.AutoSize = true;
            maxAvailableLabel.Location = new Point(514, 185);
            maxAvailableLabel.Margin = new Padding(4, 0, 4, 0);
            maxAvailableLabel.Name = "maxAvailableLabel";
            maxAvailableLabel.Size = new Size(156, 30);
            maxAvailableLabel.TabIndex = 8;
            maxAvailableLabel.Text = "(max. available)";
            // 
            // sellButton
            // 
            sellButton.BackColor = Color.FromArgb(76, 175, 80);
            sellButton.FlatStyle = FlatStyle.Flat;
            sellButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            sellButton.ForeColor = Color.White;
            sellButton.Location = new Point(200, 670);
            sellButton.Margin = new Padding(4);
            sellButton.Name = "sellButton";
            sellButton.Size = new Size(214, 67);
            sellButton.TabIndex = 0;
            sellButton.Text = "Sell Ticket";
            sellButton.UseVisualStyleBackColor = false;
            sellButton.Click += SellButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.BackColor = Color.Red;
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            cancelButton.ForeColor = Color.White;
            cancelButton.Location = new Point(426, 670);
            cancelButton.Margin = new Padding(4);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(214, 67);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Click += CancelButton_Click;
            // 
            // SellTicketController
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(814, 750);
            Controls.Add(sellButton);
            Controls.Add(topPanel);
            Controls.Add(cancelButton);
            Controls.Add(matchInfoGroupBox);
            Controls.Add(clientInfoGroupBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "SellTicketView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sell Ticket";
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            matchInfoGroupBox.ResumeLayout(false);
            matchInfoGroupBox.PerformLayout();
            clientInfoGroupBox.ResumeLayout(false);
            clientInfoGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)seatsNumericUpDown).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
