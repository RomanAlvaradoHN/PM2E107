using Microsoft.Maui.Maps;
using PM2E107.Controllers;
using PM2E107.Models;
using SQLite;
using System.Diagnostics;
using System.Windows.Input;

namespace PM2E107.Views;

public partial class Listado : ContentPage
{
  




    public Listado() { 
		InitializeComponent();
	}



    protected override async void OnAppearing() {
        base.OnAppearing();

		viewListado.ItemsSource = await App.db.SelectAll();
    }



    public ICommand SwEliminar => new Command<int>(async (id) => {
        try {
            await App.db.Delete(await App.db.SelectById(id));
            await DisplayAlert("Atencion", "Registro eliminado con exito", "Aceptar");
            viewListado.ItemsSource = await App.db.SelectAll();

        } catch(Exception ex) {
            await DisplayAlert("Error", ex.Message, "Aceptar");
        }
        
    });



    public ICommand SwMapa => new Command<int>(async (id) => {
        try {
            Sitio sitio = await App.db.SelectById(id);
            await Navigation.PushAsync(new Mapa(sitio.Locacion));

        } catch(Exception ex) {
            await DisplayAlert("Error", ex.Message, "Aceptar");
        }
        
    });

    //Colocar en el SwipeItem la propiedad:  Invoked="SwipeItem_Eliminar"
    //private async void SwipeItem_Eliminar(object sender, EventArgs e) {
    //	if(sender is SwipeItem swipeItem) {
    //           await DisplayAlert("Opcion", swipeItem.Text, "Ok");
    //       }
    //}

    //Colocar en el SwipeItem la propiedad:  Invoked="SwipeItem_Mapa"
    //   private async void SwipeItem_Mapa(object sender, EventArgs e) {
    //       if (sender is SwipeItem swipeItem) {
    //           await DisplayAlert("Opcion", swipeItem.Text, "Ok");
    //       }
    //   }





}