using UnityEngine;
using UnityEngine.UI;

public class TitleDrop : MonoBehaviour
{
    public float targetY = 267f;
    public float startY = 800f;
    public float fallSpeed = 300f;

    private RectTransform rt;
    private bool shouldMove = false;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, startY);
    }

    void Start()
    {
        Invoke(nameof(BeginMove), 0.05f);
    }

    void BeginMove()
    {
        shouldMove = true;
    }

    void Update()
    {
        if (!shouldMove) return;

        float currentY = rt.anchoredPosition.y;

        if (currentY > targetY)
        {
            float newY = currentY - fallSpeed * Time.unscaledDeltaTime;

           
            if (newY <= targetY)
            {
                newY = targetY;
                shouldMove = false;  
            }

            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, newY);
        }
        else
        {
            
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, targetY);
            shouldMove = false;
        }
    }
}