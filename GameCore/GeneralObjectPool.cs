using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Settings;


[System.Serializable]
public class GeneralObjectPool
{
    public List<PoolObjectSO> poolDatas;
    public Dictionary<PoolTypes, Queue<GameObject>> poolDictionary;
    public Dictionary<PoolTypes, GameObject> poolParents;


    public GeneralObjectPool(List<PoolObjectSO> objectsToPool){
        this.poolDatas = objectsToPool;
    }

    public void InitializeObjectsToPool()
    {
        poolDictionary = new Dictionary<PoolTypes, Queue<GameObject>>();

        // Inicializujeme nový dic. pro rodiče poolů
        poolParents = new Dictionary<PoolTypes, GameObject>();

        // Projde všechny typy poolu
        foreach (PoolObjectSO poolData in poolDatas)
        {
            // Inicializace que pro daný typ poolu
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // Pokud ještě daný rodič není => vytvoříme nového
            GameObject new_pool_parent = new GameObject("POOL_PAR:" + poolData.poolType.ToString());  // Pro každy typ poolu generujeme nového rodiče
            // Pokud se jedná o UI
            if (poolData.isUI) {
                string type_s = poolData.poolType.ToString();
                if (!type_s.ToLower().Contains("ui")){
                    Debug.LogAssertion("Pool objekt"+type_s+ " typu UI, neobsahuje v názvu identifikátor UI!!!");
                }

                if (poolData.canvasDefParent == Settings.CanvasTypes.FLOAT)
                    new_pool_parent.transform.SetParent(ScreenUIManager.Instance.mainScreenCanvas.transform);
                if (poolData.canvasDefParent == Settings.CanvasTypes.STATIC)
                    new_pool_parent.transform.SetParent(ScreenUIManager.Instance.mainScreenStaticCanvas.transform);
                if (poolData.canvasDefParent == Settings.CanvasTypes.WORLD)
                    new_pool_parent.transform.SetParent(ScreenUIManager.Instance.mainWorldCanvas.transform);
            }


            // Spawn Nových objektů z poolu na 
            for (int i = 0; i < poolData.amount; i++)
            {
                GameObject obj;

                if (poolData.objectToPool != null){
                    obj = GameObject.Instantiate(poolData.objectToPool);
                }
                else{
                    obj = new GameObject(poolData.poolType.ToString());
                }

                // Nastavím nově vytvořeného RODIČE
                obj.transform.SetParent(new_pool_parent.transform);

                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            // Přidáme rodiče do seznamu
            poolParents.Add(poolData.poolType, new_pool_parent);

            // Přidáme queue objektů do pool deníku
            poolDictionary.Add(poolData.poolType, objectPool);

        }

    }



    public GameObject GetFromPool(PoolTypes type, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        parent = null;
        GameObject pooledObj;

        if (poolDictionary[type].Count > 0)
        {
            pooledObj = poolDictionary[type].Dequeue(); // Získáme poslední objekt z řady

        }
        else 
        { //Jinak musíme do polu přidat nový
            pooledObj = AddToPool(type);

            if (poolDictionary[type].Count > 0)
                pooledObj = poolDictionary[type].Dequeue();
        }

        if (parent == null)
        {
            poolParents.TryGetValue(type, out GameObject pool_parent);
            if (pool_parent != null) 
                pooledObj.transform.SetParent(pool_parent.transform);
        }
        else
        {
            pooledObj.transform.SetParent(parent);
        }

        
        pooledObj.transform.position = position;
        pooledObj.transform.rotation = rotation;

        pooledObj.SetActive(false);

        return pooledObj;

    }

    public GameObject AddToPool(PoolTypes type)
    {
        foreach (PoolObjectSO poolData in poolDatas)
        {
            if (poolData.poolType == type && poolData.canExpand == true)
            {
                if (!poolData.canExpand)
                {
                    Debug.LogError("Pole u poolu objektů je potřeba expandovat, nastavení objektu akci však nepovoluje");
                    return null;
                }

                GameObject obj = GameObject.Instantiate(poolData.objectToPool);
                poolDictionary[type].Enqueue(obj);
                return obj;
            }
        }
        return null;
    }

    public void ReturnToPool(PoolTypes type, GameObject toEnqueue/*, bool setDefaultParent = true*/)
    {
        poolDictionary[type].Enqueue(toEnqueue); // Vrátíme zase na začátek řady

        poolParents.TryGetValue(type, out GameObject pool_parent);
        if (pool_parent != null)
                toEnqueue.transform.SetParent(pool_parent.transform);
    }

}
