using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    
    float duration = 0.25f;

    float blockMove = 1.0f;

    private bool movementLocked = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (movementLocked) {
            return;
        }

        var currentBlock = GameObject.Find("CurrentBlock");
        if (currentBlock == null)
        {
            Debug.LogError("Current block could not be found.");
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //currentBlock.transform.Translate(Vector3.forward * Time.deltaTime);  
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            var targetPosition = (Vector3.down * blockMove) + currentBlock.transform.position;
            StartCoroutine(MoveBlock(currentBlock, targetPosition));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            var targetPosition = (Vector3.left * blockMove) + currentBlock.transform.position;
            StartCoroutine(MoveBlock(currentBlock, targetPosition));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            var targetPosition = (Vector3.right * blockMove) + currentBlock.transform.position;
            StartCoroutine(MoveBlock(currentBlock, targetPosition));
        }

        // rotate clockwise
        if (Input.GetKey(KeyCode.X))
        {
            var q = Quaternion.FromToRotation(Vector3.up, Vector3.right) * currentBlock.transform.rotation;
            StartCoroutine(RotateBlock(currentBlock, q));
        }

        // rotate counter clockwise
        if (Input.GetKey(KeyCode.Z))
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
