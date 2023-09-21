using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

/// MVC 
/// 主面板Controller类，
/// 负责处理主面板的交互逻辑，
/// 包括主面板显隐，
/// 主面板点击事件，数据更新事件中间商
/// 
/// 
/// 
public class MainPanel : PanelBase
{
    public Button btn1;
    public UIMainView MainView;
    public override void Init()
    {
        UIResourcePath = "UI/UIMain";
        layer = UIPanelLayer.Panel;
    }

    private void Start()
    {
        MainView = FindObjectOfType<UIMainView>();
        UIMainModel.Data.AddListener(OnUpdateData);
        /*初始化组件*/
        btn1 = UIPanel.transform.Find("Btn1").GetComponent<Button>();
        Debug.Log(UIPanel.transform.Find("Btn1").name);
        /*添加UI事件*/
        btn1.onClick.AddListener(OnClickBtn1);

    }

    public override void OnShowing()
    {

    }

    /*注册按钮事件*/
    public void OnClickBtn1()
    {
        UIPanelMgr.instance.OpenPanel<UITipPanel>();
        UIMainModel.Data.LevUp();
    }


    private void OnUpdateData(UIMainModel Model)
    {
        MainView.UpdateData(Model);
    }


}
