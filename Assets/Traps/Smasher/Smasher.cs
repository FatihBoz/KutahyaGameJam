using System.Collections;
using UnityEngine;

public class Smasher : MovingTrap
{
    private bool canSmash;


    private void Update()
    {
        if (!canSmash)
        {
            elapsedTimeAfterFirstMove += Time.deltaTime;

            if (elapsedTimeAfterFirstMove >= timeBetweenForwardBackwardMoves)
            {
                Vector3 targetPos = new(startPos.x, startPos.y - displacementAmount, startPos.z);

                StartCoroutine(ForwardMove(targetPos));

                elapsedTimeAfterFirstMove = 0;
                return;
            }
        }
    }



}
