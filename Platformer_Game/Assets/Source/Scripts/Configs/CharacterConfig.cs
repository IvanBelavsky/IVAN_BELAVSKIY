
    using UnityEngine;

    [CreateAssetMenu (menuName = "Configs/CharacterConfig", fileName = "CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public GroundedStateConfig GroundedStateConfig { get; private set; }
        [field: SerializeField] public AirBornStateConfig AirBornState { get; private set; }
    }
