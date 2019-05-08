using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public float lerpSpeed;

    [SerializeField] private float fillAmount;
    [SerializeField] private Image bar;
    [SerializeField] private Color fullColor;
    [SerializeField] private Color lowColor;
    [SerializeField] private bool lerpColor;
    [SerializeField] private RectTransform button;

    public float MaxValue { get; set; }

    public float Value {
        set {
            fillAmount = calculation(value, 0, MaxValue, 0, 1);
        }
    }

    private void Start()
    {
        if (lerpColor)
        {
            bar.color = lowColor;
        }
    }

    private void Update()
    {
        controller();
    }

    private void controller()
    {
        if (fillAmount != bar.fillAmount)
        {
            bar.fillAmount = Mathf.Lerp(bar.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
            button.anchoredPosition = new Vector2((bar.fillAmount * 100) - 50.0f, 0);
        }

        if (lerpColor)
        {
            bar.color = Color.Lerp(lowColor, fullColor, fillAmount);
        }
    }

    private float calculation(float currentFill, float minFill, float maxFill, float fillAmountMin, float fillAmountMax)
    {
        return (currentFill - minFill) * (fillAmountMax - fillAmountMin) / (maxFill - minFill) + fillAmountMin;
        //(53 - 0) * (1 - 0) / (100 - 0) + 0
        //53 / 100 = 0.53
    }
}
