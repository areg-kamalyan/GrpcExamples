using Grpc.Core;
using Grpc.Net.Client;

namespace GrpcServer.Services
{
    public class StreamService : MyStream.MyStreamBase
    {
        private readonly ILogger<StreamService> _logger;
        public StreamService(ILogger<StreamService> logger)
        {
            _logger = logger;
        }

        public override async Task SendData(StreamRequest request, IServerStreamWriter<StreamResponse> responseStream, ServerCallContext context)
        {

            using var channel = GrpcChannel.ForAddress("https://localhost:7127");

            var client1 = new GrpcInerServer.InnerStream.InnerStreamClient(channel);

            using var call = client1.SendData(new GrpcInerServer.StreamRequest { Message = request.Message });


            await responseStream.WriteAsync(new StreamResponse { Result = "Start Stream" });

            while (await call.ResponseStream.MoveNext(new CancellationToken()))
            {
                var result = call.ResponseStream.Current.Result;
                await responseStream.WriteAsync(new StreamResponse { Result = result });
            }

            await responseStream.WriteAsync(new StreamResponse { Result = "End Stream" });

        }

    }
}
