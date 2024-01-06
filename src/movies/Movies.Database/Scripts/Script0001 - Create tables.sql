CREATE TABLE People (
    Id INT PRIMARY KEY,
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
    Id INT PRIMARY KEY,
    Title VARCHAR(255),
    Duration INT,
    Category VARCHAR(255),
    InPreview BIT DEFAULT TRUE
);


CREATE TABLE MovieScreenwriters (
    ScreenwriterId INT,
    MovieId VARCHAR(255),
    PRIMARY KEY (ScreenwriterId, MovieId),
    FOREIGN KEY (ScreenwriterId) REFERENCES Screenwriters(Id),
    FOREIGN KEY (MovieId) REFERENCES Movies(Title)
);


CREATE TABLE MovieActors (
     ActorId INT,
     MovieId VARCHAR(255),
     PRIMARY KEY (ActorId, MovieId),
     FOREIGN KEY (ActorId) REFERENCES Actors(Id),
     FOREIGN KEY (MovieId) REFERENCES Movies(Title)
);

CREATE TABLE MovieDirectors (
    DirectorId INT,
    MovieId VARCHAR(255),
    PRIMARY KEY (DirectorId, MovieId),
    FOREIGN KEY (DirectorId) REFERENCES Directors(Id),
    FOREIGN KEY (MovieId) REFERENCES Movies(Title)
);
