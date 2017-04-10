using System;
using System.Runtime.InteropServices;

namespace NickStrupat
{
    public static class CacheLine
    {
        public static readonly Int32 Size = GetSize();

        private static Int32 GetSize()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return Windows.GetSize();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return Linux.GetSize();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return OSX.GetSize();
            throw new Exception("Unrecognized OS platform.");
        }
    }
}
