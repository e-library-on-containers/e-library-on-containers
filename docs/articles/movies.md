
# Identity service

## Tech stack

* .NET 7
* ASP<span>.</span>NET Core Minimal API
* Docker

## Main libraries

* Functionality:
	* Dapper
	* Npgsql
	* DbUp

## Domain

Movies service is designed to manage information about movies, including details about people involved in the movie-making process such as actors, directors, and screenwriters.

## Architectural approach

### Experiment

This API looks like a mess at first glance. Although, because this service isn't too complicated, it was decided to treat as an experiment. The clue of it was utilizing some modern .NET features, e.g.:
- top level statements - no "class Program static void Main" boilerplate code
- minimal API - no controllers, direct route mappings
- simplified Web API setup - no Program.cs, Startup.cs classes with boilerplate code etc.

The main reason was to make development of this service feel like scripting (at least as close to it as it's possible), rather than full-fledged project development. 

Writing the code itself felt a lot "simpler" and less demanding in regards of cognitive overload (no directories/files structure). On the other hand, because of the sheer length of Program.cs file, debugging and reading through code actually imposes more cognitive overload at once. It is a tradeoff where one avoids shuffling between multiple files in the project, but he has to scroll through the whole code which results in eyes jumping between all methods and classes.

To conclude the experiment, it had its ups and downs but overall it wasn't so bad. If the development flow and structure would be optimized a bit (to find a middle ground between fully boilerplated code with structure, clean architecture, separation of concerns etc. and one file unorganized script), it could result in a lot easier and more pleasant experience for developers.

### Description

Movies service utilizes Minimal API to avoid using controllers. Endpoints are simply delegates associated with a route pattern.

For overall simplicity, service/repository pattern wasn't implemented. Simple repositories with database connection are sufficient.

### Database updater

Unrelated to Movies service in terms of code, this service is used for handling SQL scripts/migrations to database using *DbUp* library. Docker container checks schema state, applies SQL scripts/migrations if needed and exits.

## Pipelines

### Publishing Docker images
Publishing pipeline is triggered on pushes to develop (i.e. on merging pull requests). It builds docker image with tags based on commit tags and branch name and publishes it on Docker Hub.