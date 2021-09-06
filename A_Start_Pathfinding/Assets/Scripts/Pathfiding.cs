using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfiding : MonoBehaviour {
  PathRequestManager requestManager;
  Grid grid;

  void Awake() {
    requestManager = GetComponent<PathRequestManager>();
    grid = GetComponent<Grid>();
  }

  public void FindPath(PathRequest request, Action<PathResult> callback) {
    Vector3[] waypoints = new Vector3[0];
    bool pathSuccess = false;

    Node startNode = grid.NodeFromWorldPosition(request.pathStart);
    Node targetNode = grid.NodeFromWorldPosition(request.pathEnd);

    if (startNode.walkable && targetNode.walkable) {
      Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
      HashSet<Node> closeSet = new HashSet<Node>();
      openSet.Add(startNode);

      while (openSet.Count > 0) {
        Node node = openSet.RemoveFirst();
        closeSet.Add(node);

        if (node == targetNode) {
          pathSuccess = true;
          break;
        }

        foreach (Node neighbour in grid.GetNeighbours(node)) {
          if (!neighbour.walkable || closeSet.Contains(neighbour)) {
            continue;
          }
          int newCostToNeighbour = node.gCost + GetDistance(node, neighbour) + neighbour.movementPenalty;
          if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
            neighbour.gCost = newCostToNeighbour;
            neighbour.hCost = GetDistance(neighbour, targetNode);
            neighbour.parent = node;
            if (!openSet.Contains(neighbour)) {
              openSet.Add(neighbour);
            } else {
              openSet.UpdateItem(neighbour);
            }
          }
        }
      }
    }
    if (pathSuccess) {
      waypoints = RetracePath(startNode, targetNode);
      pathSuccess = waypoints.Length > 0;
    }
    callback(new PathResult(waypoints, pathSuccess, request.callback));
  }

  Vector3[] RetracePath(Node startNode, Node endNode) {
    List<Node> path = new List<Node>();
    Node currentNode = endNode;
    while (currentNode != startNode) {
      path.Add(currentNode);
      currentNode = currentNode.parent;
    }
    Vector3[] waypoints = SimplifyPath(path);
    Array.Reverse(waypoints);
    return waypoints;
  }

  Vector3[] SimplifyPath(List<Node> path) {
    List<Vector3> wayporints = new List<Vector3>();
    Vector2 directionOld = Vector2.zero;

    for (int i = 1; i < path.Count; i++) {
      Vector2 directionNew = new Vector2(path[i - 1].x - path[i].x, path[i - 1].y - path[i].y);
      if (directionNew != directionOld) {
        wayporints.Add(path[i].worldPosition);
      }
      directionOld = directionNew;
    }
    return wayporints.ToArray();
  }

  int GetDistance(Node nodeA, Node nodeB) {
    int dstX = Mathf.Abs(nodeA.x - nodeB.x);
    int dstY = Mathf.Abs(nodeA.y - nodeB.y);

    return dstX > dstY ? 14 * dstY + 10 * (dstX - dstY) : 14 * dstX + 10 * (dstY - dstX);
  }
}
