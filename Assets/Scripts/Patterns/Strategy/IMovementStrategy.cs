using UnityEngine;

public interface IMovementStrategy
{
    Vector3 GetMovement(Transform transform);
}
