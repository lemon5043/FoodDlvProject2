using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;

namespace FoodDlvAPI.Models.Infrastructures.ExtensionMethods
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

    public class ImageSizeAttribute : ValidationAttribute
    {
        private readonly int _width;
        private readonly int _height;

        public ImageSizeAttribute(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return true;

            try
            {
                var image = value as Image;
                int width = image.Width;
                int height = image.Height;

                if (width < _width && height < _height)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
