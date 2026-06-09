using System.Collections.Generic;
using UnityEngine;

public static class AStarPathfinder
{
    public static List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = new Node(startPos);
        Node targetNode = new Node(targetPos);

        List<Node> open = new List<Node>();
        HashSet<Node> closed = new HashSet<Node>();

        open.Add(startNode);

        while (open.Count > 0)
        {
            Node current = GetLowestF(open);

            if (current.Equals(targetNode))
                return RetracePath(current);

            open.Remove(current);
            closed.Add(current);

            foreach (Node neighbor in GetNeighbors(current))
            {
                if (closed.Contains(neighbor)) continue;

                float newCost = current.gCost + Vector3.Distance(current.pos, neighbor.pos);

                if (newCost < neighbor.gCost || !open.Contains(neighbor))
                {
                    neighbor.gCost = newCost;
                    neighbor.hCost = Vector3.Distance(neighbor.pos, targetNode.pos);
                    neighbor.parent = current;

                    if (!open.Contains(neighbor))
                        open.Add(neighbor);
                }
            }
        }

        return new List<Vector3>();
    }

    static Node GetLowestF(List<Node> list)
    {
        Node best = list[0];
        foreach (var n in list)
        {
            if (n.fCost < best.fCost)
                best = n;
        }
        return best;
    }

    static List<Node> GetNeighbors(Node node)
    {
        float step = 1f;
        List<Node> neighbors = new List<Node>();

        neighbors.Add(new Node(node.pos + Vector3.forward * step));
        neighbors.Add(new Node(node.pos + Vector3.back * step));
        neighbors.Add(new Node(node.pos + Vector3.left * step));
        neighbors.Add(new Node(node.pos + Vector3.right * step));

        return neighbors;
    }

    static List<Vector3> RetracePath(Node node)
    {
        List<Vector3> path = new List<Vector3>();
        Node current = node;

        while (current != null)
        {
            path.Add(current.pos);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }

    class Node
    {
        public Vector3 pos;
        public float gCost;
        public float hCost;
        public Node parent;

        public float fCost => gCost + hCost;

        public Node(Vector3 p)
        {
            pos = p;
            gCost = float.MaxValue;
        }

        public override bool Equals(object obj)
        {
            return obj is Node n && Vector3.Distance(n.pos, pos) < 0.1f;
        }

        public override int GetHashCode() => pos.GetHashCode();
    }
}