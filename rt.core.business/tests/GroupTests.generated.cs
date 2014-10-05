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
using rt.core.model;
using rt.core.business.manager;
using rt.core.model.core;

namespace rt.core.business.tests
{
	[TestFixture]
    public partial class GroupTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IGroupManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.core.business.manager.IGroupManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.core.model.core.Group CreateNewGroup()
		{
			rt.core.model.core.Group entity = new rt.core.model.core.Group();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Name = "Test Test Test Test Test Test Test Test ";
			
			return entity;
		}
		protected rt.core.model.core.Group GetFirstGroup()
        {
            IList<rt.core.model.core.Group> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.core.model.core.Group entity = CreateNewGroup();
				
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
                rt.core.model.core.Group entityA = CreateNewGroup();
				manager.Save(entityA);

                rt.core.model.core.Group entityB = manager.GetById(entityA.Id);

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
				rt.core.model.core.Group entityC = CreateNewGroup();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.core.model.core.Group entityA = GetFirstGroup();
				
				entityA.Name = "Test Test Test Test Test Test Test Test Test T";
				
				manager.Update(entityA);

                rt.core.model.core.Group entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Name, entityB.Name);
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
			    rt.core.model.core.Group entityC = CreateNewGroup();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.core.model.core.Group entity = GetFirstGroup();
				
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

