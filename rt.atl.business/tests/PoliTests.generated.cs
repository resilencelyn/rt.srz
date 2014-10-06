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
    public partial class PoliTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IPoliManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.atl.business.manager.IPoliManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.atl.model.atl.Poli CreateNewPoli()
		{
			rt.atl.model.atl.Poli entity = new rt.atl.model.atl.Poli();
			
			
			entity.Dedit = System.DateTime.Now;
			entity.Q = "Test";
			entity.Prz = "T";
			entity.Dbeg = System.DateTime.Now;
			entity.Dend = System.DateTime.Now;
			entity.Poltp = 67;
			entity.Okato = "Test Test";
			entity.Spol = "Test T";
			entity.Npol = "Test Tes";
			entity.Qogrn = "Test Te";
			entity.Dstop = System.DateTime.Now;
			entity.St = 4;
			entity.Del = true;
			entity.Rstop = 1;
			entity.Nvs = "Test T";
			entity.Dvs = System.DateTime.Now;
			entity.Et = System.DateTime.Now;
			entity.Dz = System.DateTime.Now;
			entity.Dp = System.DateTime.Now;
			entity.Dh = System.DateTime.Now;
			entity.Err = "Test Test ";
			entity.Polvid = 9;
			entity.Oldpid = 63;
			entity.Sout = System.DateTime.Now;
			entity.M2id = 47;
			entity.DstopCs = System.DateTime.Now;
			entity.Polis = 59;
			
			using(rt.atl.business.manager.IpersonManager personManager = ObjectFactory.GetInstance<IpersonManager>())
				{
				    var all = personManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.P = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.atl.model.atl.Poli GetFirstPoli()
        {
            IList<rt.atl.model.atl.Poli> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.atl.model.atl.Poli entity = CreateNewPoli();
				
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
                rt.atl.model.atl.Poli entityA = CreateNewPoli();
				manager.Save(entityA);

                rt.atl.model.atl.Poli entityB = manager.GetById(entityA.Id);

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
				rt.atl.model.atl.Poli entityC = CreateNewPoli();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Poli entityA = GetFirstPoli();
				
				entityA.Dedit = System.DateTime.Now;
				
				manager.Update(entityA);

                rt.atl.model.atl.Poli entityB = manager.GetById(entityA.Id);

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
			    rt.atl.model.atl.Poli entityC = CreateNewPoli();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Poli entity = GetFirstPoli();
				
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

