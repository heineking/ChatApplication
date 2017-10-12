# Chat Application #

**Author:** Emil Heineking

This project was written for my spring 2017 senior seminar course at the University of Akron. The following were generated for the project:

[Google Drive Powerpoint Presentation](https://docs.google.com/presentation/d/1l-saXNZMCllH0yKhfRe8lgY208aBpzSSRAhSDKFznjU/edit?usp=sharing)

[Written Report](https://docs.google.com/document/d/104o2H2GrvMzRZKqNaJq91tdlJsxpefcnvskF4EhIDEk/edit?usp=sharing)



This project is a side-project that was created purely for exploratory and academic
reasons. There are likely some issues with my implementations so beware if you
use this project as a guide for any production code.

The project investigates the following concepts:

## Server ##

#### Security ####
* JSON Web Token (JWT)
* Hashed & Salted Passwords
* Brute-force detector

#### RESTFul API ####
* NancyFx
* CORS enabled
* POST, GET, & DELETE verbs
* Stateless Authorization

#### Dependency Injection ####

Dependency injection is handled using **tinyIoC**. There are examples of
registering multiple layered decorators. This is what powers the decoupled
approach of this project.

#### Data Access Layer ####

* EntityFramework
  * Code-First
  * Configurable tracing available
* Dapper
* MongoDb
  * available on different branch
* CQRS

#### Logging ####

* Log4Net
  * Custom levels defined
  * Namespace level loggers

#### Patterns (General) ####

* Decorator
* Observer / Subscriber
* Command
* Stairway Pattern

#### Testing ####

* Moq
* NUnit
* Nancy.Testing

## Client & User Interface ##

* React
  * ReactCreateApp
* Redux
* React-Router
* WebPack 2.0
* MaterialUI
* LocalStorage


## Left to be Done (non-exhaustive) ##

* Editable rooms & messages
* Persist login on page refresh
* New user Registration

## Notice ##

It is unlikely that I will be able to put that much work into this project. I
started this project while I was still in school when I had more free time. I would
like to keep working on this project when I have time. I might shift my focus
to smaller more manageable projects.
