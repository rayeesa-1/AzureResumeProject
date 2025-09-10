using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using NSubstitute;

namespace Api.Function.Tests.Unit
{
    public static class TestFactory
    {
        public static HttpRequestData CreateHttpRequest(string body = "")
        {
            var context = Substitute.For<FunctionContext>();
            return new FakeHttpRequestData(context, body);
        }
    }

    public class FakeHttpRequestData : HttpRequestData
    {
        private readonly string _body;

        public FakeHttpRequestData(FunctionContext context, string body = "") : base(context)
        {
            _body = body;
        }

        public override Stream Body => new MemoryStream(Encoding.UTF8.GetBytes(_body));
        public override HttpHeadersCollection Headers { get; } = new HttpHeadersCollection();
        public override IReadOnlyCollection<IHttpCookie> Cookies { get; } = Array.Empty<IHttpCookie>();
        public override Uri Url => new Uri("http://localhost");
        public override IEnumerable<System.Security.Claims.ClaimsIdentity> Identities => Array.Empty<System.Security.Claims.ClaimsIdentity>();
        public override string Method => "GET";

        public override HttpResponseData CreateResponse() =>
            new FakeHttpResponseData(FunctionContext);
    }

    public class FakeHttpResponseData : HttpResponseData
    {
        public FakeHttpResponseData(FunctionContext context) : base(context)
        {
            Body = new MemoryStream();
            Headers = new HttpHeadersCollection();
        }

        public override System.Net.HttpStatusCode StatusCode { get; set; }
        public override HttpHeadersCollection Headers { get; set; }
        public override Stream Body { get; set; }

        // Returns a minimal fake cookies object
        public override HttpCookies Cookies { get; } = new FakeHttpCookies();
    }

    public class FakeHttpCookies : HttpCookies
    {
    private readonly List<IHttpCookie> _cookies = new();

    // Implement the abstract member exactly as defined
    public override void Append(IHttpCookie cookie)
    {
        _cookies.Add(cookie);
    }
    public override void Append(string name, string value)
    {
        _cookies.Add(new FakeHttpCookie(name, value));
    }


    // Implement CreateNew() returning a simple cookie
    public override IHttpCookie CreateNew() => new FakeHttpCookie("new", "value");

    // Optional helper for tests
    public IHttpCookie? Get(string name) => _cookies.Find(c => c.Name == name);
}

    public class FakeHttpCookie : IHttpCookie
    {
        public FakeHttpCookie(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }
        public string? Path { get; set; } = "/";
        public string? Domain { get; set; } = "localhost";
        public bool? HttpOnly { get; set; }
        public bool? Secure { get; set; }
        public DateTimeOffset? Expires { get; set; }
        public double? MaxAge { get; set; }
        public SameSite SameSite { get; set; } = SameSite.None;
    }
}
