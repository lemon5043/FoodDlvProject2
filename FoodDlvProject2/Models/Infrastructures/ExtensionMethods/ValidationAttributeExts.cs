using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.Infrastructures.ExtensionMethods
{
    public class DateNowAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var date = (DateTime)value;
            DateTime now = DateTime.Now;
            if (date < now)
            {
                return true;
            }
            return false;
        }
    }
}
