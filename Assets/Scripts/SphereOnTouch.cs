using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class SphereOnTouch : MonoBehaviour
    {
        private Vector3 initialScale;
        [SerializeField] private GameObject spherePrefab;
        private GameObject sphere;
        [SerializeField] private GameObject road;
        [SerializeField] private bool isScaling = false;
        [SerializeField] private float minScale; // Минимальный масштаб, который вы хотите использовать
        [SerializeField] private float maxScale = 2.0f; // Максимальный масштаб, который вы хотите использовать
        [SerializeField] private float scaleSpeed; // Скорость изменения масштаба

        private void Start()
        {
            initialScale = transform.localScale;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    isScaling = true;
                }
                
                // Создаем префаб, если он задан
                if (spherePrefab != null)
                {
   
                    
                    sphere = Instantiate(spherePrefab, transform.position - Vector3.forward * 2.0f, Quaternion.identity);
                    //sphere.transform.localScale = initialScale;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isScaling = false;
                
                
            }

            if (isScaling)
            {
                float scaleFactor = Mathf.Clamp(1.0f - scaleSpeed, minScale, maxScale);
                float scaleFactor2 = Mathf.Clamp(1.0f - scaleSpeed, maxScale, minScale);

                // Проверяем, что масштаб не достиг минимального значения
                if (transform.localScale.x >  minScale)
                {
                    transform.localScale *= scaleFactor;
                    
                    //уменшение пути
                    Vector3 newScale = road.transform.localScale;
                    newScale.y *= scaleFactor;
                    road.transform.localScale = newScale;
                    
                    sphere.transform.localScale /= scaleFactor;
                    
                }
            }
        }
    }
}