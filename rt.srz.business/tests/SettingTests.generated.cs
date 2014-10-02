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
    public partial class SettingTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<ISettingManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.ISettingManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.Setting CreateNewSetting()
		{
			rt.srz.model.srz.Setting entity = new rt.srz.model.srz.Setting();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.Name = "Test Test ";
			entity.ValueString = "Test Test ";
			
			using(rt.srz.business.manager.IOrganisationManager organisationManager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
				    var all = organisationManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Organisation = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IUserManager userManager = ObjectFactory.GetInstance<IUserManager>())
				{
				    var all = userManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.User = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IWorkstationManager workstationManager = ObjectFactory.GetInstance<IWorkstationManager>())
				{
				    var all = workstationManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Workstation = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.Setting GetFirstSetting()
        {
            IList<rt.srz.model.srz.Setting> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Setting entity = CreateNewSetting();
				
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
                rt.srz.model.srz.Setting entityA = CreateNewSetting();
				manager.Save(entityA);

                rt.srz.model.srz.Setting entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.Setting entityC = CreateNewSetting();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Setting entityA = GetFirstSetting();
				
				entityA.Name = "Test Test ";
				
				manager.Update(entityA);

                rt.srz.model.srz.Setting entityB = manager.GetById(entityA.Id);

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
			    rt.srz.model.srz.Setting entityC = CreateNewSetting();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Setting entity = GetFirstSetting();
				
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

