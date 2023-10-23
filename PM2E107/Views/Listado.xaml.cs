using PM2E107.Controllers;
using System.Windows.Input;

namespace PM2E107.Views;

public partial class Listado : ContentPage
{
	private readonly DBController db;





	public Listado() { 
		InitializeComponent();
		db = new DBController();
	}



    protected override async void OnAppearing() {
        base.OnAppearing();

		viewListado.ItemsSource = await db.SelectAll();
    }


}