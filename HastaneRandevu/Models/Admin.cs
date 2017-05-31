using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Models
{
    public class Administrator
    {
        [ForeignKey("user")]
        public virtual int AdminId { get; set; }
        [ForeignKey("hastane")]
        public virtual int HastaneId { get; set; }
    }

    public class AdminMap : ClassMapping<Administrator>
    {
        public AdminMap()
        {
            Table("admin_detay");

            Id(x => x.AdminId, x => x.Column("admin_id"));

            Property(x => x.HastaneId, x =>
            {
                x.NotNullable(true);
                x.Column("hastane_id");
            });
        }
    }
}