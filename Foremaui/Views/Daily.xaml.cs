using Foremaui.ViewModels;

namespace Foremaui.Views;

public partial class Daily : ContentPage
{
	public Daily()
	{
		InitializeComponent();

        CityEntry.Text = Preferences.Default.Get("fav_city", "Milano, IT");//valore di default se non trovo le preferenze -> ""

    }


}