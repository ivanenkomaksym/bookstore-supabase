using Postgrest.Models;
using System.Linq.Expressions;

namespace WebUI.Services
{
    public interface IDatabaseService
    {
        public Task<IReadOnlyList<TModel>> From<TModel>() where TModel : BaseModel, new();

        public Task<TModel> FromFilter<TModel>(Expression<Func<TModel, bool>> predicate) where TModel : BaseModel, new();

        public Task<List<TModel>> Delete<TModel>(TModel item) where TModel : BaseModel, new();

        public Task<bool> DeleteFilter<TModel>(Expression<Func<TModel, bool>> predicate) where TModel : BaseModel, new();

        public Task<List<TModel>?> Insert<TModel>(TModel item) where TModel : BaseModel, new();

        public Task<List<TModel>?> Update<TModel>(TModel item) where TModel : BaseModel, new();
    }
}
