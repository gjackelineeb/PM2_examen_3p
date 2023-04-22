using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFirebase.Model;
using AppFirebase.ViewModel;
using Plugin.Media;
using Xamarin.Forms;

namespace AppFirebase
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new UsersViewModel();

            PDate.MinimumDate = new DateTime(Convert.ToInt32(DateTime.Now.ToString("yyyy")), Convert.ToInt32(DateTime.Now.ToString("MM")), Convert.ToInt32(DateTime.Now.ToString("dd")));
            PDate.MaximumDate = new DateTime(Convert.ToInt32(DateTime.Now.ToString("yyyy")) + 1, 1, 31);

        }

        byte[] imageToSave;

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var pagos = e.Item as Pagos;
            if(pagos == null) return;

            await Navigation.PushAsync(new UserDetailPage(pagos));
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

                    fotoRecibo.Source = ImageSource.FromStream(() => { return takepic.GetStream(); });
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
