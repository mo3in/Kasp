using Kasp.Identity.Core.Entities.UserEntities.XEntities;

namespace Kasp.Identity.Tests.Models.UserModels.XModels {
	public class AppUserRegisterModel : IUserRegisterModel {
		public string Email { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
	}
}