using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockMovement : MonoBehaviour, IBlockMovement
{

    private float duration = 0.16f;

    private float blockMove = 1.0f;

    private bool movementLocked = false;

    private bool gravity = false;

    private GameState gameState;

    private GameObject daemon;

    private int massMovementInProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        daemon = GameObject.Find("Daemon");
    }

    // Update is called once per frame
    void Update()
    {
        if (gravity && !movementLocked)
        {
            gameState.Gravity();
            gravity = false;
        }
    }

    public void Gravity()
    {
        gravity = true;
    }

    public void MoveDown()
    {
        if (movementLocked)
        {
            return;
        }
        var currentBlock = GameObject.Find("CurrentBlock");
        var targetPosition = (Vector3.down * blockMove) + currentBlock.transform.position;
        StartCoroutine(MoveBlock(currentBlock, targetPosition));
    }

    public void MoveLeft()
    {
        if (movementLocked)
        {
            return;
        }
        var currentBlock = GameObject.Find("CurrentBlock");
        var targetPosition = (Vector3.left * blockMove) + currentBlock.transform.position;
        StartCoroutine(MoveBlock(currentBlock, targetPosition));
    }

    public void MoveRight()
    {
        if (movementLocked)
        {
            return;
        }
        var currentBlock = GameObject.Find("CurrentBlock");
        var targetPosition = (Vector3.right * blockMove) + currentBlock.transform.position;
        StartCoroutine(MoveBlock(currentBlock, targetPosition));
    }

    public void RotateClockwise()
    {
        if (movementLocked)
        {
            return;
        }
        var currentBlock = GameObject.Find("CurrentBlock");
        var q = Quaternion.FromToRotation(Vector3.up, Vector3.right) * currentBlock.transform.rotation;
        StartCoroutine(RotateBlock(currentBlock, q));
    }

    public void RotateCounterClockwise()
    {
        if (movementLocked)
        {
            return;
        }
        var currentBlock = GameObject.Find("CurrentBlock");
        var q = Quaternion.FromToRotation(Vector3.up, Vector3.left) * currentBlock.transform.rotation;
        StartCoroutine(RotateBlock(currentBlock, q));
    }

    public void MoveBlocksDown(GameObject[] blocks, int lineCount)
    {
        var movementAmount = (Vector3.down * blockMove * (float)lineCount);
        foreach (var block in blocks)
        {
            var targetPosition = movementAmount + block.transform.position;
            StartCoroutine(MoveBlockWithoutLock(block, targetPosition));
        }
    }

    public bool IsMovementInProgress()
    {
        return movementLocked || massMovementInProgress > 0;
    }

    private IEnumerator MoveBlock(GameObject currentBlock, Vector3 targetPosition)
    {
        movementLocked = true;
        var enumerator = MoveBlockWithoutLock(currentBlock, targetPosition);
        while (enumerator.MoveNext())
        {
            yield return enumerator.Current;
        }
        movementLocked = false;
    }

    private IEnumerator MoveBlockWithoutLock(GameObject currentBlock, Vector3 targetPosition)
    {
        massMovementInProgress++;
        float timeElapsed = 0;
        Vector3 startPosition = currentBlock.transform.position;
        while (timeElapsed < duration)
        {
            currentBlock.transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        currentBlock.transform.position = targetPosition;
        massMovementInProgress--;
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
