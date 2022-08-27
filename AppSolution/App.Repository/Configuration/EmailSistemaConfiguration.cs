using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.Configuration
{
    public class EmailSistemaConfiguration : IEntityTypeConfiguration<EmailSistema>
    {
        public void Configure(EntityTypeBuilder<EmailSistema> builder)
        {
            builder.ToTable("TB_EMAIL_SISTEMA", "dbo");
            // Primary Key
            builder.HasKey(m => new { m.ID });

            // Properties / Column Mapping
            builder.Property(e => e.ID).HasColumnName("GUID_EMAIL").IsRequired();
            builder.Property(e => e.PrimaryDomain).HasColumnName("DC_SERVIDOR_SMTP").IsRequired();
            builder.Property(e => e.PrimaryPort).HasColumnName("DC_PORT_SMTP").IsRequired();
            builder.Property(e => e.UsernameEmail).HasColumnName("DC_USERNAME").IsRequired();
            builder.Property(e => e.UsernamePassword).HasColumnName("DC_USER_PASSWORD").IsRequired();
            builder.Property(e => e.TipodEmail).HasColumnName("ID_TIPO_EMAIL").IsRequired();

            builder.HasData(
                    new EmailSistema
                    {
                        ID = Guid.NewGuid(),
                        PrimaryDomain = "mail.v2wstore.com.br",
                        PrimaryPort = 465,
                        UsernameEmail = "williamf.developer@outlook.com",
                        UsernamePassword = "BR@sil500",
                        ServerNameDisplay = "V2WMedia",
                        TipodEmail = 1
                    }
                );

        }
    }
}
