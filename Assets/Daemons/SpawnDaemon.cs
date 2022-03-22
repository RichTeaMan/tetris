using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDaemon : MonoBehaviour, ISpawnerTarget
{
    public GameObject blockPrefab;

    public GameObject destroyPrefab;

    private GameState gameState;

    private BlockMovement blockMovement;

    private bool spawnRequested = false;

    private IEnumerator<BlockType> blockSeqeunce;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        blockMovement = FindObjectOfType<BlockMovement>();
        blockSeqeunce = new BlockSequence().CreateSequence().GetEnumerator();
        blockSeqeunce.MoveNext();
    }
    public void SpawnRandomBlock()
    {
        spawnRequested = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnRequested && !blockMovement.IsMovementInProgress())
        {
            Debug.Log("Spawning block");

            var newBlock = new GameObject(Constants.CURRENT_BLOCK_NAME);

            BlockType blockType = blockSeqeunce.Current;

            Debug.Log(blockType.ToString());
            if (blockType == BlockType.I)
            {
                newBlock.transform.position = new Vector3(0.5f, -0.5f, 0);

                Instantiate(blockPrefab, new Vector3(-1, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(1, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(2, 0, 0), Quaternion.identity, newBlock.transform);

                var spawnPoint = new Vector3(4.5f, 0.5f, 0);
                newBlock.transform.position = spawnPoint;
            }
            else if (blockType == BlockType.J)
            {
                newBlock.transform.position = new Vector3(0.0f, 0.0f, 0);

                Instantiate(blockPrefab, new Vector3(-1, 1, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(-1, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(1, 0, 0), Quaternion.identity, newBlock.transform);

                var spawnPoint = new Vector3(4.0f, 0.0f, 0);
                newBlock.transform.position = spawnPoint;
            }
            else if (blockType == BlockType.L)
            {
                newBlock.transform.position = new Vector3(0.0f, 0.0f, 0);

                Instantiate(blockPrefab, new Vector3(1, 1, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(-1, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(1, 0, 0), Quaternion.identity, newBlock.transform);

                var spawnPoint = new Vector3(4.0f, 0.0f, 0);
                newBlock.transform.position = spawnPoint;
            }
            else if (blockType == BlockType.O)
            {
                newBlock.transform.position = new Vector3(0.0f, 0.0f, 0);

                Instantiate(blockPrefab, new Vector3(1, 1, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(1, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(0, 1, 0), Quaternion.identity, newBlock.transform);

                var spawnPoint = new Vector3(5.0f, 0.0f, 0);
                newBlock.transform.position = spawnPoint;
            }
            else if (blockType == BlockType.S)
            {
                newBlock.transform.position = new Vector3(0.0f, 0.0f, 0);

                Instantiate(blockPrefab, new Vector3(-1, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(1, 1, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(0, 1, 0), Quaternion.identity, newBlock.transform);

                var spawnPoint = new Vector3(4.0f, 0.0f, 0);
                newBlock.transform.position = spawnPoint;
            }
            else if (blockType == BlockType.T)
            {
                newBlock.transform.position = new Vector3(0.0f, 0.0f, 0);

                Instantiate(blockPrefab, new Vector3(0, 1, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(1, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(-1, 0, 0), Quaternion.identity, newBlock.transform);

                var spawnPoint = new Vector3(4.0f, 0.0f, 0);
                newBlock.transform.position = spawnPoint;
            }
            else if (blockType == BlockType.Z)
            {
                newBlock.transform.position = new Vector3(0.0f, 0.0f, 0);

                Instantiate(blockPrefab, new Vector3(-1, 1, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(1, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity, newBlock.transform);
                Instantiate(blockPrefab, new Vector3(0, 1, 0), Quaternion.identity, newBlock.transform);

                var spawnPoint = new Vector3(4.0f, 0.0f, 0);
                newBlock.transform.position = spawnPoint;
            }
            else
            {
                throw new Exception($"Unknown block type {blockType}.");
            }


            spawnRequested = false;
            blockSeqeunce.MoveNext();
        }
    }
}
