using System.Collections;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    [SerializeField] protected float timeBetweenActions;
    [SerializeField] protected float waitingTimeAfterPush;
    [SerializeField] protected float returnTimeMultiplier;
    [SerializeField] private float delayStartTime;
    [SerializeField] protected float actionDuration = .1f;

    protected bool firstMove;
    protected bool idle;
    protected float elapsedTimeAfterAction = 0f;

    private Animator anim;


    private const string animationName = "FirstMove";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        elapsedTimeAfterAction = timeBetweenActions;
        Invoke(nameof(DelayStart),delayStartTime);
    }


    void DelayStart()
    {
        idle = true;
    }

    protected virtual void Update()
    {
        if (idle)
        {
            elapsedTimeAfterAction += Time.deltaTime;
            if (elapsedTimeAfterAction >= timeBetweenActions)
            {
                StartCoroutine(ForwardMove());

                elapsedTimeAfterAction = 0;
                idle = false;
                return;
            }
        }
    }

    protected virtual IEnumerator ForwardMove()
    {
        idle = false;
        SetAnimation(true);
        yield return new WaitForSeconds(waitingTimeAfterPush + actionDuration);
        StartCoroutine(ReturnToDefault());

    }


    protected virtual IEnumerator ReturnToDefault()
    {

        SetAnimation(false);
        yield return new WaitForSeconds(actionDuration * returnTimeMultiplier);
        idle = true;
        yield return new WaitForSeconds(0.1f);
        print(idle);
    }


    protected void SetAnimation(bool FirstMove)
    {
        anim.SetBool(animationName, FirstMove);
    }


}
