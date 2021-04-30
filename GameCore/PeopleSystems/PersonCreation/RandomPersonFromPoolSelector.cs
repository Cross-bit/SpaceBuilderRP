using UnityEngine;

namespace Assets.Scripts.GameCore.PeopleSystems.PersonCreation
{
    public class RandomPersonFromPoolSelector : IPersonCreator
    {
        // silence is golden

        public GameObject CreatePerson() {
            return new GameObject();
        }

    }
}
