using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELREORS
{
    class menu
    {

        public int id;
        public string nama;
        public int harga;
        public int kategori;
        public int status;
        public string keterangan;
        public byte[] gambar;

        public menu(int id, string nama)
        {
            this.id = id;
            this.nama = nama;
        }
        public menu(int id, string nama, int harga, int kategori, int status, string keterangan, byte[] gambar)
        {
            this.id = id;
            this.nama = nama;
            this.harga = harga;
            this.kategori = kategori;
            this.status = status;
            this.keterangan = keterangan;
            this.gambar = gambar;
        }

        public void setId(int id)
        {
            this.id = id;
        }
        public int getId()
        {
            return id;
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
        public int getStatus()
        {
            return status;
        }
        public void setKategori(int kategori)
        {
            this.kategori = kategori;
        }
        public int getKategori()
        {
            return kategori;
        }
        public void setKeterangan(string keterangan)
        {
            this.keterangan = keterangan;
        }
        public string getKeterangan()
        {
            return keterangan;
        }
        public void setGambar(byte[] gambar)
        {
            this.gambar = gambar;
        }
        public byte[] getGambar()
        {
            return gambar;
        }
    }
}
