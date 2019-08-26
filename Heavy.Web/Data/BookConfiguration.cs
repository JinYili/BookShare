using BookShare.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShare.Web.Data
{
    public class BookConfiguration: IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Author).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ReleaseDate).HasColumnType("dateTime");
            builder.Property(x => x.Price).HasColumnType("decimal(6,2)");
            builder.Property(x => x.CoverUrl).IsRequired().HasMaxLength(200);
        }
    }
}
