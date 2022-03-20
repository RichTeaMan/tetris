using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameState : MonoBehaviour
{
    public int Height = 20;
    public int Width = 10;

    public GameObject blockPrefab;

    public GameObject destroyPrefab;

    private HashSet<Point> setBlocks = new HashSet<Point>();

    private LinkedList<GameObject> blocks = new LinkedList<GameObject>();

    private GameObject daemon;

    // Start is called before the first frame update
    void Start()
    {
        daemon = GameObject.Find("Daemon");
        // create grid walls
        var grid = new GameObject("GridWalls");
        foreach (var y in Enumerable.Range(0, Height))
        {
            Instantiate(blockPrefab, new Vector3(-1, -y, 0), Quaternion.identity, grid.transform);
            Instantiate(blockPrefab, new Vector3(Width, -y, 0), Quaternion.identity, grid.transform);
        }
        // bottom wall with margin to make corner pieces
        foreach (var x in Enumerable.Range(-1, Width + 2))
        {
            Instantiate(blockPrefab, new Vector3(x, -Height, 0), Quaternion.identity, grid.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 FetchSpawnPoint()
    {
        return new Vector3(Width / 2, 0, 0);
    }

    private bool PointInBounds(Point point)
    {
        var x = point.X;
        var y = -point.Y;
        return x >= 0 &&
        x < Width &&
        //y >= 0 &&
        y < Height;
    }

    public bool CanMoveLeft(GameObject block)
    {
        float originX = block.transform.position.x;
        float originY = block.transform.position.y;
        foreach (Transform child in block.transform)
        {
            var point = new Point(Convert.ToInt32(child.position.x) - 1, Convert.ToInt32(child.position.y));
            if (!PointInBounds(point) || setBlocks.Contains(point))
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
            var point = new Point(Convert.ToInt32(child.position.x) + 1, Convert.ToInt32(child.position.y));
            if (!PointInBounds(point) || setBlocks.Contains(point))
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
            var point = new Point(Convert.ToInt32(child.position.x), Convert.ToInt32(child.position.y - 1));
            if (!PointInBounds(point) || setBlocks.Contains(point))
            {
                return false;
            }
        }
        return true;
    }

    public bool CanRotateClockwise(GameObject block)
    {
        int originX = Convert.ToInt32(block.transform.position.x);
        int originY = Convert.ToInt32(block.transform.position.y);
        foreach (Transform child in block.transform)
        {
            int x = Convert.ToInt32(child.position.x) - originX;
            int y = Convert.ToInt32(child.position.y) - originY;


            var newX = -y + originX;
            var newY = x + originY;

            var point = new Point(newX, newY);
            if (!PointInBounds(point) || setBlocks.Contains(point))
            {
                return false;
            }
        }
        return true;
    }

    public bool CanRotateCounterClockwise(GameObject block)
    {
        int originX = Convert.ToInt32(block.transform.position.x);
        int originY = Convert.ToInt32(block.transform.position.y);
        foreach (Transform child in block.transform)
        {
            int x = Convert.ToInt32(child.position.x) - originX;
            int y = Convert.ToInt32(child.position.y) - originY;


            var newX = y + originX;
            var newY = -x + originY;

            var point = new Point(newX, newY);
            if (!PointInBounds(point) || setBlocks.Contains(point))
            {
                return false;
            }
        }
        return true;
    }

    public void Gravity()
    {
        var currentBlock = GameObject.Find(Constants.CURRENT_BLOCK_NAME);
        if (CanMoveDown(currentBlock))
        {
            ExecuteEvents.Execute<IBlockMovement>(daemon, null, (x, y) => x.MoveDown());
        }
        else
        {
            foreach (Transform child in currentBlock.transform)
            {
                SetSubBlock(child.gameObject);
                Instantiate(destroyPrefab, child.position, Quaternion.identity);
            }
            currentBlock.name = "";
            currentBlock.transform.DetachChildren();
            Destroy(currentBlock);

            // check for completed lines
            var completedIndexes = setBlocks.Select(p => p.Y).Distinct().Where(i => setBlocks.Count(p => p.Y == i) == Width).ToArray();
            Debug.Log(completedIndexes);
            foreach (var i in completedIndexes)
            {
                foreach (var block in blocks.Where(b => Convert.ToInt32(b.transform.position.y) == i).ToArray())
                {
                    blocks.Remove(block);
                    Destroy(block);
                    Instantiate(destroyPrefab, block.transform.position, Quaternion.identity);
                }
            }



            
            ExecuteEvents.Execute<ISpawnerTarget>(daemon, null, (x, y) => x.SpawnRandomBlock());
        }
    }

    public void SetSubBlock(GameObject block)
    {
        var point = new Point(Convert.ToInt32(block.transform.position.x), Convert.ToInt32(block.transform.position.y));
        Debug.Log(point);
        setBlocks.Add(point);

        blocks.AddLast(block);
    }
}
