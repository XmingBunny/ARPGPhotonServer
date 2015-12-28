using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARPGCommon.Model;
using FluentNHibernate.Mapping;

namespace ARPGPhotonServer.DB.Mapping
{
    internal class TaskDbMap : ClassMap<TaskDb>
    {
        public TaskDbMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.TaskId).Column("taskid");
            Map(x => x.TaskType).Column("type");
            Map(x => x.TaskState).Column("state");
            Map(x => x.LastUpdateTime).Column("lastupdatetime");
            References(x => x.Role).Column("roleid");
            Table("taskdb");
        }
    }
}