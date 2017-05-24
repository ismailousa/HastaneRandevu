using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Models
{
    public class Hastane
    {
        public virtual int Id { get; set; }
        [ForeignKey("ilce")]
        public virtual int IlceID{ get; set; }
        public virtual string HastaneAdi { get; set; }
        public virtual float Puan { get; set; }
    }

    public class HastaneMap : ClassMapping<Hastane>
    {

        public HastaneMap()
        {
            Table("hastane");

            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.IlceID, x =>
            {
                x.NotNullable(true);
                x.Column("ilce_id");
            });
            Property(x => x.HastaneAdi, x =>
            {
                x.NotNullable(true);
                x.Column("hastane_adi");
            });
            Property(x => x.Puan, x => x.NotNullable(true));
        }
    }
}