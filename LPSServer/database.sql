SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = off;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET escape_string_warning = off;

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
CREATE INDEX sys_deleted_ts on sys_deleted (ts);
CREATE INDEX sys_deleted_ts_tname on sys_deleted (ts, table_name);

CREATE TABLE sys_check (
    id bigserial not null primary key,
    kod character varying(10) not null UNIQUE,
    popis text,
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

CREATE TABLE sys_gen_cyklus (
    id bigserial not null primary key,
    kod character varying(10) not null,
    name character varying(40) not null,
    sys_date bool not null,
    year bool not null,
    month bool not null,
    week bool not null,
    day bool not null
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
    value character varying(100) not null,
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX sys_user_preferences_path on sys_user_preferences (path);
CREATE UNIQUE INDEX sys_user_preferences_path_name on sys_user_preferences (path, name);

CREATE TABLE c_druh_adresy (
    id bigserial not null primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    aktivni bool default true,
    fakturacni bool,
    dodaci bool,

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX c_druh_adresy_ts on c_druh_adresy (ts);
INSERT INTO c_druh_adresy VALUES (1, 'FA', 'Fakturační', true, true, null, 0, now(), 0, now(), now());
INSERT INTO c_druh_adresy VALUES (2, 'DODACI', 'Dodací', true, false, true, 0, now(), 0, now(), now());
INSERT INTO c_druh_adresy VALUES (3, 'JINA', 'Jiná', true, null, null, 0, now(), 0, now(), now());
INSERT INTO c_druh_adresy VALUES (4, 'NAPLATNA', 'NEPLATNÁ ADRESA', false, null, null, 0, now(), 0, now(), now());

CREATE TABLE c_dph (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    hodnota decimal(2,2) not null,
    plati_od date,
    plati_do date,
    vychozi bool not null default false,

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX c_dph_ts on c_dph (ts);
INSERT INTO c_dph VALUES (1, 'DPH0', 'Bez DPH', 0.00, null, null, false, null, null, null, null);
INSERT INTO c_dph VALUES (2, 'DPH9', 'DPH 9%', 0.09, null, '1.1.2010', false, null, null, null, null);
INSERT INTO c_dph VALUES (3, 'DPH19', 'DPH 19%', 0.19, null, '1.1.2010', false, null, null, null, null);
INSERT INTO c_dph VALUES (4, 'DPH10', 'DPH 10%', 0.10, '1.1.2010', null, false, null, null, null, null);
INSERT INTO c_dph VALUES (5, 'DPH20', 'DPH 20%', 0.20, '1.1.2010', null, true, null, null, null, null);

CREATE TABLE c_mj (
    id bigserial not null primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    popis2 character varying(100) not null default '',

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX c_mj_ts on c_mj (ts);
INSERT INTO c_mj VALUES (1, 'KS', 'Kus', 'Kusy', null, null, null, null);

CREATE TABLE c_kategorie (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    aktivni bool not null default true,

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX c_kategorie_ts on c_kategorie (ts);

CREATE TABLE c_zaruka (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    poznamka text,
    plati_od date,
    plati_do date,
    vychozi bool not null default false,

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX c_zaruka_ts on c_zaruka (ts);

CREATE TABLE c_sklad (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    poznamka text,

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX c_sklad_ts on c_sklad (ts);

CREATE TABLE c_stat (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX c_stat_ts on c_stat (ts);
INSERT INTO c_stat VALUES (1, 'CZ', 'Česká republika', null, null, null, null, now());
INSERT INTO c_stat VALUES (2, 'SK', 'Slovenská republika', null, null, null, null, now());


CREATE TABLE adresa (
    id bigserial NOT NULL primary key,
    id_druh_adresy bigint NOT NULL references c_druh_adresy(id),
    id_stat bigint references c_stat(id),
    id_group bigint,

    nazev1 character varying(100),
    nazev2 character varying(100),
    ico character varying(20),
    dic character varying(20),
    prijmeni character varying(100),
    jmeno character varying(100),
    jmeno2 character varying(100),
    ulice character varying(100),
    mesto character varying(100),
    psc character varying(20),
    email character varying(100),
    telefon1 character varying(100),
    telefon2 character varying(100),
    poznamka text,
    aktivni bool,
    fakturacni bool,
    dodaci bool,
    dodavatel bool,
    odberatel bool,
    import_php_str_hash character varying(40),
    import_objed_cislo character varying(10),

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX adresa_ts on adresa (ts);
INSERT INTO adresa VALUES (1, 3, 1, null, '24 Promotions s.r.o.');

CREATE TABLE sys_app_config (
    id bigserial not null primary key,
    sys_rok integer not null,
    sys_mesic integer not null,
    sys_den integer not null,
    sys_date timestamp not null,
    firma character varying(100) NOT NULL,
    id_adresa bigint references adresa(id),

    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
INSERT INTO sys_app_config VALUES (1, 2010,1,1,'2010-01-01', '24 Promotions s.r.o.', 1, 0, now(), now());

CREATE TABLE skl_karta (
    id bigserial NOT NULL primary key,
    cislo character varying(10) UNIQUE,
    extern_id character varying(50),

    id_sklad bigint NOT NULL references c_sklad(id),
    id_kategorie bigint references c_kategorie(id),
    id_dph bigint NOT NULL references c_dph(id),
    id_mj bigint NOT NULL references c_mj(id),
    id_zaruka bigint references c_zaruka(id),

    id_adresa_dodavatel bigint references adresa(id),

    ean character varying(100),
    nazev1 character varying(100),
    nazev2 character varying(100),
    popis1 text,
    
    skl_cena decimal(28,14),
    skl_hodnota decimal(15,2),
    skl_mnoz decimal(28,14),

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
CREATE INDEX skl_karta_ts on skl_karta (ts);


