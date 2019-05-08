using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Clear_Manager : MonoBehaviour {

    public AudioSource Koung_Sound, loseSound, clearSound;
    public Rigidbody rig;
    public bool call;
    
    private bool lose, clear;
    
    [Header("클리어 보드, 늑대")]
    public GameObject Clear_Obj;
    public GameObject Fail_Obj;

    [Header("각 파라미터 설정")]
    public int Save_Sheep = 0;
    public int Using_Sheep = 0;
    public int Timer = 0; 
    
    [Header("Scene_Change")]
    public bool Cheak = false;

    [Space(10)]
    [SerializeField, Header("점수 변동 스크립")]
    private _Score_Changer board;
    public bool Score_End;

    // 양 오브젝트 켜지는것

    private void Awake()
    {
        lose = clear = false;

        Score_End = false;

        rig = gameObject.GetComponent<Rigidbody>();

        Clear_Obj.SetActive(false);
        Fail_Obj.SetActive(false);

    }

    private void OnEnable() {}

    private void Update()
    {
        if (call)
        {
            Clear();
            call = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Koung_Sound.Play(); // 사운드 플레이
            rig.isKinematic = true;
            Cheak = true;

            Invoke("CallBoard", 0.5f);
        }
    }
    private void CallBoard()
    {
        board.ScoreOn(ref Timer);
    }

    public void Clear() // 클리어 보드 켜주기
    {
        if (!clear)
        {
            clearSound.Play();
            clear = true;
        }

        Clear_Obj.SetActive(true);
    }

    public void Fail() // 실패 보드 켜주기
    {
        if (!lose)
        {
            loseSound.Play();
            lose = true;
        }

        Fail_Obj.SetActive(true);
    }
    
}
