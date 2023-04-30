using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nameless.Infra.Common.Models;

namespace Nameless.Infra.DbContext.Configurations
{
    public class PlayerModelConfiguration : IEntityTypeConfiguration<PlayerModel>
    {
        public void Configure(EntityTypeBuilder<PlayerModel> builder)
        {
            builder.ToTable("Player");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AccountId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();
            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasOne(d => d.Account)
                .WithMany(p => p.Players)
                .HasForeignKey(d => d.AccountId);

            Seed(builder);
        }

        private static void Seed(EntityTypeBuilder<PlayerModel> builder)
        {
            builder.HasData
            (
                new PlayerModel
                {
                    Id = Guid.Parse("d60e679d-b2f9-4b01-ac91-272ea87393de"),
                    AccountId = Guid.Parse("59e4b783-e223-458f-b6b6-19cf8741a8a5"),
                    Name = "[GOD] Player 1"
                },
                new PlayerModel
                {
                    Id = Guid.Parse("b9facc5f-5ed7-41a8-a445-353da8c0c1e1"),
                    AccountId = Guid.Parse("59e4b783-e223-458f-b6b6-19cf8741a8a5"),
                    Name = "[GOD] Player 2"
                }
            );
        }
    }
}
