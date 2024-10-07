using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGimic : MonoBehaviour
{
    public Image darkness;
    private float lightFadeTime = 0.5f;
    private GameObject[] traps;
    private bool isLightOn = true;
    private bool isFading = false;


    // Start is called before the first frame update
    void Start()
    {
        traps = GameObject.FindGameObjectsWithTag("Trap");
        SetOverlayAlpha(0f);
        UpdateTraps();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ToggleLight();
        }
    }

    void ToggleLight()
    {
        isLightOn = !isLightOn;
        StartCoroutine(FadeToDarkness(isLightOn ? 0f : 0.98f));
        UpdateTraps();
    }

    IEnumerator FadeToDarkness(float targetAlpha)
    {
        isFading = true;
        float startAlpha = darkness.color.a;
        float time = 0;

        while (time < lightFadeTime)
        {
            time += Time.deltaTime;
            float t = time / lightFadeTime;

            SetOverlayAlpha(Mathf.Lerp(startAlpha, targetAlpha, t));

            yield return null;
        }

        SetOverlayAlpha(targetAlpha);
        isFading = false;
    }

    void SetOverlayAlpha(float alpha)
    {
        Color color = darkness.color;
        color.a = alpha;
        darkness.color = color;
    }

    void UpdateTraps()
    {
        foreach(GameObject trap in traps)
        {
            Trap trapScript = trap.GetComponent<Trap>();

            if(trapScript != null)
            {
                trapScript.UpdateTrapState(isLightOn);
            }
        }
    }
}
