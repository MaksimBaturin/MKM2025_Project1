using TMPro;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public string EndText = "";
    void Start()
    {
        GameObject.Find("EndText").GetComponent<TextMeshProUGUI>().text = EndText;
    }

}
