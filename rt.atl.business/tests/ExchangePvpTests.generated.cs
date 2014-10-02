using System;
using System.Collections.Generic;
using System.Text;
using System.Text;
using NHibernate;
using NHibernate.Context;
using NUnit.Framework;
using StructureMap;
using rt.core.business.nhibernate;
using rt.core.business.registry;
using rt.atl.business.manager;
using rt.atl.model.atl;

namespace rt.atl.business.tests
{
  using rt.core.model;

  [TestFixture]
    public partial class ExchangePvpTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IExchangePvpManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.atl.business.manager.IExchangePvpManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.atl.model.atl.ExchangePvp CreateNewExchangePvp()
		{
			rt.atl.model.atl.ExchangePvp entity = new rt.atl.model.atl.ExchangePvp();
			
			
			entity.StatementId = System.Guid.NewGuid();
			entity.IsExport = true;
			entity.Error = "Test Test ";
			
			using(rt.atl.business.manager.IPrzbufManager przbufManager = ObjectFactory.GetInstance<IPrzbufManager>())
				{
				    var all = przbufManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.PrzBuff = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.atl.model.atl.ExchangePvp GetFirstExchangePvp()
        {
            IList<rt.atl.model.atl.ExchangePvp> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.atl.model.atl.ExchangePvp entity = CreateNewExchangePvp();
				
                object result = manager.Save(entity);

                Assert.IsNotNull(result);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        [Test]
        public void Read()
        {
            try
            {
                rt.atl.model.atl.ExchangePvp entityA = CreateNewExchangePvp();
				manager.Save(entityA);

                rt.atl.model.atl.ExchangePvp entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA, entityB);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
		[Test]
		public void Update()
        {
            try
            {
				rt.atl.model.atl.ExchangePvp entityC = CreateNewExchangePvp();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.ExchangePvp entityA = GetFirstExchangePvp();
				
				entityA.StatementId = System.Guid.NewGuid();
				
				manager.Update(entityA);

                rt.atl.model.atl.ExchangePvp entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.StatementId, entityB.StatementId);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        [Test]
        public void Delete()
        {
            try
            {
			    rt.atl.model.atl.ExchangePvp entityC = CreateNewExchangePvp();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.ExchangePvp entity = GetFirstExchangePvp();
				
                manager.Delete(entity);

                entity = manager.GetById(entity.Id);
                Assert.IsNull(entity);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
	}
}

