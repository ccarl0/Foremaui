namespace Foremaui.Views;

public partial class MoreInfoDailyPage : ContentPage
{
	public MoreInfoDailyPage(Models.Daily daily)
	{
		InitializeComponent();
        sunRise.Text = daily.Temp.Min.ToString();
        sunSet.Text = daily.Temp.Max.ToString();
        moonRise.Text = daily.Temp.Max.ToString();
        moonSet.Text = daily.Temp.Max.ToString();

        dayTemp.Text = daily.Temp.Day.ToString();
        nighTemp.Text = daily.Temp.Night.ToString();
        dayFl.Text = daily.FeelsLike.Day.ToString();
        nighFl.Text = daily.FeelsLike.Night.ToString();

        windSpeed.Text = daily.WindSpeed.ToString();
        windDir.Text = daily.WindDeg.ToString();

        humidity.Text = daily.Humidity.ToString();
        pressure.Text = daily.Pressure.ToString();
    }
}