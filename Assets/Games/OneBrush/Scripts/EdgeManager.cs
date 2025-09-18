using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EdgeManager : Singleton<EdgeManager>
{
    public Dictionary<Edge, EdgeVisual> edgeMap = new Dictionary<Edge, EdgeVisual>(); //모든 간선 목록이 있는 맵

    protected override void Awake()
    {
        base.Awake();
        CreateAllEdges();
    }

    void CreateAllEdges()
    {
        //edge 전부 찾아 간선목록 구성
        foreach (var node in FindObjectsByType<Node>(FindObjectsSortMode.None))
            foreach (var connectedNode in node.connectedNodes)
            {
                Edge edge = new Edge(node, connectedNode);
                if (edgeMap.Keys.Contains(edge)) continue;

                GameObject lineObj = new GameObject($"Edge_{node.name}_{connectedNode.name}"); // 새 GameObject 생성
                lineObj.transform.SetParent(gameObject.transform);
                EdgeVisual edgeVisual = lineObj.AddComponent<EdgeVisual>();
                edgeVisual.Initialize(node, connectedNode);

                edgeMap[edge] = edgeVisual;
            }
    }
}