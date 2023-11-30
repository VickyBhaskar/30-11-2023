// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Reflection;

class Source
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

class Destination
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string AdditionalProperty { get; set; }
}

class Program
{
    static void Main()
    {
        Source source = new Source { Id = 1, Name = "John", Age = 25 };
        Destination destination = new Destination();

        MapProperties(source, destination);

        Console.WriteLine("Mapping Result:");
        Console.WriteLine($"Id: {destination.Id}");
        Console.WriteLine($"Name: {destination.Name}");
        Console.WriteLine($"Age: {destination.Age}");
        Console.WriteLine($"AdditionalProperty: {destination.AdditionalProperty}");
    }

    static void MapProperties(object source, object destination)
    {
        Type sourceType = source.GetType();
        Type destinationType = destination.GetType();

        PropertyInfo[] commonProperties = sourceType.GetProperties()
            .Where(s => destinationType.GetProperty(s.Name) != null)
            .ToArray();

        foreach (PropertyInfo property in commonProperties)
        {
            object value = property.GetValue(source);
            destinationType.GetProperty(property.Name)?.SetValue(destination, value);
        }
    }
}

