using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Web.Models
{
    public static class ModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Board>().HasData(
                new Board { Id = 1, Title = "General" },
                new Board { Id = 2, Title = "Technology" },
                new Board { Id = 3, Title = "Sports" });

        }        
    }
}