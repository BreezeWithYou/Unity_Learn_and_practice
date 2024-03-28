using JL_Animation_Playable;
using System.Collections;
using UnityEngine;


public class JLAnimResourceLoad
{
    public static void ResourceLoadAnimUnit(PlayerAnim playerAnim,JLAnimController m_animController, SingleAnimClipSO singleAnimClipSO,float time)
    {
        m_animController.AddAnimUnit(singleAnimClipSO?.name, singleAnimClipSO.AnimationClip, time);
    }
}