using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sarahah.Core.Domain.Entities;

namespace sarahah.Infrastructure.Data.Config
{
    public class ConfigMessage : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Content)
                .HasMaxLength(1000).
                HasColumnType("NVARCHAR")
                .IsRequired();

            builder.Property(m => m.createdAt)
               .HasDefaultValueSql("GETUTCDATE()")     
               .ValueGeneratedOnAdd()                  
               .IsRequired();


            // one - many between User and Message
            builder.HasOne(x => x.User)
                 .WithMany(x => x.messages)
                 .HasForeignKey(x => x.UserId);


        




        }
    }
}
