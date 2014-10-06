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
    public partial class ImTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IImManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.atl.business.manager.IImManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.atl.model.atl.Im CreateNewIm()
		{
			rt.atl.model.atl.Im entity = new rt.atl.model.atl.Im();
			
			
			entity.Caption = "Test Test Tes";
			entity.W = 71;
			
			return entity;
		}
		protected rt.atl.model.atl.Im GetFirstIm()
        {
            IList<rt.atl.model.atl.Im> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.atl.model.atl.Im entity = CreateNewIm();
				
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
                rt.atl.model.atl.Im entityA = CreateNewIm();
				manager.Save(entityA);

                rt.atl.model.atl.Im entityB = manager.GetById(entityA.Id);

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
				rt.atl.model.atl.Im entityC = CreateNewIm();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Im entityA = GetFirstIm();
				
				entityA.Caption = "Test ";
				
				manager.Update(entityA);

                rt.atl.model.atl.Im entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Caption, entityB.Caption);
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
			    rt.atl.model.atl.Im entityC = CreateNewIm();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Im entity = GetFirstIm();
				
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

