using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMovement : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Space]

    [Header("Offset")]
    public Vector3 offset = Vector3.zero;

    [Space]

    [Header("Limits")]
    public Vector2 limitCamera = new Vector2(5, 3);

    [Space]

    [Header("Smooth Damp Time")]
    [Range(0, 1)]
    public float smoothTime;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            transform.localPosition = offset;
        }

        FollowTarget(target);
    }

    void LateUpdate()
    {
        Vector3 localPos = transform.localPosition;

        transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -limitCamera.x, limitCamera.x), Mathf.Clamp(localPos.y, -limitCamera.y, limitCamera.y), localPos.z);
    }
    public void FollowTarget(Transform t)
    {
        Vector3 localPos = transform.localPosition;
        Vector3 targetLocalPos = t.transform.localPosition;
        transform.localPosition = Vector3.SmoothDamp(localPos, new Vector3(targetLocalPos.x + offset.x, targetLocalPos.y + offset.y, localPos.z), ref velocity, smoothTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(-limitCamera.x, -limitCamera.y, transform.position.z), new Vector3(limitCamera.x, -limitCamera.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-limitCamera.x, limitCamera.y, transform.position.z), new Vector3(limitCamera.x, limitCamera.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(-limitCamera.x, -limitCamera.y, transform.position.z), new Vector3(-limitCamera.x, limitCamera.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(limitCamera.x, -limitCamera.y, transform.position.z), new Vector3(limitCamera.x, limitCamera.y, transform.position.z));
    }
}
