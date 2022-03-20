using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDaemon : MonoBehaviour, ISpawnerTarget
{
    public GameObject blockPrefab;

    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }
    public void SpawnRandomBlock()
    {
        Debug.Log("SPAWN!!!!");
        var currentBlock = GameObject.Find(Constants.CURRENT_BLOCK_NAME);
        if (currentBlock != null) {
            currentBlock.name = "";
            gameState.SetBlock(currentBlock);
        }

        var newBlock = new GameObject(Constants.CURRENT_BLOCK_NAME);        

        var subs = new[] {
            Instantiate(blockPrefab, new Vector3(0, -1, 0), Quaternion.identity, newBlock.transform),
            Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity, newBlock.transform),
            Instantiate(blockPrefab, new Vector3(0, 1, 0), Quaternion.identity, newBlock.transform),
            Instantiate(blockPrefab, new Vector3(0, 2, 0), Quaternion.identity, newBlock.transform)
        };
        var spawnPoint = gameState.FetchSpawnPoint();
        newBlock.transform.position = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
