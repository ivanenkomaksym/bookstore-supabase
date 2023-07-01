using Postgrest.Models;

namespace WebUI.Services
{
    public interface IDatabaseService
    {
        public Task<IReadOnlyList<TModel>> From<TModel>() where TModel : BaseModel, new();

        public Task<List<TModel>> Delete<TModel>(TModel item) where TModel : BaseModel, new();

        public Task<List<TModel>?> Insert<TModel>(TModel item) where TModel : BaseModel, new();
    }
}
