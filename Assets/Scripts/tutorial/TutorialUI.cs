using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text text;

    private System.Action callback;
    private bool waitingKey = false;

    public void ShowMessage(string msg, System.Action onContinue)
    {
        panel.SetActive(true);
        text.text = msg;

        callback = onContinue;
        waitingKey = true;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        if (waitingKey && Input.anyKeyDown)
        {
            waitingKey = false;
            callback?.Invoke();
        }
    }
}
