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

## Resources ##

**Repository Pattern**

source: https://www.youtube.com/watch?v=rtXpYpZdOzM

**EntityFramework Code-First Approach**

https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application
