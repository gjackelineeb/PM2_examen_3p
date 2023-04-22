using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppFirebase.Model
{
    public class nota
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public double fecha { get; set; }
        public byte[] photo_record { get; set; }
        public byte[] audio_record { get; set; }

    }
}
