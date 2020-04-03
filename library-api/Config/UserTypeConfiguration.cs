using library_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace library_api.Config {
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserType> builder)
        {
            builder.HasKey(u => u.Id);
            builder
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

            builder
                .HasData(
                    new UserType
                    {
                        Id = 1,
                        Name = "Admin" 
                    },
                    new UserType
                    {
                        Id = 2,
                        Name = "Seller" 
                    },
                    new UserType
                    {
                        Id = 3,
                        Name = "Customer" 
                    }
            );
        }
    }
}
