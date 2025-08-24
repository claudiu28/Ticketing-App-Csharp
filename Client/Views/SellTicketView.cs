using Client;
using Models.Models;


namespace Client.Views
{
    public partial class SellTicketView : Form
    {
        private readonly GrpcProxy _services;
       
        private readonly Match match;
        public SellTicketView(Match match, GrpcProxy services)
        {
            InitializeComponent();
            this.match = match;
            this._services = services;
            InitMatch();
        }

        private void InitMatch()
        {
            matchTeamsLabel.Text = $"{match.TeamA} vs {match.TeamB}";
            matchTypeLabel.Text = $"{match.MatchType}";
            matchPriceLabel.Text = $"{match.PriceTicket}";
            matchSeatsLabel.Text = $"{match.NumberOfSeatsTotal}";
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void SellButton_Click(object sender, EventArgs e)
        {

                try
                {
                    var firstName = firstNameField.Text.Trim();
                    var lastName = lastNameField.Text.Trim();
                    var address = addressField.Text.Trim();
                    if (firstName == "" && lastName == "" && address == "")
                    {
                        throw new Exception("Ticket could have a name and address specified!");
                    }
                    var numberOfSeats = (int)seatsNumericUpDown.Value;
                    if (numberOfSeats <= 0)
                    {
                        throw new Exception("Number of seats must be greater than 0.");
                    }
                    Ticket ticket = new(match, firstName, lastName, address, numberOfSeats);
                    try
                    {
                        await _services.SellTicket(ticket);
                        Invoke(() =>
                        {
                            MessageBox.Show("Ticket sold successfully!");
                        });
                    }
                    catch (Exception ex)
                    {
                        Invoke(() =>
                        {
                            MessageBox.Show("Ticket sold failed: " + ex.Message);
                        });
                    }
                }
                catch (Exception ex)
                {
                    Invoke(() => { MessageBox.Show("Saved ticcket failed" + ex.Message); });
                }
            
        }
    }
}

