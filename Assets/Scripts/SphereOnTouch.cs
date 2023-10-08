using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VirusBall
{
    public class SphereOnTouch : MonoBehaviour
    {
        [SerializeField] private GameObject SpherePrefab;
        [SerializeField] private GameObject Road;
        [SerializeField] private GizmoDetect GizmoRoad;
        [SerializeField] private GameObject VirusShotPosition;
        [SerializeField] private float MinScale;  
        [SerializeField] private float MaxScale = 2.0f;
        [SerializeField] private float ScaleSpeed;
        [SerializeField] private float DurationMainSphere;
        [SerializeField] private GameObject TargetPositionPrefab; 
        private GameObject Sphere;
        private GameObject TrajectoryLine;
        private bool IsScaling = false;
        private bool CheckFinish = true;
        private DataHolder DataHolder;

        private void Update()
        {
            if (CheckFinish)
            {
               if (Input.GetMouseButtonDown(0))
                   MouseButtonDown();
               
               if (Input.GetMouseButtonUp(0))
                   MouseButtonUp();
            }
            

            if (IsScaling)
            {
                float scaleFactor = Mathf.Clamp(1.0f - ScaleSpeed, MinScale, MaxScale);

                // Check that the scale has not reached the minimum value
                if (transform.localScale.x > MinScale)
                {
                    transform.localScale *= scaleFactor;
                    DecreasePath(scaleFactor);
                    DecreaseGizmo(scaleFactor);
                    IncreaseSphere(scaleFactor);
                }
                else
                {
                    gameObject.GetComponent<SphereCollider>().enabled = false;
                    DataHolder = FindObjectOfType<DataHolder>();
                    DataHolder.TextToPass = "Game Over";
                    SceneManager.LoadScene(0);
                }
            }
        }

        private void MouseButtonDown()
        {
            IsScaling = true;
            Sphere = Instantiate(SpherePrefab, VirusShotPosition.transform.position, Quaternion.identity);
        }

        private void MouseButtonUp()
        {
            IsScaling = false;
            Sphere.GetComponent<ShotSphere>().Shot(TargetPositionPrefab.transform.position);
            GizmoRoad.UpdateList();
            GizmoRoad.CheckWin();
        }

        private void DecreasePath(float scaleFactor)
        {
            Vector3 newScale = Road.transform.localScale;
            newScale.x *= scaleFactor;
            Road.transform.localScale = newScale; 
        }

        private void DecreaseGizmo(float scaleFactor)
        {
            Vector3 newScaleGizmo = GizmoRoad.CubeSize;
            newScaleGizmo.x *= scaleFactor;
            GizmoRoad.CubeSize = newScaleGizmo;
        }
        
        private void IncreaseSphere(float scaleFactor)
        {
            Sphere.transform.localScale /= scaleFactor;
        }

        public void GotoFinish()
        {
            CheckFinish = false;
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