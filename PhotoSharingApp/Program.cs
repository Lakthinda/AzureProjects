using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;

namespace PhotoSharingApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            var connectionString = configuration["StorageAccountConnectionString"];

            if (!CloudStorageAccount.TryParse(connectionString, out CloudStorageAccount storageAccount))
            {
                Console.WriteLine("Unable to parse connection string");
                return;
            }

            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference("lrblob");
            bool created = await blobContainer.CreateIfNotExistsAsync();
            

            Console.WriteLine(created ? "Created the Blob container" : "Blob container already exists.");

            // Console.WriteLine("Hello World!");
        }
    }
}
