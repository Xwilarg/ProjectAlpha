using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (text.color.a > 0f)
        {
            float newAlpha = text.color.a - fadeSpeed;
            if (newAlpha < 0f)
                newAlpha = 0f;
            text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
        }
    }

    public void Restore()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }
}
