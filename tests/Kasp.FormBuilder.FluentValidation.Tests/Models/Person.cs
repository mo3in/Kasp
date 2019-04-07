using System.ComponentModel.DataAnnotations;

namespace Kasp.FormBuilder.FluentValidation.Tests.Models {
	public class Person {
		public string Name { get; set; }
		public string Family { get; set; }

		[Required]
		public string Email { get; set; }
	}
}