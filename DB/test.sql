DROP TABLE IF EXISTS [CompositionProducts];
CREATE TABLE 'CompositionProducts' (
                                'RootCode'   integer NOT NULL,
                                'WhereCode'  integer NOT NULL,
                                'WhatCode'   integer NOT NULL,
                                'Count'      integer NOT NULL
                            );
DROP TABLE IF EXISTS [FullApplication];
CREATE TABLE FullApplication (
  ProductCode     integer NOT NULL,
  PackageDetails  integer NOT NULL,
  Count           integer NOT NULL,
  /* Foreign keys */
  FOREIGN KEY (PackageDetails)
    REFERENCES ProductNames(ProductKey)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);
DROP TABLE IF EXISTS [ProductNames];
CREATE TABLE 'ProductNames' (
                                'ProductKey'   integer PRIMARY KEY NOT NULL,
                                'Name'         varchar(50) NOT NULL,
                                'Designation'  varchar(50) NOT NULL,
                                'ViewCode'     integer NOT NULL,
                                'TypeCode'     integer NOT NULL,
                                'SignCode'     integer NOT NULL,
                                FOREIGN KEY ('ProductKey')
                                    REFERENCES 'ReferenceStandarts'('ProductCode')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION, 
                                FOREIGN KEY ('TypeCode')
                                    REFERENCES 'ProductTypes'('PKey')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION, 
                                FOREIGN KEY ('ViewCode')
                                    REFERENCES 'ReferenceProducts'('PKey')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION, 
                                FOREIGN KEY ('SignCode')
                                    REFERENCES 'ReferenceSigns'('PKey')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION
                            );
DROP TABLE IF EXISTS [ProductTypes];
CREATE TABLE 'ProductTypes' (
                                'PKey'  integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                'Name'   varchar(30) NOT NULL
                            );
DROP TABLE IF EXISTS [ReferenceMaterials];
CREATE TABLE 'ReferenceMaterials' (
                                'MaterialCode'  integer PRIMARY KEY NOT NULL,
                                'Name'          varchar(50) NOT NULL,
                                'UnitType'      integer NOT NULL,
                                FOREIGN KEY (UnitType)
                                    REFERENCES 'ReferenceUnits'('PKey')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION
                            );
DROP TABLE IF EXISTS [ReferenceProducts];
CREATE TABLE 'ReferenceProducts' (
                                'PKey'  integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                'Name'   varchar(30) NOT NULL
                            );
DROP TABLE IF EXISTS [ReferenceSigns];
CREATE TABLE 'ReferenceSigns' (
                                'PKey'  integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                'Name'   varchar(30) NOT NULL
                            );
DROP TABLE IF EXISTS [ReferenceStandarts];
CREATE TABLE 'ReferenceStandarts' (
                                'ProductCode'      integer PRIMARY KEY NOT NULL,
                                'MaterialCode'     integer NOT NULL,
                                'ConsumptionRate'  real NOT NULL DEFAULT 0,
                                'RateOfWaste'      real NOT NULL DEFAULT 0,
                                FOREIGN KEY ('MaterialCode')
                                    REFERENCES 'ReferenceMaterials'('MaterialCode')
                                    ON DELETE NO ACTION
                                    ON UPDATE NO ACTION
                            );
DROP TABLE IF EXISTS [ReferenceUnits];
CREATE TABLE 'ReferenceUnits' (
                                'PKey'      integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                                'FullName'   varchar(20) NOT NULL,
                                'SmallName'  varchar(5) NOT NULL
                            );
INSERT INTO CompositionProducts VALUES(77740,77740,40141100000,1);
INSERT INTO CompositionProducts VALUES(77740,77740,50122800000,1);
INSERT INTO CompositionProducts VALUES(77740,77740,40141500000,1);
INSERT INTO CompositionProducts VALUES(77740,77740,40945310200,1);
INSERT INTO CompositionProducts VALUES(77740,77740,40945210300,1);
INSERT INTO CompositionProducts VALUES(77740,77740,40372101200,2);
INSERT INTO CompositionProducts VALUES(77740,77740,40372100500,1);
INSERT INTO CompositionProducts VALUES(77740,77740,40994307000,1);
INSERT INTO CompositionProducts VALUES(77740,77740,50911305000,2);
INSERT INTO CompositionProducts VALUES(77740,77740,50911303500,1);
INSERT INTO CompositionProducts VALUES(77740,77740,50911203600,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,40322114800,2);
INSERT INTO CompositionProducts VALUES(77740,40141100000,50651402550,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,40141102000,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,50631802450,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,50612701720,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,40141102100,2);
INSERT INTO CompositionProducts VALUES(77740,40141100000,50526105860,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,50526105960,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,40414301500,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,50612204000,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,40116207150,2);
INSERT INTO CompositionProducts VALUES(77740,40141100000,40112203000,2);
INSERT INTO CompositionProducts VALUES(77740,40141100000,40332100630,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,40372100900,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,40372101600,1);
INSERT INTO CompositionProducts VALUES(77740,40141100000,40372100700,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,50122806260,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,50911102500,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40112101800,2);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40116202200,2);
INSERT INTO CompositionProducts VALUES(77740,50122800000,50612204460,4);
INSERT INTO CompositionProducts VALUES(77740,50122800000,50911309300,2);
INSERT INTO CompositionProducts VALUES(77740,50122800000,50122804850,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40111203950,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,50221105050,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,50221105150,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40112202650,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40112203200,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40112203100,2);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40111702950,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40314205050,2);
INSERT INTO CompositionProducts VALUES(77740,50122800000,50221105160,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40332100630,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40372100900,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40372101600,1);
INSERT INTO CompositionProducts VALUES(77740,50122800000,40372100700,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40116206300,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40141101700,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40112101400,2);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40962104310,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40116208900,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40331100600,2);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40141502000,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40414302800,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40945310400,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,50512101100,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40116211600,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,50122803400,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,50122803500,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40311101700,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,50221104350,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40962104400,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40116206700,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40111202200,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40116206800,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,50221104450,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40771100100,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40112101500,2);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40962106100,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40332100630,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40372100900,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40372101600,1);
INSERT INTO CompositionProducts VALUES(77740,40141500000,40372100700,1);
INSERT INTO CompositionProducts VALUES(87750,87750,40372101200,10);
INSERT INTO CompositionProducts VALUES(87750,87750,40372100500,10);
INSERT INTO CompositionProducts VALUES(87750,87750,40994307000,10);
INSERT INTO CompositionProducts VALUES(87750,87750,50911305000,20);
INSERT INTO CompositionProducts VALUES(87750,87750,50911303500,10);
INSERT INTO CompositionProducts VALUES(87750,87750,50911203600,10);
INSERT INTO FullApplication VALUES(77740,40322114800,2);
INSERT INTO FullApplication VALUES(77740,50651402550,1);
INSERT INTO FullApplication VALUES(77740,40141102000,1);
INSERT INTO FullApplication VALUES(77740,50631802450,1);
INSERT INTO FullApplication VALUES(77740,50612701720,1);
INSERT INTO FullApplication VALUES(77740,40141102100,2);
INSERT INTO FullApplication VALUES(77740,50526105860,1);
INSERT INTO FullApplication VALUES(77740,50526105960,1);
INSERT INTO FullApplication VALUES(77740,40414301500,1);
INSERT INTO FullApplication VALUES(77740,50612204000,1);
INSERT INTO FullApplication VALUES(77740,40116207150,2);
INSERT INTO FullApplication VALUES(77740,40112203000,2);
INSERT INTO FullApplication VALUES(77740,40332100630,3);
INSERT INTO FullApplication VALUES(77740,40372100900,3);
INSERT INTO FullApplication VALUES(77740,40372101600,3);
INSERT INTO FullApplication VALUES(77740,40372100700,3);
INSERT INTO FullApplication VALUES(77740,50122806260,1);
INSERT INTO FullApplication VALUES(77740,50911102500,1);
INSERT INTO FullApplication VALUES(77740,40112101800,2);
INSERT INTO FullApplication VALUES(77740,40116202200,2);
INSERT INTO FullApplication VALUES(77740,50612204460,4);
INSERT INTO FullApplication VALUES(77740,50911309300,2);
INSERT INTO FullApplication VALUES(77740,50122804850,1);
INSERT INTO FullApplication VALUES(77740,40111203950,1);
INSERT INTO FullApplication VALUES(77740,50221105050,1);
INSERT INTO FullApplication VALUES(77740,50221105150,1);
INSERT INTO FullApplication VALUES(77740,40112202650,1);
INSERT INTO FullApplication VALUES(77740,40112203200,1);
INSERT INTO FullApplication VALUES(77740,40112203100,2);
INSERT INTO FullApplication VALUES(77740,40111702950,1);
INSERT INTO FullApplication VALUES(77740,40314205050,2);
INSERT INTO FullApplication VALUES(77740,50221105160,1);
INSERT INTO FullApplication VALUES(77740,40116206300,1);
INSERT INTO FullApplication VALUES(77740,40141101700,1);
INSERT INTO FullApplication VALUES(77740,40112101400,2);
INSERT INTO FullApplication VALUES(77740,40962104310,1);
INSERT INTO FullApplication VALUES(77740,40116208900,1);
INSERT INTO FullApplication VALUES(77740,40331100600,2);
INSERT INTO FullApplication VALUES(77740,40141502000,1);
INSERT INTO FullApplication VALUES(77740,40414302800,1);
INSERT INTO FullApplication VALUES(77740,40945310400,1);
INSERT INTO FullApplication VALUES(77740,50512101100,1);
INSERT INTO FullApplication VALUES(77740,40116211600,1);
INSERT INTO FullApplication VALUES(77740,50122803400,1);
INSERT INTO FullApplication VALUES(77740,50122803500,1);
INSERT INTO FullApplication VALUES(77740,40311101700,1);
INSERT INTO FullApplication VALUES(77740,50221104350,1);
INSERT INTO FullApplication VALUES(77740,40962104400,1);
INSERT INTO FullApplication VALUES(77740,40116206700,1);
INSERT INTO FullApplication VALUES(77740,40111202200,1);
INSERT INTO FullApplication VALUES(77740,40116206800,1);
INSERT INTO FullApplication VALUES(77740,50221104450,1);
INSERT INTO FullApplication VALUES(77740,40771100100,1);
INSERT INTO FullApplication VALUES(77740,40112101500,2);
INSERT INTO FullApplication VALUES(77740,40962106100,1);
INSERT INTO FullApplication VALUES(77740,40945310200,1);
INSERT INTO FullApplication VALUES(77740,40945210300,1);
INSERT INTO FullApplication VALUES(77740,40372101200,2);
INSERT INTO FullApplication VALUES(77740,40372100500,1);
INSERT INTO FullApplication VALUES(77740,40994307000,1);
INSERT INTO FullApplication VALUES(77740,50911305000,2);
INSERT INTO FullApplication VALUES(77740,50911303500,1);
INSERT INTO FullApplication VALUES(77740,50911203600,1);
INSERT INTO FullApplication VALUES(87750,40372101200,10);
INSERT INTO FullApplication VALUES(87750,40372100500,10);
INSERT INTO FullApplication VALUES(87750,40994307000,10);
INSERT INTO FullApplication VALUES(87750,50911305000,20);
INSERT INTO FullApplication VALUES(87750,50911303500,10);
INSERT INTO FullApplication VALUES(87750,50911203600,10);
INSERT INTO ProductNames VALUES(77740,'�����������','5�8-3�',3,3,2);
INSERT INTO ProductNames VALUES(40111202200,'��������','�8-3�151-40-20',1,3,2);
INSERT INTO ProductNames VALUES(40111203950,'��������','�34-24�-012',1,3,2);
INSERT INTO ProductNames VALUES(40111702950,'�������� ����������','�34-24�-022',1,3,2);
INSERT INTO ProductNames VALUES(40112101400,'������','�8-3�151-04-28',1,3,2);
INSERT INTO ProductNames VALUES(40112101500,'�������','�8-3�151-40-26',1,3,2);
INSERT INTO ProductNames VALUES(40112101800,'�������','�34-12-28',1,3,2);
INSERT INTO ProductNames VALUES(40112202650,'����� ����������','�34-24�-017',1,3,2);
INSERT INTO ProductNames VALUES(40112203000,'��������','2�8-3�-600-002',1,3,2);
INSERT INTO ProductNames VALUES(40112203100,'��������','�34-24�-021�',1,3,2);
INSERT INTO ProductNames VALUES(40112203200,'������ ��������','�34-24�-018',1,3,2);
INSERT INTO ProductNames VALUES(40116202200,'������','�34-14-25',1,3,2);
INSERT INTO ProductNames VALUES(40116206300,'������','�8-3�151-04-26',1,3,2);
INSERT INTO ProductNames VALUES(40116206700,'������','�8-3�151-40-19',1,3,2);
INSERT INTO ProductNames VALUES(40116206800,'������-��������','�8-3�151-40-21',1,3,2);
INSERT INTO ProductNames VALUES(40116207150,'������','2�8-3�-600-001',1,3,2);
INSERT INTO ProductNames VALUES(40116208900,'������','�8-3�151-04-31�',1,3,2);
INSERT INTO ProductNames VALUES(40116211600,'������','�8-3�151-40-04',1,3,2);
INSERT INTO ProductNames VALUES(40141100000,'�������','2�8-3�-000',2,3,2);
INSERT INTO ProductNames VALUES(40141101700,'�������','�8-3�151-04-27�',1,3,2);
INSERT INTO ProductNames VALUES(40141102000,'��������','2�8-3�-003',1,3,2);
INSERT INTO ProductNames VALUES(40141102100,'�������','2�8-3�-014',1,3,2);
INSERT INTO ProductNames VALUES(40141500000,'��������','�8-3�151-00-00',2,3,2);
INSERT INTO ProductNames VALUES(40141502000,'��������','�8-3�151-04-35',1,3,2);
INSERT INTO ProductNames VALUES(40311101700,'����������','�8-3�151-40-14',1,3,2);
INSERT INTO ProductNames VALUES(40314205050,'���������','�34-24�-023',1,3,2);
INSERT INTO ProductNames VALUES(40322114800,'������','2�34-24�-013',1,3,2);
INSERT INTO ProductNames VALUES(40331100600,'��������','�8-3�151-04-34',1,3,2);
INSERT INTO ProductNames VALUES(40332100630,'�������� ','���2�71-1-82 3-5',1,1,2);
INSERT INTO ProductNames VALUES(40372100500,'��������','���2.�71-1-82 �1/8',1,1,2);
INSERT INTO ProductNames VALUES(40372100700,'��������','���2.�71-1-82 4-�1/4',1,1,2);
INSERT INTO ProductNames VALUES(40372100900,'��������','���2�71-1-82 4-�3/8',1,1,2);
INSERT INTO ProductNames VALUES(40372101200,'��������','���2 �71-1-82 4-�3/4',1,1,2);
INSERT INTO ProductNames VALUES(40372101600,'��������','���2.�71-1-82 4-�1/2',1,1,2);
INSERT INTO ProductNames VALUES(40414301500,'�������','2�8-3�-401',1,3,2);
INSERT INTO ProductNames VALUES(40414302800,'�������','�8-3�151-40-01',1,3,2);
INSERT INTO ProductNames VALUES(40771100100,'�����','�8-3�151-40-25',1,3,2);
INSERT INTO ProductNames VALUES(40945210300,'�����','�61-6/40-01',1,3,2);
INSERT INTO ProductNames VALUES(40945310200,'�����','�61-6/22-01',1,3,2);
INSERT INTO ProductNames VALUES(40945310400,'��������','�8-3�151-40-02',1,3,2);
INSERT INTO ProductNames VALUES(40962104310,'���','�8-3�151-04-29-01',1,3,1);
INSERT INTO ProductNames VALUES(40962104400,'���','�8-3�151-40-16�',1,3,1);
INSERT INTO ProductNames VALUES(40962106100,'����','�8-3�151-40-27',1,3,1);
INSERT INTO ProductNames VALUES(40994307000,'���������','���93-35-78 �22',1,1,2);
INSERT INTO ProductNames VALUES(50122800000,'������','5�34-24�-000',2,3,2);
INSERT INTO ProductNames VALUES(50122803400,'������','�8-3�151-40-11',1,3,2);
INSERT INTO ProductNames VALUES(50122803500,'������','�8-3�151-40-12',1,3,2);
INSERT INTO ProductNames VALUES(50122804850,'������','�34-24�-011',1,3,2);
INSERT INTO ProductNames VALUES(50122806260,'������','5�34-24�-011',1,3,2);
INSERT INTO ProductNames VALUES(50221104350,'������','�8-3�151-40-15�',1,3,2);
INSERT INTO ProductNames VALUES(50221104450,'������','�8-3�151-40-23�',1,3,2);
INSERT INTO ProductNames VALUES(50221105050,'������ �����','�34-24�-014�',1,3,2);
INSERT INTO ProductNames VALUES(50221105150,'������ ������','�34-24�-015�',1,3,2);
INSERT INTO ProductNames VALUES(50221105160,'������','�34-24�-026',1,3,2);
INSERT INTO ProductNames VALUES(50512101100,'�������','�8-3�151-40-03',1,3,2);
INSERT INTO ProductNames VALUES(50526105860,'�����','2�8-3�-201',1,3,2);
INSERT INTO ProductNames VALUES(50526105960,'�����','2�8-3�-301�',1,3,2);
INSERT INTO ProductNames VALUES(50612204000,'������','2�8-3�-402',1,3,2);
INSERT INTO ProductNames VALUES(50612204460,'������','�34-22�-019',1,3,2);
INSERT INTO ProductNames VALUES(50612701720,'�������� ���������','2�8-3�-012-02',1,3,2);
INSERT INTO ProductNames VALUES(50631802450,'���������','2�8-3�-005',1,3,2);
INSERT INTO ProductNames VALUES(50651402550,'�����','2�8-3�-002�',1,3,2);
INSERT INTO ProductNames VALUES(50911102500,'�������','5�4255-072�',1,3,2);
INSERT INTO ProductNames VALUES(50911203600,'�������','��2-10',1,3,2);
INSERT INTO ProductNames VALUES(50911303500,'�������','��2-04',1,3,2);
INSERT INTO ProductNames VALUES(50911305000,'�������','��1-21',1,3,2);
INSERT INTO ProductNames VALUES(50911309300,'�������','�34-22�-021',1,3,2);
INSERT INTO ProductTypes VALUES(1,'����������� �������');
INSERT INTO ProductTypes VALUES(2,'��������������� �������');
INSERT INTO ProductTypes VALUES(3,'����������');
INSERT INTO ReferenceMaterials VALUES(951032148,'���� ��.35-�-� �������.1050-88 2590-88 �75',1);
INSERT INTO ReferenceMaterials VALUES(951035116,'���� ��.45-�-� �������.1050-88 2590-88 �16',1);
INSERT INTO ReferenceMaterials VALUES(951035120,'���� ��.45-�-� �������.1050-88 2590-88 �20',1);
INSERT INTO ReferenceMaterials VALUES(951035124,'���� ��.45-�-� �������.1050-88 2590-88 �24',1);
INSERT INTO ReferenceMaterials VALUES(951035125,'���� ��.45-�-� �������.1050-88 2590-88 �25',1);
INSERT INTO ReferenceMaterials VALUES(951035132,'���� ��.45-�-� ��������.1050-88 2590-88 �32',1);
INSERT INTO ReferenceMaterials VALUES(951035142,'���� ��.45-�-� �������.1050-88 2590-88 �42',1);
INSERT INTO ReferenceMaterials VALUES(951035150,'���� ��.45-�-� �������.1050-88 2590-88 �50',1);
INSERT INTO ReferenceMaterials VALUES(951059127,'���� ��.20� �����.4543-71 2590-71 �24',1);
INSERT INTO ReferenceMaterials VALUES(951061113,'���� ��.40� �����.4543-71 2590-71 �14',1);
INSERT INTO ReferenceMaterials VALUES(951061129,'���� ��.40� �����.4543-71 2590-71 �25',1);
INSERT INTO ReferenceMaterials VALUES(951061133,'���� ��.40� �����.4543-71 2590-71 �28',1);
INSERT INTO ReferenceMaterials VALUES(951061141,'���� ��.40� �����.4543-71 2590-71 �36',1);
INSERT INTO ReferenceMaterials VALUES(951061149,'���� ��.40� �����.4543-71 2590-71 �45',1);
INSERT INTO ReferenceMaterials VALUES(951061157,'���� ��.40� �����.4543-71 2590-71 �56',1);
INSERT INTO ReferenceMaterials VALUES(971207005,'���� ��.3�� 14637-79 19903-74 4�/�',1);
INSERT INTO ReferenceMaterials VALUES(973009010,'���� 08�� 16523-70 19903-74 5-�� �=2 �/�',1);
INSERT INTO ReferenceMaterials VALUES(1131122110,'���� ��.20� ������.1051-73 7417-75 �10,5',1);
INSERT INTO ReferenceMaterials VALUES(1131123516,'���� ��.35 ������.1051-73 7417-75 �16',1);
INSERT INTO ReferenceMaterials VALUES(1131124514,'���� ��.45 ������.1051-73 7417-75 �14',1);
INSERT INTO ReferenceMaterials VALUES(1131124516,'���� ��.45 ������.1051-73 7417-75 �16',1);
INSERT INTO ReferenceMaterials VALUES(1131124536,'���� ��.45 ������.1051-73 7417-75 �36',1);
INSERT INTO ReferenceMaterials VALUES(1131133530,'������� ��.35 ������.1051-73 8559-57 S30',1);
INSERT INTO ReferenceMaterials VALUES(1131143527,'�������.��.35 ������.1051-73 8560-67 S27',1);
INSERT INTO ReferenceMaterials VALUES(1314401103,'��������� �-1 ����.�����.9389-75 �0,6',1);
INSERT INTO ReferenceMaterials VALUES(1314401113,'��������� �-1 ����.�����. 9389-75 �1,6',1);
INSERT INTO ReferenceMaterials VALUES(1314401242,'��������� �-2-� 9389-75 �0,8',1);
INSERT INTO ReferenceMaterials VALUES(1314401243,'��������� �-2-� 9389-75 �1,0',1);
INSERT INTO ReferenceMaterials VALUES(1314401244,'��������� �-2-� 9389-75 �1,2',1);
INSERT INTO ReferenceMaterials VALUES(1811101501,'������.���� ��1 21631-76 �=0,5',1);
INSERT INTO ReferenceMaterials VALUES(1844102509,'������ ���� 859-78 495-70 1.5',1);
INSERT INTO ReferenceMaterials VALUES(1845505629,'����� ����� ���63 ����494-76 8�1',1);
INSERT INTO ReferenceMaterials VALUES(2211131001,'���������� 10803-020 ���� 16337-77 ����1',1);
INSERT INTO ReferenceMaterials VALUES(2249210400,'��������� �=0.2 2214-78',1);
INSERT INTO ReferenceMaterials VALUES(4111220110,'����� 2�8-3�-002 ��20 68�226 47,15',1);
INSERT INTO ReferenceMaterials VALUES(4111220120,'��������� 2�8-3�-005 ��20',1);
INSERT INTO ReferenceMaterials VALUES(4111220325,'������ �34-24�-026 ��20 1,82 �80',1);
INSERT INTO ReferenceMaterials VALUES(4111220332,'������ 5�34-24�-011 ��20 53�106 8,2',1);
INSERT INTO ReferenceMaterials VALUES(4111220333,'������ �34-24-014/015 ��20 37�107 4.29',1);
INSERT INTO ReferenceMaterials VALUES(4111220586,'������ �8-3�151-40-11 ��20 118�126 13,4',1);
INSERT INTO ReferenceMaterials VALUES(4111220590,'������ �8-3�151-40-12 ��20 37�107 4,091',1);
INSERT INTO ReferenceMaterials VALUES(4111230022,'������ �34-24�-011 ��20 108�138 26,77',1);
INSERT INTO ReferenceMaterials VALUES(4121600009,'������� �8-3�151-40-01 ��.45 �����',1);
INSERT INTO ReferenceProducts VALUES(1,'������');
INSERT INTO ReferenceProducts VALUES(2,'��������� �������');
INSERT INTO ReferenceProducts VALUES(3,'�������');
INSERT INTO ReferenceSigns VALUES(1,'��������');
INSERT INTO ReferenceSigns VALUES(2,'������������ ������������');
INSERT INTO ReferenceSigns VALUES(3,'��������������');
INSERT INTO ReferenceStandarts VALUES(40111202200,951061133,0,967000007629395,0,587000012397766);
INSERT INTO ReferenceStandarts VALUES(40111203950,951061133,0,910000026226044,0,310000002384186);
INSERT INTO ReferenceStandarts VALUES(40111702950,951061133,1,07500004768372,0,375);
INSERT INTO ReferenceStandarts VALUES(40112101400,1131124536,0,230000004172325,0,159999996423721);
INSERT INTO ReferenceStandarts VALUES(40112101500,951061129,0,104999996721745,6,49999976158142E-02);
INSERT INTO ReferenceStandarts VALUES(40112101800,951061113,5,70000000298023E-02,3,09999994933605E-02);
INSERT INTO ReferenceStandarts VALUES(40112202650,951061141,0,75,0,589999973773956);
INSERT INTO ReferenceStandarts VALUES(40112203000,1131124514,0,140000000596046,5,00000007450581E-02);
INSERT INTO ReferenceStandarts VALUES(40112203100,1131124514,7,69999995827675E-02,4,69999983906746E-02);
INSERT INTO ReferenceStandarts VALUES(40112203200,951061141,0,175999999046326,0,123000003397465);
INSERT INTO ReferenceStandarts VALUES(40116202200,951061149,1,10500001907349,0,305000007152557);
INSERT INTO ReferenceStandarts VALUES(40116206300,1131143527,0,158000007271767,0,090999998152256);
INSERT INTO ReferenceStandarts VALUES(40116206700,951061157,2,57999992370605,1,83000004291534);
INSERT INTO ReferenceStandarts VALUES(40116206800,951059127,2,33200001716614,1,682000041008);
INSERT INTO ReferenceStandarts VALUES(40116207150,951035132,0,165000006556511,8,50000008940697E-02);
INSERT INTO ReferenceStandarts VALUES(40116208900,973009010,0,063000001013279,0,028999999165535);
INSERT INTO ReferenceStandarts VALUES(40116211600,951035116,2,60000005364418E-02,1,60000007599592E-02);
INSERT INTO ReferenceStandarts VALUES(40141100000,951035125,0,187000006437302,0,144999995827675);
INSERT INTO ReferenceStandarts VALUES(40141101700,951059127,0,165000006556511,9,49999988079071E-02);
INSERT INTO ReferenceStandarts VALUES(40141102000,951061133,0,833999991416931,0,643999993801117);
INSERT INTO ReferenceStandarts VALUES(40141102100,1131122110,4,50000017881393E-02,0);
INSERT INTO ReferenceStandarts VALUES(40141500000,2211131001,8,00000037997961E-03,0);
INSERT INTO ReferenceStandarts VALUES(40141502000,951061133,0,783999979496002,0,483999997377396);
INSERT INTO ReferenceStandarts VALUES(40311101700,951035150,0,137999996542931,0,123000003397465);
INSERT INTO ReferenceStandarts VALUES(40314205050,2249210400,1,99999995529652E-02,0);
INSERT INTO ReferenceStandarts VALUES(40322114800,1131124536,0,219999998807907,5,99999986588955E-02);
INSERT INTO ReferenceStandarts VALUES(40331100600,1131124516,1,99999995529652E-02,4,99999988824129E-03);
INSERT INTO ReferenceStandarts VALUES(40332100630,2211131001,1,99999994947575E-04,0);
INSERT INTO ReferenceStandarts VALUES(40372100500,1314401243,3,00000002607703E-03,6,00000028498471E-04);
INSERT INTO ReferenceStandarts VALUES(40372100700,2211131001,1,79999996908009E-03,0);
INSERT INTO ReferenceStandarts VALUES(40372100900,2211131001,3,59999993816018E-03,0);
INSERT INTO ReferenceStandarts VALUES(40372101200,1314401242,1,50000001303852E-03,3,9999998989515E-04);
INSERT INTO ReferenceStandarts VALUES(40372101600,2211131001,5,9000002220273E-03,0);
INSERT INTO ReferenceStandarts VALUES(40414301500,951032148,3,41199994087219,2,81200003623962);
INSERT INTO ReferenceStandarts VALUES(40414302800,4121600009,1,0);
INSERT INTO ReferenceStandarts VALUES(40771100100,951061113,5,99999986588955E-02,2,99999993294477E-02);
INSERT INTO ReferenceStandarts VALUES(40945210300,1844102509,1,49999996647239E-02,1,09999999403954E-02);
INSERT INTO ReferenceStandarts VALUES(40945310200,2211131001,1,00000004749745E-03,0);
INSERT INTO ReferenceStandarts VALUES(40945310400,951035124,0,97000002861023,0,509999990463257);
INSERT INTO ReferenceStandarts VALUES(40994307000,1314401244,2,40000011399388E-03,6,00000028498471E-04);
INSERT INTO ReferenceStandarts VALUES(50122800000,951035142,1,10000002384186,0,5);
INSERT INTO ReferenceStandarts VALUES(50122803400,4111220586,13,3999996185303,7,59999990463257);
INSERT INTO ReferenceStandarts VALUES(50122803500,4111220590,4,09100008010864,2,94099998474121);
INSERT INTO ReferenceStandarts VALUES(50122804850,4111230022,26,7700004577637,14,3699998855591);
INSERT INTO ReferenceStandarts VALUES(50122806260,4111220332,8,19999980926514,3,77999997138977);
INSERT INTO ReferenceStandarts VALUES(50221104350,1131133530,0,800000011920929,0,490000009536743);
INSERT INTO ReferenceStandarts VALUES(50221104450,1131133530,0,800000011920929,0,519999980926514);
INSERT INTO ReferenceStandarts VALUES(50221105050,4111220333,4,28999996185303,2,44000005722046);
INSERT INTO ReferenceStandarts VALUES(50221105150,4111220333,4,28999996185303,2,44000005722046);
INSERT INTO ReferenceStandarts VALUES(50221105160,4111220325,1,82000005245209,1,47000002861023);
INSERT INTO ReferenceStandarts VALUES(50512101100,951035120,0,280000001192093,0,189999997615814);
INSERT INTO ReferenceStandarts VALUES(50526105860,1845505629,7,99999982118607E-02,3,00000002607703E-03);
INSERT INTO ReferenceStandarts VALUES(50526105960,1845505629,3,29999998211861E-02,2,0000000949949E-03);
INSERT INTO ReferenceStandarts VALUES(50612204000,1131123516,4,69999983906746E-02,3,70000004768372E-02);
INSERT INTO ReferenceStandarts VALUES(50612204460,971207005,1,79999992251396E-02,8,00000037997961E-03);
INSERT INTO ReferenceStandarts VALUES(50612701720,1811101501,4,00000018998981E-03,1,30000000353903E-03);
INSERT INTO ReferenceStandarts VALUES(50631802450,4111220120,1,0);
INSERT INTO ReferenceStandarts VALUES(50651402550,4111220110,47,1500015258789,17,1499996185303);
INSERT INTO ReferenceStandarts VALUES(50911102500,1314401113,1,20000001043081E-02,4,00000018998981E-03);
INSERT INTO ReferenceStandarts VALUES(50911309300,1314401103,1,00000004749745E-03,3,00000014249235E-04);
INSERT INTO ReferenceUnits VALUES(1,'����������','��.');
INSERT INTO ReferenceUnits VALUES(2,'������','�.');
INSERT INTO ReferenceUnits VALUES(3,'����','��.');
