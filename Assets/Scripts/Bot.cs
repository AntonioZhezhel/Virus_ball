using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private GizmoDetect GizmoDetect;
    private string TriggerSphere = "Sphere";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sphere")
        {
            gameObject.SetActive(false);
            Destroy(other.gameObject);
            GizmoDetect.UpdateList();
            GizmoDetect.CheckWin();
        }
    }
}