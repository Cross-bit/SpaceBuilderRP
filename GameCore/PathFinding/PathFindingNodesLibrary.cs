using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;

namespace Assets.Scripts.GameCore.PathFinding
{
    public static class PathFindingNodesLibrary
    {

        public static void AddBlockToPathFinding(SymetricBlock blockToAdd) {

            // Pokud se jedná o uzel
            if (blockToAdd.isNode)
            {
                //Pokud ještě daný uzel není v seznamu
                if (PathFinder.Instance.nodes_data.Find(o => o.position == blockToAdd.BlockContainer.transform.position) != null)
                {
                    // Nový diář sousedů
                    Dictionary<Vector3, float> b_neighbour_nodes = new Dictionary<Vector3, float>();

                    // Nalezneme SOUSEDNÍ NODY ve směrech kontrolérů(na stejných pozicích např. checker: Vec3(0,15,0); soused blok Vec3(0,50,0); => (true) - pokud je první! (break)
                    foreach (BlockChecker b_c in blockToAdd.Checkers)
                    {
                        Settings.Axis on_which_axis = Settings.Axis.ZERO;
                        // Podél jaké osy hledáme
                        // Získáme, na jaké OSY jsou kontroléry uzlu orientovány
                        Vector3 b_c_pop_pos = Settings.GetVector3Population_(b_c.position); // TODO: ještě nefunguje - musí se počítat i posun po ostatních osách
                        if (b_c_pop_pos.x != 0)
                            on_which_axis = Settings.Axis.x;
                        else if (b_c_pop_pos.y != 0)
                            on_which_axis = Settings.Axis.y;
                        else if (b_c_pop_pos.z != 0)
                            on_which_axis = Settings.Axis.z;
                        else
                            Debug.LogAssertion("Kontrolér bloku má nejspíše 0 souřadnice, nebylo možné definovat osu pro kontrolu.");


                        // Podíváme se opět na všechny bloky ve scéně 
                        foreach (SymetricBlock b_neighbour in BlockLibrary.blocksLib)
                        {
                            Vector3 b_neighbour_pop_pos = Settings.GetVector3Population_(b_neighbour.BlockContainer.transform.position);
                            // Pokud se nejedná o hlavní blok (blok ke kterému hledáme souseda)
                            if (b_neighbour.BlockContainer.transform.position != blockToAdd.BlockContainer.transform.position)
                            {

                                if (b_neighbour.isNode)
                                {
                                    // Pokud je blok ve stejném směru jako je orientovaný kontrolér && jedná se o uzel => jedná se o souseda
                                    if (((b_neighbour_pop_pos.x == b_c_pop_pos.x) && on_which_axis == Settings.Axis.x) ||
                                        ((b_neighbour_pop_pos.y == b_c_pop_pos.y) && on_which_axis == Settings.Axis.y) ||
                                        ((b_neighbour_pop_pos.z == b_c_pop_pos.z) && on_which_axis == Settings.Axis.z)
                                        )
                                    {
                                        float dist = Vector3.Distance(b_neighbour.BlockContainer.transform.position, b_c.CheckerTransform.transform.position); // Vypočítáme vzdálenost mezi uzly
                                        b_neighbour_nodes.Add(b_neighbour.BlockContainer.transform.position, dist);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    // Přidáme blok do hlavního datového pole
                    PathFinder.Instance.nodes_data.Add(new Node() { position = blockToAdd.BlockContainer.transform.position, neighbours = b_neighbour_nodes });
                }
            }
            else
            {
                // TODO: Pokud není, musí se domyslet => nějaký vnitřní ručně přidaný path-pointy(asi model/grafika) apod.
            }
        }
    }
}
