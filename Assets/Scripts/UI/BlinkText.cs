using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    [Header("Timing")]
    public float showDelay = 1.5f;     // time before text starts blinking
    public float blinkSpeed = 1.0f;    // how fast it blinks

    [Header("Alpha Range")]
    public float minAlpha = 0.4f;      // lowest visibility
    public float maxAlpha = 1f;        // highest visibility

    private TextMeshProUGUI txt;
    private float timer = 0f;
    private bool canBlink = false;

    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        txt.alpha = 0f;        // start fully hidden
        txt.enabled = false;   // hide text until delay passes
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!canBlink && timer >= showDelay)
        {
            canBlink = true;
            txt.enabled = true;
        }

        if (canBlink)
        {
            float t = Mathf.PingPong(Time.time * (1f / blinkSpeed), 1f);
            txt.alpha = Mathf.Lerp(minAlpha, maxAlpha, t);
        }
    }
}
