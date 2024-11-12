using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Image[] batteryImages;
    private int batteryCount = 0;

    // ���͸� ȹ�� �Լ�
    public void CollectBattery(GameObject battery)
    {
        if (batteryCount < batteryImages.Length)
        {
            batteryImages[batteryCount].gameObject.SetActive(true);
            batteryCount++;
            Destroy(battery);
        }
    }

    // �浹 ���� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
        {
            CollectBattery(collision.gameObject);
        }
    }
}
