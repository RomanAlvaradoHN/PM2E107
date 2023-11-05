using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using PM2E107.Models;


namespace PM2E107.Views;

public partial class Mapa : ContentPage {
    Sitio sitio;



    public Mapa(Sitio sitio) {
        InitializeComponent();
        this.sitio = sitio;
    }


    protected override void OnAppearing() {
        base.OnAppearing();

        mapaSitio.Pins.Add(new Pin {
            Label = sitio.Descripcion,
            Location = sitio.Locacion,
            Type = PinType.Place
        });

        mapaSitio.MapType = MapType.Satellite;
        mapaSitio.MoveToRegion(new MapSpan(sitio.Locacion, 0.01, 0.01));
    }


    public async void OnBtnCompartirClicked(object sender, EventArgs e) {
        await Share.RequestAsync(new ShareTextRequest {
            Title = sitio.Descripcion,
            Uri = $"{sitio.Latitud}, {sitio.Longitud}"
        });
    }


}