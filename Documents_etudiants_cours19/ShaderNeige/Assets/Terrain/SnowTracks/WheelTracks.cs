using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTracks : MonoBehaviour {
    public Shader _drawShader;
    public GameObject _terrain;
    public Transform[] _wheels;
    RaycastHit _groundHit;
    int _layerMask;
    [Range(0, 4)]
    public float _brushSize;
    [Range(0, 1)]
    public float _strength;
    public float distance = 3;

    private RenderTexture splatMap;
    private Material snowMaterial, drawMaterial;
    // Use this for initialization
    void Start () {
        _layerMask = LayerMask.GetMask("Ground");
        drawMaterial = new Material(_drawShader);
        snowMaterial = _terrain.GetComponent<MeshRenderer>().material;
        splatMap = new RenderTexture(1024, 1024,0,RenderTextureFormat.ARGBFloat);
        snowMaterial.SetTexture("_Splat", splatMap);
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Transform wheel in _wheels)
        {
            if(Physics.Raycast(wheel.position,Vector3.down,out _groundHit,distance,_layerMask))
            {
                drawMaterial.SetVector("_Coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
                drawMaterial.SetFloat("_Strength", _strength);
                drawMaterial.SetFloat("_Size", _brushSize);
                RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(splatMap,temp);
                Graphics.Blit(temp, splatMap, drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
	}
}
