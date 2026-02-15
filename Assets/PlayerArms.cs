using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArms : MonoBehaviour
{
    [SerializeField] Transform LeftArm,RightArm,Head;
    PlayerHands PHands;
    CharacterController CC;

    void Start()
    {
        PHands = GetComponent<PlayerHands>();
        CC = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        float Vel = CC.velocity.magnitude;
        Transform MainArm = PHands.Lefty ? LeftArm : RightArm;
        Vector3 Direction = -transform.up;
        if (PHands.IsHandFull)
        {
            Direction = Head.forward;
        }
        else if (Vel > 1)
        {
            Direction = -CC.velocity.normalized;
        }

        Vector3 Lerp = Vector3.Lerp(MainArm.forward,Direction,0.1f);
        MainArm.forward = Lerp;
    }

    /*
        FixedUpdate()
            -Oyuncunun eli boş mu?
                Evet:
                    -Oyuncu hareket ediyor mu?
                        Evet:
                            Oyuncunun kolu velocity'nin tersine doğru baksın.
                        Hayır:
                            Oyuncunun kolu yere baksın.
                Hayır:
                    Oyuncunun kolu kafanın rotasyonunda olsun
    */
}
