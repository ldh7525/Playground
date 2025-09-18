using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EdgeVisual : MonoBehaviour
{
    LineRenderer lr;
    Color normalColor = new Color(1, 1, 1, 0.2f);
    Color highlightColor = Color.red;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lr.endColor = normalColor;
        lr.startWidth = lr.endWidth = 0.3f;
    }

    // 두 노드 사이 라인 세팅
    public void Initialize(Node a, Node b)
    {
        lr.positionCount = 2;
        lr.SetPosition(0, a.transform.position);
        lr.SetPosition(1, b.transform.position);
    }

    public void Highlight() => lr.startColor = lr.endColor = highlightColor;
    public void ResetColor() => lr.startColor = lr.endColor = normalColor;
}
