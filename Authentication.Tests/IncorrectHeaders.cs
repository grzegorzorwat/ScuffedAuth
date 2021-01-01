using System.Collections;
using System.Collections.Generic;

namespace Authentication.Tests
{
    public class IncorrectHeaders : TestHeaders, IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "not encoded header", "Basic clientId:clientSecret" };
            yield return new object[] { "not basic header", Encode("clientId:clientSecret") };
            yield return new object[] { "bearer header", $"Bearer {Encode("clientId:clientSecret")}" };
            yield return new object[] { "header with multiple colons", $"Basic {Encode("clientId:clientSecret:")}" };
            yield return new object[] { "header without colon", $"Basic {Encode("clientIdclientSecret")}" };
            yield return new object[] { "header with empty clientId", $"Basic {Encode(":clientSecret")}" };
            yield return new object[] { "header with empty clientSecret", $"Basic {Encode("clientId:")}" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
