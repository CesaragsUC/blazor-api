using BlazorShop.API.Entities;
using BlazorShop.API.Interfaces;

namespace BlazorShop.API.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public Task<IEnumerable<Categoria>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Categoria> GetCategoriaAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
