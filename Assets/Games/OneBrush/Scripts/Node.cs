using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> connectedNodes;

    Collider2D col;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool IsMouseOver(Vector2 point)
    {
        return col.OverlapPoint(point);
    }

    public void OnSelect()
    {
        Debug.Log($"{gameObject.name} 선택됨");
        // ex: 색깔 변경

        spriteRenderer.color = Color.green;
    }

    public void OnReach()
    {
        Debug.Log($"{gameObject.name}로 이동 연결됨");
        // ex: 선 그리기 시작
        spriteRenderer.color = Color.green;
    }

    public void OnDeselect()
    {
        Debug.Log($"{gameObject.name} 선택 해제됨");
        // ex: 색 원래대로
        spriteRenderer.color = Color.white;
    }
}
