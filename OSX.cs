using System;
using System.Runtime.InteropServices;

namespace NickStrupat
{
    internal static class OSX
    {
        public static Int32 GetSize()
        {
            IntPtr lineSize;
            IntPtr sizeOfLineSize = (IntPtr) IntPtr.Size;
            sysctlbyname("hw.cachelinesize", out lineSize, ref sizeOfLineSize, IntPtr.Zero, IntPtr.Zero);
            return lineSize.ToInt32();
        }

        [DllImport("libc")]
        private static extern int sysctlbyname(string name, out IntPtr oldp, ref IntPtr oldlenp, IntPtr newp, IntPtr newlen);
        // int sysctlbyname(const char *name, void *oldp, size_t *oldlenp, void *newp, size_t newlen);
    }
}