using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerDialogue;
    private Label m_NonPlayerDialogueText;
    private bool talkedToNpc = false;
    private float m_TimerDisplay;
    public static UIHandler instance { get; private set; }
    private VisualElement m_Healthbar;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);
        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
        m_NonPlayerDialogueText = m_NonPlayerDialogue.Q<Label>("Text");
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }

    void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    public void DisplayDialogue()
   {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
        if (talkedToNpc == true)
        {
            m_NonPlayerDialogueText.text = "Come now, go fix those walkig cans, sweetcheeks.";
        }
        else
        {
            talkedToNpc = true;
        }
   }
}
