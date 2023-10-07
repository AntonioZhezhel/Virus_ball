using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class GizmoDetect : MonoBehaviour
{
    public Vector3 CubeSize = new(1f, 1f, 1f); // Размер куба
    public Color gizmoColor = Color.blue; // Цвет гизмоса

    private List<GameObject> ObjectsInsideCubeList = new List<GameObject>();
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
            // Проверяем, находится ли объект внутри куба
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
            // Проходимся по каждому дочернему объекту
            foreach (Transform child in ParentObject.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    // child.gameObject - это дочерний GameObject
                    ObjectsInsideCubeList.Add(child.gameObject); // Добавляем его в список
                }
            }
        }
    }

    // Функция для проверки, находится ли точка внутри куба
    private bool IsInsideCube(Vector3 point)
    {
        Vector3 cubeCenter = transform.position;
        Vector3 cubeExtents = CubeSize / 2;

        return Mathf.Abs(point.x - cubeCenter.x) <= cubeExtents.x &&
               Mathf.Abs(point.y - cubeCenter.y) <= cubeExtents.y &&
               Mathf.Abs(point.z - cubeCenter.z) <= cubeExtents.z;
    }
}