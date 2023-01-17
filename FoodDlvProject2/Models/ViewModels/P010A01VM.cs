using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace FoodDlvProject2.Models.ViewModels
{
    public class P010A01VM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public IFormFile File { get; set; }
        public string FileName { get; set; }
    }
}
