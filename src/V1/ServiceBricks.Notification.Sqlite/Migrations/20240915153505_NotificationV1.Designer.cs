﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceBricks.Notification.Sqlite;

#nullable disable

namespace ServiceBricks.Notification.Sqlite.Migrations
{
    [DbContext(typeof(NotificationSqliteContext))]
    [Migration("20240915153505_NotificationV1")]
    partial class NotificationV1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("ServiceBricks.Notification.EntityFrameworkCore.NotifyMessage", b =>
                {
                    b.Property<long>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BccAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Body")
                        .HasColumnType("TEXT");

                    b.Property<string>("BodyHtml")
                        .HasColumnType("TEXT");

                    b.Property<string>("CcAddress")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("CreateDate")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("FromAddress")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("FutureProcessDate")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsError")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsHtml")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsProcessing")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Priority")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("ProcessDate")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("ProcessResponse")
                        .HasColumnType("TEXT");

                    b.Property<int>("RetryCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SenderType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Subject")
                        .HasColumnType("TEXT");

                    b.Property<string>("ToAddress")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("UpdateDate")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("Key");

                    b.HasIndex("IsComplete", "IsProcessing", "IsError", "FutureProcessDate", "ProcessDate", "CreateDate");

                    b.ToTable("NotifyMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
