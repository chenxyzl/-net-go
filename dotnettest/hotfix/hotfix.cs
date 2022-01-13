using common;

namespace hotfix
{
    public class HotFix : cc.TestRpc
    {

        public int f(I1 ii) { return 1; }
        public long TestPluginInnerCall(I1 ii)
        {
            var random = new Random();
            var a = System.DateTime.Now.Ticks * 100;
            int v = 0;
            for (int i = 0; i < 100000000; i++)
            {
                v += f(ii);

            }
            var b = System.DateTime.Now.Ticks * 100;

            return b - a;
        }

        public int TestPluginOutterCall(I1 ii)
        {
            return f(ii);
        }

        public int TestHandler(I1 ii)
        {
            return f(ii);
        }

        public Task<int> TestTaskHandler(I1 ii)
        {
            return Task.FromResult<int>(f(ii));
        }
    }
}