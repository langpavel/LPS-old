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

