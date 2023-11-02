using PM2E107.Controllers;
using PM2E107.Models;
using PM2E107.Views;
using System.Diagnostics;

namespace PM2E107 {
    public partial class MainPage : ContentPage {
        private byte[] fotoArray;
        private Location location = new Location();
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;






        public MainPage() {
            InitializeComponent();
        }

        






        

        public async void TakeAPhoto(object sender, EventArgs e) {
            if (MediaPicker.Default.IsCaptureSupported) {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null) {
                    try {
                        //byte[] imgArray;
                        using Stream stream = await photo.OpenReadAsync();
                        using (MemoryStream memoryStream = new MemoryStream()) {
                            await stream.CopyToAsync(memoryStream);
                            //imgArray = memoryStream.ToArray();
                            fotoArray = memoryStream.ToArray();
                        }
                        //ImageSource imgSource = StreamImageSource.FromStream(() => new MemoryStream(imgArray));
                        ImageSource imgSource = StreamImageSource.FromStream(() => new MemoryStream(fotoArray));
                        imgFoto.Source = imgSource;
                        await ObtenerCoordenadas();

                    } catch (Exception ex) {
                        // Manejo de excepciones
                        await DisplayAlert("Atención", ex.Message, "Aceptar");
                    }
                }
            }
        }


        private async Task ObtenerCoordenadas() {
            try {
                _isCheckingLocation = true;
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Low, TimeSpan.FromSeconds(5));
                _cancelTokenSource = new CancellationTokenSource();
                
                location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
                if (location != null) {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    lblLongitud.Text = $"{location.Longitude}";
                    lblLatitud.Text = $"{location.Latitude}";
                } 

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex) {
                await DisplayAlert("Atencion", ex.Message, "Aceptar");

            } finally {
                _isCheckingLocation = false;
            }
        }
        public void CancelRequest() {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false) {
                _cancelTokenSource.Cancel();
            }                
        }









        private async void OnBtnAgregarClicked(object sender, EventArgs e) {
            try {
                Sitio sitio = new Sitio(
                    fotoArray,
                    location,
                    txtDescripcion.Text
                );

                if (!sitio.GetDatosInvalidos().Any()) {
                    await App.db.Insert(sitio);
                    LimpiarCampos();

                } else {
                    string msj = string.Join("\n", sitio.GetDatosInvalidos());
                    await DisplayAlert("Datos Invalidos:", msj,  "acepar");
                }

            }catch(Exception ex) {
                await DisplayAlert("Alerta de Falla!", ex.Message, "Entendido");
            }
            

            //await DisplayAlert("Atencion", "mensaje", "Aceptar");
        }













        private async void OnBtnListaSitiosClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new Listado());
            //await DisplayAlert("Atencion", "mensaje", "Aceptar");
        }



        private void LimpiarCampos() {
            imgFoto.Source = null;
            lblLatitud.Text = string.Empty;
            lblLongitud.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }
    }
}