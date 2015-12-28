using ARPGCommon.Model;
using FluentNHibernate.Mapping;

namespace ARPGPhotonServer.DB.Mapping
{
    internal class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.Username).Column("Username");
            Map(x => x.Password).Column("Password");
            Table("User");
        }
    }
}