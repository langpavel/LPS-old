--- 200_adresa.sql

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
    aktivni bool not null default true,
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
INSERT INTO adresa VALUES (1, 3, 1, null, '24 Promotions s.r.o.');

CREATE TABLE adr_obec (
    id bigserial NOT NULL primary key,


    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

