using System.Globalization;
using System.Xml;
using graph_oo;

namespace backend.bab;

public class Autobahnen
{
    private static Graph? _babsGraph; 
    public static Graph Graph
    {
        get 
        {
            if (_babsGraph is null)
            {
                _babsGraph = Read("graph_bab.xml"); 
            }

            return _babsGraph; 
        }
    }

    public static graph_oo.Graph Read(string filename)
    {
        var graph = new graph_oo.Graph(); 

        XmlDocument doc = new XmlDocument();
        doc.Load(filename);
        Console.WriteLine(doc.Value);

        Dictionary<string, Node> nodes = new Dictionary<string, Node>(); 

        var xmlGraph = doc.ChildNodes[1];
        _ = xmlGraph ?? throw new NullReferenceException(); 

        foreach (var child in xmlGraph.ChildNodes)
        {
            XmlElement? childElement = child as XmlElement;
            if (childElement == null)
                continue;

            if (childElement.Name == "node")
            {
                string nodekey = childElement.GetAttribute("id"); 
                if (nodes.ContainsKey(nodekey))
                    continue; 

                Node node = new Node(nodekey); 
                nodes.Add(nodekey, node); 
                graph.AddNode(node); 

                foreach(var dataField in childElement.ChildNodes)
                {
                    var dataFieldElement = dataField as XmlElement; 
                    _ = dataFieldElement ?? throw new NullReferenceException(); 

                    if (dataFieldElement.GetAttribute("key") == "lat")
                    {
                        float lat = float.Parse(dataFieldElement.InnerText, CultureInfo.InvariantCulture); 
                        node.Latitude = lat; 
                        continue; 
                    }

                    if (dataFieldElement.GetAttribute("key") == "lon")
                    {
                        float lon = float.Parse(dataFieldElement.InnerText, CultureInfo.InvariantCulture); 
                        node.Longitude = lon; 
                        continue; 
                    }
                }
                continue; 
            }

            if (childElement.Name == "edge")
            {
                var nodeFrom = nodes[childElement.GetAttribute("source")]; 
                var nodeTo = nodes[childElement.GetAttribute("target")]; 

                foreach(var dataField in childElement.ChildNodes)
                {
                    var dataFieldElement = dataField as XmlElement; 
                    _ = dataFieldElement ?? throw new NullReferenceException(); 

                    if (dataFieldElement.GetAttribute("key") == "gewicht")
                    {
                        float weight = float.Parse(dataFieldElement.InnerText); 
                        var edge = new Edge(nodeFrom, nodeTo, weight); 
                        graph.AddEdge(edge); 
                        nodeFrom.AddOutgoingEdge(edge); 
                    }
                }

                continue; 
            }
        }

        return graph; 
    }
}