using Nana.Interop;

namespace Nana;

/// <summary>
/// Manages the Nana application lifecycle — similar to Application.Run() in WinForms/Avalonia.
/// </summary>
public static class Application
{
    /// <summary>
    /// Enters the Nana message loop. Blocks the calling thread until nw_exit() is called.
    /// Must be called from the main thread.
    /// </summary>
    public static void Run()
    {
        NativeMethods.nw_exec();
    }

    /// <summary>
    /// Exits the Nana message loop, causing Run() to return.
    /// Can be called from any thread.
    /// </summary>
    public static void Exit()
    {
        NativeMethods.nw_exit();
    }

    /// <summary>
    /// Sleeps (pumps messages) for the given number of milliseconds.
    /// </summary>
    public static void Sleep(uint milliseconds)
    {
        NativeMethods.nw_sleep(milliseconds);
    }
}
