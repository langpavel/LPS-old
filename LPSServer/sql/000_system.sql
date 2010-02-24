--- 000_system.sql

CREATE TABLE sys_user (
    id bigserial not null primary key,
    username character varying(40) NOT NULL UNIQUE,
    passwd character varying(40) NOT NULL,
    first_name character varying(40) NOT NULL,
    surname character varying(40) NOT NULL,

    ts timestamp without time zone DEFAULT now(),
    UNIQUE (first_name, surname)
);
INSERT INTO sys_user VALUES (0, 'system', '', '', 'system');
INSERT INTO sys_user VALUES (1, 'langpa', '6eVgZj7YKaN/HcJByphPGQliw4s=', 'Pavel', 'Lang'); -- prazdne heslo pro langpa

CREATE TABLE sys_deleted (
    id bigserial not null primary key,
    table_name character varying(40) NOT NULL,
    row_id bigint NOT NULL,
    reason text,
    id_user bigint references sys_user(id),
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX sys_deleted_ts_tname on sys_deleted (ts, table_name);

CREATE TABLE sys_check (
    id bigserial not null primary key,
    kod character varying(10) not null UNIQUE,
    popis text,
    sql_cmd text,
    ts_last_run timestamp,
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE sys_error (
    id bigserial not null primary key,
    table_name character varying(40) not null,
    table_id bigint not null,
    id_check bigint references sys_check(id),
    err_code character varying(10) not null,
    popis text not null,
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX sys_error_table_id on sys_error (table_name, table_id);

CREATE TABLE sys_attachement (
    id bigserial not null primary key,
    table_name character varying(40) not null,
    table_id bigint not null,
    id_attachement bigint references sys_attachement(id), -- symlink
    filename character varying(1024) not null,
    mimetype character varying(40) not null default '',
    popis text not null,

    id_user_lock bigint references sys_user(id),
    dt_lock timestamp,

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX sys_attachement_table_id on sys_attachement (table_name, table_id);


CREATE TABLE sys_gen_cyklus (
    id bigserial not null primary key,
    kod character varying(10) not null,
    name character varying(40) not null,
    sys_date bool not null,
    year bool not null,
    month bool not null,
    week bool not null,
    day bool not null,
    
    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
INSERT INTO sys_gen_cyklus VALUES (1, 'INF',   'Stálý', false, false, false, false, false);

INSERT INTO sys_gen_cyklus VALUES (2, 'YEAR_SYS',  'Rok (systémový)',   true,  true,  false, false, false);
INSERT INTO sys_gen_cyklus VALUES (3, 'MONTH_SYS', 'Měsíc (systémový)', true,  true,  true,  false, false);
INSERT INTO sys_gen_cyklus VALUES (4, 'WEEK_SYS',  'Týden (systémový)', true,  true,  false, true,  false);
INSERT INTO sys_gen_cyklus VALUES (5, 'DAY_SYS',   'Den (systémový)',   true,  true,  true,  false, true );

INSERT INTO sys_gen_cyklus VALUES (6, 'YEAR_REAL',  'Rok (aktuální)',   false, true,  false, false, false);
INSERT INTO sys_gen_cyklus VALUES (7, 'MONTH_REAL', 'Měsíc (aktuální)', false, true,  true,  false, false);
INSERT INTO sys_gen_cyklus VALUES (8, 'WEEK_REAL',  'Týden (aktuální)', false, true,  false, true,  false);
INSERT INTO sys_gen_cyklus VALUES (9, 'DAY_REAL',   'Den (aktuální)',   false, true,  true,  false, true );

CREATE TABLE sys_gen (
    id bigserial not null primary key,
    id_cyklus bigint not null references sys_gen_cyklus(id),
    
    kod character varying(10) not null UNIQUE,
    name character varying(40) not null,

    format character varying(100) not null,
    value_first bigint not null default 1,
    value_step bigint not null default 1,

    user_lock bool not null default false,

    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE sys_gen_value (
    id bigserial not null primary key,
    id_gen bigint not null references sys_gen(id),
    
    year int not null default 0,
    month int not null default 0,
    week int not null default 0,
    day int not null default 0,

    value bigint not null,
    user_lock bool not null default false,

    ts timestamp without time zone DEFAULT now(),
    UNIQUE (id_gen, year, month, week, day)
);

CREATE TABLE sys_user_preferences (
    id bigserial not null primary key,
    id_user bigint not null references sys_user(id),
    path character varying(100) not null,
    name character varying(100) not null,
    type character varying(100) not null,
    value text not null,
    ts timestamp without time zone DEFAULT now()
);
CREATE UNIQUE INDEX sys_user_preferences_usr_path_name on sys_user_preferences (id_user, path, name);

CREATE TABLE sys_app_config (
    id bigserial not null primary key,
    sys_rok integer not null,
    sys_mesic integer not null,
    sys_den integer not null,
    sys_date timestamp not null,
    firma character varying(100) NOT NULL,
    id_adresa bigint, -- fk

    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
INSERT INTO sys_app_config VALUES (1, 2010,1,1,'2010-01-01', '24 Promotions s.r.o.', 1, 0, now(), now());


