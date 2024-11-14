using MoreMountains.Feedbacks;
using UnityEngine;

public class IntroExitManager : MonoBehaviour
{
    public MMF_Player titleExitPlayer;

    public void TriggerExitAnimation()
    {
        titleExitPlayer?.PlayFeedbacks();
    }
}
