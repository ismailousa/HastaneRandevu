using HastaneRandevu.Areas.Admin.ViewModels;
using HastaneRandevu.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.ViewModels
{
    public class HastaneNew
    {
        [Required, MaxLength(12)]
        public string KimlikNo { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required,DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DogumTarihi { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required]
        [MaxLength(256),DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public IList<CinsiyetRadioBox> Cinsiyetler { get; set; }

        public IList<SelectListItem> Iller { get; set; }
        public IList<SelectListItem> Ilceler { get; set; }
        [Required]
        public string HastaneAdi { get; set; }

        public int SecilenIlce { get; set; }
    }
}