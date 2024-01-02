using CRM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Map
{
    public class _PhoneModelMap : IEntityTypeConfiguration<_PhoneModel>
    {
        public void Configure(EntityTypeBuilder<_PhoneModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Customer);
        }
    }
}
