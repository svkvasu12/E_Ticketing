﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace New_Final_ET1.Data.Migrations
{
    public partial class lgoinmailjet2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AspNetUsers");
        }
    }
}
