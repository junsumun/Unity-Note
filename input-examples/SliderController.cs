using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform SliderBackground;

    public RectTransform Slider;

    [Range(0, 2f)]
    public float SliderLimit = 1f;

    public float Input { get { return input.x; } }

    private Vector2 sliderPosition = Vector2.zero;

    private Vector2 input = Vector2.zero;


    public void OnPointerDown(PointerEventData eventData)
    {
        SliderBackground.gameObject.SetActive(true);
        OnDrag(eventData);
        sliderPosition = eventData.position;
        SliderBackground.position = eventData.position;
        Slider.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // eventData.position == usual touched location
        Vector2 sliderDirection = eventData.position - sliderPosition;

        // sliderDirection.magnitude > SliderBackground.sizeDelta.x / 2f
        // This condition check whether user has moved the slider farther than half the width of SliderBackground
        // if not, assign the result of dividing the sliderDirection vector divide by half the width of SliderBackground to the input variable
        input = (sliderDirection.magnitude > SliderBackground.sizeDelta.x / 2f) ? sliderDirection.normalized : sliderDirection / (SliderBackground.sizeDelta.x / 2f);

        // Disregard the y value, as the slider only moves horizontally
        input = new Vector2(input.x, 0f);

        Slider.anchoredPosition = (input * SliderBackground.sizeDelta.x / 2f) * SliderLimit;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SliderBackground.gameObject.SetActive(false);
        input = Vector2.zero;
        Slider.anchoredPosition = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Input: " + Input);
    }
}
