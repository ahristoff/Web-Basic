
namespace SimpleMvc.Framework.Attributes.Validation
{
    public class NumberRangeAttribute : PropertyValidationAttribute
    {
        private readonly double minimum;
        private readonly double maximum;

        public NumberRangeAttribute(double minimum, double maximum)
        {
            this.minimum = minimum;
            this.maximum = maximum;

            //this.ErrorMessage = "Number is not in Range";
        }

        public override bool IsValid(object value)
        {
            var valueAsDouble = value as double?;
            if (valueAsDouble == null)
            {
                return true;
            }

            return valueAsDouble >= minimum && valueAsDouble <= maximum;
        }
    }
}
