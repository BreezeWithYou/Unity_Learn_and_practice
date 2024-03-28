using UnityEngine;

namespace JL_Animation_Playable
{
    [CreateAssetMenu(fileName = "SingleAnimClipSO", menuName = "JL_Animation_Playable/SingleAnimClipSO")]
    public class SingleAnimClipSO : AnimSO
    {
        private void OnEnable()
        {
            AnimType = AnimTypeEnum.SingleAnimClip;
        }
        public AnimationClip AnimationClip;
    }
}
