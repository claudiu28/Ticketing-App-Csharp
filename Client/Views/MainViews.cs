using NLog;
using Client.Events;
using Models.Models;
using Models.Models.Enums;

namespace Client.Views
{
    public partial class MainViews : Form
    {
        private readonly User user;
        private readonly GrpcProxy _service;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ClientObserver _observer;
        private List<Match> currentMatches = [];
        private readonly object _matchLock = new();

        public MainViews(User User, GrpcProxy services, ClientObserver observer)
        {
            ArgumentNullException.ThrowIfNull(User);

            InitializeComponent();
            this.user = User;
            this._service = services;
            this._observer = (ClientObserver)observer;

            _observer.MatchUpdated += OnMatchUpdate;
            this.FormClosed += (s, e) => observer.MatchUpdated -= OnMatchUpdate;
            this.FormClosing += MainViews_FormClosing;

            matchesDataGridView.AutoGenerateColumns = false;

            Logger.Info("Setting table columns");

            var IdColumn = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Id",
                DataPropertyName = "Id",
                Name = "Id",
                Visible = false
            };

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
                HeaderText = "Seats",
                DataPropertyName = "Seats",
                Name = "Seats",
            };

            var Price = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Price",
                DataPropertyName = "Price",
                Name = "Price",
            };

            matchesDataGridView.Columns.Add(IdColumn);
            matchesDataGridView.Columns.Add(TeamsColumn);
            matchesDataGridView.Columns.Add(MatchTypeColumn);
            matchesDataGridView.Columns.Add(Price);
            matchesDataGridView.Columns.Add(NumberOfSeats);

            PopulateGridView();
        }

        private void MainViews_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                Task.Run(async () =>
                {
                    try
                    {
                        await _service.LogOut(user);

                        Invoke(() =>
                        {
                            this.Hide();
                            _observer.MatchUpdated -= OnMatchUpdate;

                            this.Dispose();
                        });
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"Error during logout when closing: {ex.Message}");
                        Invoke(() =>
                        {
                            MessageBox.Show($"Error during logout: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                            this.Hide();

                            _observer.MatchUpdated -= OnMatchUpdate;

                            this.Dispose();
                        });
                    }
                });
            }
        }

        private void OnMatchUpdate(object? sender, MatchEvent e)
        {

            if (InvokeRequired)
            {
                Invoke(() => PopulateGridView());
            }
            else
            {
                PopulateGridView();
            }
        }

        private void ModifyCell(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (matchesDataGridView.Columns[e.ColumnIndex].Name == "Seats" && e.Value != null)
                {
                    if (e.Value.ToString() == "SOLD OUT")
                    {
                        if (e.CellStyle != null)
                        {
                            e.CellStyle.ForeColor = Color.Red;
                            e.CellStyle.Font = new Font(matchesDataGridView.Font, FontStyle.Bold);
                        }
                    }
                    else if (int.TryParse(e.Value.ToString(), out int numberOfSeats))
                    {
                        if (numberOfSeats <= 0)
                        {
                            e.Value = "SOLD OUT";
                            if (e.CellStyle != null)
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(matchesDataGridView.Font, FontStyle.Bold);
                            }
                        }
                        else
                        {
                            if (e.CellStyle != null)
                            {
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(matchesDataGridView.Font, FontStyle.Regular);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Logger.Error("Error in ModifyCell: {0}", err.Message);
            }
        }

        private void PopulateGridView()
        {
            try
            {
                Logger.Info("Populate table with matches");
                Task.Run(async () =>
                {
                    List<object> matchData;
                    List<Match> matches = [];

                    try
                    {
                        var m = await _service.GetAllMatches();

                        foreach (var match in m.Match)
                        {
                            matches.Add(new Match(match.TeamA, match.TeamB, (MatchTypes)match.MatchType, match.PriceTicket, match.NumberOfSeats) { Id = match.Id });
                        }

                        lock (_matchLock)
                        {
                            currentMatches = matches;
                        }

                        matchData = [.. currentMatches.Select(m => new
                        {
                            m.Id,
                            Teams = $"{m.TeamA} vs {m.TeamB}",
                            MatchType = m.MatchType.ToString(),
                            Price = m.PriceTicket,
                            Seats = m.NumberOfSeatsTotal
                        }).Cast<object>()];


                        Invoke(() =>
                        {
                            try
                            {
                                matchesDataGridView.DataSource = null;
                                matchesDataGridView.DataSource = matchData;

                                foreach (DataGridViewRow row in matchesDataGridView.Rows)
                                {
                                    if (!row.IsNewRow)
                                    {
                                        var id = row.Cells["Id"].Value;
                                        Logger.Debug("Populated row with ID: {id}");
                                    }
                                }

                                Logger.Info("DataGridView populated successfully");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error displaying matches: {ex.Message}");
                            }
                        });

                    }
                    catch (Exception ex)
                    {
                        Invoke(() => MessageBox.Show($"Error retrieving matches: {ex.Message}"));
                        return;
                    }
                });
            }
            catch (Exception err)
            {
                MessageBox.Show("Table match could not be populated correctly: " + err.Message);
            }
        }


        public void LogoutButton_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    await _service.LogOut(user);

                    Invoke(() =>
                    {
                        this.Hide();
                        var loginController = new LoginView(_service);
                        loginController.Show();
                        loginController.Text = "LogIn";
                    });
                }
                catch (Exception ex)
                {
                    Logger.Error("Error during logout: {ex.Message}");
                    Invoke(() => MessageBox.Show($"Error during logout: {ex.Message}"));
                }
            });
        }


        private void SellTicketButton_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Open Selling Ticket Controller");

                if (matchesDataGridView.SelectedRows.Count > 0)
                {
                    var selectedRow = matchesDataGridView.SelectedRows[0];
                    var matchId = Convert.ToInt64(selectedRow.Cells["Id"].Value);

                    Match? selectedMatch = null;
                    lock (_matchLock)
                    {
                        selectedMatch = currentMatches.FirstOrDefault(m => m.Id == matchId);
                    }

                    if (selectedMatch != null)
                    {
                        var sellController = new SellTicketView(selectedMatch, _service);
                        sellController.Show();
                        sellController.Text = "Selling";
                    }
                    else
                    {
                        Logger.Warn("Could not find match with ID {matchId} in currentMatches");
                        MessageBox.Show("Could not find the selected match. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a match first.");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("An error occurred trying to open Sell Ticket Service: " + err.Message);
            }
        }
        private void FindMatchButton_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Trying to open Find Section");
                var searchingController = new SearchView(_service);
                searchingController.Show();
                searchingController.Text = "Searching";
            }
            catch (Exception err)
            {
                MessageBox.Show("An error occurred trying to open Search Section Service: " + err.Message);
            }
        }

    }
}