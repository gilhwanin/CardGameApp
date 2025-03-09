using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionButton : MonoBehaviour
{
    public GameObject image1, image2, image3, image4;

    public void Active1()
    {
        image1.SetActive(true);
    }
    public void DeActive1()
    {
        image1.SetActive(false);
    }
    public void Active2()
    {
        image2.SetActive(true);
    }
    public void DeActive2()
    {
        image2.SetActive(false);
    }
    public void Active3()
    {
        image3.SetActive(true);
    }
    public void DeActive3()
    {
        image3.SetActive(false);
    }
    public void Active4()
    {
        image4.SetActive(true);
    }
    public void DeActive4()
    {
        image4.SetActive(false);
    }

}
