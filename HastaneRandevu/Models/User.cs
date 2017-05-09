using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string KimlikNo { get; set; }
        public virtual string Username { get; set; }
        public virtual string PasswordHash { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public virtual  DateTime DogumTarihi { get; set; }
        [ForeignKey("cinsiyet")]
        public virtual string Cinsiyet { get; set; }
        public virtual string Email { get; set; }
        public virtual string Telefon { get; set; }

        public virtual void SetPassword(string password)
        {
            PasswordHash = "123";
        }
    }

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("user");
            Id(x => x.Id, x => x.Generator(Generators.Identity));
            Property(x => x.KimlikNo, x =>
            {
                x.NotNullable(true);
                x.Column("kimlik_no");
            });
            Property(x => x.Username, x => x.NotNullable(true));
            Property(x => x.PasswordHash, x =>
            {
                x.NotNullable(true);
                x.Column("password_hash");
            });
            Property(x => x.DogumTarihi, x => x.NotNullable(true));
            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.Telefon, x => x.NotNullable(true));
        }
    }
}