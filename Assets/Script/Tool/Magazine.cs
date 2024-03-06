using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    enum INFO
    {
        MAX_COUNT,
        RELOAD_SPEED
    }
    private float _max_count = 0.0f;
    private float _default_max_count = 0.0f;
    private float _reload_speed = 0.0f;
    private Queue<GameObject> _bullet = new Queue<GameObject>();

    [SerializeField]
    private ToolInfo _data;
    private GameObject _parent;

    public delegate void DestroyBulletDelegate();
    public event DestroyBulletDelegate DestroyBulletEvent;

    public bool IS_EMPTY
    {
        get { return _bullet.Count <= 0; }
    }

    public void Init(GameObject parent_gun, string magazine_data_path, string bullet_name)
    {
        _data = Resources.Load<ToolInfo>("ScriptableOBjects\\" + magazine_data_path);
        _default_max_count = _data.f_datas[(int)INFO.MAX_COUNT];
        _max_count = _default_max_count;
        _reload_speed = _data.f_datas[(int)INFO.RELOAD_SPEED];
        _parent = parent_gun;

        if (_bullet.Count < 1)
        {
            if (DestroyBulletEvent != null)
            {
                DestroyBulletEvent();
                DestroyBulletEvent = null;
            }

            for (int i = 0; i < _max_count; i++)
            {
                GameObject bullet = Instantiate(Resources.Load<GameObject>("Prefabs\\Gun\\" + bullet_name), transform.position, transform.rotation, transform);
                bullet.name = bullet_name;
                DestroyBulletEvent += bullet.GetComponent<Bullet>().DestroyBullet;
                bullet.SetActive(false);
                _bullet.Enqueue(bullet);
            }
        }
        else
        {
            GameObject bullet;
            if (_bullet.TryDequeue(out bullet))
                Destroy(bullet);
            _bullet.Clear();
            if (DestroyBulletEvent != null)
            {
                DestroyBulletEvent();
                DestroyBulletEvent = null;
            }
            bullet = null;
            for (int i = 0; i < _max_count; i++)
            {
                bullet = Instantiate(Resources.Load<GameObject>("Prefabs\\Gun\\" + bullet_name), transform.position, transform.rotation, transform);
                bullet.name = bullet_name;
                DestroyBulletEvent += bullet.GetComponent<Bullet>().DestroyBullet;
                bullet.SetActive(false);
                _bullet.Enqueue(bullet);
            }
        }
    }

    public GameObject GetBullet()
    {
        GameObject bullet = null;
        if (_bullet.Count < 1)
            return bullet;

        if (_bullet.TryDequeue(out bullet))
        {
            // _max_count--;
        }
        return bullet;
    }

    public void SetBullet(GameObject bullet)
    {
        if (_bullet.Count + 1 > _default_max_count)
            return;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.transform.parent = transform;
        bullet.SetActive(false);
        _bullet.Enqueue(bullet);
    }
}
