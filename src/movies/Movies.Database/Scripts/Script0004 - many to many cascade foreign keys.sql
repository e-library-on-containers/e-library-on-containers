ALTER TABLE MovieActors
    DROP CONSTRAINT movieactors_actorid_fkey;
        
ALTER TABLE MovieActors
    DROP CONSTRAINT movieactors_movieid_fkey;

ALTER TABLE MovieActors
    ADD CONSTRAINT movieactors_actorid_fkey
    FOREIGN KEY (ActorId) REFERENCES Actors(PersonId) ON DELETE CASCADE;

ALTER TABLE MovieActors
    ADD CONSTRAINT movieactors_movieid_fkey
    FOREIGN KEY (MovieId) REFERENCES Movies(Id) ON DELETE CASCADE;

ALTER TABLE MovieScreenwriters
    DROP CONSTRAINT moviescreenwriters_screenwriterid_fkey;
        
ALTER TABLE MovieScreenwriters
    DROP CONSTRAINT moviescreenwriters_movieid_fkey;

ALTER TABLE MovieScreenwriters
    ADD CONSTRAINT moviescreenwriters_screenwriterid_fkey
    FOREIGN KEY (ScreenwriterId) REFERENCES Screenwriters(PersonId) ON DELETE CASCADE;

ALTER TABLE MovieScreenwriters
    ADD CONSTRAINT moviescreenwriters_movieid_fkey
    FOREIGN KEY (MovieId) REFERENCES Movies(Id) ON DELETE CASCADE;

ALTER TABLE MovieDirectors
    DROP CONSTRAINT moviedirectors_directorid_fkey;
        
ALTER TABLE MovieDirectors
    DROP CONSTRAINT moviedirectors_movieid_fkey;

ALTER TABLE MovieDirectors
    ADD CONSTRAINT moviedirectors_directorid_fkey
    FOREIGN KEY (DirectorId) REFERENCES Directors(PersonId) ON DELETE CASCADE;

ALTER TABLE MovieDirectors
    ADD CONSTRAINT moviedirectors_movieid_fkey
    FOREIGN KEY (MovieId) REFERENCES Movies(Id) ON DELETE CASCADE;

