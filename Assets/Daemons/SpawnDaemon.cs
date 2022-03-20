using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDaemon : MonoBehaviour, ISpawnerTarget
{
    public GameObject blockPrefab;
    public void SpawnRandomBlock()
    {
        Debug.Log("SPAWN!!!!");
        var currentBlock = GameObject.Find(Constants.CURRENT_BLOCK_NAME);
        currentBlock.name = "";

        // Instantiate at position (0, 0, 0) and zero rotation.
        //var newBlock = Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        //newBlock.name = Constants.CURRENT_BLOCK_NAME;

        var newBlock = new GameObject(Constants.CURRENT_BLOCK_NAME);

        var subs = new[] {
            Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity),
            Instantiate(blockPrefab, new Vector3(0, 1, 0), Quaternion.identity),
            Instantiate(blockPrefab, new Vector3(0, 2, 0), Quaternion.identity),
            Instantiate(blockPrefab, new Vector3(0, 3, 0), Quaternion.identity)
        };

        foreach (var sub in subs)
        {
            sub.transform.parent = newBlock.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
