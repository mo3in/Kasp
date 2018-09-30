using System.Globalization;
using System.Linq;
using Kasp.EF.Localization.Data.Repositories;
using Kasp.EF.Localization.Models;
using Kasp.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Kasp.EF.Localization.Extensions {
	public static class AppBuilderExtensions {
		public static void UseDb(this KaspRequestLocalizationOptions builder) {
			var langRepository = builder.ServiceProvider.GetService<ILangRepository>();

			var dbCultures = langRepository.ListAsync().Result;
			
			foreach (var dbCulture in dbCultures) {
				var cultureInfo = new CultureInfo(dbCulture.Code);
				builder.LocalizationOptions.SupportedCultures.Add(cultureInfo);
				builder.LocalizationOptions.SupportedUICultures.Add(cultureInfo);
			}
	
			var newsLangs = builder.LocalizationOptions.SupportedCultures.Select(x => x.Name).Except(dbCultures.Select(x => x.Code)).ToArray();

			langRepository.AddAsync(newsLangs.Select(x => new Lang {Code = x, Enable = true})).Wait();
			langRepository.SaveAsync().Wait();
		}
	}
}