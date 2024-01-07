INSERT INTO People (Surname, Name)
VALUES ('Smith', 'John'),
       ('Johnson', 'Alice'),
       ('Brown', 'Robert'),
       ('Davis', 'Emma'),
       ('Miller', 'Michael');

INSERT INTO Books (ISBN, Description, Title, Authors, CoverImg, InPreview)
VALUES ('978316148410', 'Sci-fi novel about space exploration', 'Galactic Odyssey', 'John Smith',
        'galactic_odyssey.jpg', TRUE),
       ('978123456789', 'Mystery thriller set in a small town', 'Hidden Secrets', 'Alice Johnson',
        'hidden_secrets.jpg', TRUE),
       ('978555987654', 'Romantic drama about forbidden love', 'Eternal Love', 'Robert Brown', 'eternal_love.jpg',
        TRUE),
       ('978098765432', 'Action-packed adventure in the jungle', 'Jungle Quest', 'Emma Davis', 'jungle_quest.jpg',
        FALSE),
       ('978234567890', 'Humorous tales from everyday life', 'Laugh Out Loud', 'Michael Miller',
        'laugh_out_loud.jpg', TRUE);

INSERT INTO BooksRead (ISBN, Description, Title, Authors, CoverImg, InPreview, CopiesCount)
VALUES ('978316148410', 'Sci-fi novel about space exploration', 'Galactic Odyssey', 'John Smith',
        'galactic_odyssey.jpg', TRUE, 1),
       ('978123456789', 'Mystery thriller set in a small town', 'Hidden Secrets', 'Alice Johnson',
        'hidden_secrets.jpg', TRUE, 1),
       ('978555987654', 'Romantic drama about forbidden love', 'Eternal Love', 'Robert Brown', 'eternal_love.jpg',
        TRUE, 1),
       ('978098765432', 'Action-packed adventure in the jungle', 'Jungle Quest', 'Emma Davis', 'jungle_quest.jpg',
        FALSE, 1),
       ('978234567890', 'Humorous tales from everyday life', 'Laugh Out Loud', 'Michael Miller',
        'laugh_out_loud.jpg', TRUE, 1);