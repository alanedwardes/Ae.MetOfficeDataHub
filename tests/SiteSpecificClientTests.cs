using Ae.MetOfficeDataHub;
using Newtonsoft.Json;

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

        var response = await client.GetHourlyForecastAsync("BD1", true, true, 51.509865f, -0.118092f);

        Assert.Empty(response.AdditionalProperties);
        HourlyFeature feature = Assert.Single(response.Features);
        Assert.Empty(feature.AdditionalProperties);
        Assert.Equal("Feature", feature.Type);
        Assert.Empty(feature.Geometry.AdditionalProperties);
        Assert.Equal("{\"type\":\"Point\",\"coordinates\":[-0.12480000000000001,51.5081,11.0]}", JsonConvert.SerializeObject(feature.Geometry));
        HourlyFeatureProperties properties = feature.Properties;
        Assert.Empty(properties.AdditionalProperties);
        Assert.Empty(properties.Location.AdditionalProperties);
        Assert.NotEmpty(properties.TimeSeries);
        foreach (HourlyTimeSeries series in properties.TimeSeries)
        {
            Assert.Empty(series.AdditionalProperties);
        }
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

        ThreeHourlyResponse response = await client.GetThreeHourlyForecastAsync("BD1", true, true, 51.509865f, -0.118092f);

        Assert.Empty(response.AdditionalProperties);
        ThreeHourlyFeature feature = Assert.Single(response.Features);
        Assert.Empty(feature.AdditionalProperties);
        Assert.Equal("Feature", feature.Type);
        Assert.Empty(feature.Geometry.AdditionalProperties);
        Assert.Equal("{\"type\":\"Point\",\"coordinates\":[-0.12480000000000001,51.5081,11.0]}", JsonConvert.SerializeObject(feature.Geometry));
        ThreeHourlyFeatureProperties properties = feature.Properties;
        Assert.Empty(properties.AdditionalProperties);
        Assert.Empty(properties.Location.AdditionalProperties);
        Assert.NotEmpty(properties.TimeSeries);
        foreach (ThreeHourlyTimeSeries series in properties.TimeSeries)
        {
            Assert.Empty(series.AdditionalProperties);
        }
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

        DailyResponse response = await client.GetDailyForecastAsync("BD1", true, true, 51.509865f, -0.118092f);

        Assert.Empty(response.AdditionalProperties);
        DailyFeature feature = Assert.Single(response.Features);
        Assert.Empty(feature.AdditionalProperties);
        Assert.Equal("Feature", feature.Type);
        Assert.Empty(feature.Geometry.AdditionalProperties);
        Assert.Equal("{\"type\":\"Point\",\"coordinates\":[-0.12480000000000001,51.5081,11.0]}", JsonConvert.SerializeObject(feature.Geometry));
        DailyFeatureProperties properties = feature.Properties;
        Assert.Empty(properties.AdditionalProperties);
        Assert.Empty(properties.Location.AdditionalProperties);
        Assert.NotEmpty(properties.TimeSeries);
        foreach (DailyTimeSeries series in properties.TimeSeries)
        {
            Assert.Empty(series.AdditionalProperties);
        }
    }
}