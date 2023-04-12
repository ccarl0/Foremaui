using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace Foremaui.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    // https://json2csharp.com/
    public class Current
    {
        public DateTime Date { get; set; }

        [JsonPropertyName("dt")]
        public long Dt { get; set; }

        [JsonPropertyName("sunrise")]
        public long Sunrise { get; set; }
        
        public DateTime SunRiseDt { get; set; }

        [JsonPropertyName("sunset")]
        public long Sunset { get; set; }

        public DateTime SunSetDt { get; set; }

        [JsonPropertyName("temp")]
        public float Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public float FeelsLike { get; set; }

        [JsonPropertyName("pressure")]
        public float Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public float Humidity { get; set; }

        [JsonPropertyName("dew_point")]
        public float DewPoint { get; set; }

        [JsonPropertyName("uvi")]
        public float Uvi { get; set; }

        [JsonPropertyName("clouds")]
        public float Clouds { get; set; }

        [JsonPropertyName("visibility")]
        public float Visibility { get; set; }

        [JsonPropertyName("wind_speed")]
        public float WindSpeed { get; set; }

        [JsonPropertyName("wind_deg")]
        public float WindDeg { get; set; }

        [JsonPropertyName("wind_gust")]
        public float WindGust { get; set; }

        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; }
    }

    public class Daily
    {
        public DateTime Date { get; set; }

        [JsonPropertyName("dt")]
        public uint Dt { get; set; }

        [JsonPropertyName("sunrise")]
        public uint Sunrise { get; set; }

        public DateTime SunRiseDt { get; set; }

        [JsonPropertyName("sunset")]
        public uint Sunset { get; set; }

        public DateTime SunSetDt { get; set; }

        [JsonPropertyName("moonrise")]
        public uint Moonrise { get; set; }

        public DateTime MoonRiseDt { get; set; }

        [JsonPropertyName("moonset")]
        public uint Moonset { get; set; }

        public DateTime MoonSetDt { get; set; }

        [JsonPropertyName("moon_phase")]
        public float MoonPhase { get; set; }

        [JsonPropertyName("temp")]
        public Temp Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public FeelsLike FeelsLike { get; set; }

        [JsonPropertyName("pressure")]
        public float Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public float Humidity { get; set; }

        [JsonPropertyName("dew_point")]
        public float DewPoint { get; set; }

        [JsonPropertyName("wind_speed")]
        public float WindSpeed { get; set; }

        [JsonPropertyName("wind_deg")]
        public float WindDeg { get; set; }

        [JsonPropertyName("wind_gust")]
        public float WindGust { get; set; }

        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; }

        [JsonPropertyName("clouds")]
        public float Clouds { get; set; }

        [JsonPropertyName("pop")]
        public float Pop { get; set; }

        [JsonPropertyName("uvi")]
        public float Uvi { get; set; }
    }

    public class FeelsLike
    {
        [JsonPropertyName("day")]
        public double Day { get; set; }

        [JsonPropertyName("night")]
        public double Night { get; set; }

        [JsonPropertyName("eve")]
        public double Eve { get; set; }

        [JsonPropertyName("morn")]
        public double Morn { get; set; }
    }

    public class Hourly
    {

        [JsonPropertyName("dt")]
        public int Dt { get; set; }
        public DateTime Date { get; set; }

        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        [JsonPropertyName("pressure")]
        public float Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public float Humidity { get; set; }

        [JsonPropertyName("dew_point")]
        public double DewPoint { get; set; }

        [JsonPropertyName("uvi")]
        public double Uvi { get; set; }

        [JsonPropertyName("clouds")]
        public float Clouds { get; set; }

        [JsonPropertyName("visibility")]
        public float Visibility { get; set; }

        [JsonPropertyName("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonPropertyName("wind_deg")]
        public float WindDeg { get; set; }

        [JsonPropertyName("wind_gust")]
        public double WindGust { get; set; }

        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; }

        [JsonPropertyName("pop")]
        public float Pop { get; set; }
    }

    public class Forecast
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("timezone_offset")]
        public int TimezoneOffset { get; set; }

        [JsonPropertyName("current")]
        public Current Current { get; set; }

        [JsonPropertyName("hourly")]
        public Hourly[] Hourly { get; set; }

        [JsonPropertyName("daily")]
        public Daily[] Daily { get; set; }
    }

    public class Temp
    {
        [JsonPropertyName("day")]
        public double Day { get; set; }

        [JsonPropertyName("min")]
        public double Min { get; set; }

        [JsonPropertyName("max")]
        public double Max { get; set; }

        [JsonPropertyName("night")]
        public double Night { get; set; }

        [JsonPropertyName("eve")]
        public double Eve { get; set; }

        [JsonPropertyName("morn")]
        public double Morn { get; set; }
    }

    public class Weather
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("main")]
        public string Main { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }

}
