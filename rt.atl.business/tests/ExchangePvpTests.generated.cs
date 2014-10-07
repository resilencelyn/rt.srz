using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Context;
using NUnit.Framework;
using StructureMap;
using rt.core.business.nhibernate;
using rt.core.business.registry;
using rt.core.model;
using rt.atl.business.manager;
using rt.atl.model.atl;

namespace rt.atl.business.tests
{
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
        }
        
        protected rt.atl.business.manager.IExchangePvpManager manager;
        
        protected ISession session { get; set; }
		
		public static ExchangePvp CreateNew (int depth = 0)
		{
			rt.atl.model.atl.ExchangePvp entity = new rt.atl.model.atl.ExchangePvp();
			
			
      entity.StatementId = "Test Test T";
      entity.IsExport = true;
      entity.Error = "Test Test ";
			
			using(rt.atl.business.manager.IPrzbufManager przbufManager = ObjectFactory.GetInstance<IPrzbufManager>())
				{
				    var all = przbufManager.GetAll(1);
            Przbuf entityRef = null;
					  if (all.Count > 0)
					  {
              entityRef = all[0];
					  }
          
					 if (entityRef == null && depth < 3)
           {
             depth++;
             entityRef = PrzbufTests.CreateNew(depth);
             ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Save(entityRef);
           }
           
					 entity.PrzBuff = entityRef ;
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
				rt.atl.model.atl.ExchangePvp entity = CreateNew();
				
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
                rt.atl.model.atl.ExchangePvp entityA = CreateNew();
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
				rt.atl.model.atl.ExchangePvp entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.ExchangePvp entityA = GetFirstExchangePvp();
				
				entityA.StatementId = "Test Test Test T";
				
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
			    rt.atl.model.atl.ExchangePvp entityC = CreateNew();
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

