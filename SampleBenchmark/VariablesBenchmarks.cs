using CsharpRAPL.Benchmarking.Attributes;

namespace SampleBenchmark
{
    public class VariablesBenchmarks
    {
        [Benchmark("IPC", "Benchmark with lifecycle", typeof(IpcBenchmarkLifecycle))]
        public static IpcState Foo(IpcState s) => s;

        //[Benchmark("Variables", "Tests local variables")]
        //public static ulong LocalVariable([BenchmarkLoopiterations] ulong loopIterations)
        //{
        //    ulong localA = 0, localB = 1;
        //    for (ulong i = 0; i < loopIterations; i++)
        //    {
        //        localA += localB + i;
        //    }
        //    return localA;
        //}
    }
}