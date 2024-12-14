using System.Collections;
using TMPro;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] protected Transform objectToMove;
    [SerializeField] private TextMeshProUGUI leverPullText;
    [SerializeField] private float targetAngle;
    [SerializeField] private float rotateDuration;

    private bool isPlayerInTrigger;
    private static Canvas canvas;
    private TextMeshProUGUI text;

    public bool isPulled;
    protected Animator animator;

    private void Awake()
    {
        if (canvas == null)
        {
            canvas = GameObject.FindFirstObjectByType<Canvas>();
        }

        animator = GetComponent<Animator>();

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

        //MakeTextVisible();
        isPlayerInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        MakeTextInvisible();
        isPlayerInTrigger = false;
    }

    void MakeTextInvisible()
    {
        //if (text.gameObject.activeInHierarchy)
        //{
        //    text.gameObject.SetActive(false);
        //}
    }



    protected virtual void PullLever()
    {
        if (isPlayerInTrigger && !isPulled)
        {
            animator.SetBool("Pull", !isPulled);
            StartCoroutine(Rotate(objectToMove));
            //text.gameObject.SetActive(false);
            isPulled = !isPulled;
        }
    }

    protected virtual IEnumerator Rotate(Transform t)
    {
        float startRotation = t.eulerAngles.y;
        float endRotation = startRotation + targetAngle;
        float timeElapsed = 0;

        while (timeElapsed < rotateDuration)
        {
            timeElapsed += Time.deltaTime;
            float currentAngle = Mathf.Lerp(startRotation, endRotation, timeElapsed / rotateDuration);

            t.eulerAngles = new Vector3(t.eulerAngles.x, currentAngle, t.eulerAngles.z);
            yield return null;
        }
        t.eulerAngles = new Vector3(t.eulerAngles.x, endRotation, t.eulerAngles.z);
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
