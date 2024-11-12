using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public int hp = 3;
    public int batteryCount = 0;
    public Image[] batteryUI;
    public Image[] hpUI;

    Rigidbody2D rigid;
    float h;
    float v;
    bool isHorizontal;
    Vector2 moveVec;
    Vector3 dirVec;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector2 InputVec = new Vector2(h, v);

        if(InputVec.magnitude > 1)
        {
            return;
        }

        moveVec.x = InputVec.x;
        moveVec.y = InputVec.y;

        bool hButton = Input.GetButton("Horizontal");
        bool vButton = Input.GetButton("Vertical");
        if(hButton || vButton)
        {
            dirVec = moveVec;
        }
    }

    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + moveVec * Time.deltaTime * speed);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            hp--;
            UpdateHpUI();
            if (hp <= 0)
            {
                Debug.Log("Game Over");
                // 게임 오버 로직 추가
            }
        }
        else if (collision.gameObject.CompareTag("Battery"))
        {
            batteryCount++;
            UpdateBatteryUI();
            Destroy(collision.gameObject);
            if (batteryCount == 4)
            {
                Debug.Log("모든 배터리 수집 완료! 탈출구로 이동하세요.");
            }
        }
        else if (collision.gameObject.CompareTag("Exit") && batteryCount == 4)
        {
            Debug.Log("탈출 성공!");
            // 게임 클리어 로직 추가
        }
    }
    void UpdateHpUI()
    {
        for (int i = 0; i < hpUI.Length; i++)
        {
            hpUI[i].enabled = i < hp;
        }
    }
    void UpdateBatteryUI()
    {
        for (int i = 0; i < batteryUI.Length; i++)
        {
            batteryUI[i].enabled = i < batteryCount;
        }
    }
}
