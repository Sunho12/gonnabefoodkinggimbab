using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pasta_withlaser : MonoBehaviour
{
    public Slider healthSlider;
    public AudioSource laserSound;
    public GameObject Blackout;

    // Start is called before the first frame update


    void Start()
    {
        // 처음 healthSlider의 value를 0.2로 설정
       

        Invoke("StartLate", 0.5f);


        
    }

    void StartLate()
    {
        StartCoroutine(HealthBarDown1());

    }




    IEnumerator HealthBarDown()
    {
        while (healthSlider.value > 0)  // healthSlider가 0이 될 때까지 반복
        {
            laserSound.Play();
            healthSlider.value -= 0.02f;  // healthSlider 값을 감소시킴

            

            yield return new WaitForSeconds(1f);  // 0.05초 대기
        }

   
    }


    IEnumerator HealthBarDown1()
    {
        while (healthSlider.value > 0)
        {
            float targetValue = healthSlider.value - 0.025f;  // 목표 값을 설정
            if (targetValue < 0) targetValue = 0;  // 목표 값이 0 이하로 내려가지 않도록 설정

            float duration = 0.5f;  // 감소에 걸릴 시간 (초)
            float elapsed = 0f;  // 경과 시간 초기화

            float startValue = healthSlider.value;  // 시작 값 설정

            laserSound.Play();

            // 슬라이더 값을 점진적으로 감소시킴
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;  // 경과 시간 업데이트
                healthSlider.value = Mathf.Lerp(startValue, targetValue, elapsed / duration);  // 값 점진적 감소
                yield return null;  // 프레임을 기다림
            }

            healthSlider.value = targetValue;  // 최종적으로 정확히 목표 값에 도달

           
            yield return new WaitForSeconds(0.05f);

            if (healthSlider.value <= 0)
            {
                break; // 코루틴 종료
            }
        }

        Blackout.SetActive(true);
        yield return new WaitForSeconds(4f);
        SetScene();
    }


    void SetScene()
    {
        SceneManager.LoadScene("PA4");
    }
}
    
