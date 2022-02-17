using UnityEngine;

public interface ITargetMoveController
{
    public void SetDestination(Vector3 dest);
    public void Move(Vector3 offset);
}

public class EnemyMoveController : MonoBehaviour, ITargetMoveController
{
    private Vector3 destination;

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
    }

    public void Move(Vector3 offset)
    {
        // towards destination
        Vector3 distVec = destination - /**/this.transform.parent.position;

        /*
        // FROM TestShowcaseEnemy.cs UNTIL REIMPLEMENTATION
        if (imageTargetGoal.TargetName == targetedImageName)
        {
            Vector3 distVec = augmentationObject.transform.position - this.transform.position;
            distVec.Normalize();
            distVec *= 0.08f;       // hardcoded number, can serialize

            this.transform.position += distVec * Time.deltaTime;

            // if we are detected as rendered from the ImageTarget, spawn bullets every 2 seconds
            if (GetComponent<MeshRenderer>().enabled == true && Time.time - lastBulletAt >= 2.0f)
            {
                Debug.Log("Bullet!");
                GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                bullet.transform.parent = this.transform;
                bullet.transform.SetPositionAndRotation(this.transform.position,
                    Quaternion.LookRotation(augmentationObject.transform.position - this.transform.position, Vector3.up) * Quaternion.Euler(new Vector3(90, 0, 0)));
                bullet.transform.localScale *= 0.04f;

                lastBulletAt = Time.time;
            }

            // worst part: find every already spawned one (instead of handling in its own class)
            // move it, destroy if sufficiently within target
            foreach (Transform bulletTransform in transform)
            {
                bulletTransform.position += distVec * 2.0f * Time.deltaTime;
                if (Vector3.Distance(bulletTransform.position, augmentationObject.transform.position) < 0.064f)
                {
                    Destroy(bulletTransform.gameObject);
                    Debug.Log("Bullet hit target!");
                }
            }
        }
        */
    }
}
