var graph = new Graph();

graph.AddVertex("A");
graph.AddVertex("B");
graph.AddVertex("C");
graph.AddVertex("D");
graph.AddVertex("E");
graph.AddVertex("F");
graph.AddVertex("G");

graph.AddEdge("A", "B", 22);
graph.AddEdge("A", "C", 33);
graph.AddEdge("A", "D", 61);
graph.AddEdge("B", "C", 47);
graph.AddEdge("B", "E", 93);
graph.AddEdge("C", "D", 11);
graph.AddEdge("C", "E", 79);
graph.AddEdge("C", "F", 63);
graph.AddEdge("D", "F", 41);
graph.AddEdge("E", "F", 17);
graph.AddEdge("E", "G", 58);
graph.AddEdge("F", "G", 84);

var dijkstra = new DijkstraClass(graph);
var path = dijkstra.FindShortestPath("A", "G");
Console.WriteLine(path);

internal class DijkstraClass
{
    private readonly Graph _graph;

    private List<GraphVertexInfo> _infos = new();

    public DijkstraClass(Graph graph)
    {
        _graph = graph;
    }

    private GraphVertexInfo GetVertexInfo(Vertex v) => _infos.FirstOrDefault(i => i.Vertex.Equals(v))!;

    private GraphVertexInfo? FindUnvisitedVertexWithMinSum()
    {
        var minValue = int.MaxValue;
        
        GraphVertexInfo? minVertexInfo = null;
        
        foreach (var i in _infos.Where(i => i.IsUnvisited && i.EdgesWeightSum < minValue))
        {
            minVertexInfo = i;
            minValue = i.EdgesWeightSum;
        }

        return minVertexInfo;
    }
    
    public string FindShortestPath(string startName, string finishName)
    {
        var startVertex = _graph.FindVertex(startName);
        var finishVertex = _graph.FindVertex(finishName);
        
        if(startVertex is null || finishVertex is null)
            return "not found vertex";
        
        _infos = new List<GraphVertexInfo>();
        
        _infos.AddRange(_graph.Vertices.Select(v => new GraphVertexInfo(v)));
        
        var first = GetVertexInfo(startVertex);
        
        first.EdgesWeightSum = 0;
        
        while (true)
        {
            var current = FindUnvisitedVertexWithMinSum();
            
            if (current == null)
                break;

            SetSumToNextVertex(current);
        }

        return GetPath(startVertex, finishVertex);
    }

    private void SetSumToNextVertex(GraphVertexInfo info)
    {
        info.IsUnvisited = false;
        foreach (var e in info.Vertex.Edges)
        {
            var nextInfo = GetVertexInfo(e.ConnectedVertex);

            var sum = info.EdgesWeightSum + e.EdgeWeight;
            
            if (sum >= nextInfo.EdgesWeightSum) continue;
            
            nextInfo.EdgesWeightSum = sum;
            nextInfo.PreviousVertex = info.Vertex;
        }
    }

    private string GetPath(Vertex startVertex, Vertex endVertex)
    {
        var path = endVertex.ToString();
        while (startVertex != endVertex)
        {
            endVertex = GetVertexInfo(endVertex).PreviousVertex;
            path = endVertex + path;
        }

        return path;
    }
}

internal class Graph
{
    public List<Vertex> Vertices { get; }
    
    public Graph() => Vertices = new List<Vertex>();

    public void AddVertex(string vertexName) => Vertices.Add(new Vertex(vertexName));
    
    public Vertex? FindVertex(string vertexName) => Vertices.FirstOrDefault(v => v.Name.Equals(vertexName));

    public void AddEdge(string firstName, string secondName, int weight)
    {
        var v1 = FindVertex(firstName);
        var v2 = FindVertex(secondName);
        
        if (v2 == null || v1 == null) return;
        
        v1.AddEdge(v2, weight);
        v2.AddEdge(v1, weight);
    }
}

internal class Edge
{
    public Vertex ConnectedVertex { get; }
    
    public int EdgeWeight { get; }
    
    public Edge(Vertex connectedVertex, int weight)
    {
        ConnectedVertex = connectedVertex;
        EdgeWeight = weight;
    }
}

internal class Vertex
{
    public string Name { get; }

    public List<Edge> Edges { get; }

    public Vertex(string vertexName)
    {
        Name = vertexName;
        Edges = new List<Edge>();
    }

    public void AddEdge(Vertex vertex, int edgeWeight)
    {
        Edges.Add(new Edge(vertex, edgeWeight));
    }

    public override string ToString() => Name;
}

internal class GraphVertexInfo
{
    public Vertex Vertex { get; set; }
    
    public bool IsUnvisited { get; set; }
    
    public int EdgesWeightSum { get; set; }
    
    public Vertex PreviousVertex { get; set; }

    public GraphVertexInfo(Vertex vertex)
    {
        Vertex = vertex;
        IsUnvisited = true;
        EdgesWeightSum = int.MaxValue;
        PreviousVertex = null!;
    }
}