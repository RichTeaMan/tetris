using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controls : MonoBehaviour
{

    private float duration = 0.25f;

    private float blockMove = 1.0f;

    private bool movementLocked = false;

    private bool dropRequested = false;

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
            dropRequested = true;
        }

        if (movementLocked)
        {
            return;
        }
        if (dropRequested)
        {
            ExecuteEvents.Execute<ISpawnerTarget>(daemon, null, (x, y) => x.SpawnRandomBlock());
            dropRequested = false;
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
            var targetPosition = (Vector3.down * blockMove) + currentBlock.transform.position;
            StartCoroutine(MoveBlock(currentBlock, targetPosition));
        }

        if (Input.GetKey(KeyCode.LeftArrow) && gameState.CanMoveLeft(currentBlock))
        {
            var targetPosition = (Vector3.left * blockMove) + currentBlock.transform.position;
            StartCoroutine(MoveBlock(currentBlock, targetPosition));
        }

        if (Input.GetKey(KeyCode.RightArrow) && gameState.CanMoveRight(currentBlock))
        {
            var targetPosition = (Vector3.right * blockMove) + currentBlock.transform.position;
            StartCoroutine(MoveBlock(currentBlock, targetPosition));
        }

        // rotate clockwise
        if (Input.GetKey(KeyCode.X) && gameState.CanRotateClockwise(currentBlock))
        {
            var q = Quaternion.FromToRotation(Vector3.up, Vector3.right) * currentBlock.transform.rotation;
            StartCoroutine(RotateBlock(currentBlock, q));
        }

        // rotate counter clockwise
        if (Input.GetKey(KeyCode.Z) && gameState.CanRotateCounterClockwise(currentBlock))
        {
            var q = Quaternion.FromToRotation(Vector3.up, Vector3.left) * currentBlock.transform.rotation;
            StartCoroutine(RotateBlock(currentBlock, q));
        }
    }

    private IEnumerator MoveBlock(GameObject currentBlock, Vector3 targetPosition)
    {
        movementLocked = true;
        float timeElapsed = 0;
        Vector3 startPosition = currentBlock.transform.position;
        while (timeElapsed < duration)
        {
            currentBlock.transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        currentBlock.transform.position = targetPosition;
        movementLocked = false;
    }

    private IEnumerator RotateBlock(GameObject currentBlock, Quaternion targetRotation)
    {
        movementLocked = true;
        float timeElapsed = 0;
        Quaternion startRotation = currentBlock.transform.rotation;
        while (timeElapsed < duration)
        {
            currentBlock.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        currentBlock.transform.rotation = targetRotation;
        movementLocked = false;
    }
}
