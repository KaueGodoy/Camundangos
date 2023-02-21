using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform playerTransform;
    public Transform enemyTransform;
    public Transform randomObjTransform;

    private void Awake()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }

    private void Update()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }

}
