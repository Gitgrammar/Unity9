using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;//unityちゃんをターゲット

    private void LateUpdate()
    {
        transform.position = player.position + (-player.forward * 3.0f) + (player.up * 1.0f);//player.position は最初の場所
        transform.LookAt(player.position + Vector3.up);
    }

}
