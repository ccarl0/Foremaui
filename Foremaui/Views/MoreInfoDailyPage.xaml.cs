using Android.Database;

namespace Foremaui.Views;

public partial class MoreInfoDailyPage : ContentPage
{
	public MoreInfoDailyPage(Models.Daily daily)
	{
		InitializeComponent();
        sunRise.Text = $"{daily.SunRiseDt.Hour.ToString()}:{daily.SunRiseDt.Minute.ToString()}";
        sunSet.Text = $"{daily.SunSetDt.Hour.ToString()}:{daily.SunSetDt.Minute.ToString()}";
        moonRise.Text = $"{daily.MoonRiseDt.Hour.ToString()}:{daily.MoonRiseDt.Minute.ToString()}";
        moonSet.Text = $"{daily.MoonSetDt.Hour.ToString()}:{daily.MoonSetDt.Minute.ToString()}";

        dayTemp.Text = $"{daily.Temp.Day.ToString()} °";
        nighTemp.Text = $"{daily.Temp.Night.ToString()} °";
        dayFl.Text = $"{daily.FeelsLike.Day.ToString()} °";
        nighFl.Text = $"{daily.FeelsLike.Night.ToString()} °";

        windSpeed.Text = $"{daily.WindSpeed.ToString()} km/h";
        windDir.Text = $"{daily.WindDeg.ToString()} °";

        humidity.Text = $"{daily.Humidity.ToString()}%";
        pressure.Text = $"{daily.Pressure.ToString()} mbar";

        iconSource.Source = $"https://openweathermap.org/img/wn/{daily.Weather[0].Icon}@2x.png";
    }
}