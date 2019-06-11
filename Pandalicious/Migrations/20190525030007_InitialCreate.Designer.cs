﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pandalicious.Models;

namespace Pandalicious.Migrations
{
    [DbContext(typeof(Model.PandaliciousContext))]
    [Migration("20190525030007_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Pandalicious.Models.Model+Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<string>("IngredientName")
                        .IsRequired();

                    b.Property<string>("IngredientUnit");

                    b.Property<int?>("RecipeId");

                    b.HasKey("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Pandalicious.Models.Model+Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IngredientId");

                    b.Property<string>("MenuMeasurement");

                    b.Property<int>("RecipeId");

                    b.HasKey("MenuId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Pandalicious.Models.Model+Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RecipeDirections");

                    b.Property<DateTime>("RecipeDuration");

                    b.Property<string>("RecipeName");

                    b.Property<int>("RecipeServings");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Pandalicious.Models.Model+Ingredient", b =>
                {
                    b.HasOne("Pandalicious.Models.Model+Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId");
                });
#pragma warning restore 612, 618
        }
    }
}
