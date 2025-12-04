using UnityEngine;

public interface IMoveTargetProvider
{
 bool TryGetMovePoint(out Vector3 point);
}
