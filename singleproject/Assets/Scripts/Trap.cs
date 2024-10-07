using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool isLightTrap;

    private Renderer trapRenderer;
    private Collider2D trapCollider;

    // Start is called before the first frame update
    void Start()
    {
        trapRenderer = GetComponent<Renderer>();
        trapCollider = GetComponent<Collider2D>();
    }

    public void UpdateTrapState(bool isLightOn)
    {
        bool shouldBeActive = isLightTrap ? isLightOn : !isLightOn;

        if(trapRenderer != null)
        {
            trapRenderer.enabled = shouldBeActive;
        }

        if(trapCollider != null)
        {
            trapCollider.enabled = shouldBeActive;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
