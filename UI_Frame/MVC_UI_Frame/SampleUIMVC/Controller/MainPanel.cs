using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

/// MVC 
/// �����Controller�࣬
/// �����������Ľ����߼���
/// ���������������
/// ��������¼������ݸ����¼��м���
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
        /*��ʼ�����*/
        btn1 = UIPanel.transform.Find("Btn1").GetComponent<Button>();
        Debug.Log(UIPanel.transform.Find("Btn1").name);
        /*���UI�¼�*/
        btn1.onClick.AddListener(OnClickBtn1);

    }

    public override void OnShowing()
    {

    }

    /*ע�ᰴť�¼�*/
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
