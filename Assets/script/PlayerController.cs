using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using UnityChan;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_isGroundedLength = 0.1f;

    [SerializeField] float m_movingSpeed = 5f;

    [SerializeField] float m_turnSpeed = 3f;

    [SerializeField] float m_jumpPower = 5f;

    [SerializeField] public float LauchPower = 7f;

    Rigidbody m_rb;

    Animator m_anim;

    EnemyController Enemy;

    
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
        dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
        dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする

        if (IsGrounded())
        {
            if (dir == Vector3.zero)
            {
                // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
                m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
            }
            else
            {
                // 入力方向に滑らかに回転させる
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * m_turnSpeed);  // Slerp を使うのがポイント

                Vector3 velo = dir.normalized * m_movingSpeed; // 入力した方向に移動する
                velo.y = m_rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
                m_rb.velocity = velo;   // 計算した速度ベクトルをセットする
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        //通常攻撃
        if (Input.GetButtonDown("Fire1"))
        {
            if (m_anim)
            {
                m_anim.SetTrigger("Attack");
            }
           
        }

        if (Input.GetButtonDown("Skill"))
        {
            if (m_anim)
            {
                m_anim.SetTrigger("Lauch");
            }
            Debug.Log("打ち上げ攻撃");
        }

    }

   


    void Jump()
    {
        //AudioSource.PlayClipAtPoint(m_jumpSfx, this.transform.position);
        m_rb.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);
        if (m_anim)
        {
            m_anim.SetTrigger("Jump");
        }

    }


    bool IsGrounded()
    {
        // Physics.Linecast() を使って足元から線を張り、そこに何かが衝突していたら true とする
        Vector3 start = this.transform.position;   // start: オブジェクトの中心
        Vector3 end = start + Vector3.down * m_isGroundedLength;  // end: start から真下の地点
        Debug.DrawLine(start, end); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics.Linecast(start, end); // 引いたラインに何かがぶつかっていたら true とする
        return isGrounded;
    }


    void Lauch()
    {
        m_rb.DOMoveY(LauchPower, 0.5f);
        if (m_anim)
        {
            m_anim.SetTrigger("Lauch");
        }
        //Collider[] hitEnemy = Physics.OverlapSphere
    }

    //void UpdatePosition() 
    //{
    //    var 
    //}


    void LateUpdate()
    {
        if (m_anim)
        {
            if (IsGrounded())
            {
                Vector3 velo = m_rb.velocity;
                velo.y = 0;
                m_anim.SetFloat("Speed", velo.magnitude);
            }
            else
            {
                m_anim.SetFloat("Speed", 0f);
            }
        }
    }
}
