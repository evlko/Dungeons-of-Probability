using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpBook : MonoBehaviour
{
    public List<Image> pages = new List<Image>();
    int currentIndex = 0;
    public Transform[] PageSwithchers;

    public void OpenPageByRelativeIndex(int relevantIndex){
        currentIndex += relevantIndex;
        OpenCurrentPage();
    }

    public void OpenPageByIndex(int globalIndex){
        currentIndex = globalIndex;
        OpenCurrentPage();
    }

    void OpenCurrentPage(){
        for (int i = 0; i < pages.Count; i++){
            pages[i].gameObject.SetActive(false);
        }
        pages[currentIndex].gameObject.SetActive(true);
        SetPageSwichers();
    }

    void SetPageSwichers(){
        bool PreviousPageStatus = currentIndex == 0 ? false : true;
        bool NextPageStatus = currentIndex == (pages.Count - 1) ? false : true;
        PageSwithchers[0].gameObject.SetActive(PreviousPageStatus);
        PageSwithchers[1].gameObject.SetActive(NextPageStatus);
    }
}
