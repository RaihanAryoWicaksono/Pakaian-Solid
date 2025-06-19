using PakaianApi.Models;

public interface IKeranjangService
{
    Task<bool> TambahKeKeranjangAsync(string kodePakaian, int qty);
    Task<KeranjangDto> GetKeranjangAsync();
    Task<bool> HapusItemAsync(int id);
    Task<bool> KosongkanKeranjangAsync();
    Task<List<Keranjang>> GetEntityItemsAsync();
}
