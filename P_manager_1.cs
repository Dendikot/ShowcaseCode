using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.XR;
using UnityEditor.PackageManager;
using UnityEngine;

public class P_manager_1 : MonoBehaviour
{
    /// <summary>
    /// Pages holder
    /// </summary>
    [SerializeField]
    private GameObject[] m_Pages;

    /// <summary>
    /// Page to be turned off before the next one is activated
    /// </summary>
    private GameObject m_PreviousPage = null;

    /// <summary>
    /// Starting page index
    /// </summary>
    [Range(0, 8)]
    [SerializeField]
    private int m_startMenuInd;

    /// <summary>
    /// Referece to the additional graphic that should be turned off in case of overlay menu page
    /// </summary>
    [SerializeField]
    private GameObject[] m_AddPanels;

    /// <summary>
    /// Bool to determine whether it is nessesary to deactivate additional graphic panels
    /// </summary>
    private bool m_DeactivatePanels = false;

    /// <summary>
    /// Deactivating all the active pages before the start
    /// </summary>
    private void Awake()
    {
        for (int nInd = 0; nInd < m_Pages.Length; nInd++)
        {
            m_Pages[nInd].SetActive(false);
        }
    }

    /// <summary>
    /// Activating the starting page
    /// </summary>
    public void Start()
    {
        ActivateMenu(m_startMenuInd);
    }

    /// <summary>
    /// Set additional panels deactivation to true
    /// </summary>
    public void DeactivatePanels()
    {
        m_DeactivatePanels = true;
        // P.S. due to the limit of serilising only methods with one parameter in untiy 
        // It has to be separated in a solid accessible method
    }

    /// <summary>
    /// Activate the menu page
    /// </summary>
    /// <param name="index">Index of the page to be activated</param>
    public void ActivateMenu(int index)
    {
        if (m_DeactivatePanels)
        {
            for (int nInd = 0; nInd < m_AddPanels.Length; nInd++)
            {
                m_AddPanels[nInd]?.SetActive(false);
            }
            m_DeactivatePanels = false;
        }
        /// activate additional panels back, additional case in order to prevent unnessesary looping
        else if (!m_DeactivatePanels && !m_AddPanels[0].activeSelf)
        {
            for (int nInd = 0; nInd < m_AddPanels.Length; nInd++)
            {
                m_AddPanels[nInd].SetActive(true);
            }
        }

        m_PreviousPage?.SetActive(false);

        m_Pages[index].SetActive(true);

        m_PreviousPage = m_Pages[index];
    }
}
