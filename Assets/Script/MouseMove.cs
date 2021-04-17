using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseMove : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 currentPosition = Vector3.zero;

    public float speed = 3.0f;
    public GameObject playerGO;

    bool onTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
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
                currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                currentPosition.y = 0;

                Debug.DrawRay(ray.origin, ray.direction * 30.0f, Color.red, 0.0f);

            }
            //Debug.Log("x : " + currentPosition.x + "y : " + currentPosition.y + "z : " + currentPosition.z);
            //MovePlayer();
        }

        MovePlayer();

    }

    void MovePlayer()
    {
        //playerGO.transform.Translate(currentPosition.x, currentPosition.y, currentPosition.z);
        //if (currentPosition.z == playerGO.transform.position.z && currentPosition.x == playerGO.transform.position.x)
        //    onTarget = true;
        //else
        //    onTarget = false;

        //Debug.Log(onTarget.ToString());

        //if (onTarget)
        //    playerGO.transform.position += currentPosition * speed;

        var x = currentPosition.x - playerGO.transform.position.x;
        var y = currentPosition.z - playerGO.transform.position.z;
        playerGO.transform.position += new Vector3((x / 2) * speed, 0, (y /2) * speed);

        //else
        //    playerGO.transform.position = currentPosition;

        //Debug.Log("x : " + playerGO.transform.position.x + "y : " + playerGO.transform.position.y + "z : " + playerGO.transform.position.z);
    }

    void OnDrawGizmos()
    {
        if (currentPosition != Vector3.zero)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(currentPosition, 0.5f);
        }
    }
}
