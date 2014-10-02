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
using rt.atl.business.manager;
using rt.atl.model.atl;

namespace rt.atl.business.tests
{
  using rt.core.model;

  [TestFixture]
    public partial class PrzbufTests : UnitTestbase
    {
        [SetUp]
        public void SetUp()
        {
            Bootstrapper.Bootstrap();
            session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();
            CurrentSessionContext.Bind(session);
            manager = ObjectFactory.GetInstance<IPrzbufManager>();
            manager.Session.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            manager.Session.RollbackTransaction();
            manager.Dispose();
        }
        
        protected rt.atl.business.manager.IPrzbufManager manager;
        
        protected ISession session { get; set; }
		
		protected rt.atl.model.atl.Przbuf CreateNewPrzbuf()
		{
			rt.atl.model.atl.Przbuf entity = new rt.atl.model.atl.Przbuf();
			
			
			entity.Fam = "Test Test Test Test Test Tes";
			entity.Im = "Test Test Test Test Test Test Test Test";
			entity.Ot = "Test Test Test ";
			entity.W = 11;
			entity.Dr = System.DateTime.Now;
			entity.Dra = true;
			entity.Mr = "Test Test ";
			entity.Ds = System.DateTime.Now;
			entity.Ss = "Test T";
			entity.Doctp = "T";
			entity.Docs = "Test Test Tes";
			entity.Docn = "Test Tes";
			entity.Docdt = System.DateTime.Now;
			entity.Docorg = "Test Test ";
			entity.Cn = "Te";
			entity.Subj = "Te";
			entity.Rn = "Test Tes";
			entity.Indx = "Test ";
			entity.Rnname = "Test Test Test Test Test Test Test Test Test Test Test Test Test Tes";
			entity.City = "T";
			entity.Np = "Test Test Test Test Test Test Test Test Test Test Te";
			entity.Ul = "Test Test T";
			entity.Dom = "T";
			entity.Kor = "Test ";
			entity.Kv = "Test ";
			entity.Dmj = System.DateTime.Now;
			entity.Bomj = true;
			entity.Psubj = "Test";
			entity.Prn = "Test ";
			entity.Pindx = "Tes";
			entity.Prnname = "Test Test Test Test";
			entity.Pcity = "Test Test Test Test Test Test Test Test Test Test Test Test Te";
			entity.Pnp = "Test Test Test Test Test Test Test ";
			entity.Pul = "Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test";
			entity.Pdom = "Test Test";
			entity.Pkor = "Test T";
			entity.Pkv = "Test Test";
			entity.Pdmj = System.DateTime.Now;
			entity.Rdoctp = "Te";
			entity.Rdocs = "Te";
			entity.Rdocn = "Te";
			entity.Rdocdt = System.DateTime.Now;
			entity.Rdocorg = "Test Test ";
			entity.Rdocend = System.DateTime.Now;
			entity.Email = "T";
			entity.Q = "Tes";
			entity.Prz = "Test";
			entity.Enp = "Test Test Test ";
			entity.Opdoc = 10;
			entity.Okato = "T";
			entity.Dhpol = System.DateTime.Now;
			entity.Dbeg = System.DateTime.Now;
			entity.Dend = System.DateTime.Now;
			entity.Petition = true;
			entity.Lpu = "Test ";
			entity.Lpuwk = "Test Test T";
			entity.Lpust = "Test";
			entity.Lpuuch = 31;
			entity.Sp = "T";
			entity.Kt = "T";
			entity.Okved = "T";
			entity.Kladrs = "Test Test";
			entity.Zfam = "Tes";
			entity.Zim = "Test Test Test Test Test Test Test Te";
			entity.Zot = "Test Test Test Test Test Te";
			entity.Zt = 26;
			entity.Zdr = System.DateTime.Now;
			entity.Zmr = "Test Test ";
			entity.Zdoctp = "Te";
			entity.Zdocs = "Test Test";
			entity.Zdocn = "Test Test Test ";
			entity.Zdocdt = System.DateTime.Now;
			entity.Zdocorg = "Test Test ";
			entity.Zaddr = "Test Test ";
			entity.Pid = 54;
			entity.Op = "Tes";
			entity.Oldfam = "Test Test Test Test Test Test Test";
			entity.Oldim = "Test Test Test Test Te";
			entity.Oldot = "Test Test Test Test Test Test Test Tes";
			entity.Olddr = System.DateTime.Now;
			entity.Oldw = 17;
			entity.Oldmr = "Test Test ";
			entity.Olddoctp = "Te";
			entity.Olddocs = "Test Test Test Te";
			entity.Olddocn = "Test Test Test";
			entity.Olddocdt = System.DateTime.Now;
			entity.Sflk = "Test Test ";
			entity.Eerp = "Test Test ";
			entity.St = 73;
			entity.Repl = "Test Test ";
			entity.Num = "Test Test Test Test Te";
			entity.Opid = 75;
			entity.Opsmo = 84;
			entity.Oppol = 31;
			entity.Polpr = 12;
			entity.Polvid = 15;
			entity.Rescode = 91;
			entity.Result = "Test Test ";
			entity.Phone = "Test Te";
			entity.Zphone = "Tes";
			entity.Spol = "Test T";
			entity.Npol = "Test";
			entity.Qogrn = "Tes";
			entity.Dstop = System.DateTime.Now;
			entity.Dviz = System.DateTime.Now;
			entity.Meth = 17;
			entity.Eins = 8;
			entity.Polisid = 27;
			entity.Rstop = 39;
			entity.Et = 40;
			entity.At = 69;
			entity.Del = true;
			entity.Zp1id = 15;
			entity.A08id = 30;
			entity.Vpdid = 27;
			entity.Photo = "Test Test ";
			entity.Ptype = "T";
			entity.Oldenp = "Tes";
			entity.Drt = 63;
			entity.Fiopr = "Test Test ";
			entity.Contact = "Test Test ";
			entity.Nord = "Test Test ";
			entity.Dord = System.DateTime.Now;
			entity.Erp = true;
			entity.Dedit = System.DateTime.Now;
			entity.Lock = true;
			entity.Signat = "Test";
			entity.Force = true;
			entity.Olddocorg = "Test Test ";
			entity.Dz = System.DateTime.Now;
			entity.Lpuauto = 39;
			entity.Lpudt = System.DateTime.Now;
			entity.Lpudx = System.DateTime.Now;
			entity.Kladrg = "Test Test Test ";
			entity.Kladrp = "Test Test ";
			entity.Mid = 57;
			entity.Oldss = "Test Tes";
			entity.Dost = "Test";
			entity.Docend = System.DateTime.Now;
			entity.BirthOksm = "T";
			entity.Kateg = 28;
			entity.DstopCs = System.DateTime.Now;
			entity.OldEnp = "Test Test Test ";
			entity.Olddocend = System.DateTime.Now;
			entity.Oldrdoctp = "Te";
			entity.Oldrdocs = "Test Test Test ";
			entity.Oldrdocn = "Te";
			entity.Oldrdocdt = System.DateTime.Now;
			entity.Oldrdocorg = "Test Test ";
			entity.Oldrdocend = System.DateTime.Now;
			
			using(rt.atl.business.manager.IPrzlogManager przlogManager = ObjectFactory.GetInstance<IPrzlogManager>())
				{
				    var all = przlogManager.GetAll(1);
					if (all.Count > 0)
					{
						entity.PRZLOG = all[0];
					}
				}	
			
			return entity;
		}
		protected rt.atl.model.atl.Przbuf GetFirstPrzbuf()
        {
            IList<rt.atl.model.atl.Przbuf> entityList = manager.GetAll(1);
            if (entityList.Count == 0)
                Assert.Fail("All tables must have at least one row for unit tests to succeed.");
            return entityList[0];
        }
		
		[Test]
        public void Create()
        {
            try
            {
				rt.atl.model.atl.Przbuf entity = CreateNewPrzbuf();
				
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
                rt.atl.model.atl.Przbuf entityA = CreateNewPrzbuf();
				manager.Save(entityA);

                rt.atl.model.atl.Przbuf entityB = manager.GetById(entityA.Id);

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
				rt.atl.model.atl.Przbuf entityC = CreateNewPrzbuf();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Przbuf entityA = GetFirstPrzbuf();
				
				entityA.Fam = "Test Test Test Test Test Test ";
				
				manager.Update(entityA);

                rt.atl.model.atl.Przbuf entityB = manager.GetById(entityA.Id);

                Assert.AreEqual(entityA.Fam, entityB.Fam);
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
			    rt.atl.model.atl.Przbuf entityC = CreateNewPrzbuf();
				manager.Save(entityC);
				manager.Session.GetISession().Flush();
				manager.Session.GetISession().Clear();
			
                rt.atl.model.atl.Przbuf entity = GetFirstPrzbuf();
				
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

