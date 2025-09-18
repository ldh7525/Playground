using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    public int width;
    public int height;
    public int bombNumber;
    public GameObject tilePrefab;
    public Transform tileParent;

    public bool isFirstClick = true;

    GameObject[,] tiles;

    private void Update()
    {
        TempGodMode();
    }

    void TempGodMode()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    tiles[i, j].GetComponent<Tile>().RevealTile();
    }

    void Start()
    {
        tiles = new GameObject[width, height];
        InitializeBoard();
    }

    void InitializeBoard()
    {

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                Vector3 pos = new Vector3(transform.position.x + i, transform.position.y + j, 0);
                GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity, tileParent);
                tile.name = $"tile {i},{j}";
                tiles[i, j] = tile;
                Tile a = tile.GetComponent<Tile>();
                a.x = i;
                a.y = j;
            }
    }

    public void PlantBombAndNumbers(Vector2 clickPos) //첫번째 클릭시에만 작동
    {
        int bombLeft = bombNumber;

        // 1. 모든 좌표를 리스트에 저장
        List<Vector2Int> coords = new List<Vector2Int>();
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                coords.Add(new Vector2Int(i, j));

        for (int i = coords.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = coords[i];
            coords[i] = coords[j];
            coords[j] = temp;
        }

        //  처음에 누른곳은 비어있도록 구현
        Vector2Int firstClickedTilePos = new Vector2Int(-1, -1);

        RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);
        if (hit.collider != null)
        {
            print(231);
            Tile tile = hit.collider.GetComponentInParent<Tile>();
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    if (i == tile.x && j == tile.y)
                    {
                        print(1);
                        firstClickedTilePos = new Vector2Int(i, j);
                        print(firstClickedTilePos);
                        break;
                    }
        }

        Vector2Int[] directions = new Vector2Int[]
        {
    new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1),
    new Vector2Int(-1,  0), new Vector2Int(0,  0), new Vector2Int(1,  0),
    new Vector2Int(-1,  1), new Vector2Int(0,  1), new Vector2Int(1,  1),
        };

        //폭탄추가 대상에서 없애기
        int delcount = 0; 
        foreach (var dir in directions)
        {
            Vector2Int pos = firstClickedTilePos + dir;
            if (pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height)
            {
                coords.Remove(pos);
                delcount++;
            }
        }

        // 폭탄 추가하기.
        HashSet<Vector2Int> bombCoords = new HashSet<Vector2Int>();
        for (int k = 0; k < bombNumber - delcount; k++)
        {
            bombCoords.Add(coords[k]);
        }
        int[] dx = new int[3] { 0, 1, -1 };
        int[] dy = new int[3] { 0, 1, -1 };


        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                if (bombCoords.Contains(new Vector2Int(i, j)))
                {
                    tiles[i, j].GetComponent<Tile>().hasBomb = true;
                    int nx;
                    int ny;

                    for (int x = 0; x < 3; x++)
                        for (int y = 0; y < 3; y++)
                        {
                            nx = i + dx[x];
                            ny = j + dy[y];
                            if (nx < 0 || ny < 0 || nx > width - 1 || ny > height - 1) continue;

                            Tile tileInstance = tiles[nx, ny].GetComponent<Tile>();
                            tileInstance.tileNum++;
                            if (tileInstance.isEmpty)
                                tileInstance.isEmpty = false;
                        }

                    bombLeft--;
                }




        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                print(3);

                GameObject tile = tiles[i, j];
                Tile tileInstance = tile.GetComponent<Tile>();

                if (tileInstance.hasBomb)
                {
                    tileInstance.bombSprite.SetActive(true);
                    tileInstance.numText.text = "";
                }
                else if (tileInstance.tileNum == 0)
                {
                    tileInstance.numText.text = "";
                    print(4);

                }
                else
                {
                    tileInstance.numText.text = tileInstance.tileNum.ToString();
                }
            }

        isFirstClick = false;
    }


    public void EmptyRevealBFS(Tile startTile)
    {
        int p = -1;
        int q = -1;
        GameObject tile;

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                if (tiles[x, y].GetComponent<Tile>() == startTile)
                {
                    p = x; q = y;
                    tile = tiles[x, y];
                    continue;
                }

        int nx, ny;

        bool[,] visited = new bool[width, height];

        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(new Vector2Int(p, q));
        visited[p, q] = true;
        int[] dx = new int[3] { 0, 1, -1 };
        int[] dy = new int[3] { 0, 1, -1 };

        while (queue.Count > 0)
        {
            Vector2Int cur = queue.Dequeue();
            Tile currentTile = tiles[cur.x, cur.y].GetComponent<Tile>();
            currentTile.RevealTile(false);

            if (currentTile.tileNum != 0) continue;

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    nx = cur.x + dx[x];
                    ny = cur.y + dy[y];
                    if (nx < 0 || ny < 0 || nx > width - 1 || ny > height - 1) continue;
                    if (visited[nx, ny]) continue;

                    Tile nextTile = tiles[nx, ny].GetComponent<Tile>();
                    visited[nx, ny] = true;
                    queue.Enqueue(new Vector2Int(nx, ny));
                }
        }
    }
}