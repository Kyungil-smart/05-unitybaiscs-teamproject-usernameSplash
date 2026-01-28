using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Offset = new Vector3(0f, 10f, -10f);
    [SerializeField] private float PosSmooth = 10f;
    [SerializeField] private float RotSmooth = 10f;

    private Vector3 mVel;

    private void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        Vector3 desiredPos = Target.position + Offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref mVel, PosSmooth * Time.deltaTime);

        Quaternion desiredRot = Quaternion.LookRotation(Target.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, RotSmooth * Time.deltaTime);
    }
}