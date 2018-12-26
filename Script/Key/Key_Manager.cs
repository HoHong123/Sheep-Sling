using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Key_Manager : MonoBehaviour {

    public int count;

    [SerializeField, Header("열쇠들"), Tooltip("열쇠들의 충돌을 확인하는 스크립의 모음")]
    private List<Key_trigger> keys;

    [SerializeField, Header("열쇠 소환"), Tooltip("열쇠 획득시 나오는 알림역할의 열쇠 정보")]
    private Key_small key;

    [Space(10)]
    [SerializeField, Header("철장")]
    private CellTrigger cell;

    private void Awake()
    {

        cell = GameObject.FindGameObjectWithTag("Cage_Manager").GetComponent<CellTrigger>();
        //key = GameObject.FindGameObjectWithTag("Key").GetComponent<Key_small>();

    }

    // Use this for initialization
    void Start ()
    {
        count = 0;
	}

    private void Update()
    {
        for(int i = 0; i < keys.Count; i++)
        {
            if (keys[i].isEnable)
            {
                keys.RemoveAt(i);
                count++;
                cell.TriggerCell();

                TriggerKey();
            }
        }
    }

    private void TriggerKey()
    {
        key.enabled = true;
    }
}
