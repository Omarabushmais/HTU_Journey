using UnityEngine;

public class GhostFollower : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Follow Settings")]
    public Vector3 offset = new Vector3(1.5f, 1.5f, -2f);
    public float followSpeed = 4f;
    public float rotationSpeed = 5f;
    
    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + player.TransformDirection(offset);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0f;

        if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}