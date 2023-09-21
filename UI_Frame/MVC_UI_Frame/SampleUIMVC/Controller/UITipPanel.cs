using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITipPanel : PanelBase
{
    Button btn1;
    public override void Init()
    {
        UIResourcePath = "UI/UITipPanel";
        layer = UIPanelLayer.Tips;
    }

    public override void OnShowing()
    {
        /*初始化组件*/
        btn1 = UIPanel.transform.Find("Btn1").GetComponent<Button>();

        /*添加UI事件*/
        btn1.onClick.AddListener(OnCloseClick);
    }
    /*关闭按钮事件*/
    public void OnCloseClick()
    {
        UIPanelMgr.instance.ClosePanel(this);
    }


}
