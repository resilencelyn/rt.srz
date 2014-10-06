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
    public partial class WorkstationTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IWorkstationManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IWorkstationManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.Workstation CreateNewWorkstation()
		{
			rt.srz.model.srz.Workstation entity = new rt.srz.model.srz.Workstation();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Name = "Test Test Test Test Test Test Test ";
			entity.UecReaderName = "Test Test ";
			entity.UecCerticateType = default(Byte);
			entity.SmardCardReaderName = "Test Test ";
			entity.SmardCardTokenReaderName = "Test Test ";
			
			using(rt.srz.business.manager.IOrganisationManager organisationManager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
				    var all = organisationManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.PointDistributionPolicy = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.Workstation GetFirstWorkstation()
        {
            IList<rt.srz.model.srz.Workstation> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Workstation entity = CreateNewWorkstation();
				
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
                rt.srz.model.srz.Workstation entityA = CreateNewWorkstation();
				manager.Save(entityA);

                rt.srz.model.srz.Workstation entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.Workstation entityC = CreateNewWorkstation();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Workstation entityA = GetFirstWorkstation();
				
				entityA.Name = "Test ";
				
				manager.Update(entityA);

                rt.srz.model.srz.Workstation entityB = manager.GetById(entityA.Id);

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
			    rt.srz.model.srz.Workstation entityC = CreateNewWorkstation();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Workstation entity = GetFirstWorkstation();
				
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

