using System;

public struct Edge
{
    public Node a, b;
    public Edge(Node x, Node y)
    {
        // 무향: 항상 a의 인스턴스ID가 작도록 정렬
        if (x.GetInstanceID() < y.GetInstanceID()) { a = x; b = y; }
        else { a = y; b = x; }
    }

    public bool Equals(Edge other) => a == other.a && b == other.b;



}