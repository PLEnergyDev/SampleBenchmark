using CsharpRAPL.Benchmarking;
using System.Reflection;
using SocketComm;

namespace SampleBenchmark
{
    class IpcBenchmarkLifecycle : IBenchmarkLifecycle<IpcState>
    {
        public MethodInfo BenchmarkedMethod { get; }
        public BenchmarkInfo BenchmarkInfo { get; }

        public IpcBenchmarkLifecycle(BenchmarkInfo benchmarkInfo, MethodInfo benchmarkedMethod)
        {
            BenchmarkInfo = benchmarkInfo;
            BenchmarkedMethod = benchmarkedMethod;
        }

        public IpcState Initialize(IBenchmark benchmark)
        {
            var pipe = new FPipe(BenchmarkedMethod.Name);
            pipe.ExpectReady();
            return new IpcState(pipe);            
        }

        public IpcState PostRun(IpcState oldstate)
        {
            oldstate.Pipe.SendReady();
            return oldstate;
        }

        public IpcState PreRun(IpcState oldstate)
        {

            oldstate.Pipe.ExpectReady();
            return oldstate;
        }

        public object Run(IpcState state)
        {
            state.Pipe.SendGo();
            state.Pipe.ExpectDone();
            return state;
        }

        public IpcState WarmupIteration(IpcState oldstate)
        {
            var p = oldstate.Pipe;
            p.ExpectReady();
            p.SendGo();
            p.ExpectDone();
            p.SendReady();
            return oldstate;
        }
    }
}