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
    public partial class MedicalInsuranceTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IMedicalInsuranceManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IMedicalInsuranceManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.MedicalInsurance CreateNewMedicalInsurance()
		{
			rt.srz.model.srz.MedicalInsurance entity = new rt.srz.model.srz.MedicalInsurance();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.PolisSeria = "Test ";
			entity.PolisNumber = "Test Test Test Test Test Test Test Test Test ";
			entity.DateFrom = System.DateTime.Now;
			entity.DateTo = System.DateTime.Now;
			entity.IsActive = true;
			entity.DateStop = System.DateTime.Now;
			entity.Enp = "Tes";
			entity.StateDateFrom = System.DateTime.Now;
			entity.StateDateTo = System.DateTime.Now;
			
			using(rt.srz.business.manager.IInsuredPersonManager insuredPersonManager = ObjectFactory.GetInstance<IInsuredPersonManager>())
				{
				    var all = insuredPersonManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.InsuredPerson = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager conceptManager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = conceptManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.PolisType = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IOrganisationManager organisationManager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
				    var all = organisationManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Smo = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IStatementManager statementManager = ObjectFactory.GetInstance<IStatementManager>())
				{
				    var all = statementManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Statement = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.MedicalInsurance GetFirstMedicalInsurance()
        {
            IList<rt.srz.model.srz.MedicalInsurance> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.MedicalInsurance entity = CreateNewMedicalInsurance();
				
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
                rt.srz.model.srz.MedicalInsurance entityA = CreateNewMedicalInsurance();
				manager.Save(entityA);

                rt.srz.model.srz.MedicalInsurance entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.MedicalInsurance entityC = CreateNewMedicalInsurance();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.MedicalInsurance entityA = GetFirstMedicalInsurance();
				
				entityA.PolisSeria = "Test Tes";
				
				manager.Update(entityA);

                rt.srz.model.srz.MedicalInsurance entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.PolisSeria, entityB.PolisSeria);
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
			    rt.srz.model.srz.MedicalInsurance entityC = CreateNewMedicalInsurance();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.MedicalInsurance entity = GetFirstMedicalInsurance();
				
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

