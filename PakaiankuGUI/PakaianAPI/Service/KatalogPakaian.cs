using Microsoft.EntityFrameworkCore;
using PakaianApi.Data;
using PakaianApi.Models;
using PakaianAPI.Models;
using PakaianLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PakaianApi.Services
{
    public class KatalogPakaian
    {
        private readonly ApplicationDbContext _context;

        public KatalogPakaian(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Pakaian> GetSemuaPakaian()
        {
            return _context.Pakaian.ToList();
        }

        public Pakaian CariPakaianByKode(string kode)
        {
            return _context.Pakaian.FirstOrDefault(p => p.Kode == kode);
        }

        public List<Pakaian> CariPakaian(string keyword)
        {
            keyword = keyword.ToLower();
            return _context.Pakaian
                .Where(p => p.Nama.ToLower().Contains(keyword) ||
                            p.Kategori.ToLower().Contains(keyword) ||
                            p.Warna.ToLower().Contains(keyword) ||
                            p.Ukuran.ToLower().Contains(keyword) ||
                            p.Kode.ToLower().Contains(keyword))
                .ToList();
        }

        public List<Pakaian> CariPakaianByHarga(decimal min, decimal max)
        {
            return _context.Pakaian
                .Where(p => p.Harga >= min && p.Harga <= max)
                .ToList();
        }

        public List<Pakaian> CariPakaianByKategori(string kategori)
        {
            kategori = kategori.ToLower();
            return _context.Pakaian
                .Where(p => p.Kategori.ToLower().Contains(kategori))
                .ToList();
        }
    }
}
