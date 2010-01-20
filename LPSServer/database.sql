DROP TABLE c_druh_adresy;
DROP TABLE adresa;

CREATE TABLE c_druh_adresy (
    id serial not null primary key,
    kod character varying(10) not null,
    popis character varying(100),
    aktivni bool,
    fakturacni bool,
    dodaci bool
);


CREATE TABLE adresa (
    id serial NOT NULL primary key,
    id_druh_adresy integer NOT NULL references c_druh_adresy(id),
    id_group integer,
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
    id_user_create integer references users(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify integer references users(id),
    dt_modify timestamp without time zone DEFAULT now()
);

