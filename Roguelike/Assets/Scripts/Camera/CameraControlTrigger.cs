using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects CustomInspectorObjects;

    private Collider2D _colider;

    private void Start()
    {
        _colider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CustomInspectorObjects.PanCameraOnContact)
            {
                CameraManager.Instance.PanCameraOnContact(CustomInspectorObjects.PanDistance, CustomInspectorObjects.PanTime,
                    CustomInspectorObjects.PanDirection, false);
                Debug.Log("Pan false");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 exitDirection = (collision.transform.position - _colider.bounds.center).normalized;

            if (CustomInspectorObjects.SwapCamera && CustomInspectorObjects.CameraOnLeft != null && CustomInspectorObjects.CameraOnRight != null)
            {
                CameraManager.Instance.SwapCamera(CustomInspectorObjects.CameraOnLeft, CustomInspectorObjects.CameraOnRight, exitDirection);
            }

            if (CustomInspectorObjects.PanCameraOnContact)
            {
                CameraManager.Instance.PanCameraOnContact(CustomInspectorObjects.PanDistance, CustomInspectorObjects.PanTime,
                   CustomInspectorObjects.PanDirection, true);
                Debug.Log("Pan true");

            }
        }
    }
}

[System.Serializable]
public class CustomInspectorObjects
{
    public bool SwapCamera = false;
    public bool PanCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera CameraOnRight;
    [HideInInspector] public CinemachineVirtualCamera CameraOnLeft;

    [HideInInspector] public PanDirection PanDirection;
    [HideInInspector] public float PanDistance = 3f;
    [HideInInspector] public float PanTime = 0.35f;
}

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}

#if UNITY_EDITOR 
[CustomEditor(typeof(CameraControlTrigger))]

public class MyScriptEditor : Editor
{
    CameraControlTrigger cameraControlTrigger;

    private void OnEnable()
    {
        cameraControlTrigger = (CameraControlTrigger)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (cameraControlTrigger.CustomInspectorObjects.SwapCamera)
        {
            cameraControlTrigger.CustomInspectorObjects.CameraOnLeft = EditorGUILayout.ObjectField("Camera on left",
                            cameraControlTrigger.CustomInspectorObjects.CameraOnLeft,
                            typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;

            cameraControlTrigger.CustomInspectorObjects.CameraOnRight = EditorGUILayout.ObjectField("Camera on right",
                          cameraControlTrigger.CustomInspectorObjects.CameraOnRight,
                          typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
        }

        if (cameraControlTrigger.CustomInspectorObjects.PanCameraOnContact)
        {
            cameraControlTrigger.CustomInspectorObjects.PanDirection = (PanDirection)EditorGUILayout.EnumPopup("Camera Pan Direction",
                cameraControlTrigger.CustomInspectorObjects.PanDirection);

            cameraControlTrigger.CustomInspectorObjects.PanDistance = EditorGUILayout.FloatField("Pan Distance",
                cameraControlTrigger.CustomInspectorObjects.PanDistance);
            cameraControlTrigger.CustomInspectorObjects.PanTime = EditorGUILayout.FloatField("Pan Time",
                cameraControlTrigger.CustomInspectorObjects.PanTime);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(cameraControlTrigger);
        }
    }
}
#endif
