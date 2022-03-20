using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controls : MonoBehaviour
{

    private float duration = 0.25f;

    private float blockMove = 1.0f;

    private bool movementLocked = false;

    private GameState gameState;

    private GameObject daemon;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        daemon = GameObject.Find("Daemon");
    }

    // Update is called once per frame
    void Update()
    {
        // drop
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteEvents.Execute<ISpawnerTarget>(daemon, null, (x, y) => x.SpawnRandomBlock());
            return;
        }

        var currentBlock = GameObject.Find("CurrentBlock");
        if (currentBlock == null)
        {
            ExecuteEvents.Execute<ISpawnerTarget>(daemon, null, (x, y) => x.SpawnRandomBlock());
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //currentBlock.transform.Translate(Vector3.forward * Time.deltaTime);  
        }

        if (Input.GetKey(KeyCode.DownArrow) && gameState.CanMoveDown(currentBlock))
        {
            ExecuteEvents.Execute<IBlockMovement>(daemon, null, (x, y) => x.MoveDown());
        }

        if (Input.GetKey(KeyCode.LeftArrow) && gameState.CanMoveLeft(currentBlock))
        {
            ExecuteEvents.Execute<IBlockMovement>(daemon, null, (x, y) => x.MoveLeft());
        }

        if (Input.GetKey(KeyCode.RightArrow) && gameState.CanMoveRight(currentBlock))
        {
            ExecuteEvents.Execute<IBlockMovement>(daemon, null, (x, y) => x.MoveRight());
        }

        // rotate clockwise
        if (Input.GetKey(KeyCode.X) && gameState.CanRotateClockwise(currentBlock))
        {
            ExecuteEvents.Execute<IBlockMovement>(daemon, null, (x, y) => x.RotateClockwise());
        }

        // rotate counter clockwise
        if (Input.GetKey(KeyCode.Z) && gameState.CanRotateCounterClockwise(currentBlock))
        {
            ExecuteEvents.Execute<IBlockMovement>(daemon, null, (x, y) => x.RotateCounterClockwise());
        }
    }
}
