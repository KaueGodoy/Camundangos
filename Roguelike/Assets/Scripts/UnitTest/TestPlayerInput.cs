using UnityEngine;

public class TestPlayerInput : ITestPlayerInput
{
    public float Vertical => Input.GetAxis("Vertical");
}
