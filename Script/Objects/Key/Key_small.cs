using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_small : MonoBehaviour
{

    [SerializeField]
    private AudioSource sound;

    [SerializeField]
    private GameObject child;
    private Transform childTrans;

    [SerializeField, Header("키의 유지 시간")]
    private float beginGrow; // 키 확대 시간
    [SerializeField]
    private float duration, endReduce, maxDuration, progress;
    // 키 유지 시간, 감소시간, 총 소요 시간, 현재 진행도
    private float beginToDuration; // 확대와 지속 시간의 합 시간

    [Space(10)]

    [SerializeField, Header("키 회전")]
    private float rotationSpeed;


    private void OnEnable()
    {
        child.SetActive(true);
        sound.Play();

        childTrans = child.transform;

        maxDuration = duration + beginGrow + endReduce;
        beginToDuration = duration + beginGrow;

        progress = 0;

    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        childTrans.Rotate(Vector3.up * Time.deltaTime * -rotationSpeed);

        progress += Time.deltaTime;

        if (progress < beginToDuration)
            childTrans.localScale = Vector3.Lerp(childTrans.localScale, Vector3.one, progress / beginGrow);
        else if (progress < maxDuration)
            childTrans.localScale = Vector3.Lerp(childTrans.localScale, Vector3.zero, (progress - beginToDuration) / endReduce);
        else
        {
            child.SetActive(false);
            enabled = false;
        }
    }

}
