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
    public partial class UecTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IUecManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.atl.business.manager.IUecManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.atl.model.atl.Uec CreateNewUec()
		{
			rt.atl.model.atl.Uec entity = new rt.atl.model.atl.Uec();
			
			
			entity.Ncard = "Test T";
			entity.Ufile = 99;
			
			using(rt.atl.business.manager.IpersonManager personManager = ObjectFactory.GetInstance<IpersonManager>())
				{
				    var all = personManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.P = all[0];
					}
				}	
			
			using(rt.atl.business.manager.IPoliManager poliManager = ObjectFactory.GetInstance<IPoliManager>())
				{
				    var all = poliManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.POLIS = all[0];
					}
				}	
			
			using(rt.atl.business.manager.IUechiststatusManager uechiststatusManager = ObjectFactory.GetInstance<IUechiststatusManager>())
				{
				    var all = uechiststatusManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.UECLASTSTATUS = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.atl.model.atl.Uec GetFirstUec()
        {
            IList<rt.atl.model.atl.Uec> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.atl.model.atl.Uec entity = CreateNewUec();
				
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
                rt.atl.model.atl.Uec entityA = CreateNewUec();
				manager.Save(entityA);

                rt.atl.model.atl.Uec entityB = manager.GetById(entityA.Id);

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
				rt.atl.model.atl.Uec entityC = CreateNewUec();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Uec entityA = GetFirstUec();
				
				entityA.Ncard = "Test Test Test Te";
				
				manager.Update(entityA);

                rt.atl.model.atl.Uec entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Ncard, entityB.Ncard);
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
			    rt.atl.model.atl.Uec entityC = CreateNewUec();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Uec entity = GetFirstUec();
				
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

