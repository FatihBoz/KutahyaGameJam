using UnityEngine;
using System.Collections.Generic;
using System;

public class FinalPuzzle : MonoBehaviour
{
    public List<GameObject> RotatableObjects;
    private int currentIndex = 0; // Baþlangýçta ilk nesne seçili olacak
    private GameObject currentObject;

    public GameObject middleRotatable;

    public Material highlightMaterial; // Vurgulama için materyal
    public Material defaultMaterial;
    public Material completedMaterial;

    public float rotationSpeed = 50f;

    public static bool gameFinished = false;
    
    private void Start()
    {
        currentObject = RotatableObjects[currentIndex];
        HighlightObject(currentObject);
        ChangeMaterial(defaultMaterial, middleRotatable);

    }

    void ChangeMaterial(Material material, GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            //renderer.material.color = Color.yellow;
            renderer.material = material;
        }
    }

    void HighlightObject(GameObject obj)
    {

        if (CheckRotation(currentObject.transform.localRotation, Quaternion.Euler(-180, 180, 0)))
        {
            return;
        }

        ChangeMaterial(highlightMaterial, obj);
    }

    // Vurgulamayý kaldýrma iþlevi
    void UnhighlightObject(GameObject obj)
    {
        if (CheckRotation(currentObject.transform.localRotation, Quaternion.Euler(-180, 180, 0)))
        {
            return;
        }
        ChangeMaterial(defaultMaterial, obj);

    }


    bool CheckRotation(Quaternion currentRotation, Quaternion targetRotation)
    {
        float angle = Quaternion.Angle(currentRotation, targetRotation);


        return (angle % 360) < 2f;
    }

    void RotateSelectedObject(float direction)
    {
        if (currentObject != null)
        {
            // Sadece Y ekseninde döndürme yapýyoruz
            currentObject.transform.Rotate(0, 0, direction * rotationSpeed * Time.deltaTime);

            //Debug.Log("vector rota : " + currentObject);

            if (CheckRotation(currentObject.transform.localRotation, Quaternion.Euler(-180, 180, 0)))
            {
                ChangeMaterial(completedMaterial, currentObject);
                if (isComplete())
                {
                    
                    Debug.Log("Last Puzzle Completed");
                    ChangeMaterial(completedMaterial, middleRotatable);
                    gameFinished = true;
                }
            }
            else
            {
                ChangeMaterial(defaultMaterial, middleRotatable);
                ChangeMaterial(highlightMaterial, currentObject);
            }
        }
    }
    
    bool isComplete()
    {
        foreach (GameObject obj in RotatableObjects)
        {
            Debug.Log("object : " + obj);

            // Z eksenindeki rotasyon açýsý 0 ise "Rotasyon 0" yazdýr
            if (!CheckRotation(obj.transform.localRotation, Quaternion.Euler(-180, 180, 0))){
                Debug.Log("not arranged obj : " + obj);

                return false;
            }

        }
        return true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Üstteki nesneye git
            if (currentIndex > 0)
            {
                UnhighlightObject(currentObject); // Önceki seçili nesneyi kaldýr
                currentIndex--;
                currentObject = RotatableObjects[currentIndex];
                HighlightObject(currentObject); // Yeni seçili nesneyi vurgula
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Alttaki nesneye git
            if (currentIndex < RotatableObjects.Count - 1)
            {
                UnhighlightObject(currentObject); // Önceki seçili nesneyi kaldýr
                currentIndex++;
                currentObject = RotatableObjects[currentIndex];
                HighlightObject(currentObject); // Yeni seçili nesneyi vurgula
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateSelectedObject(1); // Sað tuþa basýldýðýnda saat yönünde döndür
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateSelectedObject(-1); // Sol tuþa basýldýðýnda saat yönünün tersinde döndür
        }
        

    }
}
