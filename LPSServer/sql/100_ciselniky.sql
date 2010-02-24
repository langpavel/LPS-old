--- 100_ciselniky.sql

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
INSERT INTO c_stat VALUES (1, 'CZ', 'Česká republika', null, null, null, null, now());
INSERT INTO c_stat VALUES (2, 'SK', 'Slovenská republika', null, null, null, null, now());

CREATE TABLE c_mena (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    zkratka character varying(10) not null,
    popis character varying(100) not null default '',
    format character varying(100) not null default '#,##0.00',

    plati_od date,
    plati_do date,
    vychozi bool not null default false,

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);
INSERT INTO c_mena VALUES (1, 'CZK', 'Kč', 'Česká koruna', '#,##0.00\' Kč\'', null, null, true, 0, now(), 0, now(), now());
INSERT INTO c_mena VALUES (2, 'EUR', '€', 'Euro', '\'€\'#,##0.00', null, null, false, 0, now(), 0, now(), now());

CREATE TABLE c_pobocka
(
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    id_adresa bigint not null, -- fk

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE c_pokladna
(
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    id_pobocka bigint references c_pobocka(id),
    id_mena bigint references c_mena(id),

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE c_skl_pohyb_druh (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    strana integer not null CHECK (strana = 1 or strana = -1),
    id_sys_gen bigint not null references sys_gen(id),

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE c_skl_pohyb_pol_druh (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',
    strana integer not null CHECK (strana = 1 or strana = -1),
    id_c_skl_pohyb_druh bigint references c_skl_pohyb_druh(id),

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);


