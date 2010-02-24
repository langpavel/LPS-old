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


