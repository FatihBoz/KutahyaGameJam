using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToDestroy;
    [SerializeField] private TextMeshProUGUI leverPullText;

    private bool isPlayerInTrigger;
    private static Canvas canvas;
    private TextMeshProUGUI text;

    bool isPulled;

    private void Awake()
    {
        if (canvas == null)
        {
            canvas = GameObject.FindFirstObjectByType<Canvas>();
        }

    }


    void MakeTextVisible()
    {
        if (text == null)
        {
            text = Instantiate(leverPullText, canvas.transform);
        }
        text.gameObject.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isPulled) return;

        MakeTextVisible();
        isPlayerInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        MakeTextInvisible();
        isPlayerInTrigger = false;
    }

    void MakeTextInvisible()
    {
        if (text.gameObject.activeInHierarchy)
        {
            text.gameObject.SetActive(false);
        }
    }



    private void PullLever()
    {
        if (isPlayerInTrigger && !isPulled)
        {
            foreach (GameObject obj in objectsToDestroy)
            {
                obj.SetActive(false);
            }
            text.gameObject.SetActive(false);
            isPulled = true;
        }
    }

    private void Start()
    {
        PlayerInput.Instance.OnInteracted += PullLever;
    }

    private void OnDestroy()
    {
        PlayerInput.Instance.OnInteracted -= PullLever;
    }

}
