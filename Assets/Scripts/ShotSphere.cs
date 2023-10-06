using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class ShotSphere : MonoBehaviour
    {
        [SerializeField] private float duration = 2.0f;
        [SerializeField] private float Delay = 0.5f;
        private string TriggerBot = "Bot";
        
        public void Shot(Vector3 transformPosition)
        {
            StartCoroutine(MoveSecondarySphere(transformPosition));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == TriggerBot)
            {
                StartCoroutine(DelayedDestroy());
                gameObject.SetActive(false);
            }
        }

        private IEnumerator DelayedDestroy()
        {
            yield return new WaitForSeconds(Delay);
            Destroy(gameObject);
        }

        IEnumerator MoveSecondarySphere(Vector3 targetPosition)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                if (gameObject == null)
                {
                    yield break; // Прерываем корутину, если сфера была уничтожена
                }

                transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}