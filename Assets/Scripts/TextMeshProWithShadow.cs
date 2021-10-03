using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextMeshProWithShadow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _main;
    [SerializeField] private TextMeshProUGUI _shadow;

    public string text
    {
        get => _main.text;
        set
        {
            _main.text = value;
            _shadow.text = value;
        }
    }

    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }
}