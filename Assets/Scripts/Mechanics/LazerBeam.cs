using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeam
{
    Vector3 pos;
    Vector3 dir;
    GameObject lazerObj;
    LineRenderer lazer;
    List<Vector3> lazerIndicies = new List<Vector3>();
    int maxIterations = 10;

    public LazerBeam(Vector3 pos, Vector3 dir, Material mat)
    {
        this.lazer = new LineRenderer();
        this.lazerObj = new GameObject();   
        this.lazerObj.name = "Lazer";
        this.pos = pos;
        this.dir = dir;

        this.lazer = this.lazerObj.AddComponent<LineRenderer>();
        this.lazer.startWidth = 0.2f;
        this.lazer.endWidth = 0.2f;
        this.lazer.material = mat;
        this.lazer.positionCount = 2;
        this.lazer.startColor = Color.white;
        this.lazer.endColor = Color.white;

        int count = 0;

        this.CastRay(this.pos, this.dir, this.lazer, ref count);
    }

    void CastRay(Vector3 pos, Vector3 dir , LineRenderer lazer, ref int iteration)
    {
        if(iteration > maxIterations){
            return;
        }
        iteration++;
        this.lazerIndicies.Add(pos);
        
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 150, 1)) {
            CheckHit( hit, dir, lazer,ref iteration);
        }
        else {
            lazerIndicies.Add(this.dir*150);
            UpdateLazer();
        }

    }

    void UpdateLazer(){
        int count = 0;
        lazer.positionCount = lazerIndicies.Count;
        foreach(Vector3 pos in lazerIndicies){
            lazer.SetPosition(count, pos);
            count++;
        }
    }

    void CheckHit(RaycastHit hit, Vector3 direction, LineRenderer lazer, ref int iteration){
        if(hit.collider.tag == "Mirror"){
            Vector3 pos = hit.point;
            Vector3 dir = Vector3.Reflect(direction, hit.normal);

            CastRay(pos, dir, lazer, ref iteration);
        }
        else if(hit.collider.tag == "Bubble"){
            Vector3 bdir = hit.collider.GetComponent<Bubble>().disiredDirection;
            Vector3 bpos = hit.collider.transform.position + (bdir * 0.5f);

            lazerIndicies.Add(hit.point);
            UpdateLazer();

            {
                CastRay(bpos, bdir, lazer, ref iteration);
            }
            
        }
        else if(hit.collider.tag == "laserReceiver"){
            Debug.Log("RECEIVER HIT");
            lazerIndicies.Add(hit.point);
            UpdateLazer();
        }
        else {
            lazerIndicies.Add(hit.point);
            UpdateLazer();
        }
    }
}
