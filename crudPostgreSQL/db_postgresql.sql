CREATE TABLE users(
	id serial,
	name varchar,
	email varchar
)

CREATE OR REPLACE FUNCTION insert_users(name varchar, email varchar)
RETURNS VOID AS $$
BEGIN
    INSERT INTO users (name, email) VALUES (name, email);
END;
$$ LANGUAGE plpgsql;


CREATE OR REPLACE FUNCTION get_all_users(table_name text)
RETURNS TABLE (id integer, name varchar, email varchar)
AS $$
BEGIN
    RETURN QUERY EXECUTE 'SELECT * FROM ' || table_name;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION update_users(id_value integer,name_value varchar, email_value varchar)
RETURNS VOID AS $$
BEGIN
    UPDATE users 
	SET name = name_value, email = email_value
	WHERE id = id_value;
END;
$$ LANGUAGE plpgsql;
DROP FUNCTION update_users(integer,character varying,character varying)



SELECT * FROM users
SELECT * FROM get_all_users('users');
SELECT insert_users('Luis','luis@gmail.com')
SELECT update_users(6,'Miguel','miguel@gmail.com')