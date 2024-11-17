using System;
using System.Collections.Generic;

namespace TaskProcessorLib
{
    public class TaskProcessor
    {
        public static string ProcessTask(string[] lines)
        {
            string[] firstLine = lines[0].Split();
            int n = int.Parse(firstLine[0]);
            int m = int.Parse(firstLine[1]);

            List<int>[] graph = new List<int>[n + 1];
            for (int i = 0; i <= n; i++)
                graph[i] = new List<int>();

            HashSet<(int, int)> roads = new HashSet<(int, int)>();

            for (int i = 1; i <= m; i++)
            {
                string[] road = lines[i].Split();
                int u = int.Parse(road[0]);
                int v = int.Parse(road[1]);

                if (u > v) (u, v) = (v, u);
                if (roads.Add((u, v)))
                {
                    graph[u].Add(v);
                    graph[v].Add(u);
                }
            }

            bool[] visited = new bool[n + 1];
            bool hasCycle = false;

            void Dfs(int node, int parent)
            {
                visited[node] = true;
                foreach (int neighbor in graph[node])
                {
                    if (!visited[neighbor])
                    {
                        Dfs(neighbor, node);
                    }
                    else if (neighbor != parent)
                    {
                        hasCycle = true;
                    }
                }
            }

            for (int i = 1; i <= n; i++)
            {
                if (!visited[i])
                {
                    Dfs(i, -1);
                    if (hasCycle) break;
                }
            }

            return hasCycle ? "YES" : "NO";
        }
    }
}
