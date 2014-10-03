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
using rt.srz.business.manager;
using rt.srz.model.srz;

namespace rt.srz.business.tests
{
	[TestFixture]
    public partial class TemplateTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<ITemplateManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.ITemplateManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.Template CreateNewTemplate()
		{
			rt.srz.model.srz.Template entity = new rt.srz.model.srz.Template();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Name = "Test Test ";
			entity.PosSmo = "Test Te";
			entity.PosAddress = "Te";
			entity.PosDay1 = "Test";
			entity.PosMonth1 = "Test Test ";
			entity.PosYear1 = "Test Test ";
			entity.PosBirthplace = "Test Tes";
			entity.PosMale = "Test Te";
			entity.PosFemale = "Tes";
			entity.PosDay2 = "Test ";
			entity.PosMonth2 = "Te";
			entity.PosYear2 = "Test Tes";
			entity.PosFio = "Test Test T";
			entity.PosLine1 = "Test Te";
			entity.PosLin2 = "Test Test Test";
			entity.PosLine3 = "Test Test T";
			entity.Default = true;
			
			return entity;
		}
		protected rt.srz.model.srz.Template GetFirstTemplate()
        {
            IList<rt.srz.model.srz.Template> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Template entity = CreateNewTemplate();
				
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
                rt.srz.model.srz.Template entityA = CreateNewTemplate();
				manager.Save(entityA);

                rt.srz.model.srz.Template entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.Template entityC = CreateNewTemplate();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Template entityA = GetFirstTemplate();
				
				entityA.Name = "Test Test ";
				
				manager.Update(entityA);

                rt.srz.model.srz.Template entityB = manager.GetById(entityA.Id);

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
			    rt.srz.model.srz.Template entityC = CreateNewTemplate();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Template entity = GetFirstTemplate();
				
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

