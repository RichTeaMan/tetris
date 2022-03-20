using UnityEngine.EventSystems;

public interface ISpawnerTarget : IEventSystemHandler
{
    void SpawnRandomBlock();
}
