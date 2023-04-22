using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFirebase.Model;
using Firebase.Database;
using Firebase.Database.Query;

namespace AppFirebase.Services
{
    public class DBFirebase
    {
        public FirebaseClient client;

        public DBFirebase()
        {
            client = new FirebaseClient(
                "https://notasp3-default-rtdb.firebaseio.com/");
        }

        public ObservableCollection<Pagos> GetUsersList()
        {
            var userdata = client
                .Child("Notas")
                .AsObservable<Pagos>()
                .AsObservableCollection();
            return userdata;
        }

        public async Task AddUser(int id_nota, string descripcion, DateTime fecha, byte[] photo_record, byte[] audio_record)
        {
            nota u = new Notas() {id_nota= id_nota,Descripcion = descripcion, Fecha = fecha, Photo_record = photo_record, Audio_record= audio_record};
            await client
                .Child("Nota")
                .PostAsync(u);
        }

        public async Task UpdateUser(int id_nota, string descripcion, DateTime fecha, byte[] photo_record, byte[] audio_record)
        {
            var toUpdateUser = (await client
                .Child("Notas")
                .OnceAsync<Pagos>()).FirstOrDefault
                (a => a.Object.Descripcion == descripcion && a.Object.id_nota == monto);

            Notas u = new Notas() { int id_nota, string descripcion, DateTime fecha, byte[] photo_record, byte[] audio_record };
            await client
                .Child("Notas")
                .Child(toUpdateUser.Key)
                .PutAsync(u);
        }

        public async Task DeleteUser(int id_nota, string descripcion)
        {
            var toDeleteUser = (await client
                .Child("Notas")
                .OnceAsync<Pagos>()).FirstOrDefault
                (a => a.Object.Descripcion == descripcion && a.Object.id_nota == id_nota);
            await client.Child("Notas").Child(toDeleteUser.Key).DeleteAsync();
        }
    }
}
