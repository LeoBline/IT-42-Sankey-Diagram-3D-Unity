using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHighLight : MonoBehaviour
{
    // Start is called before the first frame update
    private HighlightableObject m_ho;

    void Awake()
    {
        //初始化组件
        m_ho = GetComponent<HighlightableObject>();
    }


    void HifhLightFunction()
    {
        //循环往复外发光开启（参数为：颜色1，颜色2，切换时间）
        m_ho.FlashingOn(Color.green, Color.blue, 1f);

        //关闭循环往复外发光
        m_ho.FlashingOff();


        //持续外发光开启（参数：颜色）
        m_ho.ConstantOn(Color.yellow);

        //关闭持续外发光
        m_ho.ConstantOff();
    }

    /// <summary>
    /// 鼠标指向模型时触发
    /// </summary>
    private void OnMouseOver()
    {
        //开启外发光
        m_ho.FlashingOn(Color.green, Color.blue, 1f);
    }

    /// <summary>
    /// 鼠标离开模型时触发
    /// </summary>
    private void OnMouseExit()
    {
        //关闭外发光
        m_ho.FlashingOff();
    }
}
