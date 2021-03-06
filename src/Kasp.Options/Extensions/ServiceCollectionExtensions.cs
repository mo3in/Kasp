using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kasp.Options.Extensions {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddKaspOptions(this IServiceCollection services, IConfiguration configuration) {
			var options = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(x => typeof(IKaspOption).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
				.ToImmutableArray();

			var configureMethod = typeof(OptionsConfigurationServiceCollectionExtensions).GetMethod("Configure", new[] {services.GetType(), typeof(IConfiguration)});

			foreach (var option in options) {
				configureMethod.MakeGenericMethod(option).Invoke(services, new object[] {services, configuration.GetSection($"app:{option.FullName}")});
			}

			return services;
		}
	}
}
