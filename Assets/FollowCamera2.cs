using UnityEngine;

public class FollowCamera2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.rotation = Quaternion.Euler(0, 10, 0);
        }
    }
}