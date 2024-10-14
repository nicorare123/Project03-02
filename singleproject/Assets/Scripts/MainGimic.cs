using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGimic : MonoBehaviour
{
    public Image darkness;
    public Camera mainCamera;
    public Camera additionalCamera;
    public Transform player;
    private GameObject[] traps;

    private float lightFadeTime = 0.5f;   
    private bool isLightOn = true;
    private bool isFading = false;
    // Start is called before the first frame update
    void Start()
    {
        traps = GameObject.FindGameObjectsWithTag("Trap");
        SetOverlayAlpha(0f);
        UpdateTraps();

        additionalCamera.enabled = false;
        additionalCamera.cullingMask = (1 << LayerMask.NameToLayer("PlayerNLights")) | (1 << LayerMask.NameToLayer("DarkTrap"));
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
        StartCoroutine(FadeToDarkness(isLightOn ? 0f : 1f));
        UpdateTraps();

        additionalCamera.enabled = !isLightOn;
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
        foreach (GameObject trap in traps)
        {
            Trap trapScript = trap.GetComponent<Trap>();

            if(trapScript != null)
            {
                trapScript.UpdateTrapState(isLightOn);
            }
        }
    }
    public bool IsLightOn()
    {
        return isLightOn;
    }
}
