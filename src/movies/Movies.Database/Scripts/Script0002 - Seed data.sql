INSERT INTO People (Surname, Name)
VALUES ('Smith', 'John'),
       ('Johnson', 'Alice'),
       ('Brown', 'Robert'),
       ('Davis', 'Emma'),
       ('Miller', 'Michael'),
       ('Wilson', 'Sophia'),
       ('Moore', 'William'),
       ('Taylor', 'Olivia');

INSERT INTO Screenwriters (PersonId)
SELECT Id
FROM People
WHERE (Surname = 'Smith' AND Name = 'John')
   OR (Surname = 'Johnson' AND Name = 'Alice')
   OR (Surname = 'Brown' AND Name = 'Robert')
   OR (Surname = 'Davis' AND Name = 'Emma');

INSERT INTO Directors (PersonId)
SELECT Id
FROM People
WHERE (Surname = 'Miller' AND Name = 'Michael')
   OR (Surname = 'Wilson' AND Name = 'Sophia')
   OR (Surname = 'Moore' AND Name = 'William')
   OR (Surname = 'Taylor' AND Name = 'Olivia');

INSERT INTO Actors (PersonId)
SELECT Id
FROM People
WHERE (Surname = 'Smith' AND Name = 'John')
   OR (Surname = 'Johnson' AND Name = 'Alice')
   OR (Surname = 'Brown' AND Name = 'Robert')
   OR (Surname = 'Davis' AND Name = 'Emma');

INSERT INTO Movies (Title, Duration, Category, InPreview)
VALUES ('The Lost World', 120, 'Action', TRUE),
       ('Eternal Love', 110, 'Drama', FALSE),
       ('Laugh Out Loud', 95, 'Comedy', TRUE),
       ('Galactic Odyssey', 130, 'Sci-Fi', TRUE),
       ('Hidden Secrets', 105, 'Thriller', FALSE);

INSERT INTO MovieScreenwriters (ScreenwriterId, MovieId)
VALUES 
(
    (SELECT PersonId FROM Screenwriters sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Smith' AND p.Name = 'John') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'The Lost World' LIMIT 1) 
),
(
    (SELECT PersonId FROM Screenwriters sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Johnson' AND p.Name = 'Alice') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'The Lost World' LIMIT 1)
),
(
    (SELECT PersonId FROM Screenwriters sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Brown' AND p.Name = 'Robert') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Eternal Love' LIMIT 1) 
),
(
    (SELECT PersonId FROM Screenwriters sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Davis' AND p.Name = 'Emma') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Eternal Love' LIMIT 1)
),
(
    (SELECT PersonId FROM Screenwriters sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Smith' AND p.Name = 'John') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Laugh Out Loud' LIMIT 1) 
),
(
    (SELECT PersonId FROM Screenwriters sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Johnson' AND p.Name = 'Alice') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Laugh Out Loud' LIMIT 1)
),
(
    (SELECT PersonId FROM Screenwriters sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Brown' AND p.Name = 'Robert') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Galactic Odyssey' LIMIT 1) 
),
(
    (SELECT PersonId FROM Screenwriters sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Davis' AND p.Name = 'Emma') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Galactic Odyssey' LIMIT 1)
),
(
    (SELECT PersonId FROM Screenwriters sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Smith' AND p.Name = 'John') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Hidden Secrets' LIMIT 1) 
),
(
    (SELECT PersonId FROM Screenwriters sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Johnson' AND p.Name = 'Alice') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Hidden Secrets' LIMIT 1)
);

INSERT INTO MovieDirectors (DirectorId, MovieId)
VALUES
(
    (SELECT PersonId FROM Directors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Miller' AND p.Name = 'Michael') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'The Lost World' LIMIT 1)
),
(
    (SELECT PersonId FROM Directors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Wilson' AND p.Name = 'Sophia') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'The Lost World' LIMIT 1)
),
(
    (SELECT PersonId FROM Directors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Moore' AND p.Name = 'William') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Eternal Love' LIMIT 1)
),
(
    (SELECT PersonId FROM Directors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Taylor' AND p.Name = 'Olivia') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Eternal Love' LIMIT 1)
),
(
    (SELECT PersonId FROM Directors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Miller' AND p.Name = 'Michael') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Laugh Out Loud' LIMIT 1)
),
(
    (SELECT PersonId FROM Directors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Wilson' AND p.Name = 'Sophia') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Laugh Out Loud' LIMIT 1)
),
(
    (SELECT PersonId FROM Directors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Moore' AND p.Name = 'William') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Galactic Odyssey' LIMIT 1)
),
(
    (SELECT PersonId FROM Directors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Taylor' AND p.Name = 'Olivia') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Galactic Odyssey' LIMIT 1)
),
(
    (SELECT PersonId FROM Directors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Miller' AND p.Name = 'Michael') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Hidden Secrets' LIMIT 1)
),
(
    (SELECT PersonId FROM Directors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Wilson' AND p.Name = 'Sophia') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Hidden Secrets' LIMIT 1)
);

INSERT INTO MovieActors (ActorId, MovieId)
VALUES
(
    (SELECT PersonId FROM Actors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Smith' AND p.Name = 'John') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'The Lost World' LIMIT 1)
),
(
    (SELECT PersonId FROM Actors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Johnson' AND p.Name = 'Alice') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'The Lost World' LIMIT 1)
),
(
    (SELECT PersonId FROM Actors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Brown' AND p.Name = 'Robert') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Eternal Love' LIMIT 1)
),
(
    (SELECT PersonId FROM Actors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Davis' AND p.Name = 'Emma') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Eternal Love' LIMIT 1)
),
(
    (SELECT PersonId FROM Actors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Smith' AND p.Name = 'John') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Laugh Out Loud' LIMIT 1)
),
(
    (SELECT PersonId FROM Actors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Johnson' AND p.Name = 'Alice') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Laugh Out Loud' LIMIT 1)
),
(
    (SELECT PersonId FROM Actors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Brown' AND p.Name = 'Robert') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Galactic Odyssey' LIMIT 1)
),
(
    (SELECT PersonId FROM Actors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Davis' AND p.Name = 'Emma') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Galactic Odyssey' LIMIT 1)
),
(
    (SELECT PersonId FROM Actors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Smith' AND p.Name = 'John') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Hidden Secrets' LIMIT 1)
),
(
    (SELECT PersonId FROM Actors sc INNER JOIN People p ON sc.PersonId = p.Id WHERE (p.Surname = 'Johnson' AND p.Name = 'Alice') LIMIT 1),
    (SELECT Id FROM Movies WHERE Title = 'Hidden Secrets' LIMIT 1)
);