using HastaneRandevu.Infrastructure;
using HastaneRandevu.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Areas.Admin.ViewModels
{

    public class UserInfo
    {
        public int Id { get; set; }
        public string KimlikNo { get; set; }
        public string AdSoyad { get; set; }
        public string Rol { get; set; }
        public string Klinik { get; set; }

        public UserInfo(User user)
        {
            Id = user.Id;
            KimlikNo = user.KimlikNo;
            AdSoyad = user.Username;
            Rol = user.Roles.First().Name;
            if (Rol == "Doktor")
                Klinik = Database.Session.Load<Klinik>(Database.Session.Query<Doctor>().FirstOrDefault(x => x.DoktorId == user.Id).KlinikId).KlinikAdi;
            else
                Klinik = "";
        }
    }
    public class UsersList
    {
        public PagedData<UserInfo> Users { get; set; }

    }
}