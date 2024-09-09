using CoreIdentity_1.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreIdentity_1.Models.Configurations
{
    public class ProfileConfiguration : BaseConfiguration<AppUserProfile>
    {
        public override void Configure(EntityTypeBuilder<AppUserProfile> builder)
        {
            base.Configure(builder);

        }
    }
}
