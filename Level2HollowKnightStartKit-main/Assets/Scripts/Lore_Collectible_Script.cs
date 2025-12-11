using UnityEngine;

public class Lore_Collectible_Script : MonoBehaviour
{
    public GameObject textObject;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(textObject != null)
        {
            textObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D  collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            textObject.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            textObject.SetActive(false);
        }
    }
}
