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
    public partial class OrganisationTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IOrganisationManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
        }
        
        protected rt.srz.business.manager.IOrganisationManager manager;
        
        protected ISession session { get; set; }
		
		public static Organisation CreateNew (int depth = 0)
		{
			rt.srz.model.srz.Organisation entity = new rt.srz.model.srz.Organisation();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
      entity.IsActive = true;
      entity.IsOnLine = true;
      entity.Code = "Test Tes";
      entity.FullName = "Test Test ";
      entity.ShortName = "Test Test ";
      entity.Inn = "Test T";
      entity.Ogrn = "Test Test Test Test";
      entity.Postcode = "Test T";
      entity.LastName = "Test Test ";
      entity.FirstName = "Test Test ";
      entity.MiddleName = "Test Test ";
      entity.Phone = "Test Test ";
      entity.Fax = "Test Test ";
      entity.EMail = "Test Test ";
      entity.Website = "Test Test ";
      entity.LicenseData = "Test Test ";
      entity.LicenseNumber = "Test Test Test Test Test Test Test Test Test Te";
      entity.DateLicensing = System.DateTime.Now;
      entity.DateExpiryLicense = System.DateTime.Now;
      entity.IsSubordination = true;
      entity.DateIncludeRegister = System.DateTime.Now;
      entity.DateExcludeRegister = System.DateTime.Now;
      entity.HasActivePolicy = true;
      entity.DateNotification = System.DateTime.Now;
      entity.CountInsured = 21;
      entity.DateLastEdit = System.DateTime.Now;
      entity.Okato = "Test";
      entity.TimeRunFrom = System.DateTime.Now;
      entity.TimeRunTo = System.DateTime.Now;
      entity.Address = "Test Test ";
			
			using(rt.srz.business.manager.IOidManager oidManager = ObjectFactory.GetInstance<IOidManager>())
				{
				    var all = oidManager.GetAll(1);
            Oid entityRef = null;
					  if (all.Count > 0)
					  {
              entityRef = all[0];
					  }
          
					 if (entityRef == null && depth < 3)
           {
             depth++;
             entityRef = OidTests.CreateNew(depth);
             ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Save(entityRef);
           }
           
					 entity.Oid = entityRef ;
				}	
			
			using(rt.srz.business.manager.IOrganisationManager organisationMember1Manager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
           entity.Parent = null;
				}	
			
			using(rt.srz.business.manager.IOrganisationManager organisationMember2Manager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
           entity.ChangedRow = null;
				}	
			
			using(rt.srz.business.manager.IConceptManager concept1Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
           entity.CauseRegistration = null;
				}	
			
			using(rt.srz.business.manager.IConceptManager concept2Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
           entity.CauseExclusion = null;
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.Organisation GetFirstOrganisation()
        {
            IList<rt.srz.model.srz.Organisation> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Organisation entity = CreateNew();
				
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
                rt.srz.model.srz.Organisation entityA = CreateNew();
				manager.Save(entityA);

                rt.srz.model.srz.Organisation entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.Organisation entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Organisation entityA = GetFirstOrganisation();
				
				entityA.IsActive = true;
				
				manager.Update(entityA);

                rt.srz.model.srz.Organisation entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.IsActive, entityB.IsActive);
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
			    rt.srz.model.srz.Organisation entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Organisation entity = GetFirstOrganisation();
				
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

