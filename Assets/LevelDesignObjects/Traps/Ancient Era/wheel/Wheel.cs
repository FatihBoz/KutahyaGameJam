using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Wheel : MonoBehaviour
{
    [SerializeField] private List<Transform> path;
    [SerializeField] private float duration;
    [SerializeField] private float rotationSpeed = 100f;

    public float speed = 2f;
    private int currentPointIndex = 0;
    bool reverse;

    void Update()
    {
        Movement();
    }

    void Rotate(Transform targetPoint)
    {
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,direction.y,direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Movement()
    {

        if (path.Count == 0) return; 

        Transform targetPoint = path[currentPointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);


        Rotate(targetPoint);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
        {

            if (!reverse)
            {
                currentPointIndex++;

                if (currentPointIndex >= path.Count)
                {
                    currentPointIndex = path.Count - 1;
                    reverse = true;
                }
            }
            else
            {
                currentPointIndex--;

                if (currentPointIndex < 0)
                {
                    currentPointIndex = 0; 
                    reverse = false;
                }
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        print(" e giriyo iþte");
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.PlayerDied();   
        }
    }
}
