using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : ILiveOrganism
{
    Settings.Gender gender;

    public string OrganismName { get; set; }
    public float OrganismAge { get; set; }
    public int OrganismHealth { get; set; }

    public GameObject person { get; }
    List<Node> currentPath; // TODO:


    public Person(float age, GameObject person)
    {
        OrganismAge = age;
    }


    public SymBlock GetCurrentBlock(Node nodeAt)
    {
        return Helpers.GetBlock(nodeAt.position);
    }


}
