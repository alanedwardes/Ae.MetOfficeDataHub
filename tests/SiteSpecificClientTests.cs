using Ae.MetOfficeDataHub;

namespace tests;

public class SiteSpecificClientTests
{
    public sealed class TestDelegatingHandler : DelegatingHandler
    {
        private readonly Stream _stream;
        public TestDelegatingHandler(Stream stream) => _stream = stream;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StreamContent(_stream)
            });
        }
    }

    [Fact]
    public async Task TestGetHourlyForecastAsync()
    {
        var fs = new FileInfo("Responses/site-specific-hourly.json");

        using var httpClient = new HttpClient(new TestDelegatingHandler(fs.OpenRead()))
        {
            BaseAddress = new Uri("https://data.hub.api.metoffice.gov.uk/sitespecific/v0/")
        };

        var client = new SiteSpecificClient(httpClient);

        var response = await client.GetHourlyForecastAsync("BD1", true, true, "51.509865", "-0.118092");
    }

    [Fact]
    public async Task TestGetThreeHourlyForecastAsync()
    {
        var fs = new FileInfo("Responses/site-specific-three-hourly.json");

        using var httpClient = new HttpClient(new TestDelegatingHandler(fs.OpenRead()))
        {
            BaseAddress = new Uri("https://data.hub.api.metoffice.gov.uk/sitespecific/v0/")
        };

        var client = new SiteSpecificClient(httpClient);

        var response = await client.GetThreeHourlyForecastAsync("BD1", true, true, "51.509865", "-0.118092");
    }

    [Fact]
    public async Task TestGetDailyForecastAsync()
    {
        var fs = new FileInfo("Responses/site-specific-daily.json");

        using var httpClient = new HttpClient(new TestDelegatingHandler(fs.OpenRead()))
        {
            BaseAddress = new Uri("https://data.hub.api.metoffice.gov.uk/sitespecific/v0/")
        };

        var client = new SiteSpecificClient(httpClient);

        var response = await client.GetDailyForecastAsync("BD1", true, true, "51.509865", "-0.118092");
    }
}