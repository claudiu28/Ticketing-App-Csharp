using Models.Models;
using Models.Models.Enums;
using System.Data;


namespace Client.Views
{
    public partial class SearchView : Form
    {
        private readonly GrpcProxy _service;
        public SearchView(GrpcProxy services)
        {
            InitializeComponent();
            resultsDataGridView.AutoGenerateColumns = false;

            var TeamsColumn = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Teams",
                DataPropertyName = "Teams",
                Name = "Teams"
            };

            var MatchTypeColumn = new DataGridViewTextBoxColumn()
            {
                HeaderText = "MatchType",
                DataPropertyName = "MatchType",
                Name = "MatchType"
            };


            var NumberOfSeats = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Number Of Seats",
                DataPropertyName = "Seats",
                Name = "Seats",
            };

            var Name = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Name",
                DataPropertyName = "Name",
                Name = "Name",
            };

            var Address = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Address",
                DataPropertyName = "AddressUser",
                Name = "Address",
            };

            resultsDataGridView.Columns.Add(Name);
            resultsDataGridView.Columns.Add(Address);
            resultsDataGridView.Columns.Add(TeamsColumn);
            resultsDataGridView.Columns.Add(MatchTypeColumn);
            resultsDataGridView.Columns.Add(NumberOfSeats);
            this._service = services;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            firstNameField.Text = "";
            lastNameField.Text = "";
            addressField.Text = "";
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    var lastName = lastNameField.Text.Trim();
                    var address = addressField.Text.Trim();
                    var firstName = firstNameField.Text.Trim();
                    List<Ticket> searchingList = [];
                    var searching = await _service.FindTickets(firstName, lastName, address);
                    foreach (var item in searching.Tickets)
                    {
                        var localMatch = new Match(
                            item.Match.TeamA,
                            item.Match.TeamB, (MatchTypes)item.Match.MatchType,
                            item.Match.PriceTicket,
                            item.Match.NumberOfSeats
                        )
                        { Id = item.Match.Id};

                        Ticket localTicket = new(localMatch, item.FirstName, item.LastName, item.Address, item.NumberOfSeats) { Id = item.Id};
                        searchingList.Add(localTicket);
                    }

                    if (searchingList != null)
                    {
                        Invoke(() => PopulateResultGridView(searchingList));
                    }
                }
                catch (Exception ex)
                {
                    Invoke(() => MessageBox.Show("Searching failed:" + ex.Message));
                }
            });
        }

        private void PopulateResultGridView(List<Ticket> searchingList)
        {

            try
            {
                var Data = searchingList.Select(t => new
                {
                    Teams = $"{t.Match.TeamA} vs {t.Match.TeamB}",
                    MatchType = t.Match.MatchType.ToString(),
                    Seats = t.NumberOfSeats.ToString(),
                    Name = $"{t.FirstName} {t.LastName}",
                    AddressUser = t.Address,

                }).ToList();

                resultsDataGridView.DataSource = Data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Populate failed: " + ex.Message);
            }
        }
    }
}
