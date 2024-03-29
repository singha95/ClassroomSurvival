﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeacherAI : MonoBehaviour {

	public Vector3 CastDirection; 
	public bool RaycastHit = false;

	int quality = 15;
	Mesh mesh;
	public Material material;
	public Material materialHit;
	private float rotationSpeed = 1.0f;
	float angle_fov = 7;
	float rayCastDisplacement = 7;
	float dist_min = 0.0f;
	float dist_max = 10.0f;
	private float viewAngle = 0;
	private float targetAngle = 180;
	private float Orientation = -1;
	private bool WillMove = true;
	public Text gameOver;
	public Text timer;

	void Start()
	{
		mesh = new Mesh();
		mesh.vertices = new Vector3[4 * quality];   // Could be of size [2 * quality + 2] if circle segment is continuous
		mesh.triangles = new int[3 * 2 * quality];

		Vector3[] normals = new Vector3[4 * quality];
		Vector2[] uv = new Vector2[4 * quality];

		for (int i = 0; i < uv.Length; i++)
			uv[i] = new Vector2(0, 0);
		for (int i = 0; i < normals.Length; i++)
			normals[i] = new Vector3(0, 1, 0);

		mesh.uv = uv;
		mesh.normals = normals;

	}

	void Update()
	{
		if (WillMove) {
			randomRotation ();
		} else {
			if (Random.Range (0.0f, 1.0f) < 0.005) {
				WillMove = true;
				if (targetAngle == 0) {
					targetAngle = 180;
				} else {
					targetAngle = 0;
				}
				if (Random.value <= 0.5) {
					Orientation = 1;
				} else {
					Orientation = -1;
				}
			}
		}
			
		if (detectPlayerCollision ()) {
			DrawVision (viewAngle, materialHit);
			RaycastHit = true;
			material = materialHit;
		} else {
			DrawVision (viewAngle, material);
		}

		if (RaycastHit == true) {
			GameEnd ();
		}
	}

	void GameEnd()
	{
		gameOver.text = "You got caught by the teacher! You Lose.";
		timer.text = "0";
	}

	float GetEnemyAngle()
	{
		return viewAngle;
		//return 90 - Mathf.Rad2Deg * Mathf.Atan2(transform.forward.z, transform.forward.x); // Left handed CW. z = angle 0, x = angle 90
	}

	bool detectPlayerCollision()
	{
		Vector3 fwd = transform.TransformDirection(new Vector3(
			Mathf.Sin(Mathf.Deg2Rad * (viewAngle - rayCastDisplacement)), 0,   // Left handed CW
			Mathf.Cos(Mathf.Deg2Rad * (viewAngle - rayCastDisplacement))));


		Vector3 cnt = transform.TransformDirection(new Vector3(
			Mathf.Sin(Mathf.Deg2Rad * (viewAngle)), 0,   // Left handed CW
			Mathf.Cos(Mathf.Deg2Rad * (viewAngle))));

		Vector3 bck = transform.TransformDirection(new Vector3(
			Mathf.Sin(Mathf.Deg2Rad * (viewAngle + rayCastDisplacement)), 0,   // Left handed CW
			Mathf.Cos(Mathf.Deg2Rad * (viewAngle + rayCastDisplacement))));
		RaycastHit hit;

		if (Physics.Raycast (transform.position, fwd, out hit, 15.0f)) {
			if (hit.transform.tag == "Player") {
				//Debug.Log ("Found Player!");
				return true;
			}
		}

		if (Physics.Raycast (transform.position, cnt, out hit, 15.0f)) {
			if (hit.transform.tag == "Player") {
				//Debug.Log ("Found Player!");
				return true;
			}
		}

		if (Physics.Raycast (transform.position, bck, out hit, 15.0f)) {
			if (hit.transform.tag == "Player") {
				//Debug.Log ("Found Player!");
				return true;
			}
		}
		return false;
	}

	void DrawVision(float viewAngle, Material material)
	{

		float angle_lookat = viewAngle;
		float angle_start = angle_lookat - angle_fov;
		float angle_end = angle_lookat + angle_fov;
		float angle_delta = (angle_end - angle_start) / quality;

		float angle_curr = angle_start;
		float angle_next = angle_start + angle_delta;

		Vector3 pos_curr_min = Vector3.zero;
		Vector3 pos_curr_max = Vector3.zero;

		Vector3 pos_next_min = Vector3.zero;
		Vector3 pos_next_max = Vector3.zero;

		Vector3[] vertices = new Vector3[4 * quality];   // Could be of size [2 * quality + 2] if circle segment is continuous
		int[] triangles = new int[3 * 2 * quality];

		for (int i = 0; i < quality; i++)
		{
			Vector3 sphere_curr = new Vector3(
				Mathf.Sin(Mathf.Deg2Rad * (angle_curr)), 0.01f,   // Left handed CW
				Mathf.Cos(Mathf.Deg2Rad * (angle_curr)));

			Vector3 sphere_next = new Vector3(
				Mathf.Sin(Mathf.Deg2Rad * (angle_next)), 0.01f,
				Mathf.Cos(Mathf.Deg2Rad * (angle_next)));

			pos_curr_min = transform.position + sphere_curr * dist_min;
			pos_curr_max = transform.position + sphere_curr * dist_max;

			pos_next_min = transform.position + sphere_next * dist_min;
			pos_next_max = transform.position + sphere_next * dist_max;

			int a = 4 * i;
			int b = 4 * i + 1;
			int c = 4 * i + 2;
			int d = 4 * i + 3;

			vertices[a] = pos_curr_min; 
			vertices[b] = pos_curr_max;
			vertices[c] = pos_next_max;
			vertices[d] = pos_next_min;

			triangles[6 * i] = a;       // Triangle1: abc
			triangles[6 * i + 1] = b;  
			triangles[6 * i + 2] = c;
			triangles[6 * i + 3] = c;   // Triangle2: cda
			triangles[6 * i + 4] = d;
			triangles[6 * i + 5] = a;

			angle_curr += angle_delta;
			angle_next += angle_delta;

		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;

		Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, material, 0);
	}

	void randomRotation()
	{
		viewAngle = viewAngle + Orientation * rotationSpeed;
		if ((viewAngle >= (360)) || (viewAngle <= -360)){
			viewAngle = 0;
		}
		//Debug.Log ("ViewAngle : " + viewAngle.ToString () + " ; Target : " + targetAngle.ToString ()); 
		if ((viewAngle >= targetAngle * Orientation - rotationSpeed) && (viewAngle <= targetAngle * Orientation + rotationSpeed)){
			viewAngle = targetAngle; 
			WillMove = false;
		}	
	}
}

