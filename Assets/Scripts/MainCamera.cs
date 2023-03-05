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

        private void CheckRotation()
        {
            if (Input.GetMouseButton(1))
            {
                float rotation = Input.GetAxis("Mouse X");
                float inclination = Input.GetAxis("Mouse Y");

                _cameraTransform.Rotate(new Vector3(-inclination, rotation, 0));

                float x = _cameraTransform.rotation.eulerAngles.x;
                
                if (x > 269f) x -= 360f;
                x = Mathf.Clamp(x, 10f, 80f);

                _cameraTransform.rotation = Quaternion.Euler(new Vector3(x, _cameraTransform.rotation.eulerAngles.y, 0));
            }
        }

        private void CheckMovement()
        {
            bool at_top   = MouseAtTop    || Input.GetKey(KeyCode.W),
                at_bottom = MouseAtBottom || Input.GetKey(KeyCode.S),
                at_right  = MouseAtRight  || Input.GetKey(KeyCode.D),
                at_left   = MouseAtLeft   || Input.GetKey(KeyCode.A);

            float limitX = _bounds.max.x;
            float limitZ = _bounds.max.z;

            var direction = new Vector3();
            direction.x = at_left ? -1 : at_right ? 1 : 0;
            direction.z = at_top ? 1 : at_bottom ? -1 : 0;

            direction = Quaternion.Euler(new Vector3(0f, _cameraTransform.eulerAngles.y, 0f)) * (direction * _moveSpeed / _maxZoom * _cameraTransform.position.y * 3f * Time.deltaTime);
            direction = _cameraTransform.InverseTransformDirection(direction);

            _cameraTransform.Translate(direction, Space.Self);

            _cameraTransform.position = new Vector3(
                Mathf.Clamp(_cameraTransform.position.x, -limitX, limitX),
                _cameraTransform.position.y,
                Mathf.Clamp(_cameraTransform.position.z, -limitZ, limitZ));
        }

        private void CheckZoom()
        {
            bool zoom_num_in  = Input.GetKeyDown(KeyCode.KeypadPlus)  || Input.GetKey(KeyCode.KeypadPlus)  || Input.GetKeyDown(KeyCode.Plus)  || Input.GetKey(KeyCode.Plus);
            bool zoom_num_out = Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKey(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus) || Input.GetKey(KeyCode.Minus);

            float zoom = zoom_num_in ? 1 : zoom_num_out ? -1 : Input.GetAxis("Mouse ScrollWheel");

            if (zoom > 0)
                _lastZoom = 10.0f;
            else if (zoom < 0)
                _lastZoom = -10.0f;

            if (Mathf.Approximately(_lastZoom, 0f))
                return;

            _zoomDecrement = Mathf.Sign(_lastZoom) * _zoomGravity;

            var zPos = _lastZoom * _zoomSpeed * Time.deltaTime * Vector3.forward;

            if (_lastZoom != 0f)
                _cameraTransform.Translate(zPos);

            if (_cameraTransform.position.y < _minZoom || _cameraTransform.position.y > _maxZoom)
            {
                _cameraTransform.Translate(-zPos);
                _lastZoom = 0f;
            }

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
            CheckRotation();

            if (_canMove)
                CheckMovement();

            if (_canZoom)
                CheckZoom();
        }
    }
}
