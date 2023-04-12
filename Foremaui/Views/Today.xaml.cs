using Microsoft.Maui.Controls;

namespace Foremaui.Views;

public partial class Today : ContentPage
{
	public Today()
	{
		InitializeComponent();

		CityEntry.Text = Preferences.Default.Get("fav_city", "Milano, IT");//valore di default se non trovo le preferenze -> ""

	}

}