using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[RequireComponent (typeof(Rigidbody), typeof(CapsuleCollider)
    , typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    Rigidbody m_rb;
    PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        Player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        m_rb.DOMoveY(Player.LauchPower, 0.5f);
    }

    public void OffGrvity()
    {
        m_rb.drag = 40;
    }

}
