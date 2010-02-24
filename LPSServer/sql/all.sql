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


--- 110_kurz.sql

-- kurz: mnozstvi id_mena_cizi == hodnota_mnozstvi id_mena_kurz
-- kurz: 1 id_mena_cizi == hodnota id_mena_kurz
CREATE TABLE kurz (
    id bigserial NOT NULL primary key,
    id_mena_cizi bigint not null references c_mena(id),
    id_mena_kurz bigint not null references c_mena(id),

    hodnota_mnozstvi decimal(28,14) not null,
    mnozstvi int not null,
    hodnota decimal(28,14) not null CHECK (hodnota = (hodnota_mnozstvi / mnozstvi)),
    
    plati_od timestamp,
    plati_do timestamp,

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);


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

--- 300_produkt.sql

/* film, plakat, tricko */
CREATE TABLE c_druh_produktu (
    id bigserial NOT NULL primary key,
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE c_produkt_varianta (
    id bigserial NOT NULL primary key,
    id_druh_produktu bigint not null references c_druh_produktu(id),
    kod character varying(10) not null UNIQUE,
    popis character varying(100) not null default '',

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE produkt (
    id bigserial NOT NULL primary key,
    id_druh_produktu bigint not null references c_druh_produktu(id),
    id_zaruka bigint references c_zaruka(id),
    extern_id character varying(50),
    id_dph bigint references c_dph(id),

    cislo character varying(10) not null,

    nazev character varying(100),
    nazev2 character varying(100),
    popis text,
    keywords text,

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE produkt_dodavatel (
    id bigserial NOT NULL primary key,
    id_produkt bigint not null references produkt(id),
    id_produkt_varianta bigint not null references c_produkt_varianta(id),
    id_adresa bigint not null references adresa(id),

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);


--- 400_sklad.sql


CREATE TABLE skl_karta (
    id bigserial NOT NULL primary key,

    cislo character varying(10) NOT NULL UNIQUE,

    id_sklad bigint NOT NULL references c_sklad(id),
    id_kategorie bigint references c_kategorie(id),
    id_dph bigint NOT NULL references c_dph(id),
    id_mj bigint NOT NULL references c_mj(id),
    id_zaruka bigint references c_zaruka(id),

    id_adresa_dodavatel bigint references adresa(id),
    id_produkt bigint references produkt(id),
    id_produkt_varianta bigint references c_produkt_varianta(id),

    ean character varying(100),
    nazev1 character varying(100),
    nazev2 character varying(100),
    popis text,
    
    skl_cena decimal(28,14),
    skl_hodnota decimal(15,2),
    skl_mnoz decimal(28,14),

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE skl_pohyb (
    id bigserial NOT NULL primary key,
    id_sklad bigint not null references c_sklad(id),
    id_skl_pohyb_druh bigint not null references c_skl_pohyb_druh(id),
    id_mena bigint not null references c_mena(id),

    cislo character varying(20) not null unique,
    popis character varying(100) not null default '',
    datum_pohybu date not null,
    datum_uc date not null,
    strana integer not null CHECK (strana = 1 or strana = -1),

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

CREATE TABLE skl_pohyb_pol (
    id bigserial NOT NULL primary key,
    id_skl_pohyb bigint not null references skl_pohyb(id),
    id_skl_karta bigint not null references skl_karta(id),
    id_skl_pohyb_pol_druh bigint not null references c_skl_pohyb_pol_druh(id),
    strana integer not null CHECK (strana = 1 or strana = -1),

    

    id_user_create bigint references sys_user(id),
    dt_create timestamp without time zone DEFAULT now(),
    id_user_modify bigint references sys_user(id),
    dt_modify timestamp without time zone DEFAULT now(),
    ts timestamp without time zone DEFAULT now()
);

--- 900_indices.sql

CREATE INDEX adr_obec_ts ON adr_obec (ts);
CREATE INDEX adresa_ts ON adresa (ts);
CREATE INDEX c_dph_ts ON c_dph (ts);
CREATE INDEX c_druh_adresy_ts ON c_druh_adresy (ts);
CREATE INDEX c_druh_produktu_ts ON c_druh_produktu (ts);
CREATE INDEX c_kategorie_ts ON c_kategorie (ts);
CREATE INDEX c_mena_ts ON c_mena (ts);
CREATE INDEX c_mj_ts ON c_mj (ts);
CREATE INDEX c_pobocka_ts ON c_pobocka (ts);
CREATE INDEX c_pokladna_ts ON c_pokladna (ts);
CREATE INDEX c_produkt_varianta_ts ON c_produkt_varianta (ts);
CREATE INDEX c_skl_pohyb_druh_ts ON c_skl_pohyb_druh (ts);
CREATE INDEX c_skl_pohyb_pol_druh_ts ON c_skl_pohyb_pol_druh (ts);
CREATE INDEX c_sklad_ts ON c_sklad (ts);
CREATE INDEX c_stat_ts ON c_stat (ts);
CREATE INDEX c_zaruka_ts ON c_zaruka (ts);
CREATE INDEX kurz_ts ON kurz (ts);
CREATE INDEX produkt_ts ON produkt (ts);
CREATE INDEX produkt_dodavatel_ts ON produkt_dodavatel (ts);
CREATE INDEX skl_karta_ts ON skl_karta (ts);
CREATE INDEX skl_pohyb_ts ON skl_pohyb (ts);
CREATE INDEX skl_pohyb_pol_ts ON skl_pohyb_pol (ts);
CREATE INDEX sys_app_config_ts ON sys_app_config (ts);
CREATE INDEX sys_attachement_ts ON sys_attachement (ts);
CREATE INDEX sys_deleted_ts ON sys_deleted (ts);
CREATE INDEX sys_error_ts ON sys_error (ts);
CREATE INDEX sys_gen_ts ON sys_gen (ts);
CREATE INDEX sys_gen_cyklus_ts ON sys_gen_cyklus (ts);
CREATE INDEX sys_gen_value_ts ON sys_gen_value (ts);
CREATE INDEX sys_check_ts ON sys_check (ts);
CREATE INDEX sys_user_ts ON sys_user (ts);
CREATE INDEX sys_user_preferences_ts ON sys_user_preferences (ts);
--- 910_foreign_keys.sql

ALTER TABLE sys_app_config ADD FOREIGN KEY (id_adresa) REFERENCES adresa;

ALTER TABLE c_pobocka ADD FOREIGN KEY (id_adresa) REFERENCES adresa;
