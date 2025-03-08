using UnityEngine;
using System.Collections.Generic;
using System;

public class FinalPuzzle : MonoBehaviour
{
    public List<GameObject> RotatableObjects;
    private int currentIndex = 0; // Ba�lang��ta ilk nesne se�ili olacak
    private GameObject currentObject;

    public GameObject middleRotatable;

    public Material highlightMaterial; // Vurgulama i�in materyal
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

    // Vurgulamay� kald�rma i�levi
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
            // Sadece Y ekseninde d�nd�rme yap�yoruz
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

            // Z eksenindeki rotasyon a��s� 0 ise "Rotasyon 0" yazd�r
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
            // �stteki nesneye git
            if (currentIndex > 0)
            {
                UnhighlightObject(currentObject); // �nceki se�ili nesneyi kald�r
                currentIndex--;
                currentObject = RotatableObjects[currentIndex];
                HighlightObject(currentObject); // Yeni se�ili nesneyi vurgula
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Alttaki nesneye git
            if (currentIndex < RotatableObjects.Count - 1)
            {
                UnhighlightObject(currentObject); // �nceki se�ili nesneyi kald�r
                currentIndex++;
                currentObject = RotatableObjects[currentIndex];
                HighlightObject(currentObject); // Yeni se�ili nesneyi vurgula
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateSelectedObject(1); // Sa� tu�a bas�ld���nda saat y�n�nde d�nd�r
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateSelectedObject(-1); // Sol tu�a bas�ld���nda saat y�n�n�n tersinde d�nd�r
        }
        

    }
}
