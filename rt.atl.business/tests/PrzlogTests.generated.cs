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
    public partial class PrzlogTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IPrzlogManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.atl.business.manager.IPrzlogManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.atl.model.atl.Przlog CreateNewPrzlog()
		{
			rt.atl.model.atl.Przlog entity = new rt.atl.model.atl.Przlog();
			
			
			entity.Filename = "Test Test ";
			entity.Q = "Tes";
			entity.Prz = "Tes";
			entity.Mm = 74;
			entity.Gg = 79;
			entity.Zz = 28;
			entity.Dtin = System.DateTime.Now;
			entity.Dtout = System.DateTime.Now;
			entity.Reccount = 57;
			entity.Tpfile = "Test Te";
			entity.Nerr = 22;
			entity.Nz = 29;
			entity.Errfile = "Test Test ";
			entity.St = 55;
			entity.Vers = "T";
			
			return entity;
		}
		protected rt.atl.model.atl.Przlog GetFirstPrzlog()
        {
            IList<rt.atl.model.atl.Przlog> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.atl.model.atl.Przlog entity = CreateNewPrzlog();
				
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
                rt.atl.model.atl.Przlog entityA = CreateNewPrzlog();
				manager.Save(entityA);

                rt.atl.model.atl.Przlog entityB = manager.GetById(entityA.Id);

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
				rt.atl.model.atl.Przlog entityC = CreateNewPrzlog();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Przlog entityA = GetFirstPrzlog();
				
				entityA.Filename = "Test Test Test Test Te";
				
				manager.Update(entityA);

                rt.atl.model.atl.Przlog entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Filename, entityB.Filename);
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
			    rt.atl.model.atl.Przlog entityC = CreateNewPrzlog();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Przlog entity = GetFirstPrzlog();
				
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

