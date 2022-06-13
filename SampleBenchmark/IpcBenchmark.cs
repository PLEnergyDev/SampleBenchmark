using CsharpRAPL.Benchmarking;
using System.Reflection;
using SocketComm;

namespace SampleBenchmark
{
    class IpcBenchmark : IBenchmarkLifecycle<IpcState>
    {
        public MethodInfo BenchmarkedMethod { get; }
        public BenchmarkInfo BenchmarkInfo { get; }

        public IpcBenchmark(BenchmarkInfo benchmarkInfo, MethodInfo benchmarkedMethod)
        {
            BenchmarkInfo = benchmarkInfo;
            BenchmarkedMethod = benchmarkedMethod;
        }

        public IpcState Initialize(IBenchmark benchmark)
        {
            var pipe = new FPipe(BenchmarkedMethod.Name);
            var cmd = pipe.ReadCmd();
            if (cmd == Cmd.Ready)
                return new IpcState(pipe);
            else throw new Exception("not ready!");
            
        }

        public IpcState PostRun(IpcState oldstate)
        {
            throw new NotImplementedException();
        }

        public IpcState PreRun(IpcState oldstate)
        {

            oldstate.Pipe.ExpectReady();
            return oldstate;
        }

        public object Run(IpcState state)
        {
            state.Pipe.Go();
            state.Pipe.ExpectDone();
            return state;
        }

        public IpcState WarmupIteration(IpcState oldstate)
        {
            
            return oldstate;
        }
    }
}