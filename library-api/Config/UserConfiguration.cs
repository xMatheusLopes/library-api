using library_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace library_api.Config {
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");
            
            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");
            
            builder
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

            builder
                .HasOne(u => u.Type);

            builder
                .Property(u => u.CPF)
                .HasColumnType("varchar(255)");

            builder
                .Property(u => u.AccessKey)
                .HasColumnType("varchar(255)");

            builder
                .Property(u => u.Picture)
                .HasColumnType("varchar(255)");

            builder
                .HasOne(u => u.GeneralStatus);

            builder
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("datetime('now')")
                .ValueGeneratedOnAdd();

            builder
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("datetime('now')")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
