using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject EndGameUI;
    [SerializeField] private float delayBeforeShowingMenu = 1f;

    private Rocket rocket;
    private bool IsMenuShow = false;

    private void Start()
    {
        if (rocket == null)
        {
            rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();
        }
    }
    void Update()
    {
      
        if (rocket.IsDeath && !IsMenuShow)
        {
            IsMenuShow = true;
            StartCoroutine(ShowEndGameMenu("Вы проиграли!"));
        }
        else if (!rocket.IsDeath && rocket.IsWin && rocket.IsOnFloor && !IsMenuShow)
        {
            IsMenuShow = true;
            StartCoroutine(ShowEndGameMenu("Вы выиграли!"));
        }
    }

    private System.Collections.IEnumerator ShowEndGameMenu(string endText)
    {
        yield return new WaitForSeconds(delayBeforeShowingMenu);

        EndGameUI.GetComponent<EndMenu>().EndText = endText;
        Instantiate(EndGameUI);

        GameObject.Find("RocketSprite").GetComponent<SpriteRenderer>().enabled = false;
        rocket.enabled = false;
    }
}