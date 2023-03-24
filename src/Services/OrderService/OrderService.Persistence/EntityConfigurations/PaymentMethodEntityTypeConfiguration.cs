using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModels.BuyerAggregate;
using OrderService.Infrastructure.Context;

namespace OrderService.Infrastructure.EntityConfigurations
{
    public class PaymentMethodEntityTypeConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("paymentMethods", OrderDbContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder
                .Property(i => i.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.CardHolderName)
                .HasColumnName("CardHolderName")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(x => x.Alias)
                .HasColumnName("Alias")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(x => x.CardNumber)
                .HasColumnName("CardNumber")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(x => x.Expiration)
                .HasColumnName("Expiration")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(25)
                .IsRequired();

            builder
                .Property(x => x.CardTypeId)
                .HasColumnName("CardTypeId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder
                .HasOne(x => x.CardType)
                .WithMany()
                .HasForeignKey(x => x.CardTypeId);
        }
    }
}
