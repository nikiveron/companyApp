CREATE OR REPLACE VIEW agent_view AS
SELECT 
    a.agent_id,
    c.short_name,
    c.full_name,
	c.inn,
	c.kpp,
	c.ogrn,
	c.ogrn_date_of_issue,
	c.rep_last_name,
	c.rep_first_name,
	c.rep_patronymic,
	c.rep_email,
	c.rep_phone,
	a.priority
FROM company c
INNER JOIN agent a ON a.agent_id = c.company_id
WHERE c.deleted_at IS NULL AND a.deleted_at IS NULL;

DROP VIEW agent_view;

select * from agent_view;
