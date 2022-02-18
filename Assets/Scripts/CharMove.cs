using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
	Animator animator;
	CharacterController cc;

	Vector3 dir = Vector3.zero;//jump用の変数
	public float gravity = 20.0f;//inspectorから調整できるようにpublic にしておく(初期値)
	public float speed = 4.0f;
	public float rotSpeed = 300.0f;
	public float jumpPower = 8.0f;

	void Start()
	{
		animator = GetComponent<Animator>();
		cc = GetComponent<CharacterController>();
	}


	void Update()
	{
		//前進成分を取得(0~1),今回はバックはしない mathmax は２つの引数の大きい方を返す。後ろには戻さない0から１のみ入る
		float acc = Mathf.Max(Input.GetAxis("Vertical"), 0f);
		//接地していたら cc.isGroundedで着地してるときtrueを返す
		if (cc.isGrounded)
		{
			//左右キーで回転　左で-1 右で+1となる。
			float rot = Input.GetAxis("Horizontal");
			//前進、回転が入力されていた場合大きい方の値をspeedにセットする(転回のみをするときも動くモーションをする)動かないと不自然
			animator.SetFloat("speed", Mathf.Max(acc, Mathf.Abs(rot)));
			//回転は直接トランスフォームをいじる rotSpeedは300 rotateで左右にrotate
			transform.Rotate(0, rot * rotSpeed * Time.deltaTime, 0);

			if (Input.GetButtonDown("Jump"))
			{
				//ジャンプモーション開始 triggerは一回trueであとはfalseになってくれる。
				animator.SetTrigger("jump");
			}
		}
		//下方向の重力成分 unityちゃんがジャンプして落ちる
		dir.y -= gravity * Time.deltaTime;

		//CharacterControllerはMoveでキャラを移動させる。向いている向きに動かす
		cc.Move((transform.forward * acc * speed + dir) * Time.deltaTime);
		//移動した後着していたらy成分を0にする。
		if (cc.isGrounded)
		{
			dir.y = 0;
		}

	}

}


