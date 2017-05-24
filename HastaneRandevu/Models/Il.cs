using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Models
{
    public class Il
    {
        public virtual int Id { get; set; }
        public virtual string PlakaNo { get; set; }
        public virtual string IlAdi { get; set; }
    }
    public class IlMap : ClassMapping<Il>
    {
        public IlMap()
        {
            Table("il");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.PlakaNo, x =>
            {
                x.NotNullable(true);
                x.Column("plaka_no");
            });
            Property(x => x.IlAdi, x =>
            {
                x.NotNullable(true);
                x.Column("il_adi");
            });
        }
        
    }
}