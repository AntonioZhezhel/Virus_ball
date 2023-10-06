using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GizmoDetect : MonoBehaviour
{
    public Vector3 cubeSize = new(1f, 1f, 1f); // Размер куба
    public Color gizmoColor = Color.blue; // Цвет гизмоса

    private List<GameObject> objectsInsideCubeList = new List<GameObject>();
    [SerializeField] private GameObject parentObject;
    [SerializeField] private SphereOnTouch SphereOnTouch;
    [SerializeField] protected UnityEvent FinishEvent;


    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, cubeSize);
    }

    
    
    // вызывать при убийстве
    public void CheckWin()
    {
        int objectsInsideCubeCount = 0;
        
        foreach (var obj in objectsInsideCubeList)
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
        objectsInsideCubeList.Clear();

        if (parentObject != null)
        {
            // Проходимся по каждому дочернему объекту
            foreach (Transform child in parentObject.transform)
            {
                if (child.gameObject.activeSelf)
                {
                    // child.gameObject - это дочерний GameObject
                    objectsInsideCubeList.Add(child.gameObject); // Добавляем его в список
                }
            }
        }
    }

    // Функция для проверки, находится ли точка внутри куба
    private bool IsInsideCube(Vector3 point)
    {
        Vector3 cubeCenter = transform.position;
        Vector3 cubeExtents = cubeSize / 2;

        return Mathf.Abs(point.x - cubeCenter.x) <= cubeExtents.x &&
               Mathf.Abs(point.y - cubeCenter.y) <= cubeExtents.y &&
               Mathf.Abs(point.z - cubeCenter.z) <= cubeExtents.z;
    }
}
