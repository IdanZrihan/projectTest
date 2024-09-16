using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
 //mouse movment
    float mouseX, mouseY;
    float lookSpeed = 500;
    float cameraRange;
    public Transform cameraTurn;

//movment and walk and run
    CharacterController cc;
    float runSpeed, walkSpeed, zAxis, xAxis;
    Vector3 localDirection;

//gravity
    float gravity = -9.81f * 2;
    float radius = 0.5f;
    public LayerMask GroundLayer;
    public Transform heightOfSphere;
    public bool groundCheck = false;
    Vector3 gravityMove;

  bool isDead = false;
  void Start()
  {
    runSpeed = 35;
    walkSpeed = 15;
    cc = GetComponent<CharacterController>();
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false; 
  }

  void Update()
  {
      Mouse();
      walkAndRun();
      gravityCheckAndJump();
      if (!isDead && this.transform.position.y < -50)
    {
        die();
    }
    
  }

void gravityCheckAndJump()
{
    if(Physics.CheckSphere(heightOfSphere.position, radius, GroundLayer))
    {
        groundCheck = true;
    }
    else
    {
        groundCheck = false;
    }
    if(!groundCheck)
    {
        gravityMove.y += gravity * Time.deltaTime;
    }
    else
    {
        gravityMove.y = 0;
    }
    cc.Move(gravityMove * Time.deltaTime);

    if(groundCheck && Input.GetButtonDown("Jump") )
      {
        gravityMove.y += 6.5f;
      }
      else if(groundCheck && Input.GetAxis("Mouse ScrollWheel") > 0f)
      {
        gravityMove.y += 6.5f;
      }
    cc.Move(gravityMove * Time.deltaTime);


}
  void walkAndRun()
{
    if(Input.GetKey(KeyCode.LeftShift))
      {
       xAxis = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;
       zAxis = Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime;
       localDirection = transform.forward * zAxis + transform.right * xAxis;
       cc.Move(localDirection);
      }
      else
      {
        xAxis = Input.GetAxis("Horizontal") * runSpeed * Time.deltaTime;
        zAxis = Input.GetAxis("Vertical") * runSpeed * Time.deltaTime;
        localDirection = transform.forward * zAxis + transform.right * xAxis;
        cc.Move(localDirection);
      }
   
}

  void Mouse()
{
    mouseX = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
       transform.Rotate(0, mouseX, 0 );

       mouseY = Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
     
       cameraRange -= mouseY;
    
      cameraRange = MyClamp(cameraRange, -45,45);
       cameraTurn.localRotation = Quaternion.Euler( cameraRange,0 ,0);

}

  float MyClamp(float valueMouse, float valueMin, float valueMax)
{
    if(valueMouse < valueMin)
    {
        return valueMin;
    }
    else if(valueMouse > valueMax)
    {
        return valueMax;
    }
    return valueMouse;
}


void die()
{
  if (this.transform.position.y < -50)
  {
    Time.timeScale = 0;
    print("youDie");
    isDead = true;
  }
}

}
