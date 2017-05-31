using HastaneRandevu.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Hasta.ViewModels
{
    public class MainPage
    {
        [Required]
        public List<Il> Iller { get; set; }
        [Required]
        public List<Ilce> Ilceler { get; set; }
        public int il { get; set; }
        public int Ilce { get; set; }
        [Required]
        public List<Hastane> Hastaneler { get; set; }
        [Required]
        public List<Klinik> Klinikler { get; set; }
        [Required]
        public List<User> Doktordetay { get; set; }
        public int hastane { get; set; }
        public int klinik { get; set; }
        public int doktor { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RandevuTarihi { get; set; }

    }
}