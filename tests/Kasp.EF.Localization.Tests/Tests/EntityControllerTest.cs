using System.Net.Http;
using System.Threading.Tasks;
using Kasp.Core.Tests;
using Kasp.EF.Localization.Tests.Data;
using Kasp.EF.Localization.Tests.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Kasp.EF.Localization.Tests.Tests {
	public class EntityControllerTest : KClassFixtureWebApp<StartupDbLocalization> {
		public EntityControllerTest(ITestOutputHelper output, KWebAppFactory<StartupDbLocalization> factory) : base(output, factory) {
			PostRepository = Factory.Server.Host.Services.GetService<PostRepository>();

			PostRepository.AddAsync(new Post {Title = "سلام", LangId = "fa-IR"}).Wait();
			PostRepository.AddAsync(new Post {Title = "hello", LangId = "en-US"}).Wait();
			PostRepository.SaveAsync().Wait();
		}

		private PostRepository PostRepository { get; }

		[Theory]
		[InlineData("fa-IR", "سلام")]
		[InlineData("en-US", "Hello")]
		public async Task Items(string culture, string result) {
			var response = await Client.GetAsync($"/api/Entity/List?culture={culture}&ui-culture={culture}");
			var items = await response.Content.ReadAsAsync<Post[]>();
			Assert.Equal(result, items[0].Title);
		}
	}
}