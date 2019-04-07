using System.ComponentModel.DataAnnotations;

namespace Kasp.FormBuilder.Validators.MvcAttributeParsers {
	public class StringLengthAttributeParser : BaseValidatorParser<StringLengthAttribute, RangeLengthValidator> {
		public override RangeLengthValidator Parse(StringLengthAttribute attribute) {
			return new RangeLengthValidator {Max = attribute.MaximumLength, Min = attribute.MinimumLength};
		}
	}
}