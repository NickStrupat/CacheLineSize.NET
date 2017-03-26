using System;
using System.Runtime.InteropServices;

namespace NickStrupat
{
    internal static class Linux
    {
        public static Int32 GetSize() => (Int32) sysconf(_SC_LEVEL1_DCACHE_LINESIZE);

        [DllImport("libc")]
        private static extern long sysconf(int name);

        private const Int32 _SC_LEVEL1_DCACHE_LINESIZE = 190;
    }
}