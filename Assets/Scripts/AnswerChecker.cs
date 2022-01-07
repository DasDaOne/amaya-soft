using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AnswerChecker : MonoBehaviour
{
    [SerializeField] private UnityEvent loadNextLevelEvent;
    [SerializeField] private TextMeshProUGUI text;
    private string _correctIdentifier;

    public void SetIdentifier(string identifier)
    {
        _correctIdentifier = identifier;
        text.text = "Find " + _correctIdentifier;
    }

    public bool CheckAnswer(string identifier)
    {
        if (identifier == _correctIdentifier)
        {
            StartCoroutine(WaitUntilEndOfAnimation());
        }

        return identifier == _correctIdentifier;
    }

    private IEnumerator WaitUntilEndOfAnimation()
    {
        yield return new WaitForSeconds(.7f);
        loadNextLevelEvent?.Invoke();
    }
}
