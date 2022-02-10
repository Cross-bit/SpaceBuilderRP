using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveLoadData
{
    public List<SymetricBlock> blocks;
    public List<string> test_st = new List<string>(6); 

   // public int health;



    public SaveLoadData(List<SymetricBlock> _block)
    {
        blocks = _block;
    }

}
