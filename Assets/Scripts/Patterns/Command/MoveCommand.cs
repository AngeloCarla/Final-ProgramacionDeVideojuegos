using UnityEngine;

public class MoveCommand : ICommand
{
    private CharacterController cc;
    private Vector3 moveDirection;

    public MoveCommand(CharacterController cc, Vector3 moveDirection)
    {
        this.cc = cc;
        this.moveDirection = moveDirection;
    }
    public void Execute()
    {
        cc.Move(moveDirection * Time.deltaTime);
    }
}
