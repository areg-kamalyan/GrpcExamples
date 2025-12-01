using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            await RungRPC();


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static async Task RungRPC()
        {
            // Set up the gRPC channel to connect to the server
            using var channel = GrpcChannel.ForAddress("https://localhost:7217");


            var client1 = new MyStream.MyStreamClient(channel);

            using var call = client1.SendData(new StreamRequest { Message = "test" });

            while (await call.ResponseStream.MoveNext(new CancellationToken()))
            {
                var result = call.ResponseStream.Current.Result;
                Console.WriteLine($"Callback received: {result}");
            }
        }
    }
}