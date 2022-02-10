using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathGenerator
{
    public List<GameObject> allMovePoints = new List<GameObject>();

    // General attributes
    public Transform pathHolder { get; set; }
    Color pathColor;

    // Generace Křivky
    Transform target;
    float smooth { get; set; } // how many points to insert inbetween TODO:
    float obsticlesOffset { get; set; }
    Vector3 startPos { get; set; }
    public bool isPathReady = false;
    private Vector3 generationDir;

    // Následování podél cesty
    public Transform currentPoint; // Každý další bod v řadě, ke kterému se přibližovat
    // TODO: - Alespoň je to rozdělané
    public AIPathGenerator(Color pathColor, Vector3 startPos, Transform target, float offset, int smooth = 0)
    {
        this.target = target;
        this.pathColor = pathColor;
        this.startPos = startPos;
        this.obsticlesOffset = offset;
        this.smooth = smooth;
    }

    public void GeneratePath()
    {
        if (pathHolder == null)
        {
            pathHolder = new GameObject("pathHolder").transform;
            Scripts.Utility.HierarchyHighlighter pathHighlight = pathHolder.gameObject.AddComponent<Scripts.Utility.HierarchyHighlighter>();
            pathHighlight.Background_Color = pathColor;

        }

        // Orientace generace křivky
        generationDir = Settings.GetVector3Population_(target.position);//(short) (Random.Range(-1f, 1f) > 0 ? 1 : -1);
        Debug.Log("TOGHLE" + generationDir);

        // Spustíme generaci bodů
        GeneratePoint(startPos);
    }

    // Pro každý bod
    public void GeneratePoint(Vector3 pointPos)
    {
        if (allMovePoints.Count > 50)
            return;
        // Získáme bod cesty z poolu.
        GameObject pathPoint = Manager.Instance.objectsToPool.GetFromPool(Settings.PoolTypes.PATH_POINT, pointPos, Quaternion.identity, pathHolder);
        pathPoint.transform.SetParent(pathHolder);
        pathPoint.SetActive(true);

        pointPos = GetSafePointPosition(pathPoint.transform.position);

        pathPoint.transform.position = pointPos;

        // NAKONEC Přidáme vytvořený bod do řady
        allMovePoints.Add(pathPoint);

        // Pokud je v cestě další překážka => rekurzívou vygenerujeme další bod na základě pozice tohoto.
        RaycastHit hitInfo;
        bool isObsticleInWay = IsObsticleInPath(pointPos, out hitInfo);
        if (isObsticleInWay)
        {
            //Debug.Log(hitInfo.transform.gameObject.name);
            GeneratePoint(hitInfo.point);
        }
        else
        {
            allMovePoints.Add(target.gameObject);

            if (smooth > 0)
            {
                SmoothPath();
                return;
            }

            SetNewPoint();
            isPathReady = true;
        }

    }

    bool IsObsticleInPath(Vector3 startPosition, out RaycastHit hitInfo)
    {
        Vector3 dirToTarget = target.position - startPosition;
        float distToTarget = Vector3.Magnitude(dirToTarget);

        if (Physics.Raycast(startPosition, dirToTarget.normalized * distToTarget, out hitInfo))
        {
            if (hitInfo.transform != target && hitInfo.transform.gameObject.layer != LayerMask.NameToLayer("PathPoint"))
            {
                //Debug.Log(hitInfo.transform.gameObject.name);
                return true;
            }
        }

        return false;
    }

    Vector3 GetSafePointPosition(Vector3 pointPos)
    {

        for (int i = 0; i < 20; i++)
        {
            Debug.Log(generationDir);
            pointPos = new Vector3(pointPos.x + 0.2f, pointPos.y + obsticlesOffset / 2 , pointPos.z + 0.2f);

            Ray ray = new Ray();
            ray.direction = Vector3.forward;
            ray.origin = pointPos;

            if (!Physics.SphereCast(ray, obsticlesOffset))
            {
                Debug.Log("Hellousuadghjasdlk");
                return pointPos;
            }
        }

        Debug.LogError("Nebylo možné vygenerovat pozici bodů");
        return new Vector3();
    }

    public void SetNewPoint()
    {
        if (allMovePoints.Count <= 1) {
            isPathReady = false;
            return;
        }

        // Vrátíme do poolu
        Manager.Instance.objectsToPool.ReturnToPool(Settings.PoolTypes.PATH_POINT, allMovePoints[0]);
        allMovePoints[0].SetActive(false);

        // Vymažeme z cesty
        allMovePoints.RemoveAt(0);
        currentPoint = allMovePoints[0].transform;
    }

    /// <summary>
    /// 
    /// </summary>
    void SmoothPath()
    {
        for (int i = 0; i < allMovePoints.Count; i++)
        {
            if (allMovePoints.Count > i + 1)
            {
                Vector3 vectorBetween = allMovePoints[i].transform.position - allMovePoints[i + 1].transform.position;
                Vector3 vectorBetweenDir = vectorBetween.normalized;
                float distBetween = Vector3.Magnitude(vectorBetween);

                // Vypočítáme novou pozici bodu
                Vector3 newPointBetweenPosition = (vectorBetweenDir * distBetween/2) + allMovePoints[i].transform.position;
                GameObject a= new GameObject();
                a.transform.position = newPointBetweenPosition;
                //GeneratePoint(newPointBetweenPosition);
            }
        }
    }
}
