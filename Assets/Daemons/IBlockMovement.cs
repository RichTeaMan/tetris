using UnityEngine.EventSystems;

public interface IBlockMovement : IEventSystemHandler
{
    void Gravity();

    void MoveDown();

    void MoveLeft();

    void MoveRight();

    void RotateClockwise();

    void RotateCounterClockwise();
}
