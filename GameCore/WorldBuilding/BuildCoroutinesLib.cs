using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameCore.BuildCoroutinesLib
{
    public static class BuildCoroutinesLib
    {
        public static IEnumerator BuildCoroutine(WaitForSeconds buildTime, GameObject timerObj, SymBlock block)
        {
            // Objekt UI časovače.
            GameObject currentTimer = timerObj;
            currentTimer.SetActive(true);
            yield return buildTime;
            block.BuildBlock();
            currentTimer.SetActive(false);
            ScreenUIManager.Instance.UIPoolList.ReturnToPool(Settings.PoolTypes.UI_TIMER, currentTimer);
        }
    }
}
