using System.Collections.Generic;
using VirusBall;
using UnityEngine;
using UnityEngine.Events;

public class GizmoDetect : MonoBehaviour
{
    private List<GameObject> ObjectsInsideCubeList = new List<GameObject>();
    public Vector3 CubeSize = new(1f, 1f, 1f);
    [SerializeField] private Color gizmoColor = Color.blue;
    [SerializeField] private GameObject ParentObject;
    [SerializeField] private SphereOnTouch SphereOnTouch;
    [SerializeField] protected UnityEvent FinishEvent;


    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, CubeSize);
    }

    public void CheckWin()
    {
        int objectsInsideCubeCount = 0;

        foreach (var obj in ObjectsInsideCubeList)
        {
            // Check if the object is inside the cube
            if (IsInsideCube(obj.transform.position))
            {
                objectsInsideCubeCount++;
            }
        }

        if (objectsInsideCubeCount == 0)
        {
            FinishEvent.Invoke();
            SphereOnTouch.GotoFinish();
        }
    }

    public void UpdateList()
    {
        ObjectsInsideCubeList.Clear();

        if (ParentObject != null)
        {
            foreach (Transform child in ParentObject.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    ObjectsInsideCubeList.Add(child.gameObject); // Добавляем его в список
                }
            }
        }
    }

    // Function to check if a point is inside a cube
    private bool IsInsideCube(Vector3 point)
    {
        Vector3 cubeCenter = transform.position;
        Vector3 cubeExtents = CubeSize / 2;

        return Mathf.Abs(point.x - cubeCenter.x) <= cubeExtents.x &&
               Mathf.Abs(point.y - cubeCenter.y) <= cubeExtents.y &&
               Mathf.Abs(point.z - cubeCenter.z) <= cubeExtents.z;
    }
}