using UnityEngine;

public class Sprite : MonoBehaviour
{

    private bool isActivated = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActivated)
        {
            isActivated = true;
            animator.SetBool("IsActivated", true); // Animasyonu baþlatmak için
        }
    }

}
