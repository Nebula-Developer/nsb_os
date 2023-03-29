using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;

namespace NSB.OS.Logic.Threads;

public static class ThreadManager {
    public static ThreadCall ThreadCall(Action threadFunc) {
        ThreadCall tc = new(threadFunc, () =>
{
}, (e) =>
{
});
        return tc;
    }
}

public class ThreadCall {
    public ThreadCall(Action threadFunc, Action thenFunc, Action<Exception> catchFunc) {
        ThreadFunc = threadFunc;
        ThenFunc = thenFunc;
        CatchFunc = catchFunc;
        this.Start();
    }

    public Action ThreadFunc { get; set; }
    public Action ThenFunc { get; set; }
    public Action<Exception> CatchFunc { get; set; }

    public ThreadCall Then(Action thenFunc) {
        ThenFunc = thenFunc;
        return this;
    }

    public ThreadCall Catch(Action<Exception> catchFunc) {
        CatchFunc = catchFunc;
        return this;
    }

    private void Start() {
        Thread t = new(() =>
{
    try
    {
        ThreadFunc();
        ThenFunc();
    }
    catch (Exception e)
    {
        CatchFunc(e);
    }
});
        t.Start();
    }
}
