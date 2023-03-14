using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PixelPerfectBlur : MonoBehaviour
{
    [Range(0.0f, 0.015f)]public float m_blurAmount;
    private Image mat;
    void Start()
    {
        // Initialize the function
        SetPixelSize();
    }

    // Corrects the pixel aspect regarding to the screen
    void SetPixelSize()
    {
        mat = GetComponent<Image>();
        float aspect = (float)Screen.width / (float)Screen.height;

        float scaleX = m_blurAmount;
        float scaleY = m_blurAmount;

        if(aspect > 1f)
            scaleX /= aspect;
        else
            scaleY *= aspect;

        mat.material.SetFloat("BlurY", scaleY);
        mat.material.SetFloat("BlurX", scaleX);
    }
}
