using CRM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Map
{
    public class _EmailModelMap : IEntityTypeConfiguration<_EmailModel>
    {
        public void Configure(EntityTypeBuilder<_EmailModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Customer);
        }
    }
}
