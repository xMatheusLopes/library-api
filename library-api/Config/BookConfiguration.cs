using library_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace library_api.Config {
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

            builder
                .Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");
            
            builder
                .Property(b => b.Picture)
                .HasColumnType("varchar(255)");
            
            builder
                .Property(b => b.Price)
                .HasColumnType("DECIMAL(10, 2)");
        }
    }
}
