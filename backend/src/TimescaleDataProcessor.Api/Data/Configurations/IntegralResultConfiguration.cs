using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimescaleDataProcessor.Api.Entities;

namespace TimescaleDataProcessor.Api.Data.Configurations
{
    public class IntegralResultConfiguration : IEntityTypeConfiguration<IntegralResult>
    {
        public void Configure(EntityTypeBuilder<IntegralResult> builder)
        {
            builder.ToTable("Results");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.FileName)
                .IsRequired()
                .HasComment("Имя файла, из которого получен интегральный результат");

            builder.Property(e => e.TimeDeltaInSeconds)
                .IsRequired()
                .HasComment("Дельта времени StartTime в секундах");

            builder.Property(e => e.MinStartTime)
                .IsRequired()
                .HasComment("Минимальное время начала операции в формате UTC");

            builder.Property(e => e.AvgExecutionTime)
                .IsRequired()
                .HasComment("Среднее время выполнения операции в секундах");

            builder.Property(e => e.AvgIndicator)
                .IsRequired()
                .HasComment("Среднее значение показателя");

            builder.Property(e => e.MedianIndicator)
                .IsRequired()
                .HasComment("Медианное значение показателя");

            builder.Property(e => e.MaxIndicator)
                .IsRequired()
                .HasComment("Максимальное значение показателя");

            builder.Property(e => e.MinIndicator)
                .IsRequired()
                .HasComment("Минимальное значение показателя");

            builder.HasIndex(e => e.FileName)
                .IsUnique();
        }
    }
}
