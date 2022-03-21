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

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        blockMovement = FindObjectOfType<BlockMovement>();
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

            int randomNumber = Random.Range(0, 7);

            // I piece
            //if (randomNumber == 0)
            {
                newBlock.transform.position = new Vector3(0.5f, -0.5f, 0);

                var subs = new[] {
                    Instantiate(blockPrefab, new Vector3(-1, 0, 0), Quaternion.identity, newBlock.transform),
                    Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity, newBlock.transform),
                    Instantiate(blockPrefab, new Vector3(1, 0, 0), Quaternion.identity, newBlock.transform),
                    Instantiate(blockPrefab, new Vector3(2, 0, 0), Quaternion.identity, newBlock.transform)
                };
                //var spawnPoint = gameState.FetchSpawnPoint();
                var spawnPoint = new Vector3(4.5f, 0.5f, 0);
                newBlock.transform.position = spawnPoint;
            }
            spawnRequested = false;
        }
    }
}
