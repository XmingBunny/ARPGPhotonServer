using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace ARPGPhotonServer.DB
{
    internal class NHibernateHelper
    {
        public static readonly NHibernateHelper Instance = new NHibernateHelper();
        private ISessionFactory _sessionFactory;

        private NHibernateHelper()
        {
            OpenSession();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }

        private void GetSessionFactory()
        {
            _sessionFactory =
                Fluently.Configure()
                    .Database(
                        MySQLConfiguration.Standard.ConnectionString(
                            c => c.Server("localhost").Database("arpgserver").Username("root").Password("1234")))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ArpgApplication>())
                    .BuildSessionFactory();
        }
    }
}