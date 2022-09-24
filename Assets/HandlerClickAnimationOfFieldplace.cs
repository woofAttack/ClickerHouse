using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerClickAnimationOfFieldplace : MonoBehaviour
{

    public void AnimateClickBuildOfFieldPlace()
    {
        HandlerFieldPlace.GetCurrentZoomedFieldPlace.animationChanger?.AnimateClickBuild();
    }

    public void AnimateClickCoinOfFieldPlace()
    {
        HandlerFieldPlace.GetCurrentZoomedFieldPlace.animationChanger?.AnimateClickCoin();
    }

    public void ReturnStartAnimationOfFieldPlace()
    {
        HandlerFieldPlace.GetCurrentZoomedFieldPlace.animationChanger?.ReturnAnimation();
    }


}
