using UnityEngine;

public class SpecialButtons : MonoBehaviour
{
    public void ActivateFreeze()
    {
        TappyFreezeManager.instance.ActivateFreeze();
    }

    public void ActivateFever()
    {
        if (OguFeverManager.instance != null)
        {
            OguFeverManager.instance.ActivateFever();
        }
    }
}
