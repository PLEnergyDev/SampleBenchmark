using SocketComm;

namespace SampleBenchmark
{
    public static class PipeExt
    {
        public static void SendGo(this FPipe p) => p.WriteCmd(Cmd.Go);
        public static void SendReady(this FPipe p) => p.WriteCmd(Cmd.Ready);
        //public static void OK(this FPipe p) => p.WriteCmd(Cmd.Ok);
        private static Cmd Expect(FPipe p, Cmd ecmd)
        {
            var cmd = p.ReadCmd();
            if (cmd == ecmd)
                return ecmd;
            throw new PipeCmdException($"Expected: {ecmd} - received: {cmd}");
        }
        public static Cmd ExpectDone(this FPipe p) => Expect(p, Cmd.Done);
        public static Cmd ExpectReady(this FPipe p) => Expect(p, Cmd.Done);
        //public static void Go(this FPipe p) => p.WriteCmd(Cmd.Go);
        //public static void Go(this FPipe p) => p.WriteCmd(Cmd.Go);
    }
}