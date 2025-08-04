using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Infrastructure.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loans");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Borrower)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.LoanDate)
                .IsRequired();

            builder.Property(l => l.ReturnDate)
                .IsRequired(false);

            builder.HasOne(l => l.Book)
                .WithMany()
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
