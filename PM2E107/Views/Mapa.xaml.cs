using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace PM2E107.Views;

public partial class Mapa : ContentPage
{
	Location locacion;



	public Mapa(Location locacion) {
		InitializeComponent();
        this.locacion = locacion;
	}


    protected override async void OnAppearing() {
        base.OnAppearing();

        MapSpan ms = new MapSpan(locacion, 0.01, 0.01);
        mapaSitio.MoveToRegion(ms);
        mapaSitio.IsShowingUser = true;
        await DisplayAlert("locacion", locacion.Latitude.ToString() + "/" + locacion.Longitude.ToString(), "OK");
    }
}