using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour
{
    public List<string> Texts;
    [SerializeField] TextMesh TM0, TM1;
    [SerializeField] Animation Animation;
    int CurrentPage = -1; //-1 is closed. 0 is first page. Last count of Texts is... well.. the last index.
    [SerializeField] bool Open = false;

    void Awake()
    {
        ChangeText(string.Empty,string.Empty);
    }

    public void IncreasePage()
    {
        int LastPage = Texts.Count - 1;

        if (LastPage == -1) return;

        int DesiredPage = CurrentPage + 1 > LastPage ? -1 : CurrentPage + 1;

        string DisplayText0 = Texts[CurrentPage], DisplayText1;
        DisplayText1 = CurrentPage + 1 > LastPage ? string.Empty : Texts[CurrentPage + 1];

        switch (DesiredPage)
        {
            case -1:
                Animation.Play(Open ? "BookClose" : "BookOpen");
                Open = !Open; //Cleanest code ever! I love myself! You should too! YOU SHOULD LOVE YOURSELF, NOW!
            break;

            default:
                Animation.Play("BookFlip");
            break;
        }
        
        ChangeText(DisplayText0,DisplayText1);
    }

    void ChangeText(string DispText0,string DispText1)
    {
        TM0.text = DispText0;
        TM1.text = DispText1;
    }
}
