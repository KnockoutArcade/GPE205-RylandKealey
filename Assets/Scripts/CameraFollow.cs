using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Quaternion rotationOffset = Quaternion.Euler(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        // Set the position to the follow point
        transform.position = target.position;

        // Find the game object that our follow point belongs to
        GameObject targetOwner = target.gameObject;

        // Look at that game object
        transform.rotation = targetOwner.transform.rotation;

        // Rotate downwards slightly (there's some weird stuff happening, which is why it is this instead of Vector3.down)
        transform.Rotate(Vector3.right, 13);
    }
}
