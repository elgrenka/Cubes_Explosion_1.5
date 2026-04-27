using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
	[Header("Настройки взрыва")]
	[SerializeField] private float _explosionForce = 100f;
	[SerializeField] private float _explosionRadius = 100f;
	[SerializeField] private float _upwardsModifier;
	[SerializeField] private GameObject _effect;
	[SerializeField] private float _effectDuration = 4f;

	public void PlayExplosionEffect(Vector3 position)
	{
		if (_effect is null)
			return;

		GameObject explosion = Instantiate(_effect, position, Quaternion.identity);
		Destroy(explosion, _effectDuration);
	}
}