using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public void openSite()
    {
        Debug.Log("TEST");
        Application.OpenURL("http://ugurinal.com/");
    }

    public void openFB()
    {
        Application.OpenURL("https://www.facebook.com/ugurinal01");
    }

    public void openTwitter()
    {
        Application.OpenURL("https://twitter.com/ugurinal01");
    }

    public void openLinkedIn()
    {
        Application.OpenURL("https://www.linkedin.com/in/ugurinall/");
    }
}
