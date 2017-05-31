using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Admin.ViewModels
{
    public class RoleCheckBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsChecked { get; set; }
    }

    public class CinsiyetRadioBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsChecked { get; set; }
    }

    public class KlinikNew
    {
        public int Id { get; set; }
        public bool isNew { get; set; }
        [Required, MaxLength(250)]
        public string KlinikAdi { get; set; }
    }

    public class UsersAdd
    {
        public bool isNew { get; set; }

        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required, MaxLength(12)]
        public string KimlikNo { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
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

        //[Required]
        public IList<SelectListItem> Klinikler { get; set; }
    }
}