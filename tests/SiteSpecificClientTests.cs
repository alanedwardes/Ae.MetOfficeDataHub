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

    [Theory]
    [InlineData("Responses/site-specific-hourly1.json")]
    [InlineData("Responses/site-specific-hourly2.json")]
    [InlineData("Responses/site-specific-hourly3.json")]
    [InlineData("Responses/site-specific-hourly4.json")]
    [InlineData("Responses/site-specific-hourly5.json")]
    [InlineData("Responses/site-specific-hourly6.json")]
    [InlineData("Responses/site-specific-hourly7.json")]
    [InlineData("Responses/site-specific-hourly8.json")]
    [InlineData("Responses/site-specific-hourly9.json")]
    public async Task TestGetHourlyForecastAsync(string file)
    {
        var fs = new FileInfo(file);

        using var httpClient = new HttpClient(new TestDelegatingHandler(fs.OpenRead()))
        {
            BaseAddress = new Uri("https://data.hub.api.metoffice.gov.uk/sitespecific/v0/")
        };

        var client = new SiteSpecificClient(httpClient);

        var response = await client.GetHourlyForecastAsync("BD1", true, true, 51.509865f, -0.118092f);
        if (response.Parameters != null)
        {
            foreach ((string name, Parameter parameter) in Assert.Single(response.Parameters))
            {
                Assert.Empty(parameter.AdditionalProperties);
                Assert.Equal("Parameter", parameter.Type);
                Assert.Empty(parameter.Unit.AdditionalProperties);
                Assert.Empty(parameter.Unit.Symbol.AdditionalProperties);
            }
        }

        Assert.Equal("FeatureCollection", response.Type);

        Assert.Empty(response.AdditionalProperties);
        HourlyFeature feature = Assert.Single(response.Features);
        Assert.Empty(feature.AdditionalProperties);
        Assert.Equal("Feature", feature.Type);
        Assert.Empty(feature.Geometry.AdditionalProperties);
        Geometry geometry = feature.Geometry;
        Assert.Empty(feature.Geometry.AdditionalProperties);
        Assert.Equal("Point", geometry.Type);
        HourlyFeatureProperties properties = feature.Properties;
        Assert.Empty(properties.AdditionalProperties);
        if (properties.Location != null)
        {
            Assert.Empty(properties.Location.AdditionalProperties);
        }
        Assert.NotEmpty(properties.TimeSeries);
        foreach (HourlyTimeSeries series in properties.TimeSeries)
        {
            Assert.Empty(series.AdditionalProperties);
        }
    }

    [Theory]
    [InlineData("Responses/site-specific-three-hourly1.json")]
    [InlineData("Responses/site-specific-three-hourly2.json")]
    [InlineData("Responses/site-specific-three-hourly3.json")]
    public async Task TestGetThreeHourlyForecastAsync(string file)
    {
        var fs = new FileInfo(file);

        using var httpClient = new HttpClient(new TestDelegatingHandler(fs.OpenRead()))
        {
            BaseAddress = new Uri("https://data.hub.api.metoffice.gov.uk/sitespecific/v0/")
        };

        var client = new SiteSpecificClient(httpClient);

        ThreeHourlyResponse response = await client.GetThreeHourlyForecastAsync("BD1", true, true, 51.509865f, -0.118092f);
        if (response.Parameters != null)
        {
            foreach ((string name, Parameter parameter) in Assert.Single(response.Parameters))
            {
                Assert.Empty(parameter.AdditionalProperties);
                Assert.Equal("Parameter", parameter.Type);
                Assert.Empty(parameter.Unit.AdditionalProperties);
                Assert.Empty(parameter.Unit.Symbol.AdditionalProperties);
            }
        }

        Assert.Equal("FeatureCollection", response.Type);

        Assert.Empty(response.AdditionalProperties);
        ThreeHourlyFeature feature = Assert.Single(response.Features);
        Assert.Empty(feature.AdditionalProperties);
        Assert.Equal("Feature", feature.Type);
        Geometry geometry = feature.Geometry;
        Assert.Empty(feature.Geometry.AdditionalProperties);
        Assert.Equal("Point", geometry.Type);
        ThreeHourlyFeatureProperties properties = feature.Properties;
        Assert.Empty(properties.AdditionalProperties);
        if (properties.Location != null)
        {
            Assert.Empty(properties.Location.AdditionalProperties);
        }
        Assert.NotEmpty(properties.TimeSeries);
        foreach (ThreeHourlyTimeSeries series in properties.TimeSeries)
        {
            Assert.Empty(series.AdditionalProperties);
        }
    }

    [Theory]
    [InlineData("Responses/site-specific-daily1.json")]
    [InlineData("Responses/site-specific-daily2.json")]
    [InlineData("Responses/site-specific-daily3.json")]
    [InlineData("Responses/site-specific-daily4.json")]
    public async Task TestGetDailyForecastAsync(string file)
    {
        var fs = new FileInfo(file);

        using var httpClient = new HttpClient(new TestDelegatingHandler(fs.OpenRead()))
        {
            BaseAddress = new Uri("https://data.hub.api.metoffice.gov.uk/sitespecific/v0/")
        };

        var client = new SiteSpecificClient(httpClient);

        DailyResponse response = await client.GetDailyForecastAsync("BD1", true, true, 51.509865f, -0.118092f);
        if (response.Parameters != null)
        {
            foreach ((string name, Parameter parameter) in Assert.Single(response.Parameters))
            {
                Assert.Empty(parameter.AdditionalProperties);
                Assert.Equal("Parameter", parameter.Type);
                Assert.Empty(parameter.Unit.AdditionalProperties);
                Assert.Empty(parameter.Unit.Symbol.AdditionalProperties);
            }
        }

        Assert.Equal("FeatureCollection", response.Type);

        Assert.Empty(response.AdditionalProperties);
        DailyFeature feature = Assert.Single(response.Features);
        Assert.Empty(feature.AdditionalProperties);
        Assert.Equal("Feature", feature.Type);
        Geometry geometry = feature.Geometry;
        Assert.Empty(feature.Geometry.AdditionalProperties);
        Assert.Equal("Point", geometry.Type);
        DailyFeatureProperties properties = feature.Properties;
        Assert.Empty(properties.AdditionalProperties);
        if (properties.Location != null)
        {
            Assert.Empty(properties.Location.AdditionalProperties);
        }
        Assert.NotEmpty(properties.TimeSeries);
        foreach (DailyTimeSeries series in properties.TimeSeries)
        {
            Assert.Empty(series.AdditionalProperties);
        }
    }
}