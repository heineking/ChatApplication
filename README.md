# ChatApplication #

Emil Heineking

Senior Seminar, Spring '17

Term Project

## Motivation ##

Learn better OOP design and best practices through trying to implement SOLID principles.

## ChatApplication.Data ##

This is the portion of the project that defines the data access layer. The approach I am taking is to model a generic repository pattern with a unit of work pattern for the persistence layer. A repository should be considered an in-memory collection of objects, and **not** a part of the application that will actually provide any logic for persisting to the database. With this in mind, the repository should be implemented similar to a IList. The repository will allow the client to load collections from the database into memory and provide the user the ability to manipulate that collection. Then the UnitOfWork implementation will handle the actual logic that persists the in-memory representation down to the database layer.

This part of the application is also a good opportunity to show the power of using SOLID principles, and leveraging code reuse.

Consider the following implementation of a RoomRepository:

```csharp

public class MessageRepository : IMessageRepository
{
    private readonly IRepositoryWriter<MessageRecord> _writerDelegate;
    private readonly IRepositoryReader<MessageRecord> _readerDelegate;

    public MessageRepository(IRepositoryWriter<MessageRecord> writerDelegate, IRepositoryReader<MessageRecord> readerDelegate)
    {
        _writerDelegate = writerDelegate;
        _readerDelegate = readerDelegate;
    }
    public MessageRecord Get(int id)
    {
        return _readerDelegate.Get(id);
    }

    /* code removed for brevity */

}

```

This pattern is decorating the ```IRepositoryWriter``` and ```IRepositoryReader``` implementations. This allows the client of the application to be able to supply different approaches to writing and reading to the database.

There are often different needs for accessing and manipulating data. In a world of short attention spans, data often needs to be accessed as quick as possible.

#### Motivation / Background ####

Consider this [article](http://www.globaldots.com/how-website-speed-affects-conversion-rates/). The article comments on how Walmart experienced a 2% increase in the number of visitors who ended up buying a product. They also found that for every 100ms of improvement they grew incremental revenue by up to 1%.

Although there are many aspects that impact page-load time like: efficient caching, code-splitting, network load and balancing, retrieving data is an important part.

A common approach to developing a Data Access Layer (DAL) is to wrap an Object Relational Mapper (ORM) over the database. The recommending Microsoft solution is to use EntityFramework, which is widely adopted. This is exactly what StackOverflow, which has 5+ billion views a year, used before experiencing detrimental performance problems. The creators of Stackoverlfow decided to role their own micro-ORM, Dapper which alleviated these problems. Dapper is now widely used in the industry for its highly performant nature.

Julie Lerman compares EntityFramework and Dapper in this [MSDN article](https://msdn.microsoft.com/en-us/magazine/mt703432.aspx). She finds that Dapper out performs LINQ to EF by over 50%, but finds marginal difference between EF Raw SQL and Dapper. She concludes that Dapper should be considered for applications that must be highly performant, but if the application is already implemented using EF then try writing raw SQL for EF before switching the whole application to Dapper.

The power of EF, in my opinion, is it's change tracking ability. We can load a record from the database, make changes (or deletion) and simply call ```saveChanges``` and EF will figure out the appropriate SQL query to execute. The benefit is that, if there were multiple changes, EF will construct the query to update in one batch. This minimizes the number of trips to the database which could potentially help applications which are sensitive to database load and network connectivity issues.

#### How does this relate to our repository pattern? ####

With our application we are allowing the client to be flexible with the implementation of how to access and save to the database. The client can decide to write both the write and reads in EntityFramework, and then if they would like to change one of the approaches they could simply swap out the read implentation for something written in Dapper or EF with Raw SQL.

This should, however, be done with care. Swapping out the read function for Dapper will break EFs change tracking.

#### Can we further abstract this Repository pattern? ####

Yes, we can further abstract this pattern. The approach we are taking here is generic, and we could create a repository class which other repositories will inherit:

```csharp

/*
  Generic Repository Class
 */
public class Repository<TEntity> : IRepositoryReader<TEntity>, IRepositoryWriter<TEntity> where TEntity : class
{
    protected readonly IRepositoryReader<TEntity> ReaderDelegate;
    protected readonly IRepositoryWriter<TEntity> WriterDelegate;

    protected Repository(IRepositoryReader<TEntity> reader, IRepositoryWriter<TEntity> writer)
    {
        ReaderDelegate = reader;
        WriterDelegate = writer;
    }
    public TEntity Get(int id)
    {
        return ReaderDelegate.Get(id);
    }
    /* code removed for brevity */
}
```

Now we can define MessageRepository and RoomRepository as the following:

```csharp

public class MessageRepository : Repository<MessageRecord>, IMessageRepository
{
    public MessageRepository(IRepositoryReader<MessageRecord> reader, IRepositoryWriter<MessageRecord> writer) : base(reader, writer) { }
}

public class RoomRepository : Repository<RoomRecord>, IRoomRepository
{
    public RoomRepository(IRepositoryReader<RoomRecord> reader, IRepositoryWriter<RoomRecord> writer) : base(reader, writer) { }
}

```

## Authentication - JWT ##

This application will be using JSON Web Token specification for stateless authentication through NancyFx piplelines.

### What is a JSON Web Token (JWT)? ###

The web is stateless which means that data cannot be saved across requests. This means that in order to protect resources the user must be validated for every request. A common approach is to store the authentication with a session on the server. This approach, however, does not scale well horizontally. Consider what might happen if the application were to be split among multiple servers? Managing a session across multiple servers is difficult. The JWT does allow for authentication to be handled across multiple servers.

The JWT is a cryptographically signed and encrypted JSON object that is saved on the client and sent back to the server. The neat thing about JWT is the flexibility of the information that can used for authentication.

A developer will need to decide where to store the JWT once the user has been authenticated. This [stormpath](https://stormpath.com/blog/where-to-store-your-jwts-cookies-vs-html5-web-storage) article outlines both approaches: Cookies & Web Storage.

#### Cookies ####

Cookies could be used if the API is being served on the same domain as the application. The advantage of using cookies is that they can be set automatically from the server. The authentication cookie is then automatically sent with every subsequent request to that domain.

Cookies, with the proper HttpOnly flag, can also be protected from being accessed by JavaScript. This helps to protect against cross-site scripting attacks (XSS). There is another concern to protect against cross-site request forgery attacks. The site can generate a **csrf token** to prevent these attacks.

#### Web Storage ####

Web storage is another storage method available for storing JWT. The user can pick between localStorage or sessionStorage. The localStorage will persist until explicitly deleted and the sessionStorage will be deleted only while the window that created it is open.

The Web Storage must be used if accessing an API on a domain that is separate from the initiating resource. The drawback to using WebStorage is that no security standards are enforced (in contrast to cookies). The user must take proper measures to send JWT over HTTPS. To make things worse, since the WebStorage is accessible by JavaScript, there is valid concern for cross-scripting attacks.

A very real concern with XSS attacks is that third-party JavaScript can access the WebStorage and compromise the security. This makes identifying and protecting against XSS attacks difficult.

#### What do we do? ####

Cookies should be used websites that need higher security. The cookies will need to address the CSRF attacks but this is easier to manage by the application.

I will continue to use WebStorage for this project because it presents an interesting problem that I do not often encounter. This should, however, be thought through carefully when developing a *real* application.

In a **real world** situation, I would plan on using cookies if I were planning on shipping this as a live application. Using cookies would require me to host the API on the same domain as the SPA. I would also need to issue a CSRF token in the JWT.

This will be something to address at the of this project if time allows.

#### JWT Sources: ####

sources:

https://jwt.io/introduction/

https://float-middle.com/json-web-tokens-jwt-vs-sessions/

https://stormpath.com/blog/where-to-store-your-jwts-cookies-vs-html5-web-storage

https://github.com/NancyFx/Nancy/blob/master/samples/Nancy.Demo.Authentication.Stateless/StatelessAuthBootstrapper.cs

***

## SOLID Code: Single Responsibility Principle ##

The first part of the SOLID principles is to write code that does one thing and does it well. The following code snippet shows that the ```ValidateLogin``` was doing **two** things:

* Getting the ```LoginRecord``` from the database
* Mapping the ```LoginRecord``` to a ```LoginToken``` with the appropriate token values (e.g., Iss, exp)

We can refactor this code so that the function doesn't need to know how to map the ```LoginRecord``` to the ```LoginToken```.

```csharp
// Code snip it from the SecurityService

public LoginToken ValidateLogin(string username, string password)
{
    var loginRecord = _loginReader.ValidateLogin(username, password);
    if (loginRecord == null)
    {
        return null;
    }
    // todo: this should be moved out of the service. Violates SRP
    return new LoginToken
    {
        Exp = DateTime.Now.AddHours(_exp).Ticks.ToString(),
        Iss = "issuer",
        LoginName = loginRecord.Login,
        UserId = loginRecord.UserId
    };
}
```

**After Refactor:**

```csharp

public LoginToken ValidateLogin(string username, string password)
{
    var loginRecord = _loginReader.ValidateLogin(username, password);
    return loginRecord == null ? null : _tokenGenerator.CreateLoginToken(loginRecord);
}

```

***

## Decorator Pattern ##

By using the SOLID principles with our Repository pattern we are able to compose different functionality around the existing implementation. The trick is to pass the implementation that we wish to wrap into the constructor of the decorator. Then we can use the decorator as a substitute for the original implementation. We are able to do this because of the decorated is defined as a ```IRepositoryReader``` and ```IRepositoryWriter```

```csharp

// Decorated Repository that adds logging to the application
public class RepositoryLogging<TEntity> : IRepositoryReader<TEntity>, IRepositoryWriter<TEntity> where TEntity : class
{

    private readonly string _loggingFile = @"./logging.txt";
    private readonly IRepositoryReader<TEntity> _readerDelegate;
    private readonly IRepositoryWriter<TEntity> _writerDelegate;

    public RepositoryLogging(IRepositoryReader<TEntity> readerDelegate, IRepositoryWriter<TEntity> writeDelegate)
    {
        _readerDelegate = readerDelegate;
        _writerDelegate = writeDelegate;
    }
    public TEntity Get(int id)
    {
        WriteLog($"Request to get entity of type {typeof(TEntity).Name} with id: {id}");
        return _readerDelegate.Get(id);
    }
    /* code removed for brevity */
}

```

***

## ChatApplication.Test ##

Testing is an important part of any project, and should be considered an important part of the development of any application. Writing SOLID style code facilitates writing tests because each class/function should be small in responsibility and easily mocked. Mocking allows us to focus on *only* testing the section of code that we interested and ignore any of it's dependencies.

There are several reason why we would want to follow unit tests for our project:

* Helps to support management of the application over time by ensuring that changes do not break functionality. In other words, changes to the application can be made with greater confidence.
* Helps to document the application
* Helps to identify code smells

## Adaptive Example ##

**User Story**: we must prevent hackers from compromising a user account through a brute force attack.

**Solution**: We will disable the ability to log into the account if there are too many unsuccessful attempts.

This is take several steps.

**Step 1**: update the login table with appropriate fields to track.

```csharp

public class LoginRecord
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int LoginAttempts { get; set; }  // <-- added

    public virtual UserRecord User { get; set; }
}

```


## Resources ##

**Repository Pattern**

source: https://www.youtube.com/watch?v=rtXpYpZdOzM

**EntityFramework Code-First Approach**

https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application

** Testing Examples w/ Nancy **

https://github.com/bytefish/NancyFileUpload/
