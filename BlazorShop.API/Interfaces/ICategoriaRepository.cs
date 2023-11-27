using BlazorShop.API.Entities;

namespace BlazorShop.API.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria> GetCategoriaAsync(int id);
    }

}
