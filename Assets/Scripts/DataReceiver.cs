using TMPro;
using UnityEngine;

public class DataReceiver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;

    private void Start()
    {
        DataHolder dataHolder = FindObjectOfType<DataHolder>();

        if (dataHolder != null)
        {
            Text.SetText(dataHolder.TextToPass);
        }
    }
}