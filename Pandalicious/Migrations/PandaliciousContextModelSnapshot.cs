﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pandalicious.Models;

namespace Pandalicious.Migrations
{
    [DbContext(typeof(Model.PandaliciousContext))]
    partial class PandaliciousContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Pandalicious.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<string>("IngredientName")
                        .IsRequired();

                    b.Property<string>("IngredientUnit");

                    b.HasKey("IngredientId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Pandalicious.Models.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IngredientId");

                    b.Property<string>("MenuMeasurement");

                    b.Property<int>("RecipeId");

                    b.HasKey("MenuId");

                    b.HasIndex("IngredientId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Pandalicious.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RecipeDuration");

                    b.Property<string>("RecipeName");

                    b.Property<int>("RecipeServings");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Pandalicious.Models.Menu", b =>
                {
                    b.HasOne("Pandalicious.Models.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
