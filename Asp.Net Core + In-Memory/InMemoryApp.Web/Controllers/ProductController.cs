using InMemoryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
	public class ProductController : Controller
	{
		private IMemoryCache _memoryCache;
		public ProductController(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}
		public IActionResult Index()
		{

			MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
			options.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
			options.SlidingExpiration = TimeSpan.FromMinutes(1);
			options.Priority = CacheItemPriority.High;
			options.RegisterPostEvictionCallback((key, value, reason, state) =>
			{
				_memoryCache.Set("callback", $"{key}-->{value} => sebep: {reason}");
			});
			_memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

			Product p = new Product {Id=1,Name="Kalem", Price=200 };

			_memoryCache.Set<Product>("product:1", p);
			_memoryCache.Set<double>("mnoney", 100.99);

			return View();
		}

		public IActionResult Show()
		{
			_memoryCache.TryGetValue("zaman", out string zamancache);
			_memoryCache.TryGetValue("callback", out string callback);
			ViewBag.callback = callback;
			ViewBag.zaman = zamancache;

			ViewBag.product = _memoryCache.Get<Product>("product:1");

			return View();
		}
	}
}
