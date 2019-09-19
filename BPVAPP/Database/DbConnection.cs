﻿using System.Collections.Generic;
using FluentNHibernate.Cfg;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using BPVAPP.Models;
using NHibernate.Tool.hbm2ddl;
using System.Linq;

namespace BPVAPP.Database
{
    public class DbConnection
    {
        private readonly ISessionFactory sessionFactory;

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString("Server=localhost; Port=3306; Database=BPVAPP; Uid=root; Pwd=;")). // Pedro will place the connection string
                Mappings(m => m.FluentMappings.AddFromAssemblyOf<Company>()).
                ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true)).
                BuildSessionFactory();
        }

        public DbConnection()
        {
            sessionFactory = CreateSessionFactory();
        }

        /// <summary>
        /// Simple methode to get all records from a table, you can use this method on all tables because its
        /// a genereric function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAllModels<T>()
        {
            /// example use age
            /// var db = new DbConnection()
            /// List<CarModel> cars = db.GetAllModels<Cars>();
            /// List<PlaneModel> cars = db.GetAllModels<Plane>();
            /// 
            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    return session.Query<T>().ToList();
                }
            }
        }
        public void AddModel<T>(T model)
        {
            using (var ses = sessionFactory.OpenSession())
            {
                using (var trans = ses.BeginTransaction())
                {
                    ses.SaveOrUpdate(model);
                    trans.Commit();
                }
            }
        }


    }
}
