--- 910_foreign_keys.sql

ALTER TABLE sys_app_config ADD FOREIGN KEY (id_adresa) REFERENCES adresa;

ALTER TABLE c_pobocka ADD FOREIGN KEY (id_adresa) REFERENCES adresa;
