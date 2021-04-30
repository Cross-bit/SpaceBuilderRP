using Assets.Scripts.GameCore.PeopleSystems.PersonCreation;
using System.Collections.Generic;


namespace Assets.Scripts.GameCore.PeopleSystems
{
    public class PeopleMaternity
    {
        public static List<Person> AllPeople;

        public SymBlock MaternityBlock;

        public IPersonCreator PersonCreator = new RandomPersonFromPoolSelector();

        public Person BornPerson() {
            var personGameObject = PersonCreator.CreatePerson();

            return new Person(0, personGameObject);

        }

        public void KillPerson() {

        }

    }
}
