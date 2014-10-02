/****** Object:  View [dbo].[PEOPLE_T]    Script Date: 07/31/2014 11:45:18 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PEOPLE_T]'))
DROP VIEW [dbo].[PEOPLE_T]
GO


/****** Object:  View [dbo].[PEOPLE_T]    Script Date: 07/31/2014 11:45:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE VIEW [dbo].[PEOPLE_T] WITH SCHEMABINDING
AS
SELECT     pb.Number
          , - pb.ID AS id
          , pb.PID
          , (SELECT     TOP (1) polis.ID
                            FROM          dbo.PRZBUFT AS p6 INNER JOIN
                               dbo.POLIS AS polis ON polis.PID = p6.PID 
                                                 AND polis.POLTP = p6.OPDOC 
                                                 AND ISNULL(polis.POLVID, 0) = ISNULL(p6.POLVID, 0) 
                                                 AND polis.NPOL = p6.NPOL 
                                                 AND ISNULL(polis.SPOL, '') = ISNULL(p6.SPOL, '') 
                                                 AND polis.Q = p6.Q 
                                                 AND ISNULL(polis.PRZ, '') = p6.PRZ
                            WHERE      (pb.PID = p6.PID) 
                                      AND (pb.DVIZ <= p6.DVIZ) 
                                      AND (p6.OP = 'Ï060' OR p6.OP = 'Ï070')
                            ORDER BY p6.DVIZ, polis.ID DESC) AS POLISNEW
         , pb.POLVID
         , case when pb.number = 1 then p.FAM else pb.FAM end AS FAM
         , case when pb.number = 1 then p.IM else pb.IM end AS IM
         , case when pb.number = 1 then p.OT else pb.OT end AS OT
         , case when pb.number = 1 then p.W else pb.W end AS W
         , case when pb.number = 1 then p.DOST else pb.DOST end AS DOST
         , case when pb.number = 1 then p.DR else pb.DR end AS DR
         , case when pb.number = 1 then p.DRA else pb.DRA end AS DRA
         , case when pb.number = 1 then p.DRT else pb.DRT end AS DRT
         , case when pb.number = 1 then p.MR else pb.MR end AS MR
         , case when pb.number = 1 then p.DS else pb.DS end AS DS
         , case when pb.number = 1 then isnull(p.SS, pb.ss) else isnull(pb.SS, p.ss) end AS SS
         , case when pb.number = 1 then p.DOCTP else pb.DOCTP end AS DOCTP
         , case when pb.number = 1 then p.DOCS else pb.DOCS end AS DOCS
         , case when pb.number = 1 then p.DOCN else pb.DOCN end AS DOCN
         , case when pb.number = 1 then p.DOCDT else pb.DOCDT end AS DOCDT
         , case when pb.number = 1 then p.DOCORG else pb.DOCORG end AS DOCORG
         , case when pb.number = 1 then p.DOCEND else pb.DOCEND end AS DOCEND
         , case when pb.number = 1 then p.RDOCTP else pb.RDOCTP end AS RDOCTP
         , case when pb.number = 1 then p.RDOCS else pb.RDOCS end AS RDOCS
         , case when pb.number = 1 then p.RDOCN else pb.RDOCN end AS RDOCN
         , case when pb.number = 1 then p.RDOCDT else pb.RDOCDT end AS RDOCDT
         , case when pb.number = 1 then p.RDOCORG else pb.RDOCORG end AS RDOCORG
         , case when pb.number = 1 then p.RDOCEND else pb.RDOCEND end AS RDOCEND
         , case when pb.number = 1 then isnull(p.BIRTH_OKSM, pb.BIRTH_OKSM) else pb.BIRTH_OKSM end AS BIRTH_OKSM
         , case when pb.number = 1 then p.CN else pb.CN end AS CN
         , case when pb.number = 1 then p.SUBJ else pb.SUBJ end AS SUBJ
         , case when pb.number = 1 then p.RN else pb.RN end AS RN
         , case when pb.number = 1 then p.INDX else pb.INDX end AS INDX
         , case when pb.number = 1 then p.RNNAME else pb.RNNAME end AS RNNAME
         , case when pb.number = 1 then p.CITY else pb.CITY end AS CITY
         , case when pb.number = 1 then p.NP else pb.NP end AS NP
         , case when pb.number = 1 then p.UL else pb.UL end AS UL
         , case when pb.number = 1 then p.DOM else pb.DOM end AS DOM
         , case when pb.number = 1 then p.KOR else pb.KOR end AS KOR
         , case when pb.number = 1 then p.KV else pb.KV end AS KV
         , case when pb.number = 1 then p.DMJ else pb.DMJ end AS DMJ
         , case when pb.number = 1 then p.BOMJ else pb.BOMJ end AS BOMJ
         , case when pb.number = 1 then p.KATEG else pb.KATEG end AS KATEG
         , case when pb.number = 1 then p.PSUBJ else pb.PSUBJ end AS PSUBJ
         , case when pb.number = 1 then p.PRN else pb.PRN end AS PRN
         , case when pb.number = 1 then p.PINDX else pb.PINDX end AS PINDX
         , case when pb.number = 1 then p.PRNNAME else pb.PRNNAME end AS PRNNAME
         , case when pb.number = 1 then p.PCITY else pb.PCITY end AS PCITY
         , case when pb.number = 1 then p.PNP else pb.PNP end AS PNP
         , case when pb.number = 1 then p.PUL else pb.PUL end AS PUL
         , case when pb.number = 1 then p.PDOM else pb.PDOM end AS PDOM
         , case when pb.number = 1 then p.PKOR else pb.PKOR end AS PKOR
         , case when pb.number = 1 then p.PKV else pb.PKV end AS PKV
         , case when pb.number = 1 then p.PDMJ else pb.PDMJ end AS PDMJ
         , case when pb.number = 1 then p.EMAIL else pb.EMAIL end AS EMAIL
         , case when pb.number = 1 then p.PHONE else pb.PHONE end AS PHONE
         , ISNULL((case when pb.number = 1 then p.ENP else pb.ENP end), (SELECT     TOP (1) p6.ENP
                            FROM          dbo.PRZBUFT AS p6 
                            WHERE      (pb.PID = p6.PID) 
                                      AND (pb.DVIZ <= p6.DVIZ) 
                                      AND (p6.OP = 'Ï060' OR p6.OP = 'Ï070')
                            ORDER BY p6.DVIZ DESC)) AS ENP
         , pb.Q
         , pb.QOGRN
         , pb.PRZ
         , pb.OPDOC
         , pb.SPOL
         , pb.NPOL
         , case when pb.number = 1 then p.OKATO else pb.OKATO end AS OKATO
         , case when pb.number = 1 then p.DHPOL else pb.DHPOL end AS DHPOL
         , case when pb.number = 1 then p.DBEG else pb.DBEG end AS DBEG
         , case when pb.number = 1 then p.DEND else pb.DEND end AS DEND
         , case when pb.number = 1 then p.DSTOP else pb.DSTOP end AS DSTOP
         , case when pb.number = 1 then p.DSTOP_CS else pb.DSTOP_CS end AS DSTOP_CS
         , pb.DVIZ AS DVIZ
         , case when pb.number = 1 then p.DZ else pb.DZ end AS DZ
         , case when pb.number = 1 then ISNULL(p.METH, pb.METH) else pb.METH end AS METH
         , ISNULL((SELECT     TOP (1) ID
                              FROM         dbo.POLIS AS polis
                              WHERE  (PID = pb.PID) 
                                  AND (POLTP = pb.OPDOC)
                                  AND (ISNULL(POLVID, 0) = ISNULL(p.POLVID, 0)) 
                                  AND (NPOL = pb.NPOL) 
                                  AND (ISNULL(SPOL, '') = ISNULL(p.SPOL, '')) 
                                  AND (Q = pb.Q) 
                                  AND (ISNULL(PRZ, '') = pb.PRZ)
                              ORDER BY ID DESC),
                          (SELECT     TOP (1) ID
                            FROM          dbo.POLIS AS polis
                            WHERE      (PID = pb.PID) AND (NPOL = pb.NPOL)
                            ORDER BY ID DESC)) AS POLISID
         , case when pb.number = 1 then p.RSTOP else pb.RSTOP end AS RSTOP
         , case when pb.number = 1 then p.LPU else pb.LPU end AS LPU
         , case when pb.number = 1 then p.LPUWK else pb.LPUWK end AS LPUWK
         , case when pb.number = 1 then p.LPUST else pb.LPUST end AS LPUST
         , case when pb.number = 1 then p.LPUUCH else pb.LPUUCH end AS LPUUCH
         , case when pb.number = 1 then p.LPUAUTO else pb.LPUAUTO end AS LPUAUTO
         , case when pb.number = 1 then p.LPUDT else pb.LPUDT end AS LPUDT
         , case when pb.number = 1 then p.LPUDX else pb.LPUDX end AS LPUDX
         , case when pb.number = 1 then p.SP else pb.SP end AS SP
         , case when pb.number = 1 then p.KT else pb.KT end AS KT
         , case when pb.number = 1 then p.OKVED else pb.OKVED end AS OKVED
         , case when pb.number = 1 then p.KLADRS else pb.KLADRS end AS KLADRS
         , case when pb.number = 1 then p.ERP else pb.ERP end AS ERP
         , case when pb.number = 1 then p.PETITION else pb.PETITION end AS PETITION
         , case when pb.number = 1 then p.FIOPR else pb.FIOPR end AS FIOPR
         , case when pb.number = 1 then isnull(p.CONTACT, pb.CONTACT) else pb.CONTACT end AS CONTACT
         , case when pb.number = 1 then p.PTYPE else pb.PTYPE end AS PTYPE
         , case when pb.number = 1 then p.NORD else pb.NORD end AS NORD
         , case when pb.number = 1 then p.DORD else pb.DORD end AS DORD
         , case when pb.number = 1 then p.OLDENP else pb.OLDENP end AS ODENP
         , case when pb.number = 1 then p.KLADRG else pb.KLADRG end AS KLADRG
         , case when pb.number = 1 then p.KLADRP else pb.KLADRP end AS KLADRP
         , case when pb.number = 1 then p.MID else pb.MID end AS MID
         , case when pb.number = 1 then p.DEDIT else pb.DEDIT end AS DEDIT
         , case when pb.number = 1 then isnull(p.ZDOCTP, pb.ZDOCTP) else pb.ZDOCTP end AS ZDOCTP
         , case when pb.number = 1 then isnull(p.ZDOCS, pb.ZDOCS) else pb.ZDOCS end AS ZDOCS
         , case when pb.number = 1 then isnull(p.ZDOCN, pb.ZDOCN) else pb.ZDOCN end AS ZDOCN
         , case when pb.number = 1 then isnull(p.ZDOCORG, pb.ZDOCORG) else pb.ZDOCORG end AS ZDOCORG
         , case when pb.number = 1 then isnull(p.ZDOCDT, pb.ZDOCDT) else pb.ZDOCDT end AS ZDOCDT
         , case when pb.number = 1 then isnull(p.ZDR, pb.ZDR) else pb.ZDR end AS ZDR
         , case when pb.number = 1 then isnull(p.ZT, pb.ZT) else pb.ZT end AS ZT
         , case when pb.number = 1 then isnull(p.ZFAM, pb.ZFAM) else pb.ZFAM end AS ZFAM
         , case when pb.number = 1 then isnull(p.ZIM, pb.ZIM) else pb.ZIM end AS ZIM
         , case when pb.number = 1 then isnull(p.ZOT, pb.ZOT) else pb.ZOT end AS ZOT
         , case when pb.number = 1 then isnull(p.ZPHONE, pb.ZPHONE) else pb.ZPHONE end AS ZPHONE
         , pb.ZADDR
         , pb.ST
         , pb.ET
         , pb.AT
         , pb.PRZLOGID
         , pb.OP
         , pb.OLD_ENP
         , pb.OLDFAM
         , pb.OLDIM
         , pb.OLDOT
         , pb.OLDDR
         , pb.OLDW
         , pb.OLDMR
         , pb.OLDSS
         , pb.OLDDOCTP
         , pb.OLDDOCS
         , pb.OLDDOCN
         , pb.OLDDOCDT
         , pb.OLDDOCORG
         , pb.OLDDOCEND
         , pb.OLDRDOCTP
         , pb.OLDRDOCS
         , pb.OLDRDOCN
         , pb.OLDRDOCDT
         , pb.OLDRDOCORG
         , pb.OLDRDOCEND
         , pb.SFLK
         , pb.EERP
         , pb.EINS
         , pb.REPL
         , pb.NUM
         , pb.OPID
         , pb.RESCODE
         , pb.RESULT
         , pb.ZP1ID
         , pb.A08ID
         , pb.VPDID
         , pb.LOCK
FROM dbo.PEOPLE AS p 
  INNER JOIN
  (select [FAM]
      ,[IM]
      ,[OT]
      ,[W]
      ,[DOST]
      ,[DR]
      ,[DRA]
      ,[DRT]
      ,[MR]
      ,[DS]
      ,[SS]
      ,[DOCTP]
      ,[DOCS]
      ,[DOCN]
      ,[DOCDT]
      ,[DOCORG]
      ,[DOCEND]
      ,[RDOCTP]
      ,[RDOCS]
      ,[RDOCN]
      ,[RDOCDT]
      ,[RDOCORG]
      ,[RDOCEND]
      ,[BIRTH_OKSM]
      ,[CN]
      ,[SUBJ]
      ,[RN]
      ,[INDX]
      ,[RNNAME]
      ,[CITY]
      ,[NP]
      ,[UL]
      ,[DOM]
      ,[KOR]
      ,[KV]
      ,[DMJ]
      ,[BOMJ]
      ,[KATEG]
      ,[PSUBJ]
      ,[PRN]
      ,[PINDX]
      ,[PRNNAME]
      ,[PCITY]
      ,[PNP]
      ,[PUL]
      ,[PDOM]
      ,[PKOR]
      ,[PKV]
      ,[PDMJ]
      ,[EMAIL]
      ,[PHONE]
      ,[ENP]
      ,[Q]
      ,[QOGRN]
      ,[PRZ]
      ,[OPDOC]
      ,[SPOL]
      ,[NPOL]
      ,[OKATO]
      ,[DHPOL]
      ,[DBEG]
      ,[DEND]
      ,[DSTOP]
      ,[DSTOP_CS]
      ,[DVIZ]
      ,[DZ]
      ,[METH]
      ,[POLISID]
      ,[RSTOP]
      ,[LPU]
      ,[LPUWK]
      ,[LPUST]
      ,[LPUUCH]
      ,[LPUAUTO]
      ,[LPUDT]
      ,[LPUDX]
      ,[SP]
      ,[KT]
      ,[OKVED]
      ,[KLADRS]
      ,[ERP]
      ,[PETITION]
      ,[FIOPR]
      ,[CONTACT]
      ,[ZFAM]
      ,[ZIM]
      ,[ZOT]
      ,[ZT]
      ,[ZDR]
      ,[ZMR]
      ,[ZDOCTP]
      ,[ZDOCS]
      ,[ZDOCN]
      ,[ZDOCDT]
      ,[ZDOCORG]
      ,[ZADDR]
      ,[ZPHONE]
      ,[PTYPE]
      ,[NORD]
      ,[DORD]
      ,[OLDENP]
      ,[KLADRG]
      ,[KLADRP]
      ,[ID]
      ,[MID]
      ,[DEDIT]
      ,[ST]
      ,[ET]
      ,[AT]
      ,[PID]
      ,[PRZLOGID]
      ,[OP]
      ,[OLD_ENP]
      ,[OLDFAM]
      ,[OLDIM]
      ,[OLDOT]
      ,[OLDDR]
      ,[OLDW]
      ,[OLDMR]
      ,[OLDSS]
      ,[OLDDOCTP]
      ,[OLDDOCS]
      ,[OLDDOCN]
      ,[OLDDOCDT]
      ,[OLDDOCORG]
      ,[OLDDOCEND]
      ,[OLDRDOCTP]
      ,[OLDRDOCS]
      ,[OLDRDOCN]
      ,[OLDRDOCDT]
      ,[OLDRDOCORG]
      ,[OLDRDOCEND]
      ,[SFLK]
      ,[EERP]
      ,[EINS]
      ,[REPL]
      ,[NUM]
      ,[OPID]
      ,[OPSMO]
      ,[OPPOL]
      ,[POLPR]
      ,[POLVID]
      ,[RESCODE]
      ,[RESULT]
      ,[DEL]
      ,[ZP1ID]
      ,[A08ID]
      ,[VPDID]
      ,[LOCK]
      ,[FORCE]
      ,ROW_NUMBER() OVER(PARTITION BY prz.pid ORDER BY prz.id DESC) as Number
      from dbo.PRZBUFT prz
      WHERE     (prz.OP NOT IN ('Ï021', 'Ï022', 'Ï023', 'Ï024', 'Ï060', 'Ï070'))) AS pb ON pb.PID = p.ID




GO


