using System;
using System.Collections.Generic;

public static class Events
{
    
    public static Action OnStartSpinAction;
    public static Action<int> OnEndSpinAction;
    public static Action OnRewardWinned;
    public static Action OnRewardProcessFinished;
    public static Action<List<RewardItem>> OnSpinExit;
    public static Action OnSpinExitFinished;
}