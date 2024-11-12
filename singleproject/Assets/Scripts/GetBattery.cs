using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Image[] batteryImages;
    private int batteryCount = 0;

    // 배터리 획득 함수
    public void CollectBattery(GameObject battery)
    {
        if (batteryCount < batteryImages.Length)
        {
            batteryImages[batteryCount].gameObject.SetActive(true);
            batteryCount++;
            Destroy(battery);
        }
    }

    // 충돌 감지 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
        {
            CollectBattery(collision.gameObject);
        }
    }
}
