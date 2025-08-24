using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ClientRestTest
{
    public class ClientHttpTest
    {

        public static void Menu()
        {
            Console.WriteLine("1. Get all matches");
            Console.WriteLine("2. Get matches by team A");
            Console.WriteLine("3. Get matches by team B");
            Console.WriteLine("4. Get matches by ID");
            Console.WriteLine("5. Create");
            Console.WriteLine("6. Update");
            Console.WriteLine("7. Delete");
            Console.WriteLine("8. Exit");
        }

        public static async Task GetAllMatches(HttpClient client, string url)
        {

            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response from server:");
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        public static async Task GetMatchesByTeamA(HttpClient client, string url, string teamA)
        {
            Console.WriteLine($"Base URL: {url}search?teamA={teamA}");
            try
            {
                var response = await client.GetAsync(url + "search?teamA=" + teamA);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response from server:");
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        public static async Task GetMatchesByTeamB(HttpClient client, string url, string teamB)
        {
            Console.WriteLine($"Base URL: {url}search?teamB={teamB}");
            try
            {
                var response = await client.GetAsync(url + "search?teamB=" + teamB);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response from server:");
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        public static async Task GetMatchesById(HttpClient client, string url, long id)
        {
            Console.WriteLine($"Base URL: {url}{id}");
            try
            {
                var response = await client.GetAsync(url + id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response from server:");
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        public static async Task Delete(HttpClient client, string url, long id)
        {
            Console.WriteLine($"Base URL: {url}{id}");
            try
            {
                var response = await client.DeleteAsync(url + id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response from server:");
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        public static async Task Update(HttpClient client, string url, long id)
        {
            var json = """
                      {
                    "TeamA": "Man Utd",
                    "TeamB": "Man City",
                    "PriceTicket": 100.00,
                    "NumberOfSeatsTotal": 300,
                    "MatchType": "FINALS"
                }
                """;
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            Console.WriteLine($"Base URL: {url}update/{id}");
            Console.WriteLine(content);
            try
            {
                var response = await client.PutAsync(url + id, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response from server:");
                    Console.WriteLine(responseContent);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        public static async Task Create(HttpClient client, string url)
        {
            var json = """
                      {
                    "TeamA": "FCSB",
                    "TeamB": "CFR Cluj",
                    "PriceTicket": 200.00,
                    "NumberOfSeatsTotal": 500,
                    "MatchType": "GROUPS"
                }
                """;
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            Console.WriteLine($"Base URL: {url}create");
            Console.WriteLine(content);
            try
            {
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response from server:");
                    Console.WriteLine(responseContent);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
        public static async Task<string> Login(HttpClient client, string url)
        {
            var json = """
        {
            "username": "admin",
            "password": "admin"
        }
        """;

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url + "login", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Login failed: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseContent);
            return doc.RootElement.GetProperty("token").GetString()!;
        }
        public static async Task Main()
        {
            var client = new HttpClient();
            var config = new ConfigurationBuilder()
                                .SetBasePath(AppContext.BaseDirectory)
                                .AddJsonFile("Content.json", optional: false, reloadOnChange: true)
                                .Build();

            var authUrl = config.GetValue<string>("auth.url") ?? throw new Exception("Auth URL null");
            var token = await Login(client, authUrl);

            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var baseUrl = config.GetValue<string>("base.url") ?? throw new Exception("Base URL null");

            bool ok = true;
            while (ok == true)
                try
                {
                    Menu();
                    Console.WriteLine("Select an option:");
                    var option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            await GetAllMatches(client, baseUrl);
                            break;
                        case "2":
                            await GetMatchesByTeamA(client, baseUrl, "Barca");
                            break;
                        case "3":
                            await GetMatchesByTeamB(client, baseUrl, "ATM Madrid");
                            break;
                        case "4":
                            await GetMatchesById(client, baseUrl, 8);
                            break;
                        case "5":
                            await Create(client, baseUrl);
                            break;
                        case "6":
                            await Update(client, baseUrl, 8);
                            break;
                        case "7":
                            await Delete(client, baseUrl, 14);
                            break;
                        case "8":
                            return;
                        default: Console.WriteLine("Invalid option."); break;

                    }
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            client.Dispose();
        }
    }
}
