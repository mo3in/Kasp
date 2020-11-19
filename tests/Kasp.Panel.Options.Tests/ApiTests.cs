﻿using System.Threading.Tasks;
using FluentAssertions;
using Kasp.Test;
using Xunit;
using Xunit.Abstractions;

namespace Kasp.Panel.Options.Tests {
	public class ApiTests : KClassFixtureWebApp<Startup> {
		public ApiTests(ITestOutputHelper output, KWebAppFactory<Startup> factory) : base(output, factory) {
		}

		[Fact]
		public async Task GetValue() {
			var response = await Client.GetAsync("/api/panel/options/kasp.panel.options.tests.globalsiteoption");

			Output.WriteLine(await response.Content.ReadAsStringAsync());

			response.IsSuccessStatusCode.Should().BeTrue();
		}

		[Fact]
		public async Task GetList() {
			var response = await Client.GetAsync("/api/panel/options");

			Output.WriteLine(await response.Content.ReadAsStringAsync());

			response.IsSuccessStatusCode.Should().BeTrue();
		}
	}
}
