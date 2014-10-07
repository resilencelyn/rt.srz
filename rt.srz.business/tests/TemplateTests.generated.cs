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
        }
        
        protected rt.srz.business.manager.ITemplateManager manager;
        
        protected ISession session { get; set; }
		
		public static Template CreateNew (int depth = 0)
		{
			rt.srz.model.srz.Template entity = new rt.srz.model.srz.Template();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
      entity.Name = "Test Test ";
      entity.PosSmo = "Test T";
      entity.PosAddress = "Test T";
      entity.PosDay1 = "Te";
      entity.PosMonth1 = "Test T";
      entity.PosYear1 = "Te";
      entity.PosBirthplace = "Test Test ";
      entity.PosMale = "Test Tes";
      entity.PosFemale = "Test T";
      entity.PosDay2 = "Test T";
      entity.PosMonth2 = "Test T";
      entity.PosYear2 = "Test T";
      entity.PosFio = "T";
      entity.PosLine1 = "Test Te";
      entity.PosLin2 = "Test Test T";
      entity.PosLine3 = "Test Tes";
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
				rt.srz.model.srz.Template entity = CreateNew();
				
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
                rt.srz.model.srz.Template entityA = CreateNew();
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
				rt.srz.model.srz.Template entityC = CreateNew();
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
			    rt.srz.model.srz.Template entityC = CreateNew();
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

