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
        }
        
        protected rt.atl.business.manager.IPrzlogManager manager;
        
        protected ISession session { get; set; }
		
		public static Przlog CreateNew (int depth = 0)
		{
			rt.atl.model.atl.Przlog entity = new rt.atl.model.atl.Przlog();
			
			
      entity.Filename = "Test Test Test Test Te";
      entity.Q = "Tes";
      entity.Prz = "Test T";
      entity.Mm = 45;
      entity.Gg = 40;
      entity.Zz = 73;
      entity.Dtin = System.DateTime.Now;
      entity.Dtout = System.DateTime.Now;
      entity.Reccount = 27;
      entity.Tpfile = "Test T";
      entity.Nerr = 29;
      entity.Nz = 77;
      entity.Errfile = "Test Test ";
      entity.St = 86;
      entity.Vers = "Te";
			
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
				rt.atl.model.atl.Przlog entity = CreateNew();
				
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
                rt.atl.model.atl.Przlog entityA = CreateNew();
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
				rt.atl.model.atl.Przlog entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Przlog entityA = GetFirstPrzlog();
				
				entityA.Filename = "Test Test Test Test Test Test Test Test Test T";
				
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
			    rt.atl.model.atl.Przlog entityC = CreateNew();
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

