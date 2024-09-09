using CoreIdentity_1.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreIdentity_1.Models.Configurations
{
    public class AppUserClaimConfiguration : BaseConfiguration<AppUserClaim>
    {
        public override void Configure(EntityTypeBuilder<AppUserClaim> builder)
        {
            base.Configure(builder);
            builder.Ignore(x => x.ID);
        }
    }
}
