## Logging ##

Logging is an important piece to any application that goes into production. A developer does not have the luxury of using a debugger to step through code in a production environment. The alternative is to log application state & information to log files that will aid in identifying bugs and issues. The logs can also help us to identify way to improve the application performance and reliability.

This project logs using the **Log4Net** library. I was able to implement configurable logging through the use of the decorator pattern and my IoC container. The project also explores how to use custom log-levels. I am logging the following:

* EntityFramework Tracing
* Data Access Layer Profiling

## Log4Net Configuration ##

The application uses a drop-in `Log4Net.config` xml file that specifies the log parameters. It is where we define the different log files & where to store them, our custom log levels, and what namespaces we specifically want to watch.

The custom config is beneficial because it can be copied and dropped into future projects. The important is to remember to set the file to always copy into the bin folder on build. The Log4Net also needs to be configured in the `assembly.cs` file to be activated.

## EntityFramework Tracing ##

When my EF tracing is set to true a log files with the following is a sample output. Now we are able to identify a potential performance problem. We see that a database connection was opened **4** times in the span of a few milliseconds. This seems to indicate that we might be running into an issue with EF lazy loading and then the entity relationships being looped over causing repeated database hits.

```sql

Opened connection at 5/7/2017 10:56:19 PM -04:00
SELECT
    [Extent1].[RoomId] AS [RoomId],
    [Extent1].[UserId] AS [UserId],
    [Extent1].[Name] AS [Name],
    [Extent1].[Description] AS [Description],
    [Extent1].[DateCreated] AS [DateCreated]
    FROM [dbo].[RoomRecords] AS [Extent1]
-- Executing at 5/7/2017 10:56:19 PM -04:00
-- Completed in 0 ms with result: SqlDataReader

Closed connection at 5/7/2017 10:56:19 PM -04:00
Opened connection at 5/7/2017 10:56:19 PM -04:00
SELECT
    [Extent1].[MessageId] AS [MessageId],
    [Extent1].[Text] AS [Text],
    [Extent1].[PostedDate] AS [PostedDate],
    [Extent1].[RoomId] AS [RoomId],
    [Extent1].[UserId] AS [UserId]
    FROM [dbo].[MessageRecords] AS [Extent1]
    WHERE [Extent1].[RoomId] = @EntityKeyValue1
-- EntityKeyValue1: '1' (Type = Int64, IsNullable = false)
-- Executing at 5/7/2017 10:56:19 PM -04:00
-- Completed in 0 ms with result: SqlDataReader

Closed connection at 5/7/2017 10:56:19 PM -04:00
Opened connection at 5/7/2017 10:56:19 PM -04:00
SELECT
    [Extent1].[UserId] AS [UserId],
    [Extent1].[Name] AS [Name]
    FROM [dbo].[UserRecords] AS [Extent1]
    WHERE [Extent1].[UserId] = @EntityKeyValue1
-- EntityKeyValue1: '1' (Type = Int64, IsNullable = false)
-- Executing at 5/7/2017 10:56:19 PM -04:00
-- Completed in 0 ms with result: SqlDataReader

Closed connection at 5/7/2017 10:56:19 PM -04:00
Opened connection at 5/7/2017 10:56:19 PM -04:00
SELECT
    [Extent1].[MessageId] AS [MessageId],
    [Extent1].[Text] AS [Text],
    [Extent1].[PostedDate] AS [PostedDate],
    [Extent1].[RoomId] AS [RoomId],
    [Extent1].[UserId] AS [UserId]
    FROM [dbo].[MessageRecords] AS [Extent1]
    WHERE [Extent1].[RoomId] = @EntityKeyValue1
-- EntityKeyValue1: '2' (Type = Int64, IsNullable = false)
-- Executing at 5/7/2017 10:56:19 PM -04:00
-- Completed in 0 ms with result: SqlDataReader

Closed connection at 5/7/2017 10:56:19 PM -04:00

```

## Data Access Profiling ##

We can also configure the application to profile the performance of accessing the database. I find this particularly useful because it can help to identify trends on when bottlenecks might occur. I previously built a project that would seem to fail at around 2:30PM. There was no logging in place for me to identify that the issue was a bottleneck within our data access. Now with the ChatApplication we will be able to figure out exactly where performance degrades and on which access request.

```
2017-05-07 22:56:19,058 [7] REPO ChatApplication.Data.EntityFramework.Repositories.RepositoryProfiler`1 - function=[GetAll];  type=[RoomRecord]; ms=[520];
```
