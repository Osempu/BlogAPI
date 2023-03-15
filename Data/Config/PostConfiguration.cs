using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAPI.Data.Config
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(
                new Post {Id = 1, Author = "Oscar Montenegro", Title = "My first Post", Body = "Hello world, this is my first post"},
                new Post {Id = 2, Author = "Oscar Montenegro", Title = "My second Post", Body = "Hello world, this is my second post"}
            );
        }
    }
}