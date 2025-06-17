// PakaianLib/KeranjangBelanja.cs
using System.Collections.Generic;
using System.Linq;

namespace PakaianLib
{
    public class KeranjangBelanja<T> where T : Pakaian // Pastikan T adalah Pakaian
    {
        private List<T> _items = new List<T>();

        public void TambahKeKeranjang(T item)
        {
            _items.Add(item);
        }

        public bool KeluarkanDariKeranjang(T item)
        {
            return _items.Remove(item);
        }

        public bool KeluarkanDariKeranjangByIndex(int index)
        {
            if (index >= 0 && index < _items.Count)
            {
                _items.RemoveAt(index);
                return true;
            }
            return false;
        }

        public List<T> GetSemuaItem()
        {
            return new List<T>(_items);
        }

        public decimal HitungTotal()
        {
            return _items.Sum(item => item.Harga);
        }

        public int JumlahItem()
        {
            return _items.Count;
        }

        public void KosongkanKeranjang()
        {
            _items.Clear();
        }
    }
}
