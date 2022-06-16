using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollView_EventTriger : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //處理ScrollView與EventTriger衝突

    public ScrollRect Scroll;

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        Scroll.OnDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Scroll.OnBeginDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Scroll.OnEndDrag(eventData);
    }
}
