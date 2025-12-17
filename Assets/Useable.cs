using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Useable : MonoBehaviour
{
    [SerializeField] UnityEvent Use;
    [SerializeField] UnityEvent AlternateUse;
    [SerializeField] float RequiredUseDelay = 0, RequiredAlternativeUseDelay = 0, DelayAfterUse = 0, AlternativeDelayAfterUse = 0;
    bool CanUse = true, CanAlternateUse = true, IsTrying, IsAlternativeTrying;
    bool UseFrameCheck = false, UseAltFrameCheck = false;

    [SerializeField]
    bool Debug = true;

    public void Use_()
    {
        if (CanUse)
        {
            if (RequiredUseDelay == 0)
            {
                Use.Invoke();
                if (DelayAfterUse > 0) StartCoroutine(Use_DelayHandle());
            }
            else
            {
                if (!IsTrying)
                    StartCoroutine(UseHoldHandle());
                else
                    UseFrameCheck = true;
            }
        }
    }

    public void Use_Alternative()
    {
        if (CanAlternateUse)
        {
            if (RequiredUseDelay == 0)
            {
                AlternateUse.Invoke();
                if (AlternativeDelayAfterUse > 0) StartCoroutine(Use_AlternativeDelayHandle());
            }
            else
            {
                if (!IsAlternativeTrying)
                    StartCoroutine(UseAltHoldHandle());
                else
                    UseAltFrameCheck = true;
            }
        }
    }

    IEnumerator Use_AlternativeDelayHandle()
    {
        float TimePassed = 0f, RequiredTime = AlternativeDelayAfterUse;

        CanAlternateUse = false;
        while (TimePassed < RequiredTime)
        {
            if (Debug)
            print("Alternative Delay:" + TimePassed + "/" + RequiredTime);
            TimePassed += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        CanAlternateUse = true;
    }

    IEnumerator Use_DelayHandle()
    {
        float TimePassed = 0f, RequiredTime = DelayAfterUse;

        CanUse = false;
        while (TimePassed < RequiredTime)
        {
            if (Debug)
            print("Primary Delay:" + TimePassed + "/" + RequiredTime);
            TimePassed += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        CanUse = true;
    }

    IEnumerator UseHoldHandle()
    {
        IsTrying = true;
        UseFrameCheck = true;
        float Holding = 0, RequiredTime = RequiredUseDelay;

        while (Holding < RequiredTime)
        {
            if (Debug) print("Primary: " + Holding + "/" + RequiredUseDelay);
            if (UseFrameCheck) { Holding += Time.fixedDeltaTime; UseFrameCheck = false; }
            else Holding -= Time.fixedDeltaTime;

            if (Holding > RequiredTime)
            {
                if (Debug) print("Use Success");
                Use.Invoke();
                if (DelayAfterUse > 0) StartCoroutine(Use_DelayHandle());
                break;
            }
            else if (Holding <= 0)
            {
                if (Debug) print("Use Fail");
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        IsTrying = false;
    }

    IEnumerator UseAltHoldHandle()
    {
        IsAlternativeTrying = true;
        UseAltFrameCheck = true;
        float Holding = 0, RequiredTime = RequiredAlternativeUseDelay;

        while (Holding < RequiredTime)
        {
            if (Debug) print("Alternative: " + Holding + "/" + RequiredAlternativeUseDelay);
            if (UseAltFrameCheck) { Holding += 0.01f; UseAltFrameCheck = false; }
            else Holding -= 0.01f;

            if (Holding > RequiredTime)
            {
                if (Debug) print("Alternate Success");
                AlternateUse.Invoke();
                if (AlternativeDelayAfterUse > 0) StartCoroutine(Use_AlternativeDelayHandle());
                break;
            }
            else if (Holding <= 0)
            {
                if (Debug) print("Alternate Fail");
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        IsAlternativeTrying = false;
    }
}
