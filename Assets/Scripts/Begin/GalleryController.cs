using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System.Collections.Generic;

public class GalleryController : MonoBehaviour
{
    [Header("图鉴面板")]
    [SerializeField] private GameObject galleryPanel;      // 图鉴主面板

    [Header("面具相关")]
    [SerializeField] private GameObject maskGalleryPanel;  // 面具图鉴面板
    [SerializeField] private List<Button> maskButtons;     // 6个面具按钮

    [Header("怪物相关")]
    [SerializeField] private GameObject monsterGalleryPanel; // 怪物图鉴面板
    [SerializeField] private List<Button> monsterButtons;   // 7个怪物按钮
    
    [Header("详情面板")]
    [SerializeField] private GameObject detailPanel;        // 详情面板

    void Start()
    {
        // 默认隐藏所有面板
        galleryPanel.SetActive(false);
        maskGalleryPanel.SetActive(false);
        monsterGalleryPanel.SetActive(false);
        detailPanel.SetActive(false);

        // 绑定面具按钮事件
        for (int i = 0; i < maskButtons.Count; i++)
        {
            int index = i;
            maskButtons[i].onClick.AddListener(() => ShowMaskDetail(index));
        }

        // 绑定怪物按钮事件
        for (int i = 0; i < monsterButtons.Count; i++)
        {
            int index = i;
            monsterButtons[i].onClick.AddListener(() => ShowMonsterDetail(index));
        }
    }

    // 打开图鉴
    public void OpenGallery()
    {
        galleryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // 打开面具图鉴
    public void OpenMaskGallery()
    {
        galleryPanel.SetActive(false);
        maskGalleryPanel.SetActive(true);
    }

    // 打开怪物图鉴
    public void OpenMonsterGallery()
    {
        galleryPanel.SetActive(false);
        monsterGalleryPanel.SetActive(true);
    }

    // 显示面具详情
    void ShowMaskDetail(int index)
    {
        maskGalleryPanel.SetActive(false);
        detailPanel.SetActive(true);

        // 获取详情面板组件
        DetailController detail = detailPanel.GetComponent<DetailController>();
        if (detail != null)
        {
            detail.ShowMask(index);
        }
    }

    // 显示怪物详情
    void ShowMonsterDetail(int index)
    {
        monsterGalleryPanel.SetActive(false);
        detailPanel.SetActive(true);

        DetailController detail = detailPanel.GetComponent<DetailController>();
        if (detail != null)
        {
            detail.ShowMonster(index);
        }
    }

    // 返回
    public void GoBack()
    {
        if (detailPanel.activeSelf)
        {
            detailPanel.SetActive(false);

            if (maskGalleryPanel.activeSelf)
                maskGalleryPanel.SetActive(true);
            else if (monsterGalleryPanel.activeSelf)
                monsterGalleryPanel.SetActive(true);
        }
        else if (maskGalleryPanel.activeSelf || monsterGalleryPanel.activeSelf)
        {
            maskGalleryPanel.SetActive(false);
            monsterGalleryPanel.SetActive(false);
            galleryPanel.SetActive(true);
        }
        else if (galleryPanel.activeSelf)
        {
            galleryPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}