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
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public virtual  DateTime DogumTarihi { get; set; }
        [ForeignKey("cinsiyet")]
        public virtual int CinsiyetRefId { get; set; }
        public virtual string Email { get; set; }
        public virtual string Telefon { get; set; }
        public virtual IList<Role> Roles { get; set; }

        public virtual Doctor DoktorDetay { get; set; }
        public virtual Admin AdminDetay { get; set; }

        public User()
        {
            Roles = new List<Role>();               
        }

        public virtual void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashString(password, 13);
        }

        public virtual Boolean CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        public static void FakeHash()
        {
            BCrypt.Net.BCrypt.HashString("", 13);
        }
        public virtual string Cinsiyet()
        {
            return Database.Session.Load<Cinsiyet>(CinsiyetRefId).Name;
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
            Property(x => x.DogumTarihi, x => {
                x.NotNullable(true);
                x.Column("dogum_tarihi");
                });
            Property(x => x.CinsiyetRefId, x =>
            {
                x.NotNullable(true);
                x.Column("cinsiyet");
            });

            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.Telefon, x => x.NotNullable(true));

            Bag(x => x.Roles, x => {
                x.Table("user_roles");
                x.Key(k => k.Column("user_id"));

            }, x => x.ManyToMany(k => k.Column("role_id")));
        }
    }
}