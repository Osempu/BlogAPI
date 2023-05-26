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
            builder.HasOne( p => p.Author)
                .WithMany( a => a.Posts)
                .HasForeignKey( p => p.AuthorId);

            builder.HasMany( p => p.Tags)
                .WithMany( t => t.Posts)
                .UsingEntity( j => j.ToTable("PostTag"));

            builder.HasData(
                new Post {Id = 1, Author = new Author {Id = 1, Name = "Oscar Montenegro"}, Title = "My first Post", Body = "Hello world, this is my first post"},
                new Post {Id = 2, Author = new Author {Id = 2, Name = "Another Author"}, Title = "My second Post", Body = "Hello world, this is my second post"}
            );
        }
    }
}