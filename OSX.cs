using System;
using System.Runtime.InteropServices;

namespace NickStrupat
{
    internal static class OSX
    {
        public static Int32 GetSize()
        {
            int lineSize;
            IntPtr sizeOfLineSize = (IntPtr) sizeof(int);
            sysctlbyname("hw.cachelinesize", out lineSize, ref sizeOfLineSize, IntPtr.Zero, IntPtr.Zero);
            return lineSize;
        }

        [DllImport("libc")]
        private static extern int sysctlbyname(string name, out int int_val, ref IntPtr length, IntPtr newp, IntPtr newlen);
    }
}