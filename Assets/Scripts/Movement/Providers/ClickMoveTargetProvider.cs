using UnityEngine;

public class ClickMoveTargetProvider : MonoBehaviour, IMoveTargetProvider
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _groundMask;

    private bool _hasNewPoint;
    private Vector3 _lastPoint;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            TryReadClickPoint(); 
        }
    }

    private void TryReadClickPoint()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; 

        if (Physics.Raycast(ray, out hit, 1000f, _groundMask)) 
        {  
            _lastPoint = hit.point;
            _hasNewPoint = true;
        }
    }

    public bool TryGetMovePoint(out Vector3 point)
    {
        if (_hasNewPoint == true)
        {
            point = _lastPoint;
            _hasNewPoint = false;
            return true;
        }

        point = Vector3.zero;
        return false;
    }
}