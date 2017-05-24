using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Models
{
    public class Doktor
    {
        [ForeignKey("user")]
        public virtual int DoktorId { get; set; }
        [ForeignKey("hastane")]
        public virtual int HastaneId { get; set; }
        [ForeignKey("klinik")]
        public virtual int KlinikId { get; set; }
        public virtual float Puan { get; set; }
    }

    public class DoktorMap : ClassMapping<Doktor>
    {
        public DoktorMap()
        {
            Table("doktor_detay");

            Id(x => x.DoktorId, x => x.Column("doktor_id"));

            Property(x => x.HastaneId, x =>
            {
                x.NotNullable(true);
                x.Column("hastane_id");
            });

            Property(x => x.KlinikId, x =>
            {
                x.NotNullable(true);
                x.Column("klinik_id");
            });

            Property(x => x.Puan, x => x.NotNullable(true));
        }
    }
}