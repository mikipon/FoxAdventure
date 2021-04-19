using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5.0f;

    //Animtion
    private CharacterController characterController;
    private Animator animator;

    [SerializeField]
    private Vector3 velocity;               //移動方向
    [SerializeField] 
    private float applySpeed = 0.2f;       // 回転の適用速度
    [SerializeField]
    private PlayerFollowCamera refCamera;  // カメラの水平回転を参照する用

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        float moveLength = speed * Time.smoothDeltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            pos.z += moveLength;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pos.z -= moveLength;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            pos.x -= moveLength;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            pos.x += moveLength;
        }

        this.transform.position = pos;
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //velocity = velocity.normalized * speed * Time.deltaTime;

        if (velocity.magnitude > 0.1f)
        {
            animator.SetFloat("speed", velocity.magnitude);
            if (velocity.magnitude > 0)
            {

                ////W->S, A->Wのような時にピュッとならないようにしたい
                //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
                //    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
                //    {
                        
                //    }
                //}

                // プレイヤーの回転(transform.rotation)の更新
                // 無回転状態のプレイヤーのZ+方向(後頭部)を、移動の反対方向(-velocity)に回す回転とします
                //transform.rotation = Quaternion.LookRotation(velocity);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(velocity),
                                                      applySpeed);

                // プレイヤーの位置(transform.position)の更新
                // カメラの水平回転(refCamera.hRotation)で回した移動方向(velocity)を足し込みます
                transform.position += refCamera.hRotation * velocity;

            }
            //transform.LookAt(transform.position + velocity);
        }
        else
        {
            animator.SetFloat("speed", 0f);
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;

    }
}
