using System.Collections.Generic;
using ARPGCommon.Model;

namespace ARPGPhotonServer.DB.Manager
{
    internal class RoleManager
    {
        public IList<Role> GetRoles(User user)
        {
            using (var session = NHibernateHelper.Instance.OpenSession())
            {
                using (var transcation = session.BeginTransaction())
                {
                    var roles = session.QueryOver<Role>().Where(x => x.User.Id == user.Id);
                    transcation.Commit();
                    return roles.List<Role>();
                }
            }
        }

        public bool AddRole(Role role)
        {
            using (var session = NHibernateHelper.Instance.OpenSession())
            {
                using (var transcation = session.BeginTransaction())
                {
                    session.Save(role);
                    transcation.Commit();
                    return true;
                }
            }
        }

        public bool ContainRole(string name)
        {
            using (var session = NHibernateHelper.Instance.OpenSession())
            {
                using (var transcation = session.BeginTransaction())
                {
                    var roles = session.QueryOver<Role>().Where(x => x.Name == name);
                    //transcation.Commit();
                    return roles.List<Role>().Count != 0;
                }
            }
        }

        public void UpdateRole(Role role)
        {
            using (var session = NHibernateHelper.Instance.OpenSession())
            {
                using (var transcation = session.BeginTransaction())
                {
                    session.Update(role);
                    transcation.Commit();
                }
            }
        }
    }
}