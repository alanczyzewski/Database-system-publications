using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Author
{
    public string Autor { get; set; }
    public int Id { get; set; }
    public Author(int id, string name)
    {
        Id = id;
        Autor = name;
    }
}