using UnityEngine;
using UnityEngine.InputSystem;
 
    public enum PlayerState
    {
        Idle,
        Walk,
        Sprint,
        CrouchIdle,
        CrouchWalk

    }
	public class PlayerInputAsset : MonoBehaviour
	{
		[Header("Character Input Values")]
		private Vector2 move; 
		private bool crouch;
		private bool sprint; 

        private PlayerState playerState;

        public Vector2 Move { get => move;  }
        public bool Crouch { get => crouch;  }
        public bool Sprint { get => sprint; }
        public PlayerState PlayerState { get => playerState; }

        public void OnMove(InputValue value)
		{
			move = value.Get<Vector2>(); 

            if(move != Vector2.zero) 
            { 
                if (crouch) playerState = PlayerState.CrouchWalk;
                else if (sprint) playerState = PlayerState.Sprint;
                else playerState = PlayerState.Walk;
            }
            else
            {
                if (crouch) playerState = PlayerState.CrouchIdle; 
                else playerState = PlayerState.Idle;
            }

		} 
		public void OnCrouch(InputValue value)
		{
            crouch = value.isPressed;
            
            if(move != Vector2.zero) 
            { 
                if (crouch) playerState = PlayerState.CrouchWalk;
                else if (sprint) playerState = PlayerState.Sprint;
                else playerState = PlayerState.Walk;
            }
            else
            {
                if (crouch) playerState = PlayerState.CrouchIdle; 
                else playerState = PlayerState.Idle;
            }
		}

		public void OnSprint(InputValue value)
		{
            sprint = value.isPressed;
            
            if(move != Vector2.zero) 
            { 
                if (crouch) playerState = PlayerState.CrouchWalk;
                else if (sprint) playerState = PlayerState.Sprint;
                else playerState = PlayerState.Walk;
            }
            else
            {
                if (crouch) playerState = PlayerState.CrouchIdle; 
                else playerState = PlayerState.Idle;
            }
		} 
	} 