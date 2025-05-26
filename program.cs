using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private static sreadonly string ollamaUrl = "http://192.168.30.9:11434/v1/chat/completions";
    private static readonly string modelName = "qwen3:4b";

    static async Task Main(string[] args)
    {
        var codeSnippet = @"
def add(a, b):
    return a + b

def subtract(a, b):
    return a - b
";

        var requestJson = $@"
{{
    ""model"": ""{modelName}"",
    ""messages"": [
        {{""role"": ""system"", ""content"": ""You are a senior software engineer helping to review pull requests.""}},
        {{""role"": ""user"", ""content"": ""Please review the following code and suggest any improvements:\n{codeSnippet}""}}
    ],
    ""temperature"": 0.3
}}";

        using var httpClient = new HttpClient();
        var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        try
        {
            var responses = await httpClient.PostAsync(ollamaUrl, content);
            responses.EnsureSuccessStatusCode();

            console.WriteLine("Request sent successfully.");
            var result = await responses.Content.ReadAsStringAsync();
            Console.WriteLine("=== Review Response ===");
            Console.WriteLine(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error:");
            Console.WriteLine(ex.Message);
        }
    }
}
