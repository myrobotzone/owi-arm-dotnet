using owi_arm_dotnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample.app.ViewModel
{
    class MovementCommander
    {
        private Action<IOwiCommand> leftMovement;
        private Action<IOwiCommand> neutralMovement;
        private Action<IOwiCommand> rightMovement;

        public MovementCommander(Action<IOwiCommand> leftMovement, Action<IOwiCommand> neutralMovement, Action<IOwiCommand> rightMovement)
        {
            this.leftMovement = leftMovement;
            this.neutralMovement = neutralMovement;
            this.rightMovement = rightMovement;
        }

        public void ApplyMovement(int movement, IOwiCommand command)
        {
            if (movement == -1)
                this.leftMovement.Invoke(command);
            else if (movement == 1)
                this.rightMovement.Invoke(command);
            else
                this.neutralMovement.Invoke(command);
        }
    }
}
