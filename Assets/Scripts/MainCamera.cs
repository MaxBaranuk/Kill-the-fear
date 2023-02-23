using UnityEngine;

namespace Industry.UI
{
    [RequireComponent(typeof(Camera))]
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] Camera _camera;
        [SerializeField] Bounds _bounds;

        [Header("Settings")]
        [SerializeField] bool _canMove = true;
        [SerializeField] bool _canZoom = true;
        [SerializeField] float _minZoom = 1f;
        [SerializeField] float _maxZoom = 5f;
        [SerializeField] float _moveSpeed = 10f;
        [SerializeField] float _zoomSpeed = 5f;
        [SerializeField] float _zoomGravity = 0.4f;

        private Transform _cameraTransform;

        private float _lastZoom;
        private float _zoomDecrement;

        private bool MouseAtTop
        {
            get
            {
                float pos = Input.mousePosition.y;
                float hgt = Screen.height;

                return pos == Mathf.Clamp(pos, hgt - 4f, hgt);
            }
        }
        private bool MouseAtBottom
        {
            get
            {
                float pos = Input.mousePosition.y;

                return pos == Mathf.Clamp(pos, 0f, 4f);
            }
        }
        private bool MouseAtRight
        {
            get
            {
                float pos = Input.mousePosition.x;
                float wdz = Screen.width;

                return pos == Mathf.Clamp(pos, wdz - 4f, wdz);
            }
        }
        private bool MouseAtLeft
        {
            get
            {
                float pos = Input.mousePosition.x;

                return pos == Mathf.Clamp(pos, 0f, 4f);
            }
        }

        public bool CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }
        public bool CanZoom
        {
            get => _canZoom;
            set => _canZoom = value;
        }
        public float MinZoom
        {
            get => _minZoom;
            set => _minZoom = value;
        }
        public float MaxZoom
        {
            get => _maxZoom;
            set => _maxZoom = value;
        }
        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }
        public float ZoomSpeed
        {
            get => _zoomSpeed;
            set => _zoomSpeed = value;
        }
        public float ZoomGravity
        {
            get => _zoomGravity;
            set => _zoomGravity = value;
        }
        
        private void CheckMovement()
        {
            Vector2Int dir = Vector2Int.zero;
            
            bool moveUp   = MouseAtTop    || Input.GetKey(KeyCode.W), 
                moveDown  = MouseAtBottom || Input.GetKey(KeyCode.S), 
                moveRight = MouseAtRight  || Input.GetKey(KeyCode.D), 
                moveLeft  = MouseAtLeft   || Input.GetKey(KeyCode.A);

            if (moveUp)    dir += Vector2Int.up;
            if (moveDown)  dir += Vector2Int.down;
            if (moveLeft)  dir += Vector2Int.left;
            if (moveRight) dir += Vector2Int.right;

            if (dir == Vector2Int.zero)
                return;

            float speed = _moveSpeed * _camera.orthographicSize * 0.5f;
            var direction = speed * Time.deltaTime * ((Vector2)dir).normalized;

            _cameraTransform.Translate(direction, Space.Self);

            float limitX = _bounds.max.x;
            float limitY = _bounds.max.y;

            _cameraTransform.position = new Vector3(
                Mathf.Clamp(_cameraTransform.position.x, -limitX, limitX),
                Mathf.Clamp(_cameraTransform.position.y, -limitY, limitY),
                _cameraTransform.position.z);
        }
        
        private void CheckZoom()
        {
            bool zoom_num_in  = Input.GetKeyDown(KeyCode.KeypadPlus)  || Input.GetKey(KeyCode.KeypadPlus)  || Input.GetKeyDown(KeyCode.Plus)  || Input.GetKey(KeyCode.Plus);
            bool zoom_num_out = Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKey(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus) || Input.GetKey(KeyCode.Minus);
            
            float zoom = zoom_num_in ? 1 : zoom_num_out ? -1 : -Input.GetAxis("Mouse ScrollWheel");
            
            if (zoom > 0)
                _lastZoom = 10.0f;
            else if (zoom < 0)
                _lastZoom = -10.0f;

            if (_lastZoom == 0f)
                return;

            _zoomDecrement = Mathf.Sign(_lastZoom) * _zoomGravity;

            float increment = _zoomSpeed * _lastZoom * Time.deltaTime;
            float newSize = _camera.orthographicSize + increment;

            _camera.orthographicSize = Mathf.Clamp(newSize, _minZoom, _maxZoom);

            if (newSize < _minZoom || newSize > _maxZoom)
                _lastZoom = 0f;

            if ((int)(_lastZoom * 100) != 0)
                _lastZoom -= _zoomDecrement;
            else
                _lastZoom = 0f;
        }

        private void Awake()
        {
            _cameraTransform = _camera.transform;
        }

        private void Update()
        {
            if (_canMove)
                CheckMovement();

            if (_canZoom)
                CheckZoom();
        }
    }
}
