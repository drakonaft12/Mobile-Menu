using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller me;

    [SerializeField]private PageBase[] allPage;
    [SerializeField] private PageBase activePage;
    private void Start()
    {
        if(me == null)
            me = this;
        activePage = FindActivePage();
    }

    public PageBase FindActivePage()
    {
        if (activePage == null)
        {
            foreach (var item in allPage)
            {
                if (item.isActiveAndEnabled) return item;
            }
            return null;
        }
        else return activePage;
    }

    public void SetPage(PageBase page)
    {
        activePage?.CloseScreen();
        page.StartScreen();
        activePage = page;
    }

}
