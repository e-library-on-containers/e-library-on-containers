INSERT INTO People (Id, Surname, Name) VALUES
    (1, 'Smith', 'John'),
    (2, 'Johnson', 'Alice'),
    (3, 'Brown', 'Robert'),
    (4, 'Davis', 'Emma'),
    (5, 'Miller', 'Michael'),
    (6, 'Wilson', 'Sophia'),
    (7, 'Moore', 'William'),
    (8, 'Taylor', 'Olivia');

INSERT INTO Screenwriters (PersonId) VALUES (1), (2), (3), (4);

INSERT INTO Directors (PersonId) VALUES (5), (6), (7), (8);

INSERT INTO Actors (PersonId) VALUES (1), (2), (3), (4);

INSERT INTO Movies (Id, Title, Duration, Category, InPreview) VALUES
      (1, 'The Lost World', 120, 'Action', 1),
      (2, 'Eternal Love', 110, 'Drama', 0),
      (3, 'Laugh Out Loud', 95, 'Comedy', 1),
      (4, 'Galactic Odyssey', 130, 'Sci-Fi', 1),
      (5, 'Hidden Secrets', 105, 'Thriller', 0);

INSERT INTO MovieScreenwriters (ScreenwriterId, MovieId) VALUES
     (1, 'The Lost World'), (2, 'The Lost World'),
     (3, 'Eternal Love'), (4, 'Eternal Love'),
     (1, 'Laugh Out Loud'), (2, 'Laugh Out Loud'),
     (3, 'Galactic Odyssey'), (4, 'Galactic Odyssey'),
     (1, 'Hidden Secrets'), (2, 'Hidden Secrets');

INSERT INTO MovieDirectors (DirectorId, MovieId) VALUES
     (5, 'The Lost World'), (6, 'The Lost World'),
     (7, 'Eternal Love'), (8, 'Eternal Love'),
     (5, 'Laugh Out Loud'), (6, 'Laugh Out Loud'),
     (7, 'Galactic Odyssey'), (8, 'Galactic Odyssey'),
     (5, 'Hidden Secrets'), (6, 'Hidden Secrets');

INSERT INTO MovieActors (ActorId, MovieId) VALUES
   (1, 'The Lost World'), (2, 'The Lost World'),
   (3, 'Eternal Love'), (4, 'Eternal Love'),
   (1, 'Laugh Out Loud'), (2, 'Laugh Out Loud'),
   (3, 'Galactic Odyssey'), (4, 'Galactic Odyssey'),
   (1, 'Hidden Secrets'), (2, 'Hidden Secrets');
