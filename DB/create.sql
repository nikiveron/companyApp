CREATE TABLE company
(
	company_id SERIAL PRIMARY KEY,
    short_name VARCHAR(30) NOT NULL,
    full_name VARCHAR(30) NOT NULL,
	inn BIGINT NOT NULL,
	kpp INTEGER NOT NULL,
	ogrn BIGINT NOT NULL,
	ogrn_date_of_issue DATE NOT NULL,
	rep_last_name VARCHAR(30) NOT NULL,
	rep_first_name VARCHAR(30) NOT NULL,
	rep_patronymic VARCHAR(30) NOT NULL,
	rep_email VARCHAR(30) NOT NULL,
	rep_phone VARCHAR(30) NOT NULL,
	deleted_at timestamp DEFAULT NULL
);
CREATE TABLE bank
(
	bank_id INTEGER UNIQUE,
	FOREIGN KEY (bank_id) REFERENCES company(company_id) ON DELETE CASCADE,
	deleted_at timestamp DEFAULT NULL
);
CREATE TABLE agent
(
	agent_id INTEGER UNIQUE,
	FOREIGN KEY (agent_id) REFERENCES company(company_id) ON DELETE CASCADE,
	priority boolean NOT NULL,
	deleted_at timestamp DEFAULT NULL
);
CREATE TABLE client
(
	client_id INTEGER UNIQUE,
	FOREIGN KEY (client_id) REFERENCES company(company_id) ON DELETE CASCADE,
	deleted_at timestamp DEFAULT NULL
);
CREATE TABLE bank_agent_relation
(
	bank_id INTEGER,
	agent_id INTEGER,
	FOREIGN KEY (bank_id) REFERENCES bank(bank_id) ON DELETE CASCADE,
	FOREIGN KEY (agent_id) REFERENCES agent(agent_id) ON DELETE CASCADE,
	PRIMARY KEY(bank_id, agent_id),
	deleted_at timestamp DEFAULT NULL
);