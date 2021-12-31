using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace csharp_async_cancel
{
    internal class Program
    {
        static readonly CancellationTokenSource s_cts = new CancellationTokenSource();

        static readonly HttpClient s_client = new HttpClient
        {
            MaxResponseContentBufferSize = 1_000_000
        };

        static readonly IEnumerable<string> s_urlList = new string[]
        {
            "https://docs.microsoft.com",
            "https://docs.microsoft.com/aspnet/core",
            "https://docs.microsoft.com/azure",
            "https://docs.microsoft.com/azure/devops",
            "https://docs.microsoft.com/dotnet",
            "https://docs.microsoft.com/dynamics365",
            "https://docs.microsoft.com/education",
            "https://docs.microsoft.com/enterprise-mobility-security",
            "https://docs.microsoft.com/gaming",
            "https://docs.microsoft.com/graph",
            "https://docs.microsoft.com/microsoft-365",
            "https://docs.microsoft.com/office",
            "https://docs.microsoft.com/powershell",
            "https://docs.microsoft.com/sql",
            "https://docs.microsoft.com/surface",
            "https://docs.microsoft.com/system-center",
            "https://docs.microsoft.com/visualstudio",
            "https://docs.microsoft.com/windows",
            "https://docs.microsoft.com/xamarin"
        };

        static async Task Main()
        {
            Console.WriteLine("Application started.");
            Console.WriteLine("Press the ENTER key to cancel...\n");

            Task cancelTask = Task.Run(() =>
            {
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    Console.WriteLine("Press the ENTER key to cancel...");
                }

                Console.WriteLine("\nENTER key pressed: cancelling downloads.\n");
                s_cts.Cancel();
            });

            Task sumPageSizesTask = SumPageSizesAsync();
            Task cancelTask2 = CancellableLoop(s_cts.Token);

            await Task.WhenAll(new[] { cancelTask, sumPageSizesTask, cancelTask2 });

            Console.WriteLine("Application ending.");
        }

        static async Task CancellableLoop(CancellationToken cToken)
        {
            try
            {
                Task loop = Task.Run(async () =>
                {
                    uint count = 0;
                    while (true)
                    {
                        cToken.ThrowIfCancellationRequested(); 
                        await Task.Delay(1000);
                        Console.WriteLine($"Waited {++count}s");
                    }
                }, cToken);
                await loop;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CancellableLoop]: {ex.Message}");
            }
        }

        static async Task SumPageSizesAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            int total = 0;

            try
            {
                foreach (string url in s_urlList)
                {
                    int contentLength = await ProcessUrlAsync(url, s_client, s_cts.Token);
                    total += contentLength;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SumPageSizesAsync]: {ex.Message}");
            }

            stopwatch.Stop();

            Console.WriteLine($"\nTotal bytes returned:  {total:#,#}");
            Console.WriteLine($"Elapsed time:          {stopwatch.Elapsed}\n");
        }

        static async Task<int> ProcessUrlAsync(string url, HttpClient client, CancellationToken token)
        {
            HttpResponseMessage response = await client.GetAsync(url, token);
            byte[] content = await response.Content.ReadAsByteArrayAsync(token);
            Console.WriteLine($"{url,-60} {content.Length,10:#,#}");

            return content.Length;
        }
    }
}
