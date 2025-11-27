using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    [Header("Timing")]
    public float showDelay = 1.5f;     
    public float blinkSpeed = 1.0f;    

    [Header("Alpha Range")]
    public float minAlpha = 0.4f;      
    public float maxAlpha = 1f;        

    private TextMeshProUGUI txt;
    private float timer = 0f;
    private bool canBlink = false;

    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        txt.alpha = 0f;       
        txt.enabled = false;   
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
