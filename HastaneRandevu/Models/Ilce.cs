using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Models
{
    public class Ilce
    {
        public virtual int Id { get; set; }
        [ForeignKey("il")]
        public virtual int IlID{ get; set; }
        public virtual string IlceAdi { get; set; }
    }

    public class IlceMap : ClassMapping<Ilce>
    {
        public IlceMap()
        {
            Table("ilce");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.IlID, x =>
            {
                x.NotNullable(true);
                x.Column("il_id");
            });
            Property(x => x.IlceAdi, x =>
            {
                x.NotNullable(true);
                x.Column("ilce_adi");
            });
        }
    }
}