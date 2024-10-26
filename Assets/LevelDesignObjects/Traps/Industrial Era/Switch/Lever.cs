using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToDestroy;
    [SerializeField] private TextMeshProUGUI leverPullText;

    private TextMeshProUGUI text;

    private static Canvas canvas;

    private void Awake()
    {
        if (canvas == null)
        {
            canvas = GameObject.FindFirstObjectByType<Canvas>();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (text == null)
        {
            text = Instantiate(leverPullText, canvas.transform);
        }

        text.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject obj in objectsToDestroy)
            {
                obj.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.gameObject.SetActive(false);
    }


}
