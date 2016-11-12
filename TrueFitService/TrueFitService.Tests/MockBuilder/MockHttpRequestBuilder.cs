using System.Net.Http;

namespace TrueFitService.Tests
{
    public class MockHttpRequestBuilder
    {
        public static HttpRequestMessage GetRequestObject()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new System.Web.Http.HttpConfiguration());
            return request;
        }
    }
}
