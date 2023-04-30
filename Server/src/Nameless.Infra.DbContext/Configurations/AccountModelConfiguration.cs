﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nameless.Infra.Common.Models;
using Nameless.Infra.Resources;

namespace Nameless.Infra.DbContext.Configurations
{
    public class AccountModelConfiguration : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure(EntityTypeBuilder<AccountModel> builder)
        {
            builder.ToTable("Account");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .IsRequired();
            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Username)
                .IsRequired();
            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.Language)
                .IsRequired();

            Seed(builder);
        }

        private static void Seed(EntityTypeBuilder<AccountModel> builder)
        {
            builder.HasData
            (
                new AccountModel
                {
                    Id = Guid.Parse("59e4b783-e223-458f-b6b6-19cf8741a8a5"),
                    Email = "god@gmail.com",
                    Username = "god",
                    Password = "god123",
                    Language = Language.EN
                }
            );
        }
    }
}
