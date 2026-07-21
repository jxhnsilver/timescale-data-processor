using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimescaleDataProcessor.Api.Entities;

namespace TimescaleDataProcessor.Api.Data.Configurations
{
    public class ValueRecordConfiguration : IEntityTypeConfiguration<ValueRecord>
    {
        public void Configure(EntityTypeBuilder<ValueRecord> builder)
        {
            builder.ToTable("Values");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.FileName)
                .IsRequired()
                .HasComment("Имя файла, из которого было получено значение операции");

            builder.Property(e => e.StartTime)
                .IsRequired()
                .HasComment("Время начала операции в формате UTC");

            builder.Property(e => e.ExecutionTime)
                .IsRequired()
                .HasComment("Время выполнения операции в секундах");

            builder.Property(e => e.Indicator)
                .IsRequired()
                .HasComment("Значение показателя");

            builder.HasIndex(e => e.FileName);
        }
    }
}
