CREATE TABLE People (
    Id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Surname VARCHAR(255),
    Name VARCHAR(255)
);

CREATE TABLE Actors (
    PersonId INT PRIMARY KEY,
    FOREIGN KEY (PersonId) REFERENCES People(Id)
);

CREATE TABLE Screenwriters (
   PersonId INT PRIMARY KEY,
   FOREIGN KEY (PersonId) REFERENCES People(Id)
);

CREATE TABLE Directors (
   PersonId INT PRIMARY KEY,
   FOREIGN KEY (PersonId) REFERENCES People(Id)
);

CREATE TABLE Movies (
    Id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Title VARCHAR(255),
    Duration INT,
    Category VARCHAR(255),
    InPreview BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE MovieScreenwriters (
    ScreenwriterId INT,
    MovieId INT,
    PRIMARY KEY (ScreenwriterId, MovieId),
    FOREIGN KEY (ScreenwriterId) REFERENCES Screenwriters(PersonId),
    FOREIGN KEY (MovieId) REFERENCES Movies(Id)
);

CREATE TABLE MovieActors (
     ActorId INT,
     MovieId INT,
     PRIMARY KEY (ActorId, MovieId),
     FOREIGN KEY (ActorId) REFERENCES Actors(PersonId),
     FOREIGN KEY (MovieId) REFERENCES Movies(Id)
);

CREATE TABLE MovieDirectors (
    DirectorId INT,
    MovieId INT,
    PRIMARY KEY (DirectorId, MovieId),
    FOREIGN KEY (DirectorId) REFERENCES Directors(PersonId),
    FOREIGN KEY (MovieId) REFERENCES Movies(Id)
);
