﻿using HastaneRandevu.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HastaneRandevu.ViewModels
{ 
    public class UsersNew
    {
        [Required, MaxLength(12)]
        public string KimlikNo { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DogumTarihi { get; set; }
        
        [Required, DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }

        [Required]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public IList<RoleCheckBox> Roles { get; set; }
        public IList<CinsiyetRadioBox> Cinsiyetler { get; set; }
    }
}