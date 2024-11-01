using System.Runtime.InteropServices;
using UnityEngine;

public static class NativeMethods
{
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);
}