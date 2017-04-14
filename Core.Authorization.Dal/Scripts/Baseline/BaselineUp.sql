CREATE TABLE public."user"
(
    id UUID PRIMARY KEY,
    email VARCHAR(200) NOT NULL,
    first_name VARCHAR(100),
    last_name VARCHAR(200),
    birth_date DATE,
    meta_info JSONB,
    password_hash VARCHAR(500),
    photo_id UUID,
    gender_id INT,
    time_zone_id INT,
    social_data JSONB,
    salt VARCHAR(500),
    location_from JSONB,
    create_timestamp TIMESTAMP NOT NULL,
    update_timestamp TIMESTAMP,
    delete_timestamp TIMESTAMP,
    is_active BOOLEAN NOT NULL,
    is_confirm BOOLEAN
);
CREATE UNIQUE INDEX user_email_uindex ON public."user" (email);