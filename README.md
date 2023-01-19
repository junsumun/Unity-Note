# Unity Note

## Table of Contents
1. [Vehicle Physics](#vehicle-physics)
    1. [Moving a vehicle](#moving-a-vehicle)
    2. [Vertical key events](#vertical-key-events)
    3. [Horizontal key events](#horizontal-key-events)
    4. [Vehicle drag]()



### Vehicle Physics

#### Moving a vehicle

To make a vehicle simply go forward

```cs

void Update()
{
  // 30 represents a speed of the vehicle

  // Time.deltaTime returns the interval in seconds from the last frame to the current one. We multiply this value to make the movement of the vehicle look smooth.

  transform.forward * 30 * Time.deltaTime;
}
```

This code could be improved by creating variables

```cs
public float MoveSpeed = 50; // This value can be modified from the Unity inspector

private Vector3 MoveForce;

void Update()
{
  MoveForce += transform.forward * MoveSpeed * Time.deltaTime;
  transform.position += MoveForce * Time.deltaTime;
}
```

#### Vertical key events

Use `Input.GetAxis("Vertical")` to control the vertical movement of the vehicle. `Input.GetAxis("Vertical")` returns a value between -1 to 1 depends on how strong the key is pressed.

```cs
MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

transform.position += MoveForce * Time.deltaTime;
```

#### Horizontal key events

Use `Input.GetAxis("Horizontal")` to control the horizontal movement of the vehicle.

```cs
public float  SteerAngle = 20;

private Vector3 MoveForce;

void Update()
{
  float steerInput = Input.GetAxis("Horizontal");

  // Vector3.up == Vector3(0, 1, 0) == Vector3(x, y, z)
  // MoveForce.magnitude == The length of the vector == sqrt(x*x + y*y + z*z)
  transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);
}
```

#### Vehicle drag

To simulate a drag force acting on a vehicle, we will be reducing a magnitude of `MoveFoce` by a given `Drag` force in every time frame.

```cs

public float Drag = 0.98f;

private Vector3 MoveForce;

void Update()
{
  MoveForce *= Drag;
  MoveFoce = Vector3.ClampMagnitude(MoveFoce, MaxSpeed); // Using ClampMagnitude method to limit the amount of MoveForce
}
```

#### Vehicle traction

With out traction, a vehicle looks like its moving on ice. Therefore, we need some friction force.

```cs
public float Traction = 1;

private Vector3 MoveForce;

void Update()
{
  // Vector3.Lerp method choose Vector3 value between a and b vectors
  // if t = 0, returns a vector
  // if t = 1, returns b vector
  MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;
}