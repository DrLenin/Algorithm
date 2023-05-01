var vertexes = new[]
{
    new Vertex(0), new Vertex(1), new Vertex(2), 
    new Vertex(3), new Vertex(4), new Vertex(5)
};

// Setting the edges
Graph.AddVertices(vertexes[0], vertexes[1..2]);
Graph.AddVertices(vertexes[1], vertexes[4..5]);
Graph.AddVertices(vertexes[2], vertexes[3]);
Graph.AddVertices(vertexes[4], vertexes[5]);
Graph.AddVertices(vertexes[5], vertexes[4]);

Console.WriteLine("Search by width");
Console.WriteLine("Is there a vertex with the number 5 = " + Graph.SearchWidth(vertexes[0], 5));

internal class Vertex
{
    public int Number { get;}
    
    public Vertex[] DaughterVertex { get; set; } = null!;

    public Vertex(int number)
    {
        Number = number;
    }
}

internal static class Graph
{
    public static void AddVertices(Vertex vertexNew, params Vertex[] vertices) => vertexNew.DaughterVertex = vertices;

    public static bool SearchWidth(Vertex elementary, int number)
    {
        var queue = new Queue<Vertex>();
        
        queue.Enqueue(elementary);

        var searched = new List<Vertex>(); 

        while (queue.Count != 0)
        {
            var vertex = queue.Dequeue();

            if (searched.Contains(vertex)) continue;
            
            if (vertex.Number == number)
                return true;
                
            foreach (var v in vertex.DaughterVertex)
            {
                queue.Enqueue(v);
            }
            
            searched.Add(vertex);
        }

        return false;
    }
}