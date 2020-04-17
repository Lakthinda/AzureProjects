using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace QueueApp
{
    class Program
    {
        private const string ConnectionString = "DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=storagelr999;AccountKey=56S+b0OmwCUixMxrnmvdmNC3az3+6Lx97pgTgH8OfWDsFosIKBU7j/edMFVSOlYeTKFwd5syGJd0qlV3+FDCfw==";
        private const string queueName = "newsqueue";
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            if (args.Length > 0)
            {
                string value = String.Join(" ", args);
                SendArticleAsync(value).Wait();
                Console.WriteLine($"Sent: {value}");
            }else{
                string value = ReceiveArticleAsync().Result;
                Console.WriteLine($"Received {value}");
            }
        }

        static async Task SendArticleAsync(string newsMessage)
        {            
            var queue = GetQueue();

            bool createdQueue = await queue.CreateIfNotExistsAsync();
            
            if(createdQueue){
                Console.WriteLine("The queue of news articles was created.");
            }

            CloudQueueMessage message= new CloudQueueMessage(newsMessage);
            await queue.AddMessageAsync(message);
        }

        static async Task<string> ReceiveArticleAsync()
        {            
            var queue = GetQueue();

            bool exists = await queue.ExistsAsync();
            
            if(exists){
                var receiveArticle = await queue.GetMessageAsync();
                if(receiveArticle != null){
                    string newsMessage = receiveArticle.AsString;
                    
                    // Delete received message
                    await queue.DeleteMessageAsync(receiveArticle);
                    return newsMessage;
                }
            }

            return "Queue Empty or no message available";
        }

        static CloudQueue GetQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            return queueClient.GetQueueReference(queueName);
        }
    }
}
