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

            modelBuilder.Entity<Thread>().HasData(
                new Thread { Id = 1, Title = "Hows the weather?", DateTime = DateTime.Now, BoardId = 1 }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Content = "It's a cold ting", DateTime = DateTime.Now, ThreadId = 1, User = "user1@localhost" }
            );

        }        
    }
}