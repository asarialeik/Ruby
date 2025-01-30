using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerDialogue;
    private Label m_NonPlayerDialogueText;
    private bool talkedToNpc = false;
    private bool NPCGotTired = false;
    private float m_TimerDisplay;
    private bool allEnemiesFixed = false;
    private VisualElement m_Healthbar;
    private VisualElement m_EnemyCount;
    private Label m_EnemyCountText;

    private void Awake()
    {
        instance = this;
        UIDocument uiDocument = GetComponent<UIDocument>();
        
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);

        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
        m_NonPlayerDialogueText = m_NonPlayerDialogue.Q<Label>("Text");
        m_EnemyCount = uiDocument.rootVisualElement.Q<VisualElement>("EnemyCount");
        m_EnemyCountText = m_EnemyCount.Q<Label>("EnemiesText");
        m_EnemyCount.style.display = DisplayStyle.None;
    }

    void Start()
    {
        
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
        m_EnemyCount.style.display = DisplayStyle.Flex;
        
        if (NPCGotTired == true)
        {
            m_NonPlayerDialogueText.text = "...";
        }
        else if (talkedToNpc == true)
        {
            m_NonPlayerDialogueText.text = "Come now, go fix those walkig cans, sweetcheeks.";
            NPCGotTired = true;
        }
        else
        {
            talkedToNpc = true;
        }
   }
   public void EnemyAmountDisplay(int enemiesAmount)
   {
        m_EnemyCountText.text = "Broken Robots: " + enemiesAmount.ToString("00");
   }
}
