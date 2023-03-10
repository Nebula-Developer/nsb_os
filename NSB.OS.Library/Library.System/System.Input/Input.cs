
using NSB.OS.Logic.Threads;

namespace NSB.OS.SystemNS.InputNS;

public static class Input {
    public static ConsoleKeyInfo Key { get; set; }
    public static bool KeyAvailable { get; set; }

    public static List<Action> KeyActions = new List<Action>();

    public static void AddKeyAction(Action action) => KeyActions.Add(action);
    public static void RemoveKeyAction(Action action) => KeyActions.Remove(action);

    public static void InputThread() {
        while (true) {
            ConsoleKeyInfo key = Console.ReadKey(true);
            Key = key;
            KeyAvailable = true;
            foreach (Action action in KeyActions) action();
        }
    }

    public static void Start() {
        ThreadManager.ThreadCall(InputThread).Then(() => {
            return;
        });
    }
}