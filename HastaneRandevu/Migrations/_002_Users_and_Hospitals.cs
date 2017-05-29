using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HastaneRandevu.Migrations
{
    [Migration(2)]
    public class _002_Users_and_Hospitals : Migration
    {
        public override void Down()
        {
            Alter.Table("il").AddColumn("plaka_no").AsByte();
        }

        public override void Up()
        {
            Delete.Column("plaka_no").FromTable("il");
        }
    }
}