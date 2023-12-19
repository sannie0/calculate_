using calculate_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Reflection;
using FluentAssertions.Execution;
using Dapper;
using System.Xml.Linq;

namespace calculate_
{
   public interface IMemory //контракт
    {
        void PushElement(string input);
        string GetLastElement();
    }
}

class RAM : IMemory
{

    private Stack<string> memoryStack = new Stack<string>();

    public void PushElement(string input)
    {
        memoryStack.Push(input);
    }
    public string GetLastElement()
    {
        return memoryStack.Count > 0 ? memoryStack.Peek() : string.Empty;
    }
}

class FileM : IMemory
{
    private string filePath = "memory.txt";
    private Stack<string> memoryStack = new Stack<string>();
    public void PushElement(string input)
    {
        memoryStack.Clear();
        memoryStack.Push(input);
        File.WriteAllLines(filePath, memoryStack);
    }
    public string GetLastElement()
    {
        if (File.Exists(filePath))
        {
            memoryStack = new Stack<string>(File.ReadAllLines(filePath));
            if (memoryStack.Count > 0)
            {
                return memoryStack.Peek();
            }
        }
        return "0";
    }
}

class BDM : IMemory
{
    public void PushElement(string input)
    {
        var connection = new System.Data.SQLite.SQLiteConnection("Data Source=BD_memory.db;");
        connection.Open();

        connection.Execute("insert into memory (InputTxt) values (:input)", new { input = input });
    }
    public string GetLastElement()
    {
        var connection = new System.Data.SQLite.SQLiteConnection("Data Source=BD_memory.db;");
        connection.Open();
        var res = connection.ExecuteScalar<string>("select InputTxt from memory where id = (select max(id) from memory)");
        if (res != null)
        {
            return res;
        }
        return "0";

    }
}
class SaveData
{
    public long Id { get; set; }
    public string InputText { get; set; }
}

