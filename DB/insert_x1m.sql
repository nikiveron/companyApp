-- Вставка данных в company
INSERT INTO company (short_name, full_name, inn, kpp, ogrn, ogrn_date_of_issue, rep_last_name, rep_first_name, rep_patronymic, rep_email, rep_phone)
SELECT 
    'short_name' || gs AS short_name,
    'full_name' || gs AS full_name,
    9999999999 - gs AS inn,
    999999999 - gs AS kpp,
    9999999999999 - gs AS ogrn,
    CURRENT_DATE - (INTERVAL '365 days' * (gs % 30)) AS ogrn_date_of_issue,
	'rep_last_name' || gs AS rep_last_name,
    'rep_first_name' || gs AS rep_first_name,
    'rep_patronymic' || gs AS rep_patronymic,
    'rep_email' || gs || '@example.com' AS rep_email,
	'8-999-' || gs AS rep_phone
FROM generate_series(1, 1000000) AS gs; 

SELECT * FROM company;

DO $$
BEGIN
	FOR i IN 1..333333 LOOP
    	INSERT INTO bank (bank_id) VALUES (i);
	END LOOP;
END$$;

DO $$
BEGIN
FOR i IN 333333..666666 LOOP
    INSERT INTO agent (agent_id, priority) VALUES (i, CASE WHEN i % 2 = 0 THEN true ELSE false END);
END LOOP;
END$$;

DO $$
BEGIN
FOR i IN 666666..1000000 LOOP
    INSERT INTO client (client_id) VALUES (i);
END LOOP;
END$$;

DO $$
BEGIN
FOR i IN 1..333333 LOOP
    INSERT INTO bank_agent_relation (bank_id, agent_id) VALUES (1 + TRUNC(RANDOM() * 333332), 666666 - TRUNC(RANDOM() * 333333));
END LOOP;
END$$;
