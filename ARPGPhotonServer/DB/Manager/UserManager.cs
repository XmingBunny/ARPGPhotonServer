using System;
using System.Collections.Generic;
using ARPGCommon;
using ARPGCommon.Model;
using ARPGPhotonServer.Tools;

namespace ARPGPhotonServer.DB.Manager
{
    internal class UserManager
    {
        /// <summary>
        /// 与数据库中数据进行对比
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User GetUserByUsername(String username)
        {
            using (var session = NHibernateHelper.Instance.OpenSession())
            {
                using (var transcation = session.BeginTransaction())
                {
                    var list = session.QueryOver<User>().Where(x => x.Username == username);
                    transcation.Commit();
                    if (list.List() != null && list.List().Count > 0)
                    {
                        return list.List()[0];
                    }
                }
            }
            return null;
        }

        public bool RegisterUser(User user)
        {
            using (var session = NHibernateHelper.Instance.OpenSession())
            {
                using (var transcation = session.BeginTransaction())
                {
                    user.Password = MD5Tool.GetMD5(user.Password);
                    session.Save(user);
                    transcation.Commit();
                    return true;
                }
            }
        }
    }
}