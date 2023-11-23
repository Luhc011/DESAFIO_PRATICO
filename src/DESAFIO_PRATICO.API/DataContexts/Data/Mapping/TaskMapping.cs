using DESAFIO_PRATICO.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DESAFIO_PRATICO.API.DataContexts.Data.Mapping
{
    public class TaskMapping : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.ToTable("Task");
            builder.HasKey(x => x.Id);

            builder.Property(x=> x.Title)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(x=> x.Description)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnType("varchar(500)");

            builder.Property(x=> x.Status)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(x => x.UserId)
                .HasColumnType("int"); 

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
