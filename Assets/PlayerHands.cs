using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerHands : MonoBehaviour
{
    Item Equipped, Holding;
    //Equipped one is you're currently using.
    //Holding one is you have to hold Fire1.

    [SerializeField] Transform RightHand, LeftHand, Head, LeftArm, RightArm;
    [SerializeField] PlayerInput PInput;

    int RStatus; //0 is idle, 1 is picking up, 2 is holding

    void FixedUpdate()
    {
        RaycastHit hit = new RaycastHit(); Useable HitUse = null; Item HitItem = null;
        bool DidHit = Physics.Raycast(Head.position, Head.forward, out hit, 2f);
        bool HitUseable = DidHit && hit.transform.TryGetComponent(out HitUse);
        bool HitAItem = DidHit && hit.transform.TryGetComponent(out HitItem);

        if (Equipped != null)
        {
            //DO NOT CHANGE BELOW LINE. IF THE ITEM IS LOOKING AT NON-DESIRED ROTATION OPEN BLENDER AND FUCKING FIX IT. FUCK YOU. DO WHAT I SAY.
            Equipped.transform.SetPositionAndRotation(RightHand.position, RightHand.rotation);
            Equipped.RigidBody_.velocity = Vector3.zero;
            Equipped.RigidBody_.angularVelocity = Vector3.zero;
        }

        switch (RStatus)
        {
            default: //Not equipped with a item
                if (PInput.PrimaryHandUse)
                {
                    if (HitAItem)
                        StartCoroutine(PickupHandle(HitItem));
                    else if (HitUseable)
                        HitUse.Use_();
                }
                else if (PInput.SecondaryHandUse)
                {
                    if (HitAItem && HitUseable) HitUse.Use_();
                }
            break;

            case 2: //Equipped with a item
                if (!HitUseable && PInput.InteractiveUse) StartCoroutine(DropHandle());
                if (PInput.PrimaryHandUse) Equipped.PrimaryUse();
                if (PInput.SecondaryHandUse) Equipped.SecondaryUse();
            break;
        }
    }

    IEnumerator DropHandle()
    {
        float TimePassed = 0, RequiredTime = 1f;
        while (PInput.InteractiveUse && TimePassed < RequiredTime)
        {
            TimePassed += Time.deltaTime;
            if (TimePassed >= RequiredTime)
            {
                Equipped = null;
                RStatus = 0;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator PickupHandle(Item I)
    {
        RStatus = 1;
        float TimePassed = 0, RequiredTime = I.PickupDelay;
        while (PInput.PrimaryHandUse && TimePassed < RequiredTime)
        {
            TimePassed += Time.deltaTime;
            if (TimePassed >= RequiredTime)
            {
                Equipped = I;
                RStatus = 2;
            }
            yield return new WaitForEndOfFrame();
        }

        if (Equipped == null) RStatus = 0;
    }
}
