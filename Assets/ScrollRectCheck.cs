using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectCheck : MonoBehaviour
{
    private ScrollRect scrollRect;

    RectTransform viewport;
    RectTransform content;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        viewport = scrollRect.viewport;

    }
    void SnapIntoView(RectTransform item)
    {
        Vector3[] itemCorners = new Vector3[4];
        Vector3[] veiewportCorners = new Vector3[4];

        item.GetWorldCorners(itemCorners);
        viewport.GetWorldCorners(veiewportCorners);

        float itemTop = itemCorners[1].y;
        float itemBottom = itemCorners[0].y;

        float viewportTop = veiewportCorners[1].y;
        float viewportBottom = veiewportCorners[0].y;

        float offset = 0f;

        if (itemTop > viewportTop)
        {
            offset = itemTop - viewportTop;
        }
        else if (itemBottom < viewportBottom)
        {
            offset = itemBottom - viewportBottom;
        }

        if (Mathf.Abs(offset) > 0.01f)
        {
            Vector2 newPos = content.anchoredPosition;
            newPos.y -= offset;
            content.anchoredPosition = newPos;
        }
    }
    // Update is called once per frame
    void Update()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        if (!selected || selected.transform.parent != content.transform)
            return;
        RectTransform item = selected.GetComponent<RectTransform>();
        SnapIntoView(item);
    
    }
}
