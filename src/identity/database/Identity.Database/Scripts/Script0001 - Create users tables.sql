CREATE TABLE users (
	id UUID PRIMARY KEY,
	email VARCHAR  unique NOT NULL,
	password VARCHAR  NOT NULL,
	first_name VARCHAR  NOT NULL,
	last_name VARCHAR  NOT NULL
);

CREATE TABLE user_roles (
	user_id UUID,
	role_name VARCHAR  NOT NULL,
	CONSTRAINT fk_user
		FOREIGN KEY(user_id)
			REFERENCES users(id)
);