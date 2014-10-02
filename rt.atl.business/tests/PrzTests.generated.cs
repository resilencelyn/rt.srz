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
    public partial class PrzTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IPrzManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.atl.business.manager.IPrzManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.atl.model.atl.Prz CreateNewPrz()
		{
			rt.atl.model.atl.Prz entity = new rt.atl.model.atl.Prz();
			
			
			entity.Dedit = System.DateTime.Now;
			entity.Caption = "Test Test Test Test Test Test Test Test Test Tes";
			entity.Code = "Test Te";
			entity.Fullname = "Test Test ";
			entity.Ogrn = "Test ";
			entity.Bossname = "Test Test Test Test Test Test Test ";
			entity.Buhname = "Test Test Test Test Test";
			entity.Email = "Test Test Test Test Test Test Test T";
			entity.Tel1 = "Test Test ";
			entity.Tel2 = "Test Test ";
			entity.Addr = "Test Test ";
			entity.Okato = "T";
			entity.Extcode = "Test Te";
			entity.Main = true;
			
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
		protected rt.atl.model.atl.Prz GetFirstPrz()
        {
            IList<rt.atl.model.atl.Prz> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.atl.model.atl.Prz entity = CreateNewPrz();
				
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
                rt.atl.model.atl.Prz entityA = CreateNewPrz();
				manager.Save(entityA);

                rt.atl.model.atl.Prz entityB = manager.GetById(entityA.Id);

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
				rt.atl.model.atl.Prz entityC = CreateNewPrz();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Prz entityA = GetFirstPrz();
				
				entityA.Dedit = System.DateTime.Now;
				
				manager.Update(entityA);

                rt.atl.model.atl.Prz entityB = manager.GetById(entityA.Id);

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
			    rt.atl.model.atl.Prz entityC = CreateNewPrz();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Prz entity = GetFirstPrz();
				
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

