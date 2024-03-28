using JL_Animation_Playable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerAnimDatas
{
    public List<SingleAnimClipSO> singleAnimClips;
    public List<BlendTree1DSO> blendTree1Ds;
    public List<BlendTree2DSO> blendTree2Ds;
    public ComboAssetsSO WeaponDatas;
}