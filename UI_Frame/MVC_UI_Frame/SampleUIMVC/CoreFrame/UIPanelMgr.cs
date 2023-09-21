using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelMgr : MonoBehaviour
{
    public static UIPanelMgr instance;
    //����
    private GameObject canvas;
    //�����У���¼�Ѿ��򿪵Ľ���
    public List<PanelBase> dict;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        InitLayer();
        dict = new List<PanelBase>();
    }
    private void Start()
    {
        OpenPanel<MainPanel>();
    }
    private void InitLayer()
    {
        // �ҵ�������
        canvas = GameObject.Find("UICanvas");
        if (canvas == null)
            Debug.LogError("UIpanelMgr.InitLayer fail, canvas is null");
    }

    public void OpenPanel<T>() where T : PanelBase
    {
        /*Ϊcanvas��ӽű�*/
        PanelBase panel = canvas.AddComponent<T>();
        panel.Init();
        if (panel.UIResourcePath != null)
            dict.Add(panel);
        Debug.Log("�����" + panel.UIResourcePath);

        GameObject UIPanel = Resources.Load<GameObject>(panel.UIResourcePath);
        if (UIPanel == null)
            Debug.LogError("panelMgr.OpenPanel fail, GameObject is null,skinPath = " + panel.UIResourcePath);

        panel.UIPanel = Instantiate(UIPanel);

        panel.UIPanel.transform.SetParent(canvas.transform, false);

        panel.OnShowing();
        panel.OnShowed();
    }

    public void ClosePanel(PanelBase panel)
    {
        panel.OnClosing();
        dict.Remove(panel);
        Debug.Log("�ر����" + panel.UIResourcePath);
        Destroy(panel.UIPanel);
        Destroy(panel);
        panel.OnClosed();
    }
    //�ر��������
    public void CloseAllPanel()
    {
        for (int i = 0; i < dict.Count;)
        {
            ClosePanel(dict[0]);
            Debug.Log("�ر�ҳ��");
        }
    }

}

//�ֲ�����
public enum UIPanelLayer
{
    //���
    Panel,
    //��ʾ
    Tips
}