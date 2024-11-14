using UnityEngine;

public class ButtonPositionInitializer : MonoBehaviour
{
    public Transform[] buttons;

    private Vector3 offScreenPosition = new Vector3(0, 0, 9000);

    private void Start()
    {
        foreach (Transform button in buttons) {
            button.localPosition = offScreenPosition;
        }
    }
}
