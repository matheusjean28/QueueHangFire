﻿// <auto-generated />
using DeviceContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace QueueWorker.Migrations
{
    [DbContext(typeof(DeviceDb))]
    [Migration("20231003232219_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("MacDeviceModels.MacDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Mac")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Problem")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RemoteAcess")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
