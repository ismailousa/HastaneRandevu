﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Areas.Hasta.ViewModels
{
    public class ProfileForm
    {
        public bool modifyPassword { get; set; }

        [Required, MaxLength(12)]
        public string KimlikNo { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }

        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}