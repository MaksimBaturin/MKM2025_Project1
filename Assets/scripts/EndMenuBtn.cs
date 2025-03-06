using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuBtn : MonoBehaviour
{

    public void OnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
