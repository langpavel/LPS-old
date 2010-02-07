SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = off;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET escape_string_warning = off;

CREATE TABLE users (
    id bigserial not null primary key,
    username character varying(40) NOT NULL,
    passwd character varying(40) NOT NULL,
    first_name character varying(40) NOT NULL,
    surname character varying(40) NOT NULL
);
INSERT INTO users VALUES (1, 'langpa', '6eVgZj7YKaN/HcJByphPGQliw4s=', 'Pavel', 'Lang'); -- prazdne heslo pro langpa

CREATE TABLE c_druh_adresy (
    id bigserial not null primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    aktivni bool default true,
    fakturacni bool,
    dodaci bool,

    id_user_create bigint references users(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references users(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
INSERT INTO c_druh_adresy VALUES (1, 'FA', 'Fakturační', true, true, null, null, null, null, null);
INSERT INTO c_druh_adresy VALUES (2, 'DODACI', 'Dodací', true, false, true, null, null, null, null);
INSERT INTO c_druh_adresy VALUES (3, 'JINA', 'Jiná', true, null, null, null, null, null, null);
INSERT INTO c_druh_adresy VALUES (4, 'NAPLATNA', 'NEPLATNÁ ADRESA', false, null, null, null, null, null, null);

CREATE TABLE c_dph (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    hodnota decimal(2,2) not null,
    plati_od date,
    plati_do date,
    vychozi bool not null default false,

    id_user_create bigint references users(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references users(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
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

    id_user_create bigint references users(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references users(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
INSERT INTO c_mj VALUES (1, 'KS', 'Kus', 'Kusy', null, null, null, null);

CREATE TABLE c_kategorie (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    aktivni bool not null default true,

    id_user_create bigint references users(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references users(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE c_zaruka (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    poznamka text,
    plati_od date,
    plati_do date,
    vychozi bool not null default false,

    id_user_create bigint references users(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references users(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE c_sklad (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    poznamka text,

    id_user_create bigint references users(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references users(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE adresa (
    id bigserial NOT NULL primary key,
    id_druh_adresy bigint NOT NULL references c_druh_adresy(id),
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
    zeme character varying(100),
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

    id_user_create bigint references users(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references users(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);


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

    id_user_create bigint references users(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references users(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

