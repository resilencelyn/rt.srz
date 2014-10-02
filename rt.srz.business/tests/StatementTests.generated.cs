﻿using System;
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
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IStatementManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.Statement CreateNewStatement()
		{
			rt.srz.model.srz.Statement entity = new rt.srz.model.srz.Statement();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.DateFiling = System.DateTime.Now;
			entity.HasPetition = true;
			entity.NumberPolicy = "Test Test Test Te";
			entity.AbsentPrevPolicy = true;
			entity.IsActive = true;
			entity.PolicyIsIssued = true;
			entity.PrzBuffId = 65;
			entity.PidId = 72;
			entity.PolisId = 59;
			entity.IsExportTemp = true;
			entity.IsExportPolis = true;
			entity.PrzBuffPolisId = 51;
			entity.Version = 5;
			
			using(rt.srz.business.manager.IOrganisationManager organisationManager = ObjectFactory.GetInstance<IOrganisationManager>())
				{
				    var all = organisationManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.PointDistributionPolicy = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IStatementManager statementMemberManager = ObjectFactory.GetInstance<IStatementManager>())
				{
				    var all = statementMemberManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.PreviousStatement = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept1Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept1Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Status = all[0];
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
			
			using(rt.srz.business.manager.IInsuredPersonDatumManager insuredPersonDatumManager = ObjectFactory.GetInstance<IInsuredPersonDatumManager>())
				{
				    var all = insuredPersonDatumManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.InsuredPersonData = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IInsuredPersonManager insuredPersonManager = ObjectFactory.GetInstance<IInsuredPersonManager>())
				{
				    var all = insuredPersonManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.InsuredPerson = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept2Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept2Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.CauseFiling = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept3Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept3Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.ModeFiling = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager concept4Manager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = concept4Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.FormManufacturing = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IDocumentManager document1Manager = ObjectFactory.GetInstance<IDocumentManager>())
				{
				    var all = document1Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.DocumentUdl = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IContactInfoManager contactInfoManager = ObjectFactory.GetInstance<IContactInfoManager>())
				{
				    var all = contactInfoManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.ContactInfo = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IRepresentativeManager representativeManager = ObjectFactory.GetInstance<IRepresentativeManager>())
				{
				    var all = representativeManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Representative = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IDocumentManager document2Manager = ObjectFactory.GetInstance<IDocumentManager>())
				{
				    var all = document2Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.ResidencyDocument = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IaddressManager address1Manager = ObjectFactory.GetInstance<IaddressManager>())
				{
				    var all = address1Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Address = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IDocumentManager document3Manager = ObjectFactory.GetInstance<IDocumentManager>())
				{
				    var all = document3Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.DocumentRegistration = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IaddressManager address2Manager = ObjectFactory.GetInstance<IaddressManager>())
				{
				    var all = address2Manager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Address2 = all[0];
					}
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
				rt.srz.model.srz.Statement entity = CreateNewStatement();
				
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
                rt.srz.model.srz.Statement entityA = CreateNewStatement();
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
				rt.srz.model.srz.Statement entityC = CreateNewStatement();
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
			    rt.srz.model.srz.Statement entityC = CreateNewStatement();
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
