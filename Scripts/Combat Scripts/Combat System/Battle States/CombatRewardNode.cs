using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatRewardNode : CombatStateNode
    {
        [Export] private PackedScene rewardMenuScene;
        private CombatRewardDisplay rewardMenu;
        private StageCompletionData completionData;

        private bool expShown = false;

        protected override void OnStateActivated()
        {
            base.OnStateActivated();
            DisplayRewards();
        }

        private void DisplayRewards()
        {
            rewardMenu = Utils.InstantiateCopy<CombatRewardDisplay>(rewardMenuScene);
            battle.AddChild(rewardMenu);
            completionData = battle.State.CurrentStage.CheckRewards(battle.State);
            rewardMenu.Init(completionData.Rewards);
        }

        private void DisplayExp()
        {

        }

        protected override void RunState(double delta)
        {
            if (!expShown && !IsInstanceValid(rewardMenu))
            {
                expShown = true;
                rewardMenu = null;
                DisplayExp();
                Utils.Print(this, "Showing exp animation");
            }
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
