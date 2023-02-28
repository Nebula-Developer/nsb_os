using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;

namespace NSB.OS.Logic.Threads;

public class ThreadCall {
    public Thread thread;
    public Action thenA;
    public Action catchA;

    public ThreadCall(Thread thread, Action thenA, Action catchA) {
        this.thread = thread;
        this.thenA = thenA;
        this.catchA = catchA;
    }

    public ThreadCall Then(Action a) {
        thenA = a;
        return this;
    }

    public ThreadCall Catch(Action a) {
        catchA = a;
        return this;
    }

    public ThreadCall Run() {
        try {
            ThreadManager.RunThread(new Thread(() => {
                thread.Start();
                thread.Join();
                thenA();
            }));
        } catch {
            catchA();
        }
        return this;
    }
}

public static class ThreadManager {
    public static ThreadCall ThreadCall(Action threadFunc) {
        Thread thread = new Thread(() => {
            threadFunc();
        });
        return new ThreadCall(thread, () => { }, () => { });
    }

    public static void RunThread(Thread thread) {
        thread.Start();
    }
}