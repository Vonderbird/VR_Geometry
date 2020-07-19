using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Panels currentPanel = null;
    private List<Panels> panelHistory = new List<Panels>();

    private void Start()
    {
        SetupPanels();
    }

    private void SetupPanels()
    {
        Panels[] panels = GetComponentsInChildren<Panels>();

        foreach (Panels panel in panels)
        {
            panel.Setup(this);
        }

        currentPanel.Show();

    }

    //Should go to previous, still incomplete
    private void Update()
    {
        
    }

    public void GoToPrevious()
    {
        if (panelHistory.Count == 0)
        {
            //Add menus that you don't want to include in back button, still incomplete
            return;
        }

        int lastIndex = panelHistory.Count - 1;
        SetCurrent(panelHistory[lastIndex]);
        panelHistory.RemoveAt(lastIndex);
    }

    public void SetCurrentWithHistory(Panels newPanel)
    {
        panelHistory.Add(currentPanel);
        SetCurrent(newPanel);
    }

    private void SetCurrent(Panels newPanel)
    {
        currentPanel.Hide();
        currentPanel = newPanel;
        currentPanel.Show();
    }
    
    

}
