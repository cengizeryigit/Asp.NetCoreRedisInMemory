using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
	public class HashTypeController : Controller
	{
		private readonly RedisService _redisService;
		private readonly IDatabase db;
        public string hashKey { get; set; } = "sozluk";
		public HashTypeController(RedisService redisService)
		{
			_redisService = redisService;
			db = _redisService.GetDb(4);
		}
		public IActionResult Index()
		{
			Dictionary<string,string> list = new Dictionary<string,string>();

			if(db.KeyExists(hashKey))
			{
				db.HashGetAll(hashKey).ToList().ForEach(x=>
				{
					list.Add(x.Name, x.Value);
				});
			}
			return View(list);
		}

		[HttpPost]
		public IActionResult Add(string name, string val)
		{
			db.HashSet(hashKey, name, val);

			return RedirectToAction("Index");
		}

		public IActionResult Remove(string name)
		{
			db.HashDelete(hashKey, name);

			return RedirectToAction("Index");
		}
	}
}
