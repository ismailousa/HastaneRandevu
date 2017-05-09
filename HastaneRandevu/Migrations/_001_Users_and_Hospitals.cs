using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Migrations
{
    [Migration(1)]
    public class _001_Users_and_Hospitals : Migration
    {
        public override void Down()
        {
            Delete.Table("il");
            Delete.Table("ilce");
            Delete.Table("hastane");
            Delete.Table("klinik");
            Delete.Table("role");
            Delete.Table("cinsiyet");
            Delete.Table("user");
            Delete.Table("user_roles");
            Delete.Table("randevu");
            Delete.Table("doktor_detay");
            Delete.Table("admin_detay");
        }

        public override void Up()
        {
            Create.Table("il")
               .WithColumn("id").AsInt32().PrimaryKey().Identity()
               .WithColumn("plaka_no").AsByte()
               .WithColumn("il_adi").AsString(50);

            Create.Table("ilce")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("il_id").AsInt32().ForeignKey("il", "id").OnDelete(Rule.Cascade)
                .WithColumn("ilce_adi").AsString(100);

            Create.Table("hastane")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("hastane_adi").AsString(128)
                .WithColumn("ilce_id").AsInt32().ForeignKey("ilce", "id").OnDelete(Rule.Cascade)
                .WithColumn("puan").AsFloat();

            Create.Table("klinik")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("klinik_adi").AsString(128);

            Create.Table("role")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("role_adi").AsString(50);

            Create.Table("cinsiyet")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("cinsiyeti").AsString(50);

            Create.Table("user")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("kimlik_no").AsString(12)
                .WithColumn("username").AsString(50)
                .WithColumn("password_hash").AsString(128)
                .WithColumn("dogum_tarihi").AsDate()
                .WithColumn("cinsiyet").AsInt32().ForeignKey("cinsiyet", "id").OnDelete(Rule.Cascade)
                .WithColumn("email").AsString(256)
                .WithColumn("telefon").AsString(15);

            Create.Table("user_roles")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("user_id").AsInt32().ForeignKey("user", "id").OnDelete(Rule.Cascade)
                .WithColumn("role_id").AsInt32().ForeignKey("role", "id").OnDelete(Rule.Cascade);

            Create.Table("randevu")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("doktor_id").AsInt32().ForeignKey("user", "id").OnDelete(Rule.Cascade)
                .WithColumn("hasta_id").AsInt32().ForeignKey("user", "id").OnDelete(Rule.Cascade)
                .WithColumn("tarih_saat").AsDateTime()
                .WithColumn("puan").AsFloat()
                .WithColumn("durum").AsString(20);

            Create.Table("doktor_detay")
                .WithColumn("id").AsInt32().Identity().PrimaryKey()
                .WithColumn("hastane_id").AsInt32().ForeignKey("hastane", "id").OnDelete(Rule.Cascade)
                .WithColumn("doktor_id").AsInt32().ForeignKey("user", "id").OnDelete(Rule.Cascade)
                .WithColumn("klinik_id").AsInt32().ForeignKey("klinik", "id").OnDelete(Rule.Cascade)
                .WithColumn("puan").AsFloat();

            Create.Table("admin_detay")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("hastane_id").AsInt32().ForeignKey("hastane", "id").OnDelete(Rule.Cascade)
                .WithColumn("admin_id").AsInt32().ForeignKey("user", "id").OnDelete(Rule.Cascade);
        }
    }
}