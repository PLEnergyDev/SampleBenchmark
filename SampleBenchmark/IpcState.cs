using SocketComm;

namespace SampleBenchmark
{
    public class IpcState
    {
        public FPipe Pipe { get; set; }

        public IpcState(FPipe pipe)
        {
            Pipe = pipe;
        }
    }
}