using UnityEngine;

public class EnemyAutoMoveController : MonoBehaviour
{
    private Vector3 destination;

    private const float SPEED_ADJUST_MULT = 0.08f;

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
    }

    public void MoveTowardsDestination(float speed, out Vector3 distVec)
    {
        distVec = destination - this.transform/*.parent*/.position; // parent when (at least trying with) ImageTarget, else self (more logical)
        Vector3 distVecAdjust = Vector3.Normalize(distVec);
        distVecAdjust *= SPEED_ADJUST_MULT;

        this.transform.position += distVecAdjust * speed * Time.deltaTime;
    }
}
