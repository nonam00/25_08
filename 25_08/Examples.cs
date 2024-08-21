using System.Net;
using System.Web;

namespace _25_08
{
    public static class Examples
    {
        private static HttpClient client = new HttpClient();

        public static async Task Weather()
        {
            Console.WriteLine("Specify your city (or press enter to get forecat for the current location)");
            string? query = Console.ReadLine();

            if (query != string.Empty)
            {
                Console.WriteLine("Specify your country if you want (Press enter to skip)");
                string? country = Console.ReadLine();

                if (country != string.Empty)
                {
                    query = $"{country}+{query}";
                }
            }

            using (var response = await client.GetAsync($"https://wttr.in/{query}?T&format=2"))
            {
                var data = await response.Content.ReadAsStringAsync();
            
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var str = HttpUtility.HtmlDecode(data);
                    Console.WriteLine(data);
                }
                else
                {
                    Console.WriteLine("Request failed");
                    Console.WriteLine(data);
                }
            }
        }

        public static async Task Random()
        {
            Console.WriteLine("Press enter to throw:");
            Console.ReadKey();

            int myScore, enemyScore;

            using (var response = await client.GetAsync("https://www.random.org/integers/?num=4&min=1&max=6&col=1&base=10&format=plain&rnd=new"))
            {
                var str = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    string[] nums = str.Split("\n");

                    myScore = Int32.Parse(nums[0]) + Int32.Parse(nums[1]);
                    enemyScore = Int32.Parse(nums[2]) + Int32.Parse(nums[3]);

                    Console.WriteLine($"Your score: {myScore}\nEnemy score: {enemyScore}\n");
                    if (myScore > enemyScore)
                    {
                        Console.WriteLine("You won");
                    }
                    else if (myScore < enemyScore)
                    {
                        Console.WriteLine("You lose");
                    }
                    else
                    {
                        Console.WriteLine("Draw");
                    }
                }
                else
                {
                    Console.WriteLine("Request failed");
                    Console.WriteLine(str);
                }
            }
        }
    }
}

