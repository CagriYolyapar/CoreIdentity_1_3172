using CoreIdentity_1.Models.Enums;
using CoreIdentity_1.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CoreIdentity_1.Models.Entities
{

    //IdentityUser class'ı bir Identity Class'ıdır...İndirdigimiz AspNetCore.Identity kütüphanesinden gelir...Ve bu sınıfı default kullanırsanız tabloyo dönüsütürülürken edinecegi primary key string tipte olur. Bizim sistemimizde tablolarımızın primary key'i int oldugu icin biz IdentityUser'in key'inin int olarak tanımlanmasını söylemeliyiz(generic olarak belirterek bunu yapabiliriz)... 

    //Eger siz Identity kütüphanesindeki tablo yapılarını sekillendirmek istemezseniz hic AppUser class'ı acmadan işlemleri iIdentity'ye bırakabilirsiniz... Kendisi bir AspNetUsers isimli tablo acarak bu işlemi yapacaktır...Ancak bilmelisiniz ki eger özel bir müdahale yapmazsanız primary key string olacaktır...

    //Bizim burada özellikle AppUser class'ı acma istegimiz Identity tarafındaki yapıları şekillendirmek(kendi istedigimiz özel property'leri koymak, ilişkileri kendi tarafımızdaki class'lar ile saglamak vs...) istedigimiz icin kaynaklanmıstır...Dolayısıyla biz bu emri verdigimiz zaman Identity kütüpahnesi hem kendi yapısını kendi property'leri ile olusturacak hem de bizim istedigimiz yapıları da icerisine eklemiş olacak...
    public class AppUser : IdentityUser<int>, IEntity
    {
        public AppUser()
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
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual AppUserProfile Profile { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


    }
}
