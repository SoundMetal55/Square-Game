using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementControllerSquare))]
[RequireComponent(typeof(MovementControllerCircle))]
public class ShapeController : MonoBehaviour
{
    private MovementControllerSquare square_movement;
    private MovementControllerCircle circle_movement;

    [SerializeField]
    private Shape default_shape = Shape.Square;
    private Shape current_shape;

    private float size = 1f;

    public enum Shape
    {
        Square,
        Circle,
    }

    void Awake()
    {
        square_movement = this.GetComponent<MovementControllerSquare>();
        circle_movement = this.GetComponent<MovementControllerCircle>();

        ChangeShape(default_shape);
    }

    // temporary test for shape change
    void Update()
    {
        Keyboard kb = Keyboard.current;

        if (kb.qKey.isPressed == true)
        {
            ChangeShape(Shape.Square);
        }
        if (kb.eKey.isPressed == true)
        {
            ChangeShape(Shape.Circle);
        }
    }

    // change the shape of an object
    // return true if successfull, return false if no change was made
    public bool ChangeShape(Shape new_shape)
    {
        if (new_shape == current_shape)
        {
            // allow shape changing to itself for now so it can initialize properly
            //return false;
        }
        
        if (new_shape == Shape.Square)
        {
            TransformIntoSquare();
        }
        if (new_shape == Shape.Circle)
        {
            TransformIntoCircle();
        }

        current_shape = new_shape;
        return true;
    }

    private void DisableMovementControllers()
    {
        square_movement.Dismantle();
        circle_movement.Dismantle();
    }

    public void TransformIntoSquare()
    {
        DisableMovementControllers();
        square_movement.enabled = true;
        square_movement.Initialize(size);
    }

    public void TransformIntoCircle()
    {
        DisableMovementControllers();
        circle_movement.enabled = true;
        circle_movement.Initialize(size);
    }
}
