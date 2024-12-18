using UnityEngine;

public class CharacterFactory
{
   private Character _character;

   public CharacterFactory()
   {
      _character = Resources.Load<Character>("Prefabs/Character/Player");
   }

   public Character CreateCharacter(Transform position)
   {
       Character character  = Object.Instantiate(_character, position.position, Quaternion.identity);
       return character;
   }
}
