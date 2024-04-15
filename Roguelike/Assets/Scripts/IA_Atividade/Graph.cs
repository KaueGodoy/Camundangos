using System.Collections.Generic;
using System;
using System.Linq;

public class City
{
    public string Name { get; private set; }
    public List<City> Neighbors { get; private set; }

    public City(string name)
    {
        Name = name;
        Neighbors = new List<City>();
    }

    public void AddNeighbor(City neighbor)
    {
        Neighbors.Add(neighbor);
    }
}

public class Graph
{

    public List<City> Cities { get; private set; }

    public Graph()
    {
        Cities = new List<City>();
    }

    public void AddCity(City city)
    {
        Cities.Add(city);
    }

    public List<City> BFS(City start, City target)
    {
        Queue<City> queue = new Queue<City>();
        queue.Enqueue(start);

        Dictionary<City, City> cameFrom = new Dictionary<City, City>();

        cameFrom[start] = null;

        while (queue.Count > 0)
        {
            City current = queue.Dequeue();
            if (current == target)
            {
                return ReconstructPath(cameFrom, start, target);
            }

            foreach (City neighbor in current.Neighbors)
            {
                if (!cameFrom.ContainsKey(neighbor))
                {
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return null; // No path found
    }

    public List<City> DFS(City start, City target)
    {
        Stack<City> stack = new Stack<City>();
        stack.Push(start);

        Dictionary<City, City> cameFrom = new Dictionary<City, City>();
        cameFrom[start] = null;

        while (stack.Count > 0)
        {
            City current = stack.Pop();
            if (current == target)
            {
                return ReconstructPath(cameFrom, start, target);
            }

            foreach (City neighbor in current.Neighbors)
            {
                if (!cameFrom.ContainsKey(neighbor))
                {
                    cameFrom[neighbor] = current;
                    stack.Push(neighbor);
                }
            }
        }

        return null; // No path found
    }

    private List<City> ReconstructPath(Dictionary<City, City> cameFrom, City start, City target)
    {
        List<City> path = new List<City>();
        City step = target;
        while (step != null)
        {
            path.Add(step);
            step = cameFrom[step];
        }
        path.Reverse();
        return path;
    }
}
