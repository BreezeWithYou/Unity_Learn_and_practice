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
        /*��ʼ�����*/
        btn1 = UIPanel.transform.Find("Btn1").GetComponent<Button>();

        /*���UI�¼�*/
        btn1.onClick.AddListener(OnCloseClick);
    }
    /*�رհ�ť�¼�*/
    public void OnCloseClick()
    {
        UIPanelMgr.instance.ClosePanel(this);
    }


}
