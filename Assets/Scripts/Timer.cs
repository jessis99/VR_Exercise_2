using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject timerCanvas;
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] TMP_Text scoreText;

    [SerializeField] Image timerImage;

    float timeElapsed = 0f;
    [SerializeField] float timeTotal = 120f;

    void Update()
    {
        if(timeElapsed < timeTotal)
        {
            timeElapsed += Time.deltaTime;
            timerImage.fillAmount = 1 - timeElapsed/timeTotal;
        }
        else
        {
            Points.setTimeIsOver(true);
            timerCanvas.SetActive(false);
            scoreText.text = "Score: " + Points.getScore();
            scoreCanvas.SetActive(true);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
