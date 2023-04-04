using AndroidX.Lifecycle;
using MauiMeteo.ViewModels;
using Java.Nio.FileNio.Attributes;

namespace Foremaui.Views;

public partial class Settings : ContentPage
{
    public Settings()
	{
		InitializeComponent();
        DefaultCity.Text = Preferences.Default.Get("fav_city", "Milano, IT");//valore di default se non trovo le preferenze -> ""

    }
}