using library_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace library_api.Config {
    public class GeneralStatusConfiguration : IEntityTypeConfiguration<GeneralStatus>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GeneralStatus> builder)
        {
            builder.HasKey(g => g.Id);
            builder
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");

            builder
                .HasData(
                    new UserType
                    {
                        Id = 1,
                        Name = "Active" 
                    },
                    new UserType
                    {
                        Id = 2,
                        Name = "Inactive" 
                    },
                    new UserType
                    {
                        Id = 3,
                        Name = "Disabled" 
                    }
            );
        }
    }
}
