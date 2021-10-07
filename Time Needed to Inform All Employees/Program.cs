using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_Needed_to_Inform_All_Employees
{
  class Program
  {
    class Node
    {
      public int employee;
      public int cummulativeTime;
      public Node(int emp, int time)
      {
        employee = emp;
        cummulativeTime = time;
      }
    }

    static void Main(string[] args)
    {
      //int n = 15;
      //int headId = 0;
      //var manager = new int[] { -1, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };
      //var informTime = new int[] { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
      int n = 7;
      int headId = 6;
      var manager = new int[] { 1, 2, 3, 4, 5, 6, -1 };
      var informTime = new int[] { 0, 6, 5, 4, 3, 2, 1 }; // array values indicate cost to reach to the next level. eg - 1 cost required to reach next level i.e 5
      Console.WriteLine(NumOfMinutes(n, headId, manager, informTime));
    }

    public static int NumOfMinutes(int n, int headID, int[] manager, int[] informTime)
    {
      int maxTime = int.MinValue;
      Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>(); // to keeep the parent and its child, Key = parent and List() are the child.
      for (int i = 0; i < n; i++)
      {
        if (!graph.ContainsKey(manager[i]))
          graph.Add(manager[i], new List<int>());
        graph[manager[i]].Add(i);
      }
      if (!graph.ContainsKey(headID))
        return maxTime;

      // perform BFS
      Queue<Node> queue = new Queue<Node>();
      queue.Enqueue(new Node(-1, 0));
      while(queue.Count > 0)
      {
        Node current = queue.Dequeue();
        maxTime = Math.Max(maxTime, current.cummulativeTime);
        if(graph.ContainsKey(current.employee))
        {
          var children = graph[current.employee];
          for(int i = 0; i < children.Count; i++)
          {
            var child = children[i];
            queue.Enqueue(new Node(child, informTime[child] + current.cummulativeTime));
          }
        }
      }
      return maxTime;
    }
  }
}
