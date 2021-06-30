using aitcHUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageScroller : MonoBehaviour
{
    enum DotStyle { IncreaseSize, SeperateImage }

    [SerializeField]
    [Tooltip("The parent object of all the pages")]
    Transform pagesParent;

    [SerializeField]
    [Tooltip("The parent where all dots are created. Use horizontal layout group and content size fitter for better results")]
    Transform dotsParent;

    [SerializeField]
    GameObject nextPageBtn;

    [SerializeField]
    GameObject previousPageBtn;

    [SerializeField]
    [Tooltip("Prefab of dots")]
    GameObject dotObjectPrefab;

    [SerializeField]
    [Tooltip("Hide the next page btn on last page and previous page button on first page")]
    bool hideOnLastPage;

    [SerializeField]
    [Tooltip("How the selected page dot is displayed")]
    DotStyle selectedDotStyle;

    [SerializeField]
    [Tooltip("Dot size will be multiplied by this")]
    float selectedDotSize;

    private int _numberOfPages;
    private int currentPage = 0;
    private Transform[] pages;
    private GameObject[] dots;

    // Start is called before the first frame update
    void Start()
    {
        _numberOfPages = pagesParent.childCount;
        pages = MiscUtils.GetChildren(pagesParent);
        dots = new GameObject[_numberOfPages];

        for (int i = 0; i < dots.Length; i++)
        {
            dots[i] = Instantiate(dotObjectPrefab, dotsParent);
        }

        SetPages(currentPage);
    }

    public void onClick_NextPage() 
    {
        if (currentPage == _numberOfPages - 1)
            return;

        currentPage++;
        SetPages(currentPage);
    }

    public void onClick_PreviousPage()
    {
        if (currentPage == 0)
            return;

        currentPage--;
        SetPages(currentPage);
    }

    void SetPages(int _selectedPageIndex) 
    {
        if (_selectedPageIndex == 0)
            previousPageBtn.SetActive(false);
        else
            previousPageBtn.SetActive(true);

        if (_selectedPageIndex == _numberOfPages - 1)
            nextPageBtn.SetActive(false);
        else
            nextPageBtn.SetActive(true);

        for (int i = 0; i < pages.Length; i++)
        {
            if (i == _selectedPageIndex)
            {
                pages[i].gameObject.SetActive(true);
                dots[i].transform.localScale = Vector2.one * selectedDotSize; 
            }
            else 
            {
                pages[i].gameObject.SetActive(false);
                dots[i].transform.localScale = Vector2.one; 
            }
        }
    }
}
