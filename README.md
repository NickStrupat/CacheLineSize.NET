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
    static void Main(string[] args) => Console.WriteLine(CacheLine.Size);
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