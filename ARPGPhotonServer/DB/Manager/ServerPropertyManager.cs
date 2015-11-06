using System.Collections.Generic;
using ARPGCommon.Model;
using NHibernate;

namespace ARPGPhotonServer.DB.Manager
{
    internal class ServerPropertyManager
    {
        public IList<ServerProperty> GetserverpropertyList()
        {
            using (var session = NHibernateHelper.Instance.OpenSession())
            {
                using (var transction = session.BeginTransaction())
                {
                    var list = session.QueryOver<ServerProperty>();
                    return list.List<ServerProperty>();
                }
            }
        }
    }
}