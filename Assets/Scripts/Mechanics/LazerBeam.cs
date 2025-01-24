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

    public LazerBeam(Vector3 pos, Vector3 dir, Material mat)
    {
        this.lazer = new LineRenderer();
        this.lazerObj = new GameObject();   
        this.lazerObj.name = "Lazer";
        this.pos = pos;
        this.dir = dir;

        this.lazer = this.lazerObj.AddComponent<LineRenderer>();
        this.lazer.startWidth = 0.1f;
        this.lazer.endWidth = 0.1f;
        this.lazer.material = mat;
        this.lazer.positionCount = 2;
        this.lazer.startColor = Color.red;
        this.lazer.endColor = Color.red;

        this.CastRay(this.pos, this.dir, this.lazer);
    }

    void CastRay(Vector3 pos, Vector3 dir , LineRenderer lazer)
    {
        this.lazerIndicies.Add(pos);
        
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 30, 1)) {
            CheckHit( hit, dir, lazer);
        }
        else {
            lazerIndicies.Add(ray.GetPoint(30));
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

    void CheckHit(RaycastHit hit, Vector3 direction, LineRenderer lazer){
        if(hit.collider.tag == "Mirror"){
            Vector3 pos = hit.point;
            Vector3 dir = Vector3.Reflect(direction, hit.normal);

            CastRay(pos, dir, lazer);
        }
        else {
            lazerIndicies.Add(hit.point);
            UpdateLazer();
        }
    }
}
