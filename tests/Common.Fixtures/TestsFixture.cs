using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Common.Fixtures
{
	public class TestFixture<TStartup> : IDisposable where TStartup : class
	{
		private readonly HttpClient Client;
		public readonly TestServer Server;


		public TestFixture()
		{
			var builder = new WebHostBuilder()
				.UseContentRoot($"..\\..\\..\\..\\..\\src\\HeadHunter.API\\")
				.UseStartup<TStartup>();

			Server = new TestServer(builder);
			Client = new HttpClient();
		}


		public void Dispose()
		{
			Client.Dispose();
			Server.Dispose();
		}
	}
}