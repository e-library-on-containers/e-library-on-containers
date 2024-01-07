ALTER TABLE Actors
    DROP CONSTRAINT actors_personid_fkey;

ALTER TABLE Actors
    ADD CONSTRAINT actors_personid_fkey
    FOREIGN KEY (PersonId) REFERENCES People(Id) ON DELETE CASCADE;

ALTER TABLE Screenwriters
    DROP CONSTRAINT screenwriters_personid_fkey;

ALTER TABLE Screenwriters
    ADD CONSTRAINT screenwriters_personid_fkey
    FOREIGN KEY (PersonId) REFERENCES People(Id) ON DELETE CASCADE;

ALTER TABLE Directors
    DROP CONSTRAINT directors_personid_fkey;

ALTER TABLE Directors
    ADD CONSTRAINT directors_personid_fkey
    FOREIGN KEY (PersonId) REFERENCES People(Id) ON DELETE CASCADE;

