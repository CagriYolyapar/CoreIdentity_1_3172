using CoreIdentity_1.Models.Enums;
using CoreIdentity_1.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CoreIdentity_1.Models.Entities
{
    public class AppUserClaim : IdentityUserClaim<int>, IEntity
    {
        public AppUserClaim()
        {
            CreatedDate = DateTime.Now;
            Status = DataStatus.Inserted;
        }
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus Status { get; set; }

        //Relational Properties
    }
}
