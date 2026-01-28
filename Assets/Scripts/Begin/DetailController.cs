using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailController : MonoBehaviour
{
    [Header("UI组件")]
    [SerializeField] private GameObject detailPanel;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI skillText;
    [SerializeField] private TextMeshProUGUI specialText;
    
    [Header("面具数据")]
    [SerializeField] private Sprite[] maskIcons = new Sprite[6];
    [SerializeField] private string[] maskNames = new string[6];
    [SerializeField] private string[] maskInfos = new string[6];
    [SerializeField] private string[] maskAttacks = new string[6];
    [SerializeField] private string[] maskSkills = new string[6];
    [SerializeField] private string[] maskSpecials = new string[6];

    [Header("怪物数据")]
    [SerializeField] private Sprite[] monsterIcons = new Sprite[7];
    [SerializeField] private string[] monsterNames = new string[7];
    [SerializeField] private string[] monsterInfos = new string[7];
    [SerializeField] private string[] monsterAttacks = new string[7];
    [SerializeField] private string[] monsterSkills = new string[7];
    [SerializeField] private string[] monsterSpecials = new string[7];

    // 显示第index个面具
    public void ShowMask(int index)
    {
        if (index < 0 || index >= 6) return;

        detailPanel.SetActive(true);

        iconImage.sprite = maskIcons[index];
        nameText.text = maskNames[index];
        infoText.text = maskInfos[index];
        attackText.text = $"攻击: {maskAttacks[index]}";
        skillText.text = $"技能: {maskSkills[index]}";
        specialText.text = $"特殊: {maskSpecials[index]}";
    }

    // 显示第index个怪物
    public void ShowMonster(int index)
    {
        if (index < 0 || index >= 7) return;

        detailPanel.SetActive(true);

        iconImage.sprite = monsterIcons[index];
        nameText.text = monsterNames[index];
        infoText.text = monsterInfos[index];
        attackText.text = $"攻击: {monsterAttacks[index]}";
        skillText.text = $"技能: {monsterSkills[index]}";
        specialText.text = $"特殊: {monsterSpecials[index]}";
    }

    // 初始化默认数据
    void Start()
    {
        InitializeData();
    }

    void InitializeData()
    {
        // 面具默认数据
        for (int i = 0; i < 6; i++)
        {
            if (string.IsNullOrEmpty(maskNames[i]))
                maskNames[i] = $"面具 {i + 1}";
            if (string.IsNullOrEmpty(maskInfos[i]))
                maskInfos[i] = "描述待补充";
            if (string.IsNullOrEmpty(maskAttacks[i]))
                maskAttacks[i] = "攻击信息待补充";
            if (string.IsNullOrEmpty(maskSkills[i]))
                maskSkills[i] = "技能信息待补充";
            if (string.IsNullOrEmpty(maskSpecials[i]))
                maskSpecials[i] = "特殊效果待补充";
        }

        // 怪物默认数据
        for (int i = 0; i < 7; i++)
        {
            if (string.IsNullOrEmpty(monsterNames[i]))
                monsterNames[i] = $"怪物 {i + 1}";
            if (string.IsNullOrEmpty(monsterInfos[i]))
                monsterInfos[i] = "描述待补充";
            if (string.IsNullOrEmpty(monsterAttacks[i]))
                monsterAttacks[i] = "攻击信息待补充";
            if (string.IsNullOrEmpty(monsterSkills[i]))
                monsterSkills[i] = "技能信息待补充";
            if (string.IsNullOrEmpty(monsterSpecials[i]))
                monsterSpecials[i] = "特殊能力待补充";
        }
    }
}