using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObjectWithOutline : MonoBehaviour
{
    public float floatSpeed = 1f; // Speed of floating motion
    public float floatHeight = 0.5f; // Height of floating motion
    public Color outlineColor = Color.black; // Outline color
    public float outlineSize = 0.1f; // Outline size

    private Vector3 originalPosition;
    private SpriteRenderer spriteRenderer;
    private GameObject outlineObject;
    private SpriteRenderer outlineSpriteRenderer;

    void Start()
    {
        originalPosition = transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        outlineObject = new GameObject("Outline");
        outlineObject.transform.parent = transform;
        outlineObject.transform.localPosition = Vector3.zero;
        outlineSpriteRenderer = outlineObject.AddComponent<SpriteRenderer>();
        outlineSpriteRenderer.sprite = spriteRenderer.sprite;
        outlineSpriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
        outlineSpriteRenderer.color = outlineColor;
        outlineSpriteRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
        outlineSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
        outlineObject.transform.localScale = new Vector3(1 + outlineSize, 1 + outlineSize, 1);
    }

    void Update()
    {
        Float();
        UpdateOutline();
    }

    void Float()
    {
        transform.position = originalPosition + new Vector3(0.0f, Mathf.Sin(Time.time * floatSpeed) * floatHeight, 0.0f);
    }

    void UpdateOutline()
    {
        outlineSpriteRenderer.sprite = spriteRenderer.sprite;
    }
}
