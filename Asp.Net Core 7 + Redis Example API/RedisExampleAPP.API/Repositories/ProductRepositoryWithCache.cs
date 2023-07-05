using RedisExampleApp.API.Models;
using RedisExampleApp.Cache;

namespace RedisExampleApp.API.Repositories
{
	public class ProductRepositoryWithCache : IProductRepository
	{
		private readonly IProductRepository _repository;
		private readonly RedisService _redisService;


		public ProductRepositoryWithCache(IProductRepository repository, RedisService redisService)
		{
			_repository = repository;
			_redisService = redisService;
		}

		public Task<Product> CreateAsync(Product product)
		{
			throw new NotImplementedException();
		}

		public Task<List<Product>> GetAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Product> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
