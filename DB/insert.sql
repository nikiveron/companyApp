INSERT INTO company (short_name, full_name, inn, kpp, ogrn, ogrn_date_of_issue, rep_last_name, rep_first_name, rep_patronymic, rep_email, rep_phone)
VALUES 
('Альфа', 'ООО Альфа-Финанс', 1234567890, 123456789, 1027700000000, '2020-01-15', 'Иванов', 'Иван', 'Иванович', 'ivanov@alfa.ru', '+79991234567'),
('Бета', 'ООО Бета-Капитал', 2345678901, 234567891, 1027700000001, '2019-06-20', 'Петров', 'Петр', 'Петрович', 'petrov@beta.ru', '+79993456789'),
('Гамма', 'ООО Гамма-Инвест', 3456789012, 345678912, 1027700000002, '2021-03-10', 'Сидоров', 'Сидор', 'Сидорович', 'sidorov@gamma.ru', '+79997654321'),
('Дельта', 'ООО Дельта-Финанс', 4567890123, 456789123, 1027700000003, '2018-11-05', 'Кузнецов', 'Николай', 'Алексеевич', 'kuznetsov@delta.ru', '+79998887766'),
('Эпсилон', 'ООО Эпсилон-Банк', 5678901234, 567890134, 1027700000004, '2022-08-01', 'Морозов', 'Дмитрий', 'Сергеевич', 'morozov@epsilon.ru', '+79990001122');

SELECT * FROM company;
---------------------------------

INSERT INTO bank (bank_id) VALUES (1);
INSERT INTO bank (bank_id) VALUES (2);
INSERT INTO bank (bank_id) VALUES (3);
INSERT INTO bank (bank_id) VALUES (4);
INSERT INTO bank (bank_id) VALUES (5);

SELECT * FROM bank;
---------------------------------

INSERT INTO agent (agent_id, priority) VALUES (1, true);
INSERT INTO agent (agent_id, priority) VALUES (2, false);
INSERT INTO agent (agent_id, priority) VALUES (3, true);
INSERT INTO agent (agent_id, priority) VALUES (4, false);
INSERT INTO agent (agent_id, priority) VALUES (5, true);

SELECT * FROM agent;
---------------------------------

INSERT INTO client (client_id) VALUES (1);
INSERT INTO client (client_id) VALUES (2);
INSERT INTO client (client_id) VALUES (3);
INSERT INTO client (client_id) VALUES (4);
INSERT INTO client (client_id) VALUES (5);

SELECT * FROM client;
---------------------------------

INSERT INTO bank_agent_relation (bank_id, agent_id) VALUES (1, 2);
INSERT INTO bank_agent_relation (bank_id, agent_id) VALUES (1, 3);
INSERT INTO bank_agent_relation (bank_id, agent_id) VALUES (5, 4);
INSERT INTO bank_agent_relation (bank_id, agent_id) VALUES (5, 2);
INSERT INTO bank_agent_relation (bank_id, agent_id) VALUES (3, 4);

SELECT * FROM bank_agent_relation;
---------------------------------
