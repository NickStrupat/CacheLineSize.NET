using NickStrupat;
using Xunit;

namespace CacheLineSize.NET
{
    public class Tests
    {
        [Fact]
        public void Test() => Assert.NotEqual(0, CacheLine.Size);
    }
}
