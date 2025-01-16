using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace CharacterCreator
{
    public partial class CharacterFieldMenu : Control
    {
        protected CreateCharacterMenu menu;
        private int currState = 0;
        private Vector2 positionToGoTo;
        private float lerpSpeed = 3f;

        public bool Enabled { get; private set; } = false;

        /**********************
         * Main Functions
         * *******************/

        #region Main Functions

        public virtual void _Init(CreateCharacterMenu menu)
        {
            this.menu = menu;
            SetState(0);
        }

        public override void _Process(double delta)
        {
            MoveTowardsPosition(delta);
        }

        public virtual void _Disable()
        {
            menu = null;
        }

        #endregion

        public virtual void SaveData(CustomGameCharacter character)
        {

        }

        public virtual bool FieldValid()
        {
            return true;
        }


        /// <summary>
        /// Sends menu to left
        /// </summary>
        /// <returns></returns>
        public void CompleteMenu()
        {
            SetState(2);
        }
        /// <summary>
        /// Sends menu to front
        /// </summary>
        /// <returns></returns>
        public void ActivateMenu()
        {
            SetState(1);
        }
        /// <summary>
        /// Sends menu back to right-side
        /// </summary>
        /// <returns></returns>
        public void CancelMenu()
        {
            SetState(0);
        }

        /// <summary>
        /// 0 = menu on right-side off-screen waiting to be used
        /// 1 = menu on-screen
        /// 2 = menu on left-side off-screen after having been filled
        /// </summary>
        /// <param name="state"></param>
        private void SetState(int state)
        {
            positionToGoTo = GetPositionFromState(state);
            if(state != 1)
            {
                Enabled = false;
            }
            else
            {
                Enabled = true;
            }
        }

        private void MoveTowardsPosition(double delta)
        {
            Position = Position.Lerp(positionToGoTo, lerpSpeed * (float)delta);
        }

        private Vector2 GetPositionFromState(int state)
        {
            var viewport = GetTree().Root.GetViewport();
            float width = viewport.GetVisibleRect().Size.X;
            var result = new Vector2(-width, 0);
            switch (state)
            {
                case 0:
                    result.X = width;
                    break;
                case 1:
                    result.X = 0;
                    break;
                case 2:
                    break;
                default:
                    break;
            }

            return result;
        }

    }
}