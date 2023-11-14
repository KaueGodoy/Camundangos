using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 _parallaxEffectMultiplier;
    [SerializeField] private bool _infiniteHorizontal;
    [SerializeField] private bool _infiniteVertical;

    //public Transform _cameraTransformInitial;
    // initial public transform = empty game obj for solving the camera initiaziling problem getting the current position of the player
    private Transform _cameraTransform;
    private Vector3 _lastCameraPosition;

    private Sprite _sprite;
    private Texture2D _texture;

    private float _textureUnitSizeX;
    private float _textureUnitSizeY;

    private void Awake()
    {
        //_cameraTransform = _cameraTransformInitial;
        _cameraTransform = Camera.main.transform;
        _lastCameraPosition = _cameraTransform.position;

        _sprite = GetComponent<SpriteRenderer>().sprite;
        _texture = _sprite.texture;
        _textureUnitSizeX = _texture.width / _sprite.pixelsPerUnit;
        _textureUnitSizeY = _texture.height / _sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * _parallaxEffectMultiplier.x, deltaMovement.y * _parallaxEffectMultiplier.y);
        _lastCameraPosition = _cameraTransform.position;


        if (_infiniteHorizontal)
        {
            if (Mathf.Abs(_cameraTransform.position.x - transform.position.x) >= _textureUnitSizeX)
            {
                float offsetPositionX = (_cameraTransform.position.x - transform.position.x) % _textureUnitSizeX;
                transform.position = new Vector3(_cameraTransform.position.x + offsetPositionX, transform.position.y);

                Debug.Log($"Camera: {_cameraTransform.position}, Layer: {transform.position}, OffsetX: {offsetPositionX}");
            }
        }

        if (_infiniteVertical)
        {
            if (Mathf.Abs(_cameraTransform.position.y - transform.position.y) >= _textureUnitSizeY)
            {
                float offsetPositionY = (_cameraTransform.position.y - transform.position.y) % _textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, _cameraTransform.position.y + offsetPositionY);
            }
        }
    }
}
