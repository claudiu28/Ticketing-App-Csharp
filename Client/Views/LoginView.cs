using Models.Models;
using NLog;

namespace Client.Views
{
    public partial class LoginView : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly GrpcProxy _services;
        public LoginView(GrpcProxy services)
        {
            InitializeComponent();
            this._services = services;
            this.Text = "LogIn";
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Take username and password...");
                var username = UsernameInput.Text.Trim();
                var password = PasswordInput.Text.Trim();

                User user = new(username, password);

                var observer = new ClientObserver();
                Task.Run(async () =>
                {
                    try
                    {
                        var response = await _services.LogIn(user);
                        if (response.Success)
                        {
                            User loggedInUser = new(response.User.Username, response.User.Password) { Id = response.User.Id };
                            _ = _services.ListenToMatchUpdates(user.Username, observer);
                            Invoke(() =>
                            {
                                RedirectToMain(loggedInUser, observer);
                            });
                        }

                    }
                    catch (Exception ex)
                    {
                        Invoke(() =>
                        {
                            MessageBox.Show("An error occurred in the process of Log In: " + ex.Message);
                        });
                    }
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur in proccess of Log In: " + ex.Message);
            }
        }

        public void RedirectToMain(User User, ClientObserver observer)
        {
            try
            {
                var mainController = new MainViews(User, this._services, observer);
                mainController.Show();
                mainController.Text = "Main ->" + User.Username;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur in proccess of Log In: " + ex.Message);
            }
        }
    }
}
