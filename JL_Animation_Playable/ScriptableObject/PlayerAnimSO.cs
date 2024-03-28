using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JL_Animation_Playable
{
    [CreateAssetMenu(fileName = "PlayerAnimClipSO", menuName = "JL_Animation_Playable/PlayerInitFile")]
    public class PlayerAnimSO :ScriptableObject
    {
        public PlayerAnimDatas AnimDatas;
    }
}
