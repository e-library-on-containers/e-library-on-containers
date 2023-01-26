﻿DROP TABLE IF EXISTS BookInstances;
DROP TABLE IF EXISTS BooksRead;
DROP TABLE IF EXISTS Books;

CREATE TABLE Books(
	BookId		INT GENERATED ALWAYS AS IDENTITY,
	ISBN		VARCHAR(13) NOT NULL,
	Description VARCHAR(1000) NULL,
	Title		VARCHAR(100) NOT NULL,
	Authors		VARCHAR(100) NOT NULL,
	CoverImg	VARCHAR(100) NOT NULL,
	PRIMARY KEY	(BookId)
);

CREATE TABLE BookInstances(
	InstanceId	INT GENERATED ALWAYS AS IDENTITY,
	BookId		INT NOT NULL,
	ISBN		VARCHAR(13) NOT NULL,
	IsAvailable BIT NOT NULL,
	PRIMARY KEY	(InstanceId),
	CONSTRAINT	BookId
		FOREIGN KEY(BookId)
			REFERENCES Books(BookId)
);

CREATE TABLE BooksRead(
	BookId		INT GENERATED ALWAYS AS IDENTITY,
	ISBN		VARCHAR(13) NOT NULL,
	Description VARCHAR(1000) NULL,
	Title		VARCHAR(100) NOT NULL,
	Authors		VARCHAR(100) NOT NULL,
	PRIMARY KEY	(BookId)
);