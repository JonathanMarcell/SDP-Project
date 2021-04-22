using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELREORS
{
    class menu
    {
        public string nama;
        public int harga;
        public int status;
        public string keterangan;

        public menu(string nama, int harga, int status, string keterangan)
        {
            this.nama = nama;
            this.harga = harga;
            this.status = status;
            this.keterangan = keterangan;
        }

        public void setNama(string nama)
        {
            this.nama = nama;
        }

        public string getNama()
        {
            return nama;
        }

        public void setHarga(int harga)
        {
            this.harga = harga;
        }
        public int getHarga()
        {
            return harga;
        }
        public void setStatus(int status)
        {
            this.status = status;
        }
        public int setStatus()
        {
            return status;
        }
        public void setKeterangan(string keterangan)
        {
            this.keterangan = keterangan;
        }
        public string getKeterangan()
        {
            return keterangan;
        }
    }
}
