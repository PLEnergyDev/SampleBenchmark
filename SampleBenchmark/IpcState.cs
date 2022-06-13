using SocketComm;

namespace SampleBenchmark
{
    class IpcState
    {
        public FPipe Pipe { get; set; }

        public IpcState(FPipe pipe)
        {
            Pipe = pipe;
        }
    }
}