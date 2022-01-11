using common;

namespace hotfix
{
    public class HotFix
    {
        public void emp() { }
        public long TestPluginInnerCall(I1 ii)
        {
            var random = new Random();
            var a = System.DateTime.Now.Ticks * 100;
            int v = 0;
            for (int i = 0; i < 100000000; i++)
            {
                v += 1;
                emp();
            }
            var b = System.DateTime.Now.Ticks * 100;

            return b - a;
        }
        public int TestPluginOutterCall(I1 ii)
        {
            return 1;
        }
    }
}