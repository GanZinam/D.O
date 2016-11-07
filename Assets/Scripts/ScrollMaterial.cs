using UnityEngine;
using System.Collections;

public class ScrollMaterial : MonoBehaviour
{
    public float scrollSpeed = 0.1F;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
