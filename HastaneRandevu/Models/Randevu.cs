using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Models
{
    public class Randevu
    {
        public virtual int Id { get; set; }
        [ForeignKey("user")]
        public virtual int DoktorId { get; set; }
        [ForeignKey("user")]
        public virtual int HastaId { get; set; }
        public virtual DateTime TarihSaat { get; set; }
        public virtual float Puan { get; set; }
        public virtual string Durum { get; set; }
        public virtual string Doktor { get; set; }
        public virtual string Hasta { get; set; }
        public virtual string Klinik { get; set; }

        public virtual void setProperies()
        {
            Doktor = Database.Session.Load<User>(DoktorId).Username;
            Hasta = Database.Session.Load<User>(HastaId).Username;
            Klinik = Database.Session.Load<Klinik>(Database.Session.Load<Doctor>(DoktorId).KlinikId).KlinikAdi;
        }
    }

    public class RandevuMap : ClassMapping<Randevu>
    {
        public RandevuMap()
        {
            Table("randevu");
            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.DoktorId, x =>
            {
                x.NotNullable(true);
                x.Column("doktor_id");
            });
            Property(x => x.HastaId, x =>
            {
                x.NotNullable(true);
                x.Column("hasta_id");
            });
            Property(x => x.TarihSaat, x =>
            {
                x.NotNullable(true);
                x.Column("tarih_saat");
            });
            Property(x => x.Puan, x => x.NotNullable(true));
            Property(x => x.Durum, x => x.NotNullable(true));
        }
    }
}