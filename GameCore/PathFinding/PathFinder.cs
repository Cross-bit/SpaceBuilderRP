using System.Collections.Generic;
using Assets.Scripts.GameCore.API.Extensions;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using Assets.Scripts.GameCore.WorldBuilding.BlockLibrary;

public class PathFinder : Singleton<PathFinder>
{
    // Dijkstrův algoritmus - seznam(tabulka)
    //public Dictionary<Vector3, Dictionary<Vector3, Vector3>> nodes_path_data = new Dictionary<Vector3, Dictionary<Vector3, Vector3>>();


    //List všech uzlů s jejich sousedy a vzdálenostmi
    public List<Node> nodes_data = new List<Node>();

    public List<Vector3> visited = new List<Vector3>();
    public List<Vector3> unvisited = new List<Vector3>();

    public float ctr;
    /*Dictionary<Vector3,Vector3> test_p = new Dictionary<Vector3, Vector3>();

    int num = 1200;

    private void Start()
    {
        for (int i = 0; i < num; i++){
            Vector3 main_nod = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            if (!test_p.ContainsKey(main_nod)) {
                test_p.Add(main_nod, main_nod);
            }
        }
    }*/

    private void OnEnable()
    {
        /*foreach (Block b in Settings.blocks)
        {
            if (b.block.transform.position == new Vector3(0, 0, -18f))
            {
                // Debug.Log("b_c" + c.position)
                foreach (BlockChecker c in b.checkers) {
                    Debug.Log("b_c" + c.position + " " + c.c_nexto.checker_game_obj.position);
                }
            }
        }


        return;*/
        Debug.Log("start");
        nodes_data = new List<Node>();
        nodes_data = LoadAllNodeData();
        Debug.Log(LoadAllNodeData().Count());
#if true
        foreach (Node n in nodes_data) {
            Debug.Log(n.position);

            foreach (Vector3 a in n.neighbours.Keys) {
                Debug.Log(">>" + a);
            }
        }
#endif 
        //   GeneratePath();

    }

    /// <summary>
    /// Vygeneruje data pro pathfinding pro všechny bloky ve scéně. (neefektivní)
    /// </summary>
    /// <returns></returns>
    public List<Node> LoadAllNodeData()
    {
        nodes_data = new List<Node>();

        foreach (SymetricBlock b in BlockLibrary.blocksLib)
        {
            if (b.IsNode)
            {
                //Pokud ještě daný uzel není v seznamu
                if (nodes_data.FindIndex(o => o.position == b.BlockContainer.transform.position) < 0) // Pokud ještě v seznamu není, resp. pokud nenalezneme index
                {
                    // Přidáme data uzlu do hlavního datového pole
                    nodes_data.Add(new Node()
                    {
                        position = b.BlockContainer.transform.position,
                        neighbours = FindBlockNeighbourNodes(b),
                        hasInnerPath = b.HasManualNodes

                    });
                 //   nodes_data.Add(b.block.transform.position, FindBlockNeighbourNodes(b));
                }
            }
        }

        return nodes_data;
    }

    public Dictionary<Vector3, float> FindBlockNeighbourNodes(SymetricBlock b_base)
    {
        Dictionary<Vector3, float> b_neighbour_nodes = new Dictionary<Vector3, float>();

        foreach (BlockChecker b_c in b_base.Checkers)
        {
            if (b_c.checkerNextTo == null)
                continue;

            Dictionary<SymetricBlock, float> b_potencionals = new Dictionary<SymetricBlock, float>();

            SymetricBlock neighbour_b = IsNeighbourNode(b_c);
            if (neighbour_b != null)
            {
                float dist = Vector3.Distance(b_base.BlockContainer.transform.position, neighbour_b.BlockContainer.transform.position);
                b_neighbour_nodes.Add(neighbour_b.BlockContainer.transform.position, dist);
            }
        }

        return b_neighbour_nodes;
    }

    public SymetricBlock IsNeighbourNode(BlockChecker current_c)
    {

        if (current_c.checkerNextTo != null)
        {
            SymetricBlock b_in_row = Helpers.GetBlock(current_c.checkerNextTo.checkers_container.position);
            Vector3 dir_to_current_c = current_c.position.RotateOnY(-b_in_row.BlockRotation.y);

            if (b_in_row != null)
                if (b_in_row.IsNode || b_in_row.HasManualNodes)
                {
                    return b_in_row;
                }
                else
                {

                    if (b_in_row.HasManualNodes)
                    {
                        // TODO: pokud blok má vnitřní nody

                    }

                    foreach (BlockChecker b_in_row_c in b_in_row.Checkers)
                    {
                        Vector3 dir_to_in_row_c = b_in_row_c.position.RotateOnY(-b_in_row.BlockRotation.y);

                        if (dir_to_in_row_c == dir_to_current_c)
                        {
                            return IsNeighbourNode(b_in_row_c);
                        }
                    }
                }
        }
        return null;
    }

    public List<Node> GetPath(Node startNode, Node endNode)
    {
        /*System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
        st.Start()*/

        if (startNode == null || endNode == null) {
            return null;
        }
        
        
        // -- Získáme hrubou cestu

        List<AstarPoint> all_star_data = GenerateAstarPath(startNode, endNode.position);

        // -- Refaktorujemen cestu

        if (all_star_data != null){
            List<Node> finalPath = RefactorPath(endNode.position, startNode.position, all_star_data);
            finalPath.Reverse();

            return finalPath;
        }
        else {
            return null;
        }

        /*st.Stop();
        Debug.Log(string.Format("MyMethod took {0} ms to complete", st.ElapsedMilliseconds));*/
        // debug
        if (false)
        {
            string html = "";
            html += "<table class=\"table table-striped table-dark\">";
            html += "<thead>";
            html += "<th>Vertex</th>";
            html += "<th>Shortest dist</th>";
            html += "<th>Prev vertex</th>";
            html += "</thead>";
            html += "<tbody>";

            if(all_star_data != null)
            foreach (AstarPoint star_data in all_star_data)
            {
                html += "<tr>";

                html += "<td>" + star_data.pointPosition + "</td>";
                html += "<td>" + star_data.distance + "</td>";
                html += "<td>" + star_data.prewPointPosition + "</td>";

                html += "</tr>";

            }
            html += "</tbody>";
            html += "</table>";
            //  Debug.Log(html);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="aStarPath"></param>
    /// <param name="nodeToProcess"> Poslední bod aka první od kterého začínáme. </param>
    /// <param name="firstNode"></param>
    /// <param name="finalPath"></param>
    /// <returns></returns>
    private List<Node> RefactorPath (Vector3 nodeToProcess, Vector3 firstNode, List<AstarPoint> aStarPath, List<Node> finalPath = null) {
        // Rekurzivně, rekurzivně složíme cestu :)

        if (firstNode == null || nodeToProcess == null || aStarPath == null) {
            return null;
        }

      /*  if (nodeToProcess == firstNode)
            return finalPath;*/

        // První kolo
        if (finalPath == null)
            finalPath = new List<Node>();

        AstarPoint aStar = aStarPath.Find(o => o.pointPosition == nodeToProcess);
        if (aStar != null)
        {
            aStarPath.Remove(aStar);
            // Přidáme nový bod
            finalPath.Add( nodes_data.Find(o => o.position == aStar.pointPosition));

            return RefactorPath(aStar.prewPointPosition, firstNode, aStarPath, finalPath);
        }
        else {
            return finalPath;
        }


    }


    public List<AstarPoint> GenerateAstarPath(Node node, Vector3 finalNodePos, List<AstarPoint> d_table = null, List<Vector3> visited_v = null, float closest_dist = float.PositiveInfinity)
    { // TODO: GC ALLOC!!!- MOC (cca. 24 KB)
        if (node == null)
            return null;


        if (node.position == finalNodePos)//A* modifikace
            return d_table;

        // Navštívené body
        if (visited_v == null)
            visited_v = new List<Vector3>();


        //Dijkskrova tabulka pro daný bod: BOD; vzdálenost; předchozí BOD
        if (d_table == null)
        {
            d_table = new List<AstarPoint>(); //new Dictionary<Vector3, KeyValuePair<float, Vector3>>();
            foreach (Node node_ in nodes_data)
            {
                d_table.Add(new AstarPoint() {
                    pointPosition = node_.position,
                    weight = Vector3.Distance(node_.position, finalNodePos),
                    distance = (node_.Equals(node.position) ? 0 : float.PositiveInfinity),
                    prewPointPosition = new Vector3()
                });
            }
        }

        // Momentálně se nacházíme na bodě tzn. navštívili jsme ho tzn. přidáme do visited
        visited_v.Add(node.position);

        List<AstarPoint> unvisited_neighbour_nodes = new List<AstarPoint>();

        foreach (KeyValuePair<Vector3, float> neighbour_to_node in node.neighbours)
        {

            if (!visited_v.Contains(neighbour_to_node.Key))
            {

                AstarPoint tablePointData = d_table.FirstOrDefault(o => o.pointPosition == neighbour_to_node.Key);

                // Přidáme do pole, pro určení následujícího bodu
                unvisited_neighbour_nodes.Add(tablePointData);

                /*if (d_table.ContainsKey(neighbour_to_node.Key))// Pouze ověření, protože vždy bude obsahovat viz foreach nahoře
                {*/
                if (float.IsInfinity(closest_dist)) // Pokud je vzdálenost nekonečná
                {
                    closest_dist = 0 + neighbour_to_node.Value;
                }
                else
                {
                    closest_dist = closest_dist + neighbour_to_node.Value;
                }



                // Pokud je nová vzdálenost (menší)
                if ((tablePointData.distance > closest_dist) /*|| float.IsInfinity(tablePointData.distance)*/)
                {                        
                        float weight = Vector3.Distance(neighbour_to_node.Key, finalNodePos) + closest_dist;
                        tablePointData.weight = weight;
                        tablePointData.distance = closest_dist;
                        tablePointData.prewPointPosition = node.position;
                }

               /* }
                else { Debug.LogError("WTF ?????"); }*/


            }
        }

        // Nalezneme nejbližší uzel
        Node closest_node = null;
        // Získáme nejbližší uzel, pro další návštěvu
        if (unvisited_neighbour_nodes.Count != 0)
        {

            var closest_node_data = unvisited_neighbour_nodes.OrderBy(kvp => kvp.weight).First();
            closest_node = nodes_data.Find(o => o.position == closest_node_data.pointPosition);
        }
        else
        {
            return d_table;
        }

        if (visited_v.Count != (nodes_data.Count - 1))
            return GenerateAstarPath(closest_node, finalNodePos, d_table, visited_v, closest_dist);
        else
            return d_table;


    }

}
