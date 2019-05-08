using System.Collections;
using UnityEngine;
using System;

[Serializable]
public class ProgressStat
{
    [SerializeField]
    private ProgressBar bar;

    [SerializeField]
    private float maxVal;

    [SerializeField]
    private float currentVal;

    public float CurrentVal {
        get {
            return currentVal;
        }

        set {
            currentVal = value;
            bar.Value = currentVal;
        }
    }

    public float MaxVal {
        get {
            return maxVal;
        }

        set {
            maxVal = value;
            bar.MaxValue = maxVal;
        }
    }

    public void Initialize()
    {
        MaxVal = maxVal;
        CurrentVal = currentVal;
    }
}
