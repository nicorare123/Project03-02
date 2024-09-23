using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

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
    void LateUpdate()
    {
        
    }
}
