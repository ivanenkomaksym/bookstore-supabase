using Postgrest.Models;
using System.Linq.Expressions;

namespace WebUI.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly Supabase.Client _client;
        private readonly ILogger<DatabaseService> _logger;

        public DatabaseService(
            Supabase.Client client,
            ILogger<DatabaseService> logger)
        {
            logger.LogInformation("------------------- CONSTRUCTOR -------------------");

            _client = client;
            _logger = logger;
        }

        public async Task<IReadOnlyList<TModel>> From<TModel>() where TModel : BaseModel, new()
        {
            var modeledResponse = await _client
                .From<TModel>()
                .Get();
            return modeledResponse.Models;
        }

        public async Task<TModel> FromFilter<TModel>(Expression<Func<TModel, bool>> predicate) where TModel : BaseModel, new()
        {
            var modeledResponse = await _client
                .From<TModel>()
                .Where(predicate)
                .Get();
            return modeledResponse.Models.Single();
        }

        public async Task<List<TModel>> Delete<TModel>(TModel item) where TModel : BaseModel, new()
        {
            var modeledResponse = await _client
                .From<TModel>()
                .Delete(item);
            return modeledResponse.Models;
        }

        public async Task<List<TModel>?> Insert<TModel>(TModel item) where TModel : BaseModel, new()
        {
            Postgrest.Responses.ModeledResponse<TModel> modeledResponse;
            try
            {
                modeledResponse = await _client
                    .From<TModel>()
                    .Insert(item);

                return modeledResponse.Models;
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
