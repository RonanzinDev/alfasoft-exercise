using System.Net.Http.Headers;
using System.Linq;
using Newtonsoft.Json.Linq;
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
                            string id = GetId(user).Result;
                            client.BaseAddress = new Uri($"https://api.bitbucket.org/2.0/");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jsons"));
                            HttpResponseMessage response = client.GetAsync($"users/{id}").GetAwaiter().GetResult();
                            var stream = response.Content.ReadAsStream();
                            var reader = new StreamReader(stream);
                            var result = await reader.ReadToEndAsync();
                            await Logfile.WriteLineAsync($"{result} - Date({DateTime.UtcNow})");
                            Console.WriteLine($"\nUser: {user}");
                            Console.WriteLine($"\nUrl: https://api.bitbucket.org/2.0/user/{id}");
                            Console.WriteLine($"\nRESPONSE: \n{result}");
                            Console.WriteLine(id);
                            Console.WriteLine("----------------------------------------------------------------------------------------------");
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
    public static async Task<string> GetId(string user)
    {
        string id;
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri($"https://api.bitbucket.org/2.0/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jsons"));
            HttpResponseMessage response = client.GetAsync($"workspaces/{user}?fields=uuid").GetAwaiter().GetResult();
            var stream = response.Content.ReadAsStream();
            var reader = new StreamReader(stream);
            var result = await reader.ReadToEndAsync();
            JObject o = JObject.Parse(result);
            var value = (string)o.SelectToken("uuid");
            id = value ?? "";

        }
        if (id != null)
        {
            return id;
        }
        else
        {
            return "Error find the id ";
        }
    }
}
