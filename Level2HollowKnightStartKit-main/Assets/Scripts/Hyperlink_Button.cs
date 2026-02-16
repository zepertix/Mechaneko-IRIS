using UnityEngine;

public class OpenLinkButton : MonoBehaviour
{
    [Tooltip("URL to open when the button is clicked")]
    public string url = "https://example.com";

    // This function can be linked directly to the button
    public void OpenURL()
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
            Debug.Log("Opening URL: " + url);
        }
        else
        {
            Debug.LogWarning("URL is empty!");
        }
    }
}
