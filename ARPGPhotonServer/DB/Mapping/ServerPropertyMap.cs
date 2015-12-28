using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARPGCommon.Model;
using FluentNHibernate.Mapping;

namespace ARPGPhotonServer.DB.Mapping
{
    internal class ServerPropertyMap : ClassMap<ServerProperty>
    {
        public ServerPropertyMap()
        {
            Id(x => x.Id).Column("id");
            Map(x => x.Ip).Column("ip");
            Map(x => x.Name).Column("name");
            Map(x => x.Count).Column("count");
            Table("serverproperty");
        }
    }
}