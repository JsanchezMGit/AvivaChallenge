using System;

namespace Products.WebApi.Data.Models;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Descrition { get; set; }
    public int Stock { get; set; }
}
