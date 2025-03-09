using UnityEngine;
using UnityEngine.UI;

public class SpeedControl : MonoBehaviour
{
    public void Start()
    {
        gameObject.GetComponent<Slider>().value = Utils.speed;
    }
    public void UpdateSpeed()
    {
        Utils.speed = gameObject.GetComponent<Slider>().value;
    }
}