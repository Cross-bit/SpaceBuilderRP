using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrapCodes
{

    // Vytvoří globalní pozice checkeru
    /*public Vector3 GetCheckerGlobalCordinates(BlockChecker b_checker)
    {
        Vector3 global_pos = b_checker.position + b_checker.checkers_container.position;
        return global_pos;
    }*/


    // Získá checker objektu, podle pozice parent objektu TODO: smaž jednu, jsou to ty samé funkce...
    /*public BlockChecker GetCheckerLocal(Vector3 _position = new Vector3())
    {
        BlockChecker block_c = null;

        // Projdeme všechny záznamy checkerů
        foreach (BlockChecker c in checkers)
        {
            if (c.position == _position)
            {
                block_c = c;
                break;
            }
        }


        // Vrátíme (Jop nejde to dát do 1x loopy, musí to být takto kokotsky...)
        if (block_c != null)
        {
            return block_c;
        }
        else
        {
            return null;
        }

    }*/

    // Získá grafiku chekerů na modelu, pro změnu barvy podle typu apod. HM vážně možná dost zbytečná funkce? :|
    /*Dictionary<GameObject, BlockChecker> GetGraphicsAndCheckerPairs(string checker_name = "checker")
    {
        Dictionary<GameObject, BlockChecker> checkers_g = new Dictionary<GameObject, BlockChecker>();

        Transform[] all_graphics = main_graphics.GetComponentsInChildren<Transform>();

        foreach (Transform obj in all_graphics)
        {
            string obj_name = obj.gameObject.name.ToLower();

            if (obj_name.Contains(checker_name))
            {

                // Musíme převést globální souřadnice objektu checkeru na lokální objektu
                Vector3 localCheckerPos = obj.position - block.transform.position;

                Vector3 roundedPos = Settings.GetRoundedPosition(localCheckerPos);

                bool isPosOk = Settings.Vector3Exists(roundedPos, checkers_position);

                if (isPosOk)
                {
                    BlockChecker bl_c = GetCheckerLocal(localCheckerPos);
                    checkers_g.Add(obj.gameObject, bl_c);
                    // Změň materiál podle bl_c.checkerType
                }
                else
                {
                    Settings.ThrowError(" U bloku s názvem:"+ obj_name + " o pozici: "+ obj.transform.position +" v ifu(bool), řádek: 194, neexistuje taková pozice, objektu, jaká je uvedena u checkeru!!!");
                }
                // checker_graphics.Add(obj);
            }
        }

        return checkers_g;
    }*/

}
#if false
public class CameraController : MonoBehaviour
{
    [Header("Obecné nastavení")]
    public float camRotateSpeed;
    public float camZoomSpeed;
    private float camTransformSpeed = 10f;
    [Space]
    private float maxHeight = 14.06f;
    public float minHeight;

    [Header("")]
    public float maxRotateAngle;
    public float minRotateAngle;

    private float cameraSmooth = 15f;

    private Vector3 camOffset = new Vector3(0, 0, 5f);

    /*[Header("Faktor rychlosti přibližování")]
    [Tooltip("Pokud hráč začne zoomovat, tak tohle říká jak rychle, až do maxima.")]
    public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));*/

    [SerializeField]
    private float mouseScroll;


    [Header("Input")]
    //public KeyCode 

    [Header("Cíl pro follow")]
    public bool isCameraFollowingTarget;

    [Header("Levly po ose z")]
    [Tooltip("Výška země. (také se dá označit jako 0 level, na kterém může hráč operovat.)")]
    public float[] defaultGroundHigh = new float[1] { 14.06f };

    public Transform defaultFollowTarget;

    public Camera cam;

    float currentX = 0;
    float currentY = 0;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        if (defaultFollowTarget == null)
        {
            defaultFollowTarget = new GameObject("Camera focus point.").transform;
            defaultFollowTarget.position = new Vector3(0, defaultGroundHigh[0], 0);
        }


        //  if (Manager.Instance.mainCamera != null)
        cam = Camera.main; //Manager.Instance.mainCamera;

        /*
         * TODO Načítání z savu
         */

        // defaultní 
        cam.transform.position = defaultFollowTarget.position + camOffset;


        //  cam.transform.LookAt(defaultFollowTarget.position);
    }

    void FixedUpdate()
    {
        Vector3 translation = GetInputTranslationDirection() * Time.deltaTime * camTransformSpeed;
        mouseScroll = Input.mouseScrollDelta.y;
        //  Debug.Log(mouseScroll);
        //camOffset *= mouseScroll;

        camOffset.z -= mouseScroll;

        // camZoomSpeed
        defaultFollowTarget.Translate(translation, Space.World);

        if (Input.GetMouseButton(0))
        {
            currentX += Input.GetAxis("Mouse Y") * camRotateSpeed;
            currentY += Input.GetAxis("Mouse X") * camRotateSpeed;

            currentX = Mathf.Clamp(currentX, -80, -25);
        }


    }

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion camTurnAngles = Quaternion.Euler(currentX, currentY, 0);
        Vector3 roatatedPos = defaultFollowTarget.position + camTurnAngles * camOffset;

        defaultFollowTarget.rotation = Quaternion.Euler(defaultFollowTarget.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y, defaultFollowTarget.rotation.eulerAngles.z);

        cam.transform.position = Vector3.Lerp(cam.transform.position, roatatedPos, 500f);
        cam.transform.LookAt(defaultFollowTarget.transform);
    }


    // TODO: Později vytáhnout do separátního souboru
    Vector3 GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            /*if (AddAfterDistCheck(transform.forward))
            {*/
            direction += defaultFollowTarget.forward;
            //}
        }
        if (Input.GetKey(KeyCode.S))
        {
            /*if (AddAfterDistCheck(-transform.forward))
            {*/
            direction -= defaultFollowTarget.forward;
            //}
        }
        if (Input.GetKey(KeyCode.A))
        {
            /*if (AddAfterDistCheck(-transform.right))
            {*/
            direction -= defaultFollowTarget.right;
            //}
        }
        if (Input.GetKey(KeyCode.D))
        {
            /* if (AddAfterDistCheck(transform.right))
             {*/
            direction += defaultFollowTarget.right;
            //}
        }

        return direction;
    }



    void OnDrawGizmos()
    {
        if (defaultFollowTarget != null)
        {
            // Draw a yellow sphere at the transform's position
            /*   Gizmos.color = Color.yellow;
               Gizmos.DrawSphere(defaultFollowTarget.position, 1);*/
        }
    }

}
*/

// Staré Kontroly orientace boku
                    // 4. KONTROLA - KONTROLUJE SE, ZDA KONCOVÉ BODY DANÉHO BLOKU (KONTROLÉRY) NEZASAHUJÍ DO DEFINIČNÍ OBLASTI BLOKU JINÉHO./*Settings.GetRotated3Dy(c.checker_game_obj.transform.position, this.block_rotation.y).x*/
                    /*float c_x = c.checker_game_obj.transform.position.x;
                    float c_z = c.checker_game_obj.transform.position.z;

                    float b_around_x = (Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).x + b_around.block_dimensions.x / 2);
                    float b_around_x_ = (Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).x - b_around.block_dimensions.x / 2);
                    float b_around_z = (Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).z + b_around.block_dimensions.z / 2);
                    float b_around_z_ = (Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).z - b_around.block_dimensions.z / 2);*/
                        if ( // PRO X 
                       (Settings.RoundOnTwoDigits(c.checker_game_obj.transform.position.x) > (Settings.RoundOnTwoDigits(Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).x - b_around.block_dimensions.x / 2))) &&
                       (Settings.RoundOnTwoDigits(c.checker_game_obj.transform.position.x) < Settings.RoundOnTwoDigits((Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).x + b_around.block_dimensions.x / 2)))
                       && // PRO Z 
                       (Settings.RoundOnTwoDigits(c.checker_game_obj.transform.position.z) > Settings.RoundOnTwoDigits((Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).z - b_around.block_dimensions.z / 2))) &&
                       (Settings.RoundOnTwoDigits(c.checker_game_obj.transform.position.z) < Settings.RoundOnTwoDigits((Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).z + b_around.block_dimensions.z / 2))))
                       {
                        Debug.Log("Selhala 4.");

#region DEBUG 4 
                        /*Debug.Log(Settings.GetRotated3Dy(c.checker_game_obj.transform.position, this.block_rotation.y) + "(x)" + ">" + (Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).x - b_around.block_dimensions.x / 2));
                        Debug.Log("&&");
                        Debug.Log(Settings.GetRotated3Dy(c.checker_game_obj.transform.position, this.block_rotation.y)+ "(x)" +  "<" + (Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).x + b_around.block_dimensions.x / 2));
                        Debug.Log("&&");
                        Debug.Log(Settings.GetRotated3Dy(c.checker_game_obj.transform.position, this.block_rotation.y) + "(z)" + ">" + (Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).z - b_around.block_dimensions.z / 2));
                        Debug.Log("&&");
                        Debug.Log(Settings.GetRotated3Dy(c.checker_game_obj.transform.position, this.block_rotation.y) + "(z)" + "<" + (Settings.GetRotated3Dy(b_around.block.transform.position, b_around.block_rotation.y).z + b_around.block_dimensions.z / 2));
                        Debug.Log("Selhala 4.");*/
#endregion
                        return false;
                    } // Nefunguje správně nechávám jen na ray



        // 5. KONTROLA - KONTROLUJE SE, JESTLI DANÝ BLOK NEKŘIŽUJE BLOK JINÝ.

        // -- ZAČÁTEK 5. KONTROLY - VYPNEME VEŠKERÉ NEŽÁDOUCÍ KONTROLÉRY

      /*  foreach (Block b_around in all_blocks_near) // TODO: Mohlo by být efektivnější s využitím znalosti rotace(tzn. get Objet rotation)
        {
            foreach (BlockChecker b_around_c in b_around.checkers)
            {
                b_around_c.checkerCollider.enabled = false;
            }
        }

        // Vypneme vlastní Collidery bloku
        SetBlockCollidersTo(false);
        UpdateCheckersActivity(true, false);

        foreach (BlockChecker c in checkers)
        {
            Vector3 position_pop = Settings.GetVector3Population_(c.position);
            // Debug.Log(position_pop);

            if (position_pop.x >= 0 && position_pop.y >= 0 && position_pop.z >= 0)
            {
                RaycastHit hitData;
                Ray ray = new Ray(c.checker_game_obj.transform.position, position_pop * -3f);

#region Debug ray
                block_controller.drawRay = true;
                block_controller.origin.Add(c.checker_game_obj.transform.position);
                block_controller.direction.Add(position_pop * -1);
                block_controller.lenght = 3f;
#endregion

                // Pokud jsme nalezli intersekci
                if (Physics.Raycast(ray, out hitData, 3f))
                {
                    Debug.Log(hitData.transform.gameObject.name);
                    //a return
                    isOrientationValid = false;
                    Debug.Log("Selhala 5.");
                    break;
                }
            }
        }

        // -- NÁVRAT DO NORMÁLU

        // Vrátíme vše do normálu
        SetBlockCollidersTo(true);
        UpdateCheckersActivity(false);

        foreach (Block b_around in all_blocks_near) // TODO: Mohlo by být efektivnější s využitím znalosti rotace(tzn. get Objet rotation)
        {
            foreach (BlockChecker b_around_c in b_around.checkers)
            {
                b_around_c.UpdateChecker(true, false);
            }
        }*/


#endif

#if false


// Hledání sousedních bloků

        //continue;
        // Nový diář sousedů
        //Dictionary<Vector3, float> b_neighbour_nodes = new Dictionary<Vector3, float>();

        // Nalezneme SOUSEDNÍ NODY ve směrech kontrolérů(na stejných pozicích např. checker: Vec3(0,15,0); soused blok Vec3(0,50,0); => (true) - pokud je první! (break)
        foreach (BlockChecker b_c in b_base.checkers)
        {
            Settings.Axis on_which_axis = Settings.Axis.ZERO;
            // Podél jaké osy hledáme
            // Získáme, na jaké OSY jsou kontroléry uzlu orientovány
           // Debug.Log("TADA"+Helpers.GetVectorValueByAxis(new Vector3(4,0,0), Settings.Axis.x)); 

            Vector3 b_c_pop_pos = Settings.GetVector3Population_(b_c.position); // TODO: ještě nefunguje - musí se počítat i posun po ostatních osách
            if (b_c_pop_pos.x != 0)
                on_which_axis = Settings.Axis.x;
            else if (b_c_pop_pos.y != 0)
                on_which_axis = Settings.Axis.y;
            else if (b_c_pop_pos.z != 0)
                on_which_axis = Settings.Axis.z;
            else
                Debug.LogAssertion("Kontrolér bloku má nejspíše 0 souřadnice, nebylo možné definovat osu pro kontrolu.");

            //Debug.Log(on_which_axis);
            Dictionary<Vector3, float> possible_b_neighbour = new Dictionary<Vector3, float>();

            foreach (Block b_neighbour in Settings.blocks)
            {
                if (b_neighbour.block.transform.position != b_base.block.transform.position)
                {
                    if (b_neighbour.isNode)
                    {
                        if (((b_neighbour.block.transform.position.z == b_base.block.transform.position.z) && on_which_axis == Settings.Axis.x) || ((b_neighbour.block.transform.position.x == b_base.block.transform.position.x) && on_which_axis == Settings.Axis.z))
                        {
                            //
                            if(b_base.block.transform.position == new Vector3(3f,0f,-12f))
                            if (on_which_axis == Settings.Axis.x){
                                float dist = b_neighbour.block.transform.position.x - b_base.block.transform.position.x;
                                Debug.Log("Soused:" + b_neighbour.block.transform.position + " - " + b_base.block.transform.position + " " + dist /**(Helpers.GetVectorValueByAxis(b_c_pop_pos, on_which_axis))*/ + " Kontroler:" + b_c.position);

                            }
                            else if (on_which_axis == Settings.Axis.z) {
                            }

                            //possible_b_neighbour.Add(b_neighbour.block.transform.position, );

                           /* if (!b_neighbour_nodes.ContainsKey(b_neighbour.block.transform.position)) {


                                //Debug.Log(b_c.position);
                                float dist = Vector3.Distance(b_neighbour.block.transform.position, b_c.checker_game_obj.transform.position); // Vypočítáme vzdálenost mezi uzly
                                b_neighbour_nodes.Add(b_neighbour.block.transform.position, dist);
                                break;
                            }*/
                        }
                    }
                }
            }

            /*foreach (Vector3 possible_neigh_b in possible_b_neighbour){

            }*/



            continue;
                Debug.Log(b_c_pop_pos);
            Debug.Log(on_which_axis);

            float offset_value = Helpers.GetVectorValueByAxis(b_c_pop_pos, on_which_axis);
            Debug.Log(offset_value);
            // Podíváme se opět na všechny bloky ve scéně 
            foreach (Block b_neighbour in Settings.blocks)
            {
                Vector3 b_neighbour_pop_pos = Settings.GetVector3Population_(Settings.vec3Add(b_neighbour.block.transform.position, offset_value*(-1)));
                // Pokud se nejedná o hlavní blok (blok ke kterému hledáme souseda)
                if (b_neighbour.block.transform.position != b_base.block.transform.position)
                {

                    if (b_neighbour.isNode)
                    {
                        if (b_base.block.transform.position == new Vector3(15, 0, 0) && on_which_axis == Settings.Axis.x) {
                                Debug.Log(b_neighbour_pop_pos);
                            /*if (b_neighbour_pop_pos.x == b_c_pop_pos.x) {
                            }*/
                            /*Debug.Log(b_c_pop_pos);
                            Debug.Log(b_neighbour_pop_pos);
                            Debug.Log(b_neighbour.block.transform.position);*/
                            }

                            // Pokud je blok ve stejném směru jako je orientovaný kontrolér && se jedná o uzel => jedná se o souseda
                            if (((b_neighbour_pop_pos.x == b_c_pop_pos.x) && (b_neighbour.block.transform.position.z == b_base.block.transform.position.z) && on_which_axis == Settings.Axis.x) ||
                            ((b_neighbour_pop_pos.y == b_c_pop_pos.y) && on_which_axis == Settings.Axis.y) ||
                            ((b_neighbour_pop_pos.z == b_c_pop_pos.z) && (b_neighbour.block.transform.position.x == b_base.block.transform.position.x) && on_which_axis == Settings.Axis.z)
                            )
                        {
                            float dist = Vector3.Distance(b_neighbour.block.transform.position, b_c.checker_game_obj.transform.position); // Vypočítáme vzdálenost mezi uzly
                            b_neighbour_nodes.Add(b_neighbour.block.transform.position, dist);
                            break;
                        }
                    }
                }
            }
        }

        return b_neighbour_nodes;







// Skoro Ok část, ale neumí řešit případy, kdy 


    public Dictionary<Vector3, float> FindBlockNeighbourNodes(Block b_base)
    {
        Dictionary<Vector3, float> b_neighbour_nodes = new Dictionary<Vector3, float>();

        foreach (BlockChecker b_c in b_base.checkers){

            if (b_c.c_nexto == null)
                continue;

            Dictionary<Block, float> b_potencionals = new Dictionary<Block, float>();


            /*Vector3 b_c_pop_pos = Settings.GetVector3Population_(b_c.position);
            Vector3 dir_to_check = Settings.GetRotated3Dy(b_c_pop_pos, -b_base.block_rotation.y); // Natočíme zpět => ()*/



            /*foreach (Block b_other in Settings.blocks) { vs 1
                if (b_other == b_base || !b_other.isNode)
                    continue;

                float distance = 0;
                if (b_other.block.transform.position.z == b_base.block.transform.position.z)
                {
                    if (dir_to_check.x != 0)
                    {
                        distance = Mathf.Abs(b_c.checker_game_obj.transform.position.x - b_other.block.transform.position.x);
                        b_potencionals.Add(b_other, distance);
                    }
                }
                else if (b_other.block.transform.position.x == b_base.block.transform.position.x)
                {
                    if (dir_to_check.z != 0)
                    {
                        distance = Mathf.Abs(b_c.checker_game_obj.transform.position.z - b_other.block.transform.position.z);
                        b_potencionals.Add(b_other, distance);
                    }
                }
            }*/




            /*  if (b_potencionals.Count != 0) vs 1
              {
                  var closest_block = b_potencionals.OrderBy(kvp => kvp.Value).First();

                  if (b_neighbour_nodes.ContainsKey(closest_block.Key.block.transform.position)) {

                      if (closest_block.Value < b_neighbour_nodes[closest_block.Key.block.transform.position]) {
                          b_neighbour_nodes[closest_block.Key.block.transform.position] = closest_block.Value;
                      }
                  }
                  else {
                      b_neighbour_nodes.Add(closest_block.Key.block.transform.position, closest_block.Value);
                  }

              }*/

            foreach (KeyValuePair<Block, float> b_pot in b_potencionals) {
                
                //Debug.Log("B-base:\n"+b_base.block.transform.position + "\n c_dir:" + dir_to_check + "B-poten:\n" + b_pot.Key.block.transform.position + "dist:\n" + b_pot.Value);
            }


        }
        return b_neighbour_nodes;
    }



// Dijkstra classic



    public Dictionary<Vector3, KeyValuePair<float, Vector3>> GenerateDijkstrasTable(Node node, Vector3 finalNode, Dictionary<Vector3, KeyValuePair<float, Vector3>> d_table = null, List<Vector3> visited_v = null, float closest_dist = 11111.128391f)
    {

        if (node.position == finalNode)//A* modifikace
            return d_table;

        // Navštívené body
        if (visited_v == null)
            visited_v = new List<Vector3>();


        //Dijkskrova tabulka pro daný bod: BOD; vzdálenost; předchozí BOD
        if (d_table == null) {
            d_table = new Dictionary<Vector3, KeyValuePair<float, Vector3>>();
            foreach (Node node_ in nodes_data) {
                if (node_.Equals(node.position)){
                    d_table.Add(node_.position, new KeyValuePair<float, Vector3>(0, new Vector3()));
                }
                else {
                    d_table.Add(node_.position, new KeyValuePair<float, Vector3>(11111.128391f, new Vector3()));
                }
            }
        }

        
        visited_v.Add(node.position);


        Dictionary<Vector3, float> unvisited_neighbour_nodes = new Dictionary<Vector3, float>();
        Dictionary<Vector3, float> weights = new Dictionary<Vector3, float>();// Pro A star 

        foreach (KeyValuePair<Vector3, float> neighbour_to_node in node.neighbours) {

//            Debug¨.Log(!visited_v.Contains(neighbour_to_node.Key))

            if (!visited_v.Contains(neighbour_to_node.Key)) {
                // Přidáme do pole, pro určení následujícího bodu
                unvisited_neighbour_nodes.Add(neighbour_to_node.Key, neighbour_to_node.Value);
                if (d_table.ContainsKey(neighbour_to_node.Key))// Pouze ověření, protože vždy bude obsahovat viz foreach nahoře
                {
                    if (closest_dist == 11111.128391f){
                        closest_dist = 0 + neighbour_to_node.Value;
                    }
                    else {
                        closest_dist = closest_dist + neighbour_to_node.Value;
                    }
                        
                    // Pokud je nová vzdálenost 
                    if ((d_table[neighbour_to_node.Key].Key > closest_dist) || (d_table[neighbour_to_node.Key].Key != 11111.128391f)) {
                        d_table[neighbour_to_node.Key] = new KeyValuePair<float,Vector3>(/*neighbour_to_node.Value*/ closest_dist, node.position);
                        float weight = Vector3.Distance(neighbour_to_node.Key, finalNode) + closest_dist;
                        weights.Add(neighbour_to_node.Key, weight);
                    }

                }
                else { Debug.LogError("WTF?????"); }


                }
            }

        var closest_node_data = new Node();
        // Získáme nejbližší uzel, pro další návštěvu
        if (unvisited_neighbour_nodes.Count != 0) {
            var closest_node = unvisited_neighbour_nodes.OrderBy(kvp => kvp.Key).First();
            closest_node_data = nodes_data.Find(o => o.position == closest_node.Key);
        }
        else
        {
            return d_table;
        }

            if (visited_v.Count != (nodes_data.Count - 1)) {
                return GenerateDijkstrasTable(closest_node_data
                    , finalNode, d_table, visited_v, closest_dist);
            }
            else
                return d_table;


    }


        // Ověření, zda je gamesa načtená
        /*  byte correct_scenes_ctr = 0;
          foreach (string scene_name in Settings.loadedScenes.Keys)
          {
              if (scene_name == Settings.MAIN_SCEEN_NAME || scene_name == Settings.UI_SCEEN_NAME)
                  correct_scenes_ctr++;
          }

          if (correct_scenes_ctr != Settings.ACTIVE_SCENES_COUNT)
              Settings.ThrowError("NEBYLY NAČTENY VEŠKERÉ SCÉNY!!!!!", true);
          else
        Settings.isGameLoaded = true;*/ // TODO: Rozšířenější kontrola, než pouze podle aktivních scén

#endif
#if false
// Update is called once per frame
void Update()
{
    /* if (InputManager.Instance.GeneralInputs.IsPlayerInteracting)
         RayOnMousePressed();*/

    //  Settings.rotate = rotate; //TODO: odstranit
    //  Settings.highlightBlocksINRange = this.highlightBlocksINRange;
    //
    //  if (/*Input.GetKeyDown(KeyCode.Mouse0)  &&*/ Settings.canInteract)
    //  {
    //
    //      // Ray data
    //      RaycastHit hitData;
    //      Ray ray = Manager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);
    //
    //      if (Physics.Raycast(ray, out hitData, Settings.hitDistance) && !Settings.IsPointerOverUIObject())
    //      {
    //          // -- 1. FOCUS LEVEL - Statistiky bloku
    //          if (hitData.transform.gameObject.layer == LayerMask.NameToLayer(Settings.BLOCK_LAYER))
    //          {
    //              
    //              // Získáme blok na který se kliklo
    //              lastActiveBlock = Helpers.GetBlock(hitData.transform.position); // Podíváme se do databáze bloků
    //
    //              if(lastActiveBlock != null)
    //              if (lastActiveBlock.isBlockBuilded)
    //              {
    //                  // Zapneme okno bloku
    //                  UI.BlockDetailWindowState(true, lastActiveBlock); // On okna
    //
    //                  AudioManager.Instance.Play(Settings.SoundEffects.mouse_click_1);
    //
    //                      //Block.RotateBlockForCertainAngle(lastActiveBlock, new Vector3(0,20,0));
    //                     /* lastActiveBlock.RotateBlock(new Vector3(0, 20, 0));
    //                      foreach (var item in lastActiveBlock.Checkers)
    //                      {
    //                          Debug.LogError(item.Checker_transform);
    //                      }*/
    //                  }
    //              else // Pokud blok ještě není postaven
    //              {
    //
    //              }
    //          }
    //
    //
    //          // -- 2. FOCUS LEVEL - Build bloků na vybraný checker
    //          if (hitData.transform.gameObject.layer == LayerMask.NameToLayer(Settings.CHECKER_LAYER))
    //          {
    //              //  BUILDMODE === OFF; Před započetím vypneme (reset kontrolérů apod.)
    //              TerminateBuildMode();
    //
    //              // HITDATA
    //              //Pozice pro umístění 
    //              Vector3 placePos = hitData.transform.position; //!!! GLOBÁLNÍ POZICE CHECKERU !!!
    //              GameObject checker_obj = hitData.transform.gameObject; // objekt(grafika checkeru) na kterou jsme klikli
    //              Vector3 checkers_pos = checker_obj.transform.parent.gameObject.transform.position; //!!! GLOBÁLNÍ POZICE Bloku (GameObjektu)!!!
    //              
    //              // Získáme blok kontroléru, na který se kliklo
    //              lastActiveBlock = Helpers.GetBlock(checkers_pos);
    //
    //              if (lastActiveBlock != null)
    //              {
    //                  // Vynulujeme checker
    //                  lastActiveChecker = null;
    //                  
    //                  // Tady se dohledá checker na který se kliklo (v db)
    //                  foreach (BlockChecker c in lastActiveBlock.Checkers)
    //                  {
    //                      Vector3 checker_pos = c.Checker_transform.position; // GLOBÁLNÍ POZICE CHECKERU
    //
    //                      // Pokud se pozice daného checkeru rovná pozici kterou nám vrátili hit data
    //                      if (checker_pos == placePos)
    //                      {
    //                          // Tak se jedná o last active
    //                          lastActiveChecker = c;
    //
    //                          // Ujistíme se, že jsou dány správně codinates, jinak přepíšeme na správné
    //                          if (lastActiveChecker.checkers_container.position != lastActiveBlock.blockPosition)
    //                          {
    //                              lastActiveChecker.checkers_container.position = lastActiveBlock.blockPosition;
    //                          }
    //                          break;
    //                      }
    //                  }
    //                  /*Debug.Log("TUNAJ");
    //                  Debug.Log(lastActiveChecker.position);
    //                  Debug.Log(lastActiveChecker.checker_game_obj.position);*/
    //
    //                  //  BUILDMODE === ON; především zapneme
    //                  BuildModeON(lastActiveChecker); // Pošleme na jakém checkeru chceme stavět
    //              }
    //
    //              // Zvuk po kliknutí na kontrolér
    //              AudioManager.Instance.Play(Settings.SoundEffects.mouse_click_1);
    //          }
    //      }
    //  }
    //
    //  // TERMINATION
    //  if (Input.GetKeyDown(KeyCode.Escape))
    //  {
    //      Settings.isCamera = true;
    //
    //      if (Settings.isBuildMode && Settings.isPlacingBlock)
    //          this.TerminateBlockPlace();
    //      else if(Settings.isBuildMode)
    //          this.TerminateBuildMode();
    //  }
    //
}
#endif


// Pěkně kokotská ochrana, ale když je kód mess.... TODO: idiote
// z rotate block
/*if (this.rotateForAngle == new Quaternion())
{
    Debug.Log("Padne to sem někdy???");
    RotateBlockTo();
    RotateBlockCheckers();
    return;
}*/

