using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseMove : MonoBehaviour
{
    //メインカメラ
    private Camera mainCamera;
    //クリックした場所を保管
    private Vector3 targetPosition = Vector3.zero;//初期化
    //ベクトル計算用
    float x, y;

    //player
    public float speed = 3.0f;//スピード
    public GameObject playerGO;
    
    //地面をクリックをしたか
    //bool onClick = false;
    //bool onTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        //Debug.Log("最初　x : " + playerGO.transform.position.x + "y : " + playerGO.transform.position.y + "z : " + playerGO.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //原点
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //レイキャスト、ヒットした全てのオブジェクト情報を取得
            var raycasthitList = Physics.RaycastAll(ray).ToList();
            if (raycasthitList.Any())
            {
                //カメラとの距離を計測
                var distance = Vector3.Distance(mainCamera.transform.position, raycasthitList.First().point);
                Debug.Log(raycasthitList.First().point);
                //マウスの場所
                var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

                //3Dの座標にする（Unity凄い）
                targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                targetPosition.y = 0;

                Debug.DrawRay(ray.origin, ray.direction * 30.0f, Color.red, 0.0f);

            }
            //Debug.Log("x : " + targetPosition.x + "y : " + targetPosition.y + "z : " + targetPosition.z);
            //MovePlayer();
            //onClick = true;
        }
        else
        {
            //onClick = false;
        }

        //Debug.Log("Move前　x : " + playerGO.transform.position.x + "y : " + playerGO.transform.position.y + "z : " + playerGO.transform.position.z);

        MovePlayer();

    }

    void MovePlayer()
    {
        //playerGO.transform.Translate(targetPosition.x, targetPosition.y, targetPosition.z);
        //if (targetPosition.z == playerGO.transform.position.z && targetPosition.x == playerGO.transform.position.x)
        //    onTarget = true;
        //else
        //    onTarget = false;

        //Debug.Log(onTarget.ToString());

        //if (onTarget)
        //    playerGO.transform.position += targetPosition * speed;

        //移動するためのベクトル
        //if (onClick)
        //{
        //    x = targetPosition.x - playerGO.transform.position.x;
        //    y = targetPosition.z - playerGO.transform.position.z;
        //}
        x = targetPosition.x - playerGO.transform.position.x;
        y = targetPosition.z - playerGO.transform.position.z;
        Debug.Log("Move前　x : " + playerGO.transform.position.x + "y : " + playerGO.transform.position.y + "z : " + playerGO.transform.position.z);
        playerGO.transform.position += new Vector3((x / 2) * speed, 0, (y /2) * speed);

        //else
        //    playerGO.transform.position = targetPosition;

        //Debug.Log("x : " + playerGO.transform.position.x + "y : " + playerGO.transform.position.y + "z : " + playerGO.transform.position.z);
    }

    void OnDrawGizmos()
    {
        if (targetPosition != Vector3.zero)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(targetPosition, 0.5f);
        }
    }
}
