using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragManager : MonoBehaviour
{
    // MyInputActions myInputActions;
    LineRenderer lr;
    bool isDragging = false;
    Vector2 dragStartPos, dragEndPos, currentMousePos;
    Collider2D[] prevColliders;

    public GameObject detectSquare;


    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = lr.endWidth = 0.036f;
        lr.startColor = lr.endColor = Color.white;
        lr.loop = true;

        //  myInputActions = new MyInputActions();
        //   myInputActions.Player.Click.started += OnClickPerformed;
        //   myInputActions.Player.Click.canceled += OnClickCanceled;
        //    myInputActions.Player.Enable();

    }

    void Start()
    {
        isDragging = false;
        detectSquare.SetActive(false);
        lr.enabled = false;
    }


    void Update()
    {
        //    currentMousePos = myInputActions.Player.Pointer.ReadValue<Vector2>();

        if (!isDragging) return;
        Vector2 maxvec = Camera.main.ScreenToWorldPoint(Vector2.Max(dragStartPos, currentMousePos));
        Vector2 minvec = Camera.main.ScreenToWorldPoint(Vector2.Min(dragStartPos, currentMousePos));
        DrawEdge(maxvec, minvec);
        ExpandSquareAndChoose(maxvec, minvec);
    }

    void DrawEdge(Vector2 max, Vector2 min)
    {
        lr.enabled = true;
        lr.positionCount = 4;
        lr.SetPosition(0, min);
        lr.SetPosition(1, new Vector2(min.x, max.y));
        lr.SetPosition(2, max);
        lr.SetPosition(3, new Vector2(max.x, min.y));
    }

    void ExpandSquareAndChoose(Vector2 max, Vector2 min)
    {
        Vector2 size = max - min;
        Vector2 center = (min + max) / 2f;
        detectSquare.SetActive(true);
        detectSquare.transform.position = center;
        detectSquare.transform.localScale = new Vector3(size.x, size.y, 1f);

        Collider2D[] currentColliders = Physics2D.OverlapAreaAll(min, max);
        if (prevColliders != null)
        {
            HashSet<Collider2D> exitedColliders = new HashSet<Collider2D>(prevColliders);
            exitedColliders.ExceptWith(currentColliders);
            foreach (var collider in exitedColliders ?? Enumerable.Empty<Collider2D>())
                collider.GetComponent<Apple>().fuck();
        }


        foreach (var a in currentColliders ?? Enumerable.Empty<Collider2D>())
            a.GetComponent<Apple>().ChosenOne();
        prevColliders = currentColliders;
    }

    void OnClickPerformed(InputAction.CallbackContext context)
    {
        StartCoroutine(DelayedSetDragging());
    }

    IEnumerator DelayedSetDragging()
    {
        yield return null;
        //   dragStartPos = myInputActions.Player.Pointer.ReadValue<Vector2>();
        prevColliders = new Collider2D[0];  // ← 이전 콜라이더 초기화
        isDragging = true;
    }

    public void OnClickCanceled(InputAction.CallbackContext context)
    {
        dragEndPos = currentMousePos;
        Vector2 maxvec = Camera.main.ScreenToWorldPoint(Vector2.Max(dragStartPos, dragEndPos));
        Vector2 minvec = Camera.main.ScreenToWorldPoint(Vector2.Min(dragStartPos, dragEndPos));

        Collider2D[] currentColliders = Physics2D.OverlapAreaAll(minvec, maxvec);
        if (prevColliders != null)
        {
            HashSet<Collider2D> exitedColliders = new HashSet<Collider2D>(prevColliders);
            exitedColliders.ExceptWith(currentColliders);
            foreach (var collider in exitedColliders ?? Enumerable.Empty<Collider2D>())
                collider.GetComponent<Apple>().fuck();
        }
        int sum = 0;
        foreach (var collider in currentColliders)
            sum += collider.GetComponent<Apple>().value;

        if (sum == 10)
        {
            foreach (var a in currentColliders ?? Enumerable.Empty<Collider2D>())
                a.GetComponent<Apple>().ExecuteDie();
        }
        else
        {
            foreach (var a in currentColliders ?? Enumerable.Empty<Collider2D>())
                a.GetComponent<Apple>().fuck();
        }

        isDragging = false;
        detectSquare.SetActive(false);
        lr.enabled = false;
    }
}
