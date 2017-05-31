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
        //internal List<Il> il;
        //internal List<SelectListItem> hastaneler;

        [Required]
        public List<SelectListItem> Iller { get; internal set; }
        [Required]
        public List<SelectListItem> Ilceler { get; set; }
        [Required]
        public IList<SelectListItem> Hastaneler { get; set; }

        [Required]
        public List<SelectListItem> Klinikler { get; set; }
        [Required]
        public List<SelectListItem> Doktorlar { get; set; }

        public DateTime Tarih { get; set; }
    }
}