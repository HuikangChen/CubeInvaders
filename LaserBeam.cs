using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : BeamScript {

    public bool active;

    public float damage_rate;
    float time_stamp;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		
	}

    public void StartLaser()
    {
        active = true;
        GetComponent<BoxCollider2D>().enabled = true;
        beamStart = Instantiate(beamStartPrefab[currentBeam], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        beamEnd = Instantiate(beamEndPrefab[currentBeam], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        beam = Instantiate(beamLineRendererPrefab[currentBeam], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        line = beam.GetComponent<LineRenderer>();
    }

    public void EndLaser()
    {
        if (active)
        {
            active = false;
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(beamStart);
            Destroy(beamEnd);
            Destroy(beam);
        }
    }

    public void ShootLaser(Vector2 start, Vector2 dir)
    {
        if (line == null)
        {
            return;
        }
        line.SetVertexCount(2);
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector2 end = Vector2.zero;
        RaycastHit2D hit = Physics2D.Raycast(start, dir, 100);
        if (hit && hit.transform.tag == "Ground")
        {
            end = hit.point - (dir.normalized * beamEndOffset);
        }

        Debug.Log(hit);
        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        float distance = Vector2.Distance(start, end);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            col.GetComponent<Enemy>().TakeHealth(.5f);
        }
    }
}
