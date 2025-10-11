using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Vector2 mousePos;
    //    MyInputActions input;

    void Awake()
    {
        //       input = new MyInputActions();
        //       input.Player.Click.performed += OnClickActionTriggered;
        //      input.Enable();
    }

    private void Update()
    {

    }

    // InputAction에서 클릭 처리
    void OnClickActionTriggered(InputAction.CallbackContext ctx)
    {
        //    mousePos = input.Player.Pointer.ReadValue<Vector2>();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);


        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null)
        {
            Tile tile = hit.collider.GetComponentInParent<Tile>();
            if (tile != null)
            {
                if (TileManager.Instance.isFirstClick)
                {
                    TileManager.Instance.PlantBombAndNumbers(worldPos);
                    print("plant");
                }
                tile.RevealTile();
            }
        }
    }
}
