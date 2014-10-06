using System;
using System.Collections.Generic;
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
    public partial class VsdiapTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IVsdiapManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.atl.business.manager.IVsdiapManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.atl.model.atl.Vsdiap CreateNewVsdiap()
		{
			rt.atl.model.atl.Vsdiap entity = new rt.atl.model.atl.Vsdiap();
			
			
			entity.Dedit = System.DateTime.Now;
			entity.Lo = 61;
			entity.Hi = 22;
			
			using(rt.atl.business.manager.ISmoManager smoManager = ObjectFactory.GetInstance<ISmoManager>())
				{
				    var all = smoManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.SMO = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.atl.model.atl.Vsdiap GetFirstVsdiap()
        {
            IList<rt.atl.model.atl.Vsdiap> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.atl.model.atl.Vsdiap entity = CreateNewVsdiap();
				
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
                rt.atl.model.atl.Vsdiap entityA = CreateNewVsdiap();
				manager.Save(entityA);

                rt.atl.model.atl.Vsdiap entityB = manager.GetById(entityA.Id);

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
				rt.atl.model.atl.Vsdiap entityC = CreateNewVsdiap();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Vsdiap entityA = GetFirstVsdiap();
				
				entityA.Dedit = System.DateTime.Now;
				
				manager.Update(entityA);

                rt.atl.model.atl.Vsdiap entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Dedit, entityB.Dedit);
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
			    rt.atl.model.atl.Vsdiap entityC = CreateNewVsdiap();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Vsdiap entity = GetFirstVsdiap();
				
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

