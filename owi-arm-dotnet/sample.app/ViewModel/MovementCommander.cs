using System;
using owi_arm_dotnet;

namespace sample.app.ViewModel;

internal class MovementCommander
{
    private readonly Action<IOwiCommand> leftMovement;
    private readonly Action<IOwiCommand> neutralMovement;
    private readonly Action<IOwiCommand> rightMovement;

    public MovementCommander(Action<IOwiCommand> leftMovement, Action<IOwiCommand> neutralMovement,
        Action<IOwiCommand> rightMovement)
    {
        this.leftMovement = leftMovement;
        this.neutralMovement = neutralMovement;
        this.rightMovement = rightMovement;
    }

    public void ApplyMovement(int movement, IOwiCommand command)
    {
        if (movement == -1)
            leftMovement.Invoke(command);
        else if (movement == 1)
            rightMovement.Invoke(command);
        else
            neutralMovement.Invoke(command);
    }
}