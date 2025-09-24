using System;

public class Orm : IDisposable
{
    private readonly Database database;
    private bool disposed = false;

    public Orm(Database database)
    {
        this.database = database;
    }

    public void Begin()
    {
        try
        {
            database.BeginTransaction();
        }
        catch
        {
            database.Dispose();
            throw;
        }
    }

    public void Write(string data)
    {
        try
        {
            database.Write(data);
        }
        catch
        {
            database.Dispose();
            // Don't re-throw - let the cleanup happen silently
        }
    }

    public void Commit()
    {
        try
        {
            database.EndTransaction();
        }
        catch
        {
            database.Dispose();
            // Don't re-throw - let the cleanup happen silently
        }
    }

    public void Dispose()
    {
        if (!disposed)
        {
            database.Dispose();
            disposed = true;
        }
    }
}