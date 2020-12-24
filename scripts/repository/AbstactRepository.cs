using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;
using System.IO;

public class AbstactRepository
{
    private string connection = "URI=file:" + Application.persistentDataPath + "/db.bytes";
    IDbConnection dbcon;

    public IDataReader Execute(string query)
    {
        IDbCommand cmndRead = dbcon.CreateCommand();
        IDataReader reader;

        cmndRead.CommandText = query;
        reader = cmndRead.ExecuteReader();

        return reader;
    }

    public void closeConnection()
    {
        dbcon.Close();
    }

    public void openConnection()
    {
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
    }

}