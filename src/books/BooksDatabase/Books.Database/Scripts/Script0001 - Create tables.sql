﻿CREATE TABLE Books(
	BookId		INT GENERATED ALWAYS AS IDENTITY,
	ISBN		VARCHAR(13) NOT NULL,
	Description VARCHAR(1000) NULL,
	Title		VARCHAR(100) NOT NULL,
	Authors		VARCHAR(100) NOT NULL,
	CoverImg	VARCHAR(100),
	PRIMARY KEY	(BookId)
);

CREATE TABLE BooksRead(
	BookId		INT GENERATED ALWAYS AS IDENTITY,
	ISBN		VARCHAR(13) NOT NULL,
	Description VARCHAR(1000) NULL,
	Title		VARCHAR(100) NOT NULL,
	Authors		VARCHAR(100) NOT NULL,
	CoverImg	VARCHAR(100),
	CopiesCount INT,
	PRIMARY KEY	(BookId)
);

CREATE TABLE BookInstances(
	InstanceId	INT GENERATED ALWAYS AS IDENTITY,
	BookId		INT NOT NULL,
	ISBN		VARCHAR(13) NOT NULL,
	IsAvailable BOOL NOT NULL,
	PRIMARY KEY	(InstanceId),
	CONSTRAINT	BookId
		FOREIGN KEY(BookId)
			REFERENCES BooksRead(BookId)
);