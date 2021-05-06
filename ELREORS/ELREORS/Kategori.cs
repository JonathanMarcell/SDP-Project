using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELREORS
{
    class Kategori
    {
        public string id { get; set; }
        public string nama { get; set; }

        public Kategori(string id, string namaKategori)
        {
            this.id = id;
            this.nama = namaKategori;
        }

        public override string ToString()
        {
            return nama;
        }

    }
}
