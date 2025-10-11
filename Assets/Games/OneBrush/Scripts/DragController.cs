using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DragController : MonoBehaviour
{
    public LayerMask nodeLayer;


    List<Node> visitedNodeList = new List<Node>();
    List<EdgeVisual> visitedEdgeList = new List<EdgeVisual>();
    Node node;
    Node currentNode;
    bool isDragging = false;
    PlayerInput playerInput;



    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Vector2 asdf = playerInput.actions["Pointer"].ReadValue<Vector2>();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(asdf);
        node = DetectNodeUnderMouse(mouseWorldPos);

        if (!isDragging) return;
        if (node != null && node != currentNode)
            DraggingOnAnotherNode();
    }

    private void DraggingOnAnotherNode()
    {
        Edge a = new Edge(currentNode, node);
        EdgeManager.Instance.edgeMap.TryGetValue(a, out EdgeVisual edgeVisual);

        if (edgeVisual == null) return; //간선이 없으면 그냥 리턴
        if(visitedEdgeList.Contains(edgeVisual)) return; //이미 방문한 간선이면 리턴
        edgeVisual.Highlight();
        visitedEdgeList.Add(edgeVisual);
        currentNode = node;
        currentNode.OnReach();
        visitedNodeList.Add(currentNode);
    }

    public void Onclick(InputAction.CallbackContext context)
    {
        if (context.started)
            OnClickStarted();

        if (context.canceled)
            OnClickCanceled();
    }

    void OnClickStarted()
    {
        if (node == null) return;
        isDragging = true;
        currentNode = node;
        currentNode.OnSelect();
        visitedNodeList.Add(currentNode);
    }

    void OnClickCanceled()
    {
        isDragging = false;
        for (int i = visitedNodeList.Count - 1; i >= 0; i--)
        {
            visitedNodeList[i].OnDeselect();
            visitedNodeList.RemoveAt(i);
        }
        for (int i = visitedEdgeList.Count - 1; i >= 0; i--)
        {
            visitedEdgeList[i].ResetColor();
            visitedEdgeList.RemoveAt(i);
        }
    }

    private Node DetectNodeUnderMouse(Vector2 mousePos)
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, nodeLayer);
        if (hit.collider != null)
        {
            return hit.collider.GetComponent<Node>();
        }
        return null;
    }
}
