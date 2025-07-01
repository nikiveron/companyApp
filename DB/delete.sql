DELETE FROM company WHERE company_id = 1;
DELETE FROM company WHERE company_id = 2;
DELETE FROM company WHERE company_id = 3;
DELETE FROM company WHERE company_id = 4;
DELETE FROM company WHERE company_id = 5;

SELECT * FROM company;
---------------------------------

DELETE FROM bank WHERE bank_id = 1;
DELETE FROM bank WHERE bank_id = 2;
DELETE FROM bank WHERE bank_id = 3;
DELETE FROM bank WHERE bank_id = 4;
DELETE FROM bank WHERE bank_id = 5;

SELECT * FROM bank;
---------------------------------

DELETE FROM agent WHERE agent_id = 1;
DELETE FROM agent WHERE agent_id = 2;
DELETE FROM agent WHERE agent_id = 3;
DELETE FROM agent WHERE agent_id = 4;
DELETE FROM agent WHERE agent_id = 5;

SELECT * FROM agent;
---------------------------------

DELETE FROM client WHERE client_id = 1;
DELETE FROM client WHERE client_id = 2;
DELETE FROM client WHERE client_id = 3;
DELETE FROM client WHERE client_id = 4;
DELETE FROM client WHERE client_id = 5;

SELECT * FROM client;
---------------------------------

DELETE FROM bank_agent_relation WHERE bank_id = 1 AND agent_id = 2;
DELETE FROM bank_agent_relation WHERE bank_id = 1 AND agent_id = 3;
DELETE FROM bank_agent_relation WHERE bank_id = 5 AND agent_id = 4;
DELETE FROM bank_agent_relation WHERE bank_id = 5 AND agent_id = 2;
DELETE FROM bank_agent_relation WHERE bank_id = 3 AND agent_id = 4;

SELECT * FROM bank_agent_relation;
---------------------------------
