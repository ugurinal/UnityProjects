using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadLine : MonoBehaviour
{
    [SerializeField] private GameObject gui;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private PowerUp powerUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals("Ball")) return;
        powerUp.TriggerSetter(false);
        Time.timeScale = 1;
        PopUpGui();
    }



    private void PopUpGui()
    {
        FindObjectOfType<Ball>().gameObject.SetActive(false);
        gui.SetActive(true);
        gui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "DEFEAT";
        gui.transform.GetChild(2).gameObject.SetActive(false);
        gui.transform.GetChild(3).gameObject.SetActive(true);
        gui.transform.GetChild(4).gameObject.SetActive(false);
        gui.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = score.text;
        gui.transform.GetChild(9).GetComponent<Button>().interactable = false;
    }
}