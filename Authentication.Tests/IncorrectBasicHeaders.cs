using System.Collections;
using System.Collections.Generic;

namespace Authentication.Tests
{
    public class IncorrectBasicHeaders : TestHeaders, IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "not encoded header", $"Basic {ClientId}:{ClientSecret}" };
            yield return new object[] { "header without type", Encode($"{ClientId}:{ClientSecret}") };
            yield return new object[] { "not basic header", $"NotBasic {Encode($"{ClientId}:{ClientSecret}")}" };
            yield return new object[] { "header with multiple colons", $"Basic {Encode($"{ClientId}:{ClientSecret}:")}" };
            yield return new object[] { "header without colon", $"Basic {Encode($"{ClientId}{ClientSecret}")}" };
            yield return new object[] { $"header with empty {ClientId}", $"Basic {Encode($":{ClientSecret}")}" };
            yield return new object[] { $"header with empty {ClientSecret}", $"Basic {Encode($"{ClientId}:")}" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
