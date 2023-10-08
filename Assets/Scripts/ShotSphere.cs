using System.Collections;
using UnityEngine;

namespace VirusBall
{
    public class ShotSphere : MonoBehaviour
    {
        [SerializeField] private float Duration = 2.0f;
        [SerializeField] private float Delay = 0.5f;

        public void Shot(Vector3 transformPosition)
        {
            StartCoroutine(MoveSecondarySphere(transformPosition));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Bot")
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

            while (elapsedTime < Duration)
            {
                if (gameObject == null)
                {
                    yield break;
                }

                transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / Duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}