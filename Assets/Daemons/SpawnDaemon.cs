using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDaemon : MonoBehaviour, ISpawnerTarget
{
    public GameObject blockPrefab;

    public GameObject destroyPrefab;

    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }
    public void SpawnRandomBlock()
    {
        Debug.Log("SPAWN!!!!");


        var newBlock = new GameObject(Constants.CURRENT_BLOCK_NAME);

        var subs = new[] {
            Instantiate(blockPrefab, new Vector3(0, -1, 0), Quaternion.identity, newBlock.transform),
            Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity, newBlock.transform),
            Instantiate(blockPrefab, new Vector3(0, 1, 0), Quaternion.identity, newBlock.transform),
            Instantiate(blockPrefab, new Vector3(0, 2, 0), Quaternion.identity, newBlock.transform)
        };
        var spawnPoint = gameState.FetchSpawnPoint();
        newBlock.transform.position = spawnPoint;
        Debug.Log($"COUNT!!!! {newBlock.transform.childCount}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
