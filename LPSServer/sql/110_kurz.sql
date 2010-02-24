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


