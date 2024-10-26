using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowThrower : MonoBehaviour
{
    public static ArrowThrower Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void MakeActiveWithDelay(GameObject obj , float delayTime)
    {
        StartCoroutine(SetActive(obj, delayTime));
    }

    private IEnumerator SetActive(GameObject obj, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        obj.SetActive(true);
    }
}
