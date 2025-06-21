using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Paginator : MonoBehaviour
{
    [Header("设定")]
    public int onePageCount = 9;
    [Header("组件")]
    public TMP_InputField pageInputField;
    public Button prevBtn;
    public Button nextBtn;
    public Transform content;
    public TMP_Text totalCountText;
    public TMP_Text totalPageTextBox;
    [Header("预制件或模板")]
    public GameObject ellipsisItem;
    public GameObject pageItem;
    [Header("变量")]
    public int currentPage = 1;
    public int totalCount = 5;

    [ContextMenu("UpdateTest")]
    public void UpdateTest()
    {
        UpdateDataList(totalCount * onePageCount);
    }


    [Header("UI 元素引用")]
    public Transform contentArea; // 存放当前页内容的容器
    public Image contentImage;   // 图片
    public TMP_Text contentName; // 名字文本
    public TMP_Text contentDesc; // 介绍文本

    [Header("页面数据")]
    public PageData[] pageContents; // 存储每页的数据

    [System.Serializable]
    public class PageData
    {
        public Sprite image;  // 当前页的图片
        public string name;   // 当前页的名字
        public string description; // 当前页的介绍
    }

    private void Start()
    {
        UpdateTest();
        UpdatePageContent(currentPage);
        pageInputField.onEndEdit.AddListener((string text) =>
        {
            SwitchPage(int.Parse(text), true);
            pageInputField.text = string.Empty;
        });

        prevBtn.onClick.AddListener(() =>
        {
            if (currentPage > 1)
            {
                currentPage--;
                if (totalCount <= 7)
                {
                    content.GetChild(currentPage - 1).GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(true);
                }
                else
                {
                    if (currentPage == 4)
                    {
                        SetMiddlePage(currentPage);

                        Destroy(content.GetChild(1).gameObject);
                        CreatePageToggle(1, 2);
                        CreateEllLast();
                    }
                    if (currentPage == totalCount - 4)
                    {
                        CreateEllLast();
                        SetMiddlePage(currentPage);
                        CreateEllFirst();
                    }
                    else if (currentPage > totalCount - 4)
                    {
                        content.GetChild(content.childCount - (totalCount - currentPage) - 1).GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(true);
                    }
                    else if (currentPage > 4)
                    {
                        SetMiddlePage(currentPage);
                        CreateEllFirst();
                    }
                    else if (currentPage != 4)
                    {
                        content.GetChild(currentPage - 1).GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(true);
                    }
                }
                UpdatePageContent(currentPage);
            }
        });

        nextBtn.onClick.AddListener(() =>
        {
            if (currentPage < totalCount)
            {
                currentPage++;
                if (totalCount <= 7)
                {
                    content.GetChild(currentPage - 1).GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(true);
                }
                else
                {
                    if (currentPage == totalCount - 3)
                    {
                        Destroy(content.GetChild(5).gameObject);
                        CreatePageToggle(5, totalCount - 1);

                        SetMiddlePage(currentPage);
                        CreateEllFirst();
                    }
                    else if (currentPage > totalCount - 3)
                    {
                        content.GetChild(content.childCount - (totalCount - currentPage) - 1).GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(true);
                    }
                    else if (currentPage > 4)
                    {
                        SetMiddlePage(currentPage);
                        CreateEllFirst();
                    }
                    else
                    {
                        content.GetChild(currentPage - 1).GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(true);
                    }
                }
                UpdatePageContent(currentPage);
            }
        });
    }

    public void SwitchPage(int page, bool isInput = false)
    {
        currentPage = page;
        UpdatePageContent(currentPage);
        if (totalCount <= 7)
        {
            content.GetChild(currentPage - 1).GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(true);
        }
        else if (page <= totalCount)
        {
            if (page <= 4)
            {
                SetMiddlePage(4, page == 4);
                if (content.GetChild(1).TryGetComponent<Toggle>(out var toggle))
                {
                    toggle.GetComponentInChildren<TMP_Text>().SetText($"{2}");
                }
                else
                {
                    Destroy(content.GetChild(1).gameObject);
                    CreatePageToggle(1, 2);
                }
                if (isInput)
                {
                    content.GetChild(page - 1).GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(true);
                }
                CreateEllLast();
            }
            else if (page >= totalCount - 3)
            {
                if (content.GetChild(5).TryGetComponent<Toggle>(out var toggle))
                {
                    toggle.GetComponentInChildren<TMP_Text>().SetText($"{totalCount - 1}");
                }
                else
                {
                    Destroy(content.GetChild(5).gameObject);
                    CreatePageToggle(5, totalCount - 1);
                }
                SetMiddlePage(totalCount - 3, page == totalCount - 3);
                if (isInput)
                {
                    content.GetChild(content.childCount - (totalCount - page) - 1).GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(true);
                }
                CreateEllFirst();
            }
            else
            {
                SetMiddlePage(page);
                CreateEllFirst();
                CreateEllLast();
            }
        }
    }

    private void UpdatePageContent(int pageIndex)
    {
        // 确保索引有效
        if (pageIndex < 1 || pageIndex > pageContents.Length)
            return;

        // 获取当前页的数据
        PageData currentPageData = pageContents[pageIndex - 1];

        // 更新 UI 元素（不销毁，直接替换内容）
        if (contentImage != null && currentPageData.image != null)
            contentImage.sprite = currentPageData.image;

        if (contentName != null)
            contentName.text = currentPageData.name;

        if (contentDesc != null)
            contentDesc.text = currentPageData.description;
    }

    Toggle CreatePageToggle(int index, int pageCount)
    {
        var page = Instantiate(pageItem, content).transform;
        page.SetSiblingIndex(index);
        page.GetComponentInChildren<TMP_Text>().SetText($"{pageCount}");
        page.GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) =>
        {
            if (isOn)
            {
                SwitchPage(int.Parse(page.GetComponentInChildren<TMP_Text>().text));
            }
        });
        return page.GetComponentInChildren<Toggle>();
    }

    private void SetMiddlePage(int page, bool isOn = true, bool SetIsOnWithoutNotify = true)
    {
        content.GetChild(2).GetComponentInChildren<TMP_Text>().SetText($"{page - 1}");
        content.GetChild(3).GetComponentInChildren<TMP_Text>().SetText($"{page}");
        content.GetChild(4).GetComponentInChildren<TMP_Text>().SetText($"{page + 1}");
        if (isOn != false)
        {
            if (SetIsOnWithoutNotify)
            {
                content.GetChild(3).GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(true);
            }
            else
            {
                content.GetChild(3).GetComponentInChildren<Toggle>().isOn = true;
            }
        }
    }

    private void CreateEllFirst()
    {
        if (content.GetChild(1).TryGetComponent<Toggle>(out var toggle))
        {
            Destroy(content.GetChild(1).gameObject);
            Instantiate(ellipsisItem, content).transform.SetSiblingIndex(1);
        }
    }

    private void CreateEllLast()
    {
        if (content.GetChild(content.childCount - 2).TryGetComponent<Toggle>(out var toggle))
        {
            Destroy(content.GetChild(content.childCount - 2).gameObject);
            Instantiate(ellipsisItem, content).transform.SetSiblingIndex(content.childCount - 2);
        }
    }

    public void UpdateDataList(int length)
    {
        int totalPage = (int)Mathf.Ceil(length / (float)onePageCount);
        totalCountText.SetText($"{length} items");
        totalPageTextBox.SetText($"{totalPage} pages");
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        if (totalPage > 7)
        {
            for (int i = 0; i < 7; i++)
            {
                if (i == 5)
                {
                    Instantiate(ellipsisItem, content);
                    continue;
                }
                var item = Instantiate(pageItem, content);
                item.GetComponentInChildren<Toggle>().onValueChanged.AddListener((bool isOn) =>
                {
                    if (isOn)
                    {
                        SwitchPage(int.Parse(item.GetComponentInChildren<TMP_Text>().text));
                    }
                });
                if (i == 6)
                {
                    item.GetComponentInChildren<TMP_Text>().SetText($"{totalPage}");
                }
                else
                {
                    item.GetComponentInChildren<TMP_Text>().SetText($"{i + 1}");
                }
            }
        }
        else
        {
            for (int i = 0; i < totalPage; i++)
            {
                var item = Instantiate(pageItem, content);
                item.GetComponentInChildren<TMP_Text>().SetText($"{i + 1}");
                if (i == 0)
                {
                    item.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
                }
                item.GetComponentInChildren<Toggle>().onValueChanged.AddListener((bool isOn) =>
                {
                    if (isOn)
                    {
                        SwitchPage(int.Parse(item.GetComponentInChildren<TMP_Text>().text));
                    }
                });
            }
        }
        content.GetComponent<LayoutElement>().preferredWidth = 1;
        content.GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(true);
    }
}