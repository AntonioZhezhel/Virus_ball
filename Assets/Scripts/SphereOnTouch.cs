using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SphereOnTouch : MonoBehaviour
    {
        [SerializeField] private GameObject SpherePrefab;
        [SerializeField] private GameObject Road;
        [SerializeField] private GizmoDetect GizmoRoad;
        [SerializeField] private GameObject VirusShotPosition;
        [SerializeField] private float MinScale; // Минимальный масштаб, который вы хотите использовать
        [SerializeField] private float MaxScale = 2.0f; // Максимальный масштаб, который вы хотите использовать
        [SerializeField] private float ScaleSpeed; // Скорость изменения масштаба
        [SerializeField] private float DurationMainSphere;
        [SerializeField] private GameObject TargetPositionPrefab; // Целевая позиция для второй сферы
        private GameObject Sphere;
        private GameObject TrajectoryLine;
        private bool IsScaling = false;
        private DataHolder DataHolder;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                IsScaling = true;
                Sphere = Instantiate(SpherePrefab, VirusShotPosition.transform.position, Quaternion.identity);
            }

            if (Input.GetMouseButtonUp(0))
            {
                IsScaling = false;
                Sphere.GetComponent<ShotSphere>().Shot(TargetPositionPrefab.transform.position);
                GizmoRoad.UpdateList();
                GizmoRoad.CheckWin();
            }

            if (IsScaling)
            {
                float scaleFactor = Mathf.Clamp(1.0f - ScaleSpeed, MinScale, MaxScale);

                // Проверяем, что масштаб не достиг минимального значения
                if (transform.localScale.x > MinScale)
                {
                    transform.localScale *= scaleFactor;

                    //уменшение пути
                    Vector3 newScale = Road.transform.localScale;
                    newScale.x *= scaleFactor;
                    Road.transform.localScale = newScale;

                    // //уменшение Gizm
                    Vector3 newScaleGizmo = GizmoRoad.CubeSize;
                    newScaleGizmo.x *= scaleFactor;
                    GizmoRoad.CubeSize = newScaleGizmo;

                    //увелечение сферы
                    Sphere.transform.localScale /= scaleFactor;

                }
                else
                {
                    gameObject.GetComponent<SphereCollider>().enabled = false;
                    DataHolder = FindObjectOfType<DataHolder>();
                    DataHolder.textToPass = "Game Over";
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

            while (elapsedTime < DurationMainSphere)
            {
                transform.position = Vector3.Lerp(transform.position, TargetPositionPrefab.transform.position, 
                    elapsedTime / DurationMainSphere);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}