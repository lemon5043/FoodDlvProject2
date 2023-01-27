using System.ComponentModel.DataAnnotations;

namespace FoodDlvProject2.Models.Infrastructures.ExtensionMethods
{
    public class DateNowAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            var date = (DateTime)value;
            DateTime now = DateTime.Now;

            if (date < now)
            {
                return true;
            }
            return false;
        }
    }

    public class ExtensionAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public ExtensionAttribute(params string[] extensions)
        {
            _extensions = extensions;
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            IFormFile file = (IFormFile)value;
            string extension = Path.GetExtension(file.FileName);

            if (_extensions.Contains(extension))
            {
                return true;
            }
            return false;
        }
    }
}
