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

			return View();
		}

		public IActionResult Show()
		{
			_memoryCache.TryGetValue("zaman", out string zamancache);
			_memoryCache.TryGetValue("callback", out string callback);
			ViewBag.callback = callback;
			ViewBag.zaman = zamancache;

			return View();
		}
	}
}
