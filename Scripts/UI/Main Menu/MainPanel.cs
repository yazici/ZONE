using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainPanel : MonoBehaviour
{

    public GameObject[] panels;
    private string currentlyOpenedPanel;

    public void OpenPanel()
    {
        currentlyOpenedPanel = EventSystem.current.currentSelectedGameObject.name;
        foreach (GameObject panel in panels)
        {        
            bool active = panel.name == currentlyOpenedPanel ? true : false;
            panel.SetActive(active);
        }
    }

    public void CancelToMain()
    {
        Utils.FindObject(GameObject.Find("Canvas"), "Main").SetActive(true);
        gameObject.SetActive(false);

    }

    public virtual void Cancel()
    {
        Application.Quit();
    }

}
