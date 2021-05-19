using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


public class LinkOpener : MonoBehaviour, IPointerClickHandler//, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{

    [SerializeField]
    public TextMeshProUGUI textMessage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMessage, eventData.position, eventData.pressEventCamera);
        if (linkIndex == -1)
        {
            Debug.Log("Open link -1");
            return;
        }
        TMP_LinkInfo linkInfo = textMessage.textInfo.linkInfo[linkIndex];
        string selectedLink = linkInfo.GetLinkID();
        Debug.Log("Open link " + selectedLink);
        Application.OpenURL(selectedLink);
    }
}