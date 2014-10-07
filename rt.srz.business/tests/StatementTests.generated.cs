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
    public partial class StatementTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IStatementManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
        }
        
        protected rt.srz.business.manager.IStatementManager manager;
        
        protected ISession session { get; set; }
		
		public static Statement CreateNew (int depth = 0)
		{
			rt.srz.model.srz.Statement entity = new rt.srz.model.srz.Statement();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
      entity.DateFiling = System.DateTime.Now;
      entity.HasPetition = true;
      entity.NumberPolicy = "Test Test Test Test Test T";
      entity.AbsentPrevPolicy = true;
      entity.IsActive = true;
      entity.PolicyIsIssued = true;
      entity.PrzBuffId = 46;
      entity.PidId = 10;
      entity.PolisId = 77;
      entity.IsExportTemp = true;
      entity.IsExportPolis = true;
      entity.PrzBuffPolisId = 57;
      entity.Version = 28;
      entity.UserId = new Guid("01000000-0000-0000-0000-000000000000");
			
			using(rt.srz.business.manager.IOrganisationManager organisationManager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
           entity.PointDistributionPolicy = null;
				}	
			
			using(rt.srz.business.manager.IStatementManager statementMemberManager = ObjectFactory.GetInstance<IStatementManager>())
				{
           entity.PreviousStatement = null;
				}	
			
			using(rt.srz.business.manager.IConceptManager concept1Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept1Manager.GetAll(1);
            Concept entityRef = null;
					  if (all.Count > 0)
					  {
              entityRef = all[0];
					  }
          
					 if (entityRef == null && depth < 3)
           {
             depth++;
             entityRef = ConceptTests.CreateNew(depth);
             ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Save(entityRef);
           }
           
					 entity.Status = entityRef ;
				}	
			
			using(rt.srz.business.manager.IInsuredPersonDatumManager insuredPersonDatumManager = ObjectFactory.GetInstance<IInsuredPersonDatumManager>())
				{
           entity.InsuredPersonData = null;
				}	
			
			using(rt.srz.business.manager.IInsuredPersonManager insuredPersonManager = ObjectFactory.GetInstance<IInsuredPersonManager>())
				{
           entity.InsuredPerson = null;
				}	
			
			using(rt.srz.business.manager.IConceptManager concept2Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
           entity.CauseFiling = null;
				}	
			
			using(rt.srz.business.manager.IConceptManager concept3Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
           entity.ModeFiling = null;
				}	
			
			using(rt.srz.business.manager.IConceptManager concept4Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
           entity.FormManufacturing = null;
				}	
			
			using(rt.srz.business.manager.IDocumentManager document1Manager = ObjectFactory.GetInstance<IDocumentManager>())
				{
           entity.DocumentUdl = null;
				}	
			
			using(rt.srz.business.manager.IContactInfoManager contactInfoManager = ObjectFactory.GetInstance<IContactInfoManager>())
				{
           entity.ContactInfo = null;
				}	
			
			using(rt.srz.business.manager.IRepresentativeManager representativeManager = ObjectFactory.GetInstance<IRepresentativeManager>())
				{
           entity.Representative = null;
				}	
			
			using(rt.srz.business.manager.IDocumentManager document2Manager = ObjectFactory.GetInstance<IDocumentManager>())
				{
           entity.ResidencyDocument = null;
				}	
			
			using(rt.srz.business.manager.IaddressManager address1Manager = ObjectFactory.GetInstance<IaddressManager>())
				{
           entity.Address = null;
				}	
			
			using(rt.srz.business.manager.IDocumentManager document3Manager = ObjectFactory.GetInstance<IDocumentManager>())
				{
           entity.DocumentRegistration = null;
				}	
			
			using(rt.srz.business.manager.IaddressManager address2Manager = ObjectFactory.GetInstance<IaddressManager>())
				{
           entity.Address2 = null;
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.Statement GetFirstStatement()
        {
            IList<rt.srz.model.srz.Statement> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Statement entity = CreateNew();
				
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
                rt.srz.model.srz.Statement entityA = CreateNew();
				manager.Save(entityA);

                rt.srz.model.srz.Statement entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.Statement entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Statement entityA = GetFirstStatement();
				
				entityA.DateFiling = System.DateTime.Now;
				
				manager.Update(entityA);

                rt.srz.model.srz.Statement entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.DateFiling, entityB.DateFiling);
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
			    rt.srz.model.srz.Statement entityC = CreateNew();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Statement entity = GetFirstStatement();
				
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

