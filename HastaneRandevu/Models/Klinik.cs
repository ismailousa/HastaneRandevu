using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Models
{
    public class Klinik
    {
        public virtual int Id { get; set; }
        public virtual string KlinikAdi { get; set; }
    }

    public class KlinikMap : ClassMapping<Klinik>
    {
        public KlinikMap()
        {
            Table("klinik");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.KlinikAdi, x =>
            {
                x.NotNullable(true);
                x.Column("klinik_adi");
            });
        }
    }
}