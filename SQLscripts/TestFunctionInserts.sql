--ROOM INSERTS
INSERT INTO TES_DIMENSION_ROOM (ID, ROOMNUMBER) VALUES ('1', '1');
INSERT INTO TES_DIMENSION_ROOM (ID, ROOMNUMBER) VALUES ('2', '2');
INSERT INTO TES_DIMENSION_ROOM (ID, ROOMNUMBER) VALUES ('3', '3');
INSERT INTO TES_DIMENSION_ROOM (ID, ROOMNUMBER) VALUES ('4', '4');
INSERT INTO TES_DIMENSION_ROOM (ID, ROOMNUMBER) VALUES ('5', '5');
INSERT INTO TES_DIMENSION_ROOM (ID, ROOMNUMBER) VALUES ('6', '6');

--TIME INSERTS
INSERT INTO TES_DIMENSION_TIME( ID,TIMET)  VALUES (1, to_timestamp( '01/01/2012 10:00', 'MM/DD/YYYY HH:MI') );
INSERT INTO TES_DIMENSION_TIME( ID,TIMET)  VALUES (2, to_timestamp( '01/02/2012 10:00', 'MM/DD/YYYY HH:MI') );
INSERT INTO TES_DIMENSION_TIME( ID,TIMET)  VALUES (3, to_timestamp( '01/03/2012 10:00', 'MM/DD/YYYY HH:MI') );
INSERT INTO TES_DIMENSION_TIME( ID,TIMET)  VALUES (4, to_timestamp( '01/04/2012 10:00', 'MM/DD/YYYY HH:MI') );
INSERT INTO TES_DIMENSION_TIME( ID,TIMET)  VALUES (5, to_timestamp( '01/05/2012 10:00', 'MM/DD/YYYY HH:MI') );
INSERT INTO TES_DIMENSION_TIME( ID,TIMET)  VALUES (6, to_timestamp( '01/06/2012 10:00', 'MM/DD/YYYY HH:MI') );
INSERT INTO TES_DIMENSION_TIME( ID,TIMET)  VALUES (7, to_timestamp( '01/07/2012 10:00', 'MM/DD/YYYY HH:MI') );
INSERT INTO TES_DIMENSION_TIME( ID,TIMET)  VALUES (8, to_timestamp( '01/08/2012 10:00', 'MM/DD/YYYY HH:MI') );
INSERT INTO TES_DIMENSION_TIME( ID,TIMET)  VALUES (9, to_timestamp( '01/09/2012 10:00', 'MM/DD/YYYY HH:MI') );

--FACT INSERTS
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (1,1,1,1);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (2,2,1,2);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (3,3,1,5);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (4,4,1,9);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (5,5,1,18);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (6,6,1,10);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (7,7,1,22);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (8,8,1,34);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (9,9,1,35);

INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (11,1,2,10);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (12,2,2,20);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (13,3,2,50);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (14,4,2,90);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (15,5,2,180);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (16,6,2,100);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (17,7,2,220);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (18,8,2,340);
INSERT INTO TES_FACT_ENERGY( ID,ID_TIME,ID_ROOM,MEASUREVALUE)  VALUES (19,9,2,350);