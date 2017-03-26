using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace NickStrupat
{
    internal static class Windows
    {
        public static Int32 GetSize()
        {
            var info = ManagedGetLogicalProcessorInformation();
            if (info == null)
                throw new Exception("Could not retrieve the cache line size.");
            return info.First(x => x.Relationship == LOGICAL_PROCESSOR_RELATIONSHIP.RelationCache).ProcessorInformation.Cache.LineSize;
        }

        // http://stackoverflow.com/a/6972620/232574

        [StructLayout(LayoutKind.Sequential)]
        struct PROCESSORCORE
        {
            public byte Flags;
        };

        [StructLayout(LayoutKind.Sequential)]
        struct NUMANODE
        {
            public uint NodeNumber;
        }

        enum PROCESSOR_CACHE_TYPE
        {
            CacheUnified,
            CacheInstruction,
            CacheData,
            CacheTrace
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CACHE_DESCRIPTOR
        {
            public byte Level;
            public byte Associativity;
            public ushort LineSize;
            public uint Size;
            public PROCESSOR_CACHE_TYPE Type;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION_UNION
        {
            [FieldOffset(0)]
            public PROCESSORCORE ProcessorCore;
            [FieldOffset(0)]
            public NUMANODE NumaNode;
            [FieldOffset(0)]
            public CACHE_DESCRIPTOR Cache;
            [FieldOffset(0)]
            private UInt64 Reserved1;
            [FieldOffset(8)]
            private UInt64 Reserved2;
        }

        enum LOGICAL_PROCESSOR_RELATIONSHIP
        {
            RelationProcessorCore,
            RelationNumaNode,
            RelationCache,
            RelationProcessorPackage,
            RelationGroup,
            RelationAll = 0xffff
        }

        private struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION
        {
#pragma warning disable 0649
            public UIntPtr ProcessorMask;
            public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;
            public SYSTEM_LOGICAL_PROCESSOR_INFORMATION_UNION ProcessorInformation;
#pragma warning restore 0649
        }

        [DllImport(@"kernel32.dll", SetLastError = true)]
        private static extern bool GetLogicalProcessorInformation(IntPtr Buffer, ref uint ReturnLength);

        private const int ERROR_INSUFFICIENT_BUFFER = 122;

        private static SYSTEM_LOGICAL_PROCESSOR_INFORMATION[] ManagedGetLogicalProcessorInformation()
        {
            uint ReturnLength = 0;
            GetLogicalProcessorInformation(IntPtr.Zero, ref ReturnLength);
            if (Marshal.GetLastWin32Error() != ERROR_INSUFFICIENT_BUFFER)
                return null;
            IntPtr Ptr = Marshal.AllocHGlobal((int)ReturnLength);
            try
            {
                if (GetLogicalProcessorInformation(Ptr, ref ReturnLength))
                {
                    int size = Marshal.SizeOf<SYSTEM_LOGICAL_PROCESSOR_INFORMATION>();
                    int len = (int)ReturnLength / size;
                    SYSTEM_LOGICAL_PROCESSOR_INFORMATION[] Buffer = new SYSTEM_LOGICAL_PROCESSOR_INFORMATION[len];
                    IntPtr Item = Ptr;
                    for (int i = 0; i < len; i++)
                    {
                        Buffer[i] = Marshal.PtrToStructure<SYSTEM_LOGICAL_PROCESSOR_INFORMATION>(Item);
                        Item += size;
                    }
                    return Buffer;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(Ptr);
            }
            return null;
        }
    }
}
