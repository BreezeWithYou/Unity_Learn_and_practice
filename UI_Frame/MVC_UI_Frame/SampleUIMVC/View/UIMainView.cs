using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMainView : MonoBehaviour
{
    /// �����View��View����Ҫ��������ʾ���ݣ���֤��ʾ��ʵʱ���¡�
    /// ����View����һ��ʵ����Ӧ����UpdateData��
    /// Ϊʲô��ʵ����Ӧ������
    /// ��Ϊʵ����Controller�����Ӧ����ֻ��һ���м��̣�Controller��Ӧ�������һ���ַ��¼���
    /// 

    //��ҵȼ�
    public Text playerLev;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        // ����ٸ�����
        playerLev.text = UIMainModel.Data.PlayerLev.ToString();
    }
    public void UpdateData(UIMainModel UIMainModel)
    {
        playerLev.text = UIMainModel.Data.PlayerLev.ToString();
    }


}
