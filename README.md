# CacheLineSize.NET
A cross-platform .NET Standard 1.6 library to get the cache line size of the processor, in bytes. Windows, Linux, and macOS supported.

[![AppVeyor](https://img.shields.io/appveyor/ci/NickStrupat/cachelinesize-net.svg)](https://ci.appveyor.com/project/NickStrupat/cachelinesize-net)
[![NuGet Status](http://img.shields.io/nuget/v/CacheLine.Size.svg?style=flat)](https://www.nuget.org/packages/CacheLine.Size/)

## Usage

```csharp
using System;
using NickStrupat;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(CacheLine.Size); // print the cache line size in bytes
        
        var array = new CacheLineAlignedArray<string>(10);
        Interlocked.Exchange(ref array[0], "Hello"); // all threads can now see the latest value at `array[0]` without risk of ruining performance with false-sharing

        // This can be used to build collections which share elements across threads at the fastest possible synchronization.
    }
    
    // An array-like type where each element is on it's own cache-line. This is a building block for avoiding false-sharing.
    public struct CacheLineAlignedArray<T> where T : class {
        private readonly T[] buffer;
        public Array(Int32 size) => buffer = new T[Multiplier * size];
        public Int32 Length => buffer.Length / Multiplier;
        public ref T this[Int32 index] => ref buffer[Multiplier * index];
        private static readonly Int32 Multiplier = CacheLine.Size / IntPtr.Size;
    }
}
```

## See also

- [https://github.com/NickStrupat/CacheLineSize](https://github.com/NickStrupat/CacheLineSize) for the equivalent C function

## Contributing

1. [Create an issue](https://github.com/NickStrupat/CacheLineSize.NET/issues/new)
2. Let's find some point of agreement on your suggestion.
3. Fork it!
4. Create your feature branch: `git checkout -b my-new-feature`
5. Commit your changes: `git commit -am 'Add some feature'`
6. Push to the branch: `git push origin my-new-feature`
7. Submit a pull request :D

## History

[Commit history](https://github.com/NickStrupat/CacheLineSize.NET/commits/master)

## License

[MIT License](https://github.com/NickStrupat/CacheLineSize.NET/blob/master/LICENSE)
