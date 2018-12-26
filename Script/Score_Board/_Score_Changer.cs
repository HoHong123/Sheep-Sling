using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class _Score_Changer : MonoBehaviour {

    public _Clear_Manager C_Manager;
    public _Sheep_Manager S_Manager;
    
    private int remainNumberOfSheep, time;    // 각 요소를 점수로 변환하기 전의 수들
    private int next, score;    // 다음 계산을 하도록 도와주는 변수
    private int numberOfUsedSheep;

    [SerializeField]
    private float duration; // 점수를 구하는 시간 설정
    private float lerp, scaleOverTime; // 매번 시간단위로 증가하는 값, deltaTime과 연산하여 넣는 변수
    private float sheepLerp, sheepScaleOverTime; // 매번 시간단위로 증가하는 값, deltaTime과 연산하여 넣는 변수
    private int sheepScore;

    // 초기 양의 수
    [Header("전체 양의 크기*"), SerializeField]
    private int maxNumberOfSheep;
    
    [Header("3별을 얻을 수 있는 점수*"), SerializeField]
    private int maximumScore;
    
    // [Space(5), Header("점수"), SerializeField]
    private int remainNumberOfSheepScore, timeScore, finalScore;
    // 사용한 양의 점수, 시간 점수, 최종 점수
    
    [Space(5)]
    [SerializeField, Header("별 스프라이트*")]
    private GameObject[] Stars; // 양 스프라이트 표시를 위한 배열
    private int starCounter;
    
    [Space(5)]
    [SerializeField, Header("텍스트 (1.남은양, 2.시간, 3.Score)*")]
    private Text[] texts; // 각 점수를 표현할 객체 삽입
    [SerializeField, Header("실행된 시간, 남은 양 텍스트")]
    private Text remainTime;
    [SerializeField]
    private Text remainSheep;

    [Space(5)]
    [SerializeField, Header("파티클*")]
    private GameObject starDust; // 스타 파티클
    [SerializeField]
    private GameObject starBoxDust; // 스타 박스 파티클
    [SerializeField, Header("날리는 양*")]
    private GameObject flyingSheep;
    [SerializeField]
    private Transform resultSheep; // 양 파티클

    [Space(5)] // 각종 사운드 이팩트
    [SerializeField, Header("사운드*")]
    private AudioSource YAY;
    [SerializeField]
    private AudioSource coin, count, sheep;

    [SerializeField]
    private bool yay;

    
    // 아래 함수 발동시 연산 시작
    public void ScoreOn(ref int Time)
    {
        // 모든 변수 초기화
        next = score = starCounter = 0;
        lerp = scaleOverTime = 0.0f;

        sheepLerp = sheepScaleOverTime = 0.0f;
        sheepScore = 1;

        maxNumberOfSheep = S_Manager.Sheeps_Length; // 최대 양의 수
        remainNumberOfSheep = S_Manager.Sheep_Count; // 남은 양의 수
        numberOfUsedSheep = maxNumberOfSheep - remainNumberOfSheep; // 사용한 양의 수

        if (remainNumberOfSheep < 0)
            remainNumberOfSheep = 0;

        if (numberOfUsedSheep > maxNumberOfSheep)
            numberOfUsedSheep = maxNumberOfSheep;

        //remainNumberOfSheep = 1;
        //numberOfUsedSheep = 9;

        time = Time; // 시간
        timeScore = Time * 100; // 시간을 점수로 변환

        remainTime.text = time.ToString(); // 남은 시간 텍스트화
        remainSheep.text = maxNumberOfSheep.ToString(); // 남은 양을 최대 양수로 초기화
        
        if (numberOfUsedSheep == maxNumberOfSheep)
            remainSheep.text = "0";

        remainNumberOfSheepScore = remainNumberOfSheep * 100;
        finalScore = remainNumberOfSheepScore + timeScore;

        count.Play(); // 연산 음성 시작

        enabled = true;

    }

    // Update is called once per frame
    void Update() {
        
        switch (next)
        {
            case 0: // 남은 수

                ScoreOverTime(remainNumberOfSheepScore);
                SheepOverTime(numberOfUsedSheep);
                
                if (score == remainNumberOfSheepScore)
                {
                    if (texts[0].text != remainNumberOfSheepScore.ToString())
                        texts[0].text = remainNumberOfSheepScore.ToString();

                    scaleOverTime = 0;
                    score = 0;
                    next++;
                }

                break;
            case 1: // 시간 수

                ScoreOverTime(timeScore, 0);

                if (score == timeScore)
                {
                    if (texts[1].text != timeScore.ToString())
                        texts[1].text = timeScore.ToString();

                    scaleOverTime = 0;
                    score = 0;
                    next++; 
                }

                break;
            case 2: // 별 수

                if(finalScore > score + scaleOverTime) // 현재 계산 중인 점수가 최종 점수보다 낮으면 실행
                {

                    if (!(ScoreOverTime(maximumScore / 3, 0, 3, true)) && starCounter != 3) // 각 별을 받을 수 있는 점수면 실행
                    {
                        Stars[starCounter].SetActive(true); // 별 활성화

                        Destroy(Instantiate(starDust, Stars[starCounter].transform.position, starDust.transform.rotation), 1f);

                        starCounter++;

                        if(starCounter == 3)
                            YAY.Play();

                        return;
                    }


                }
                else // 점수를 초과하여 계산하면
                {

                    if (texts[2].text != finalScore.ToString())
                        texts[2].text = finalScore.ToString();

                    scaleOverTime = 0;
                    score = 0;
                    next++;

                    count.Stop();

                }
                                
                break;

            default:

                StartCoroutine("Wait"); // 0.7초후 최종결과 발표

                C_Manager.Score_End = true;

                enabled = false;

                break;

        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);
        
        StopCoroutine("Wait");
    }


    // 모두 점수를 duration안에 목표 점수까지 올리는 함수
    // 하나의 목표 점수에 도달하면 파티클과 소리 재생
    private void ScoreOverTime(int max, int min = 0, float dividedTime = 1.0f)
    {

        if (scaleOverTime < max)
        {

            lerp = (max - min) / (duration / dividedTime);
            scaleOverTime += (lerp * Time.deltaTime);

            scaleOverTime = (int)scaleOverTime;
            texts[next].text = scaleOverTime.ToString();

            return;

        }
        else
        {

            coin.Play();

            score += max;

            Destroy(Instantiate(starDust, texts[next].transform.position, starBoxDust.transform.rotation), 1f);

            scaleOverTime = (int)scaleOverTime;
            texts[next].text = scaleOverTime.ToString();
            
        }

    }
    
    // 매 목표에 도달시 다른 추가 작업이 필요할때 사용하는 함수
    private bool ScoreOverTime(int max, int min, int dividedTime, bool empty = true)
    {

        if (scaleOverTime < max)
        {
            lerp = (max - min) / (duration / dividedTime);
            scaleOverTime += (lerp * Time.deltaTime);

            texts[next].text = (score + (int)scaleOverTime).ToString();

            return true;
        }
        else
        {

            coin.Play();

            score += max;

            scaleOverTime = 0;

            Destroy(Instantiate(starDust, texts[next].transform.position, starBoxDust.transform.rotation), 1f);

            texts[next].text = (score + (int)scaleOverTime).ToString();

            return false;

        }
    }
    
    private void SheepOverTime(int max, int min = 0)
    {

        if (sheepScaleOverTime < max)
        {

            sheepLerp = (max - min) / duration;
            sheepScaleOverTime += (sheepLerp * Time.deltaTime);
            
            if(sheepScaleOverTime >= sheepScore)
            {

                sheep.Play();

                Destroy(Instantiate(flyingSheep, resultSheep.transform.position, flyingSheep.transform.rotation), 1f);

                remainSheep.text = (maxNumberOfSheep - sheepScore).ToString();
                sheepScore++;
                
            }
        }
    }
}
