using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScaleLock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private MMF_Player feedbackPlayer;
    private bool _isReversed = true; // Track if the feedback has been fully reversed

    // Called on pointer enter to play feedback if reversed
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isReversed) {
            feedbackPlayer?.PlayFeedbacks();
            _isReversed = false; // Lock until reverse is triggered
        }
    }

    // Called on pointer exit to reverse feedback
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isReversed) {
            feedbackPlayer?.PlayFeedbacksInReverse();
            _isReversed = true; // Unlock so play can be triggered again
        }
    }
}

