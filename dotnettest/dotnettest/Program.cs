
using common;
using System.Reflection;



public delegate int Del<T>(T a) where T : I;
public delegate long DelLong<T>(T a) where T : I;
namespace cc
{
    class A
    {
        static async public Task Main()
        {
            var a = new A();
            Console.Out.WriteLine($"local call {a.TestLocalCall(new I1())}");
            Console.Out.WriteLine("-------------");
            Console.Out.WriteLine($"local delegate call {a.TestLocalDelegateCall(new I1())}");
            Console.Out.WriteLine("-------------");
            Console.Out.WriteLine($"plugin inner delegate call {a.TestPluginInerCall(new I1())}");
            Console.Out.WriteLine("-------------");
            Console.Out.WriteLine($"plugin outer delegate call {a.TestPluginOuterCall(new I1())}");
            Console.Out.WriteLine("-------------");
            Console.Out.WriteLine($"plugin outer interface call {a.TestPluginInterfaceCall(new I1())}");
            Console.Out.WriteLine("-------------");
            Console.Out.WriteLine($"plugin outer interface task call {await a.TestPluginInterfaceTaskCall(new I1())}");

            var t1 = DateTime.Now.Ticks * 100;
            var arr = new Task[32];
            for (var i = 0; i < 32; i++)
            {
                arr[i] = Task.Factory.StartNew(() => a.TestPluginOuterCall(new I1()));
            }
            Task.WaitAll(arr);
            var t2 = DateTime.Now.Ticks * 100;
            Console.Out.WriteLine("-------------");
            Console.Out.WriteLine($"multi plugin outer call {t2 - t1}");
        }


        public int f(I1 ii) { return 1; }
        long TestLocalCall(I1 ii)
        {
            var a = DateTime.Now.Ticks * 100;
            var v = 0;
            for (var i = 0; i < 100000000; i++)
            {
                v += f(ii);
            }
            var b = DateTime.Now.Ticks * 100;
            return b - a;
        }

        long TestPluginInerCall(I1 ii)
        {
            var asm = Assembly.LoadFile("/home/lin/hotfixperformance/dotnettest/hotfix/bin/Release/net6.0/hotfix.dll");
            var type = asm.GetType("hotfix.HotFix");
            var methed = type.GetMethod("TestPluginInnerCall");
            var ins = Activator.CreateInstance(type);
            var a = DateTime.Now.Ticks * 100;
            var func1 = (DelLong<I1>)methed.CreateDelegate(typeof(DelLong<I1>), ins);
            if (func1 == null) throw new Exception("err");
            var r = func1(ii);
            var b = DateTime.Now.Ticks * 100;
            return b - a;
        }

        long TestPluginOuterCall(I1 ii)
        {
            var asm = Assembly.LoadFile("/home/lin/hotfixperformance/dotnettest/hotfix/bin/Release/net6.0/hotfix.dll");
            var type = asm.GetType("hotfix.HotFix");
            var methed = type.GetMethod("TestPluginOutterCall");
            var ins = Activator.CreateInstance(type);
            if (ins == null) throw new Exception("err");
            var func1 = (Del<I1>)methed.CreateDelegate(typeof(Del<I1>), ins);
            if (func1 == null) throw new Exception("err");
            var a = DateTime.Now.Ticks * 100;
            var v = 0;
            for (var i = 0; i < 100000000; i++)
            {
                v += func1(ii);
            }
            var b = DateTime.Now.Ticks * 100;
            return b - a;
        }

        long TestPluginInterfaceCall(I1 ii)
        {
            var asm = Assembly.LoadFile("/home/lin/hotfixperformance/dotnettest/hotfix/bin/Release/net6.0/hotfix.dll");
            var type = asm.GetType("hotfix.HotFix");
            var ins = Activator.CreateInstance(type) as TestRpc;
            if (ins == null) throw new Exception("err");
            var a = DateTime.Now.Ticks * 100;
            var v = 0;
            for (var i = 0; i < 100000000; i++)
            {
                v += ins.TestHandler(ii);
            }
            var b = DateTime.Now.Ticks * 100;
            return b - a;
        }


        async Task<long> TestPluginInterfaceTaskCall(I1 ii)
        {
            var asm = Assembly.LoadFile("/home/lin/hotfixperformance/dotnettest/hotfix/bin/Release/net6.0/hotfix.dll");
            var type = asm.GetType("hotfix.HotFix");
            var ins = Activator.CreateInstance(type) as TestRpc;
            if (ins == null) throw new Exception("err");
            var a = DateTime.Now.Ticks * 100;
            var v = 0;
            for (var i = 0; i < 100000000; i++)
            {
                v += await ins.TestTaskHandler(ii);
            }
            var b = DateTime.Now.Ticks * 100;
            return b - a;
        }


        public int testFun1(I1 a)
        {
            return 1;
        }

        long TestLocalDelegateCall(I1 ii)
        {
            var type = this.GetType();
            var methed1 = type.GetMethod("testFun1");
            var ins = Activator.CreateInstance(type);
            if (ins == null) throw new Exception("err");
            var func1 = (Del<I1>)methed1.CreateDelegate(typeof(Del<I1>), ins);
            if (func1 == null) throw new Exception("err");
            var a = DateTime.Now.Ticks * 100;
            var v = 0;
            for (var i = 0; i < 100000000; i++)
            {
                v += func1(ii);
            }
            var b = DateTime.Now.Ticks * 100;
            return b - a;
        }
    }
}
