CREATE TABLE Audiobooks (
    Id INT PRIMARY KEY,
    BookId INT,
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);

CREATE TABLE People (
    Id INT PRIMARY KEY,
    Surname VARCHAR(255),
    Name VARCHAR(255)
);