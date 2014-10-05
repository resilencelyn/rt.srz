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
    public partial class RepresentativeTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IRepresentativeManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.srz.business.manager.IRepresentativeManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.srz.model.srz.Representative CreateNewRepresentative()
		{
			rt.srz.model.srz.Representative entity = new rt.srz.model.srz.Representative();
			
			// You may need to maually enter this key if there is a constraint violation.
			entity.Id = System.Guid.NewGuid();
			
			entity.FirstName = "T";
			entity.LastName = "Test T";
			entity.MiddleName = "Test Test Test Test ";
			entity.HomePhone = "T";
			entity.WorkPhone = "Test Test Test Test Test Test Test Test";
			
			using(rt.srz.business.manager.IDocumentManager documentManager = ObjectFactory.GetInstance<IDocumentManager>())
				{
				    var all = documentManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.Document = all[0];
					}
				}	
			
			using(rt.srz.business.manager.IConceptManager conceptManager = ObjectFactory.GetInstance<IConceptManager>())
				{
				    var all = conceptManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.RelationType = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.srz.model.srz.Representative GetFirstRepresentative()
        {
            IList<rt.srz.model.srz.Representative> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.srz.model.srz.Representative entity = CreateNewRepresentative();
				
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
                rt.srz.model.srz.Representative entityA = CreateNewRepresentative();
				manager.Save(entityA);

                rt.srz.model.srz.Representative entityB = manager.GetById(entityA.Id);

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
				rt.srz.model.srz.Representative entityC = CreateNewRepresentative();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Representative entityA = GetFirstRepresentative();
				
				entityA.FirstName = "Test Test Test Test Test Test Test Test Test Tes";
				
				manager.Update(entityA);

                rt.srz.model.srz.Representative entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.FirstName, entityB.FirstName);
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
			    rt.srz.model.srz.Representative entityC = CreateNewRepresentative();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.srz.model.srz.Representative entity = GetFirstRepresentative();
				
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

