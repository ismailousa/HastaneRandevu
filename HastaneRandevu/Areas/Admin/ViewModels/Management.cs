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

    public class KlinikInfo
    {
        public int Id { get; set; }
        public string KlinikAdi { get; set; }

        public KlinikInfo(Klinik klinik)
        {
            Id = klinik.Id;
            KlinikAdi = klinik.KlinikAdi;
        }
    }

    public class RandevuInfo
    {
        public int Id { get; set; }
        public string HastaAdi { get; set; }
        public string DoktorAdi { get; set; }
        public string Durum { get; set; }
        public float Puan { get; set; }
        public string KlinikAdi { get; set; }
        public DateTime Tarihi { get; set; }
        public string Hastane { get; set; }

        public RandevuInfo(Randevu randevu)
        {
            randevu.setProperies();
            Id = randevu.Id;
            Puan = randevu.Puan;
            Durum = randevu.Durum;
            HastaAdi = randevu.Hasta;
            DoktorAdi = randevu.Doktor;
            KlinikAdi = randevu.Klinik;
            Tarihi = randevu.TarihSaat;
            Hastane = Database.Session.Load<Hastane>(Database.Session.Query<Doctor>().First(x => x.DoktorId == randevu.DoktorId).HastaneId).HastaneAdi;
        }
    }

    public class RandevuList
    {
        public PagedData<RandevuInfo> Randevular { get; set; }
    }

    public class KlinikList
    {
        public PagedData<KlinikInfo> Klinikler { get; set; }
    }
    public class UsersList
    {
        public PagedData<UserInfo> Users { get; set; }

    }
}