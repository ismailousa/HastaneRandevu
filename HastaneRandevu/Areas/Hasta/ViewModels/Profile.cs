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

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DogumTarihi { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "Telefon numarayi 0'siz girin")]
        public string Telefon { get; set; }

        [MaxLength(256),DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(256)]
        public string Klinik { get; set; }

        [MaxLength(256)]
        public string Hastane { get; set; }

    }
}