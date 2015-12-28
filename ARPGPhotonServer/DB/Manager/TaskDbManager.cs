using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using ARPGCommon.Model;

namespace ARPGPhotonServer.DB.Manager
{
    internal class TaskDbManager
    {
        public IList<TaskDb> GeTaskDbs(Role role)
        {
            using (var session = NHibernateHelper.Instance.OpenSession())
            {
                using (var transcation = session.BeginTransaction())
                {
                    var taskDbs = session.QueryOver<TaskDb>().Where(x => x.Role.Id == role.Id);
                    transcation.Commit();
                    return taskDbs.List<TaskDb>();
                }
            }
        }

        public void AddTaskDb(TaskDb taskDb)
        {
            using (var session = NHibernateHelper.Instance.OpenSession())
            {
                using (var transcation = session.BeginTransaction())
                {
                    session.Save(taskDb);
                    transcation.Commit();
                }
            }
        }

        public void UpdateTaskDb(TaskDb taskDb)
        {
            using (var session = NHibernateHelper.Instance.OpenSession())
            {
                using (var transcation = session.BeginTransaction())
                {
                    session.Update(taskDb);
                    transcation.Commit();
                }
            }
        }
    }
}