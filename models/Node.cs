using System.Numerics;

namespace backend.models;

public class Node
{
    public string Name  { get; set; }
    public Coordinates Coordinates{ get; set; }
    public Node(string name, Coordinates coords)
    {
        Name = name;
        Coordinates = coords; 
    }
}