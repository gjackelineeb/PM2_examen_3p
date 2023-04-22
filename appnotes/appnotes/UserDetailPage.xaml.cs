using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFirebase.Model;
using AppFirebase.Services;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFirebase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailPage : ContentPage
    {
        private DBFirebase services;
        byte[] imageToSave;
        public UserDetailPage(Notas notas)
        {
            InitializeComponent();
            BindingContext = notas;
            services = new DBFirebase();

            PFecha.MinimumDate = new DateTime(Convert.ToInt32(DateTime.Now.ToString("yyyy")), Convert.ToInt32(DateTime.Now.ToString("MM")), Convert.ToInt32(DateTime.Now.ToString("dd")));
            PFecha.MaximumDate = new DateTime(Convert.ToInt32(DateTime.Now.ToString("yyyy")) + 1, 1, 31);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await services.UpdateUser(
                PDescripcion.Text, Convert.ToDouble(PId.Text), PFecha.Date, imageToSave);
            await Navigation.PopAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await services.DeleteUser(
                PDescripcion.Text, Convert.ToDouble(PMonto.Text));
            await Navigation.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                var takepic = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "PhotoApp",
                    Name = DateTime.Now.ToString() + "_Pic.jpg",
                    SaveToAlbum = true,
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                    CompressionQuality = 40
                });

                //  await DisplayAlert("Ubicacion de la foto: ", takepic.Path, "Ok");

                if (takepic != null)
                {
                    imageToSave = null;
                    MemoryStream memoryStream = new MemoryStream();

                    takepic.GetStream().CopyTo(memoryStream);
                    imageToSave = memoryStream.ToArray();

                    PPhoto.Source = ImageSource.FromStream(() => { return takepic.GetStream(); });
                }

                //descripcion_entry.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}