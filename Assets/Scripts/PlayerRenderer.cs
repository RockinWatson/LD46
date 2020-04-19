using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    public static readonly string[] directions = {
        "Ani-Player-Up",
        "Ani-Player-UpLeft",
        "Ani-Player-Left",
        "Ani-Player-DownLeft",
        "Ani-Player-Down",
        "Ani-Player-DownRight",
        "Ani-Player-Right",
        "Ani-Player-UpRight"   
    };

    private Animator _animator;
    private int _lastDirection;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 direction)
    {
        string[] directionArray;
        if (direction.magnitude < 0.1f)
        {
            //directionArray = directions;
            _animator.Play(directions[7]);
        }
        else
        {
            directionArray = directions;
            _lastDirection = DirectionToIndex(direction, 8);
            _animator.Play(directionArray[_lastDirection]);
        }
    }

    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {
        //get the normalized direction
        Vector2 normDir = dir.normalized;
        //calculate how many degrees one slice is
        float step = 360f / sliceCount;
        //calculate how many degress half a slice is.
        //we need this to offset the pie, so that the North (UP) slice is aligned in the center
        float halfstep = step / 2;
        //get the angle from -180 to 180 of the direction vector relative to the Up vector.
        //this will return the angle between dir and North.
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        //add the halfslice offset
        angle += halfstep;
        //if angle is negative, then let's make it positive by adding 360 to wrap it around.
        if (angle < 0)
        {
            angle += 360;
        }
        //calculate the amount of steps required to reach this angle
        float stepCount = angle / step;
        //round it, and we have the answer!
        return Mathf.FloorToInt(stepCount);
    }
}
