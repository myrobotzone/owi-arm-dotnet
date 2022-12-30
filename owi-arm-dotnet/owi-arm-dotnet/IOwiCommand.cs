namespace owi_arm_dotnet;

/// <summary>
/// Interface for owi robotic arm commands. The owi arm protocol consists of
/// three bytes, an arm byte, an LED byte and a base byte. These bytes are
/// manipulated by using the methods on this interface. The interface
/// provides a fluent API so that each commanding method can be chained.
/// </summary>
public interface IOwiCommand : IEquatable<IOwiCommand>
{
    /// <summary>
    /// Gets a value for the command byte representing arm movements.
    /// </summary>
    byte ArmByte { get; }

    /// <summary>
    /// Gets a value for the command byte representing the LED.
    /// </summary>
    byte LedByte { get; }

    /// <summary>
    /// Gets a value for the command byte representing the base movement.
    /// </summary>
    byte BaseByte { get; }

    /// <summary>
    /// Get a value indicating whether all movement is off (state of LED is ignored).
    /// </summary>
    /// <returns>
    /// True if all movement is off, otherwise false.
    /// </returns>
    bool IsAllMovementOff { get; }

    /// <summary>
    /// Command for stopping all movement.
    /// </summary>
    IOwiCommand StopAllMovements();

    /// <summary>
    /// Command for opening the gripper.
    /// </summary>
    IOwiCommand GripperOpen();

    /// <summary>
    /// Command for closing the gripper.
    /// </summary>
    IOwiCommand GripperClose();

    /// <summary>
    /// Command to stop gripper movement.
    /// </summary>
    IOwiCommand GripperStop();

    /// <summary>
    /// Command to move wrist up.
    /// </summary>
    IOwiCommand WristUp();

    /// <summary>
    /// Command to move wrist down.
    /// </summary>
    IOwiCommand WristDown();

    /// <summary>
    /// Command to stop wrist movement.
    /// </summary>
    IOwiCommand WristStop();

    /// <summary>
    /// Command to move elbow up.
    /// </summary>
    IOwiCommand ElbowUp();

    /// <summary>
    /// Command to move elbow down.
    /// </summary>
    IOwiCommand ElbowDown();

    /// <summary>
    /// Command to stop elbow movement.
    /// </summary>
    IOwiCommand ElbowStop();

    /// <summary>
    /// Command to move shoulder up.
    /// </summary>
    IOwiCommand ShoulderUp();

    /// <summary>
    /// Command to move shoulder down.
    /// </summary>
    IOwiCommand ShoulderDown();

    /// <summary>
    /// Command to stop shoulder movement.
    /// </summary>
    IOwiCommand ShoulderStop();

    /// <summary>
    /// Command to rotate the base clockwise.
    /// </summary>
    IOwiCommand BaseRotateClockwise();

    /// <summary>
    /// Command to rotate the base counterclockwise.
    /// </summary>
    IOwiCommand BaseRotateCounterClockwise();

    /// <summary>
    /// Command to stop base rotation.
    /// </summary>
    IOwiCommand BaseRotateStop();

    /// <summary>
    /// Command to turn the LED off.
    /// </summary>
    IOwiCommand LedOff();

    /// <summary>
    /// Command to turn the LED on.
    /// </summary>
    IOwiCommand LedOn();
}