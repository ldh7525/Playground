using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tile : MonoBehaviour
{
    public int x, y;
    public int tileNum; //ÁÖº¯ 8Ä­ ÆøÅº °³¼ö
    public bool hasBomb;
    public bool isEmpty;

    public TMP_Text numText;
    public SpriteRenderer bg;
    public SpriteRenderer topTileSpriteRenderer;
    public GameObject bombSprite;



    private void Awake()
    {
        tileNum = 0;
        hasBomb = false;
        isEmpty = true;
    }

    public void RevealTile(bool triggerBFS = true)
    {
        topTileSpriteRenderer.enabled = false;

        if (hasBomb)
            MSGameStateManager.Instance.GameOver();//°ÔÀÓ¿À¹ö

        if (isEmpty && triggerBFS)
            TileManager.Instance.EmptyRevealBFS(this);

    }




}
