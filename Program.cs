using System.Net.Http.Headers;
namespace Alfasotf_Exercise;
class Program
{
    public static async Task Main(string[] args)
    {

        try
        {
            var currentDir = Directory.GetCurrentDirectory();
            Console.Write("Please enter the file path: ");
            string path = Console.ReadLine() ?? "";
            var file = new StreamReader(path);
            string? user;
            bool program = true;
            while (program)
            {
                while ((user = file.ReadLine()) != null)
                {
                    using (var Logfile = File.AppendText(currentDir + "/log.log"))
                    {
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri($"https://api.bitbucket.org/2.0/");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jsons"));
                            HttpResponseMessage response = client.GetAsync($"users/{user}").GetAwaiter().GetResult();
                            var stream = response.Content.ReadAsStream();
                            var reader = new StreamReader(stream);
                            var result = await reader.ReadToEndAsync();
                            await Logfile.WriteLineAsync($"{result} - Date({DateTime.UtcNow})");
                            Console.WriteLine(currentDir);
                            Console.WriteLine($"User: {user}");
                            Console.WriteLine($"Url: https://api.bitbucket.org/2.0/{user}");
                            Console.WriteLine($"Request output: \n{result}");
                            Logfile.Close();
                            Thread.Sleep(5000);
                        }

                    }
                }
                Console.WriteLine("Exiting...");
                Thread.Sleep(5000);
                program = false;

            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message.ToString()}");
        }
    }
}
