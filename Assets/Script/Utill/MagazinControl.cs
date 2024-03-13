using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazinControl : MonoBehaviour
{
    private NormalMovement _nm;
    private Magazine _magazine;
    private void Awake()
    {
        _magazine = GetComponent<Magazine>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _magazine.Init(gameObject, "GrapplingBulletMagazine", "Grappling Bullet");
        //_magazine.Init(gameObject, "WallBulletMagazine", "Wall Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
