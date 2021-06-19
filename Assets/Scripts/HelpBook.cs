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
        if (currentIndex == 0){
            PageSwithchers[0].gameObject.SetActive(false);
        }
        else if(currentIndex == pages.Count - 1){
            PageSwithchers[1].gameObject.SetActive(false);
        }
        else{
            PageSwithchers[0].gameObject.SetActive(true);
            PageSwithchers[1].gameObject.SetActive(true);
        }
    }
}
