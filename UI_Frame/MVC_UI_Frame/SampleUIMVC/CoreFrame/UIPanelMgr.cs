using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelMgr : MonoBehaviour
{
    public static UIPanelMgr instance;
    //画板
    private GameObject canvas;
    //面板队列，记录已经打开的界面
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
        // 找到基物体
        canvas = GameObject.Find("UICanvas");
        if (canvas == null)
            Debug.LogError("UIpanelMgr.InitLayer fail, canvas is null");
    }

    public void OpenPanel<T>() where T : PanelBase
    {
        /*为canvas添加脚本*/
        PanelBase panel = canvas.AddComponent<T>();
        panel.Init();
        if (panel.UIResourcePath != null)
            dict.Add(panel);
        Debug.Log("打开面板" + panel.UIResourcePath);

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
        Debug.Log("关闭面板" + panel.UIResourcePath);
        Destroy(panel.UIPanel);
        Destroy(panel);
        panel.OnClosed();
    }
    //关闭所有面板
    public void CloseAllPanel()
    {
        for (int i = 0; i < dict.Count;)
        {
            ClosePanel(dict[0]);
            Debug.Log("关闭页面");
        }
    }

}

//分层类型
public enum UIPanelLayer
{
    //面板
    Panel,
    //提示
    Tips
}