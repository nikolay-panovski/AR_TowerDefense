using UnityEngine;

public interface ITargetMoveController
{
    public void SetDestination(Vector3 dest);
    public void MoveTowardsDestination(float speed, out Vector3 distVec);
}

public class EnemyAutoMoveController : MonoBehaviour, ITargetMoveController
{
    private Vector3 destination;

    private const float SPEED_ADJUST_MULT = 0.08f;

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
    }

    public void MoveTowardsDestination(float speed, out Vector3 distVec)
    {
        distVec = destination - this.transform.parent.position;
        Vector3 distVecAdjust = Vector3.Normalize(distVec);
        distVecAdjust *= SPEED_ADJUST_MULT;

        this.transform.position += distVecAdjust * speed * Time.deltaTime;
    }
}
