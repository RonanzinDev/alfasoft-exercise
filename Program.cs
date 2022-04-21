using System.Net.Http.Headers;
namespace Alfasotf_Exercise;
class Program
{
    public static void Main(string[] args)
    {

        try
        {
            var currentDir = Directory.GetCurrentDirectory();
            Console.Write("Please enter the file path: ");
            string path = Console.ReadLine() ?? "";
            var file = new StreamReader(path);
            string? user;
            bool a = true;
            while (a)
            {
                while ((user = file.ReadLine()) != null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri($"https://api.bitbucket.org/2.0/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jsons"));
                        HttpResponseMessage response = client.GetAsync($"users/{user}").GetAwaiter().GetResult();
                        var stream = response.Content.ReadAsStream();
                        var reader = new StreamReader(stream);
                        Console.WriteLine($"User: {user}");
                        Console.WriteLine($"Url: https://api.bitbucket.org/2.0/{user}");
                        Console.WriteLine($"Request output: \n{reader.ReadToEnd()}");
                        Thread.Sleep(5000);

                    }

                }

            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message.ToString()}");
        }
    }
}
