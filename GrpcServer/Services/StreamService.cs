using Grpc.Core;

namespace GrpcInerServer.Services
{
    public class StreamService : InnerStream.InnerStreamBase
    {
        private readonly ILogger<StreamService> _logger;
        public StreamService(ILogger<StreamService> logger)
        {
            _logger = logger;
        }

        public override async Task SendData(StreamRequest request, IServerStreamWriter<StreamResponse> responseStream, ServerCallContext context)
        {

            await responseStream.WriteAsync(new StreamResponse { Result = "Start InnerStream" });

            await responseStream.WriteAsync(new StreamResponse { Result = request.Message });

            await Task.Delay(1000);
            await responseStream.WriteAsync(new StreamResponse { Result = "End InnerStream" });
        }

    }
}
