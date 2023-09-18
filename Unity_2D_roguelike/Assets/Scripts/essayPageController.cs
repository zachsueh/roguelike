using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controller class for the essay page collectibles
public class essayPageController : MonoBehaviour
{
    public static essayPageController instance;

    public int currentNumPages;
    public int maxNumPages;

    public Sprite[] essayPageUI;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // player starts with no pages at beginning of level
        currentNumPages = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // method run when player walks over essay page
    // increments current number of pages by 1 and updates UI
    public void pickupPage()
    {
        currentNumPages++;
        if(currentNumPages > maxNumPages)
        {
            currentNumPages = maxNumPages;
        }

        UIController.instance.essayPageSlider.maxValue = maxNumPages;
        UIController.instance.essayPageSlider.value = currentNumPages;
        UIController.instance.essayPageText.text = currentNumPages.ToString() + " / " + maxNumPages.ToString();
    }
}
