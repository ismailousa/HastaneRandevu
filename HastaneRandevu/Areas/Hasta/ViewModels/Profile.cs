using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Areas.Hasta.ViewModels
{
    public class ProfileForm
    {
        public bool modifyPassword { get; set; }

        [MaxLength(12)]
        public string KimlikNo { get; set; }

        [MaxLength(12)]
        public string Cinsiyet { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DogumTarihi { get; set; }

        [Required,DataType(DataType.PhoneNumber)]
        [RegularExpression(@"/^(0)[5|2]\d{9}\$/g", ErrorMessage = "Telefon numarayi 0xxxxxxxxxx formatinda girin")]
        public string Telefon { get; set; }

        [MaxLength(256)]
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}