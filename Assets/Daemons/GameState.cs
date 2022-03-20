using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int Height = 20;
    public int Width = 10;

    public GameObject blockPrefab;

    private HashSet<PointF> setBlocks = new HashSet<PointF>();

    // Start is called before the first frame update
    void Start()
    {
        // create grid walls
        var grid = new GameObject("GridWalls");
        foreach(var y in Enumerable.Range(0, Height)) {
            Instantiate(blockPrefab, new Vector3(-1, -y, 0), Quaternion.identity, grid.transform);
            Instantiate(blockPrefab, new Vector3(Width, -y, 0), Quaternion.identity, grid.transform);
        }
        // bottom wall with margin to make corner pieces
        foreach (var x in Enumerable.Range(-1, Width + 2)) {
            Instantiate(blockPrefab, new Vector3(x, -Height, 0), Quaternion.identity, grid.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 FetchSpawnPoint() {
        return new Vector3(Width / 2, 0, 0);
    }

    public bool CanMoveLeft(GameObject block)
    {
        foreach (Transform child in block.transform)
        {
            var point = new PointF(child.position.x - 1, child.position.y);
            if (child.position.x <= 0f || setBlocks.Contains(point))
            {
                return false;
            }
        }
        return true;
    }

    public bool CanMoveRight(GameObject block)
    {
        foreach (Transform child in block.transform)
        {
            var point = new PointF(child.position.x + 1, child.position.y);
            if (child.position.x + 1 >= Width || setBlocks.Contains(point))
            {
                return false;
            }
        }
        return true;
    }

    public bool CanMoveDown(GameObject block)
    {
        foreach (Transform child in block.transform)
        {
            var point = new PointF(child.position.x, child.position.y - 1);
            if (child.position.y -1 <= (-Height) || setBlocks.Contains(point))
            {
                return false;
            }
        }
        return true;
    }

    public void SetBlock(GameObject block)
    {
        Debug.Log(block.transform.position);
        foreach (Transform child in block.transform)
        {
            var point = new PointF(child.position.x, child.position.y);
            Debug.Log(point);
            setBlocks.Add(point);
        }
    }
}
