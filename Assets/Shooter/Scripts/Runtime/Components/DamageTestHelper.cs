using UnityEngine;
using Blocks.Gameplay.Core;

namespace Blocks.Gameplay.Shooter
{
    /// <summary>
    /// Ayudante para probar la recepción de daño de los jugadores desde el Inspector.
    /// </summary>
    public class DamageTestHelper : MonoBehaviour
    {
        [Header("Test Settings")]
        [Tooltip("Cantidad de daño a aplicar en la prueba.")]
        [SerializeField] private float damageAmount = 25f;

        [Tooltip("El procesador de golpes del jugador. Si se deja vacío, buscará uno en la escena al hacer la prueba.")]
        [SerializeField] private ShooterHitProcessor targetProcessor;

        /// <summary>
        /// Aplica un golpe falso al jugador. Accesible mediante clic derecho en el componente en Play Mode.
        /// </summary>
        [ContextMenu("Apply Test Damage")]
        public void ApplyTestDamage()
        {
            if (targetProcessor == null)
            {
#if UNITY_2023_1_OR_NEWER
                targetProcessor = FindAnyObjectByType<ShooterHitProcessor>();
#else
                targetProcessor = FindObjectOfType<ShooterHitProcessor>();
#endif
            }

            if (targetProcessor == null)
            {
                Debug.LogError("[DamageTestHelper] No se encontró ningún ShooterHitProcessor activo en la escena.");
                return;
            }

            // Crear un HitInfo simulado.
            // Para el 'attackerId', usamos el del jugador + 999 para asegurarnos de que el script
            // no lo considere daño propio (self-damage), el cual se ignora por diseño.
            HitInfo fakeHit = new HitInfo
            {
                amount = damageAmount,
                hitPoint = targetProcessor.transform.position + Vector3.up * 1.0f,
                hitNormal = Vector3.forward,
                attackerId = targetProcessor.OwnerClientId + 999,
                impactForce = Vector3.zero
            };

            Debug.Log($"[DamageTestHelper] Enviando impacto de prueba con {damageAmount} de daño a {targetProcessor.name}");
            targetProcessor.OnHit(fakeHit);
        }
    }
}
