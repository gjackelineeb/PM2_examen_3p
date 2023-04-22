using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AppFirebase.Model;
using AppFirebase.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace AppFirebase.ViewModel
{
    public class UsersViewModel :BaseViewModel
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public double fecha { get; set; }
        public byte[] photo_record { get; set; }
        public byte[] audio_record { get; set; }

        private DBFirebase services;
        public Command AddUsersCommand { get; }
        private ObservableCollection<Pagos> _pagos = new ObservableCollection<Pagos>();

        public ObservableCollection<Pagos> UsersList
        {
            get { return _notas; }
            set
            {
                _notas = value;
                OnPropertyChanged();
            }
        }

        public UsersViewModel()
        {
            services = new DBFirebase();
            UsersList = services.GetUsersList();
            //Photo_Recibo = null;
            AddUsersCommand = new Command(async () => await AddUsersAsync(Descripcion, Monto, Fecha, Photo_record, Audio_record) ;
        }

        public async Task AddUsersAsync(int id_nota, string descripcion, DateTime fecha, byte[] photo_record, byte[] audio_record)
        {
            await services.AddUser(id_nota,descripcion, monto, fecha, photo_record,audio_record);
            
        }
    }
}
