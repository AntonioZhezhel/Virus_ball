using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class SphereOnTouch : MonoBehaviour
    {
        //private Vector3 initialScale;
        [SerializeField] private GameObject spherePrefab;
        private GameObject sphere;
        [SerializeField] private GameObject road;
        [SerializeField] private GizmoDetect GizmoRoad;
        [SerializeField] private GameObject VirusShotPosition;
        private bool isScaling = false;
        [SerializeField] private float minScale; // Минимальный масштаб, который вы хотите использовать
        [SerializeField] private float maxScale = 2.0f; // Максимальный масштаб, который вы хотите использовать
        [SerializeField] private float scaleSpeed; // Скорость изменения масштаба
        private GameObject trajectoryLine;
        [SerializeField] private float durationMainSphere;
        [SerializeField] private GameObject targetPositionPrefab; // Целевая позиция для второй сферы

        // private void Start()
        // {
        //     initialScale = transform.localScale;
        // }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    isScaling = true;

                    sphere = Instantiate(spherePrefab, VirusShotPosition.transform.position, Quaternion.identity);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isScaling = false;
                sphere.GetComponent<ShotSphere>().Shot(targetPositionPrefab.transform.position);
                GizmoRoad.UpdateList();
                GizmoRoad.CheckWin();
            }

            if (isScaling)
            {
                float scaleFactor = Mathf.Clamp(1.0f - scaleSpeed, minScale, maxScale);

                // Проверяем, что масштаб не достиг минимального значения
                if (transform.localScale.x > minScale)
                {
                    transform.localScale *= scaleFactor;

                    //уменшение пути
                    Vector3 newScale = road.transform.localScale;
                    newScale.x *= scaleFactor;
                    road.transform.localScale = newScale;

                    // //уменшение Gizm
                    Vector3 newScaleGizmo = GizmoRoad.cubeSize;
                    newScaleGizmo.x *= scaleFactor;
                    GizmoRoad.cubeSize = newScaleGizmo;

                    //увелечение сферы
                    sphere.transform.localScale /= scaleFactor;

                }
                else
                {
                    gameObject.GetComponent<SphereCollider>().enabled = false;
                    SceneManager.LoadScene(0);
                }
            }
        }

        public void GotoFinish()
        {
            StartCoroutine(GotoFinishSphere());
        }

        IEnumerator GotoFinishSphere()
        {
            float elapsedTime = 0f;

            while (elapsedTime < durationMainSphere)
            {
                transform.position = Vector3.Lerp(transform.position, targetPositionPrefab.transform.position,
                    elapsedTime / durationMainSphere);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}